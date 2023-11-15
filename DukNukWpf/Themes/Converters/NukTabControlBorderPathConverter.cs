using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukTabControlBorderPathConverter : IMultiValueConverter {
        /// <summary>Creates a <see cref="T:System.Windows.Media.VisualBrush" /> that draws the border for a <see cref="T:System.Windows.Controls.GroupBox" /> control.</summary>
        /// <param name="values">An array of three numbers that represent the <see cref="T:System.Windows.Controls.GroupBox" /> control parameters.  See Remarks for more information.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">The width of the visible line to the left of the <see cref="P:System.Windows.Controls.HeaderedContentControl.Header" /> in the <see cref="T:System.Windows.Controls.GroupBox" />.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>A <see cref="T:System.Windows.Media.VisualBrush" /> that draws the border around a <see cref="T:System.Windows.Controls.GroupBox" /> control that includes a gap for the <see cref="P:System.Windows.Controls.HeaderedContentControl.Header" /> content.</returns>
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            TabControl tc = (TabControl)values[0];
            TabItem sti = (TabItem)values[1];
            double tcWidth = (double)values[2];
            double tcHeight = (double)values[3];
            //double tiWidth = (double)values[4];
            //double tiHeight = (double)values[5];
            Dock tcPlace = (Dock)values[4];
            double thiccness = (double)values[5];
            double scrollOffsetH = (double)values[6];
            double scrollOffsetV = (double)values[7];
            CornerRadius corner = (CornerRadius)values[8];

            if (tcWidth == 0 || tcHeight == 0) return null;

            double offX = 0.5 * thiccness;
            double offY = 0.5 * thiccness;
            tcWidth -= 1 * thiccness;
            tcHeight -= 1 * thiccness;
            
            // NOTE: all points are defined counter clockwise
            
            Point[] topLeft = new Point[3] {
               new Point(offX + corner.TopLeft, offY + 0),
               new Point(offX + 0, offY + 0),
               new Point(offX + 0, offY + corner.TopLeft),
            };
            Point[] bottomLeft = new Point[3] {
                new Point(offX + 0, offY + tcHeight - corner.BottomLeft),
                new Point(offX + 0, offY + tcHeight),
                new Point(offX + corner.BottomLeft, offY + tcHeight),
            };
            Point[] bottomRight = new Point[3] {
                new Point(offX + tcWidth - corner.BottomRight, offY + tcHeight),
                new Point(offX + tcWidth, offY + tcHeight),
                new Point(offX + tcWidth, offY + tcHeight - corner.BottomRight),
            };

            Point[] topRight = new Point[3] {
                new Point(offX + tcWidth, offY + corner.TopRight),
                new Point(offX + tcWidth, offY + 0),
                new Point(offX + tcWidth - corner.TopRight, offY + 0),
            };
            
            Point[] points = new Point[14] {
                new Point(offX + tcWidth/2, offY + 0),
                topLeft[0], topLeft[1], topLeft[2],
                bottomLeft[0], bottomLeft[1], bottomLeft[2],
                bottomRight[0], bottomRight[1], bottomRight[2],
                topRight[0], topRight[1], topRight[2],
                new Point(offX + tcWidth/2, offY + 0),
            };

            if (tcPlace == Dock.Top) {
                Point rp = sti.TranslatePoint(new Point(0, 0), tc);
                double offset1 = rp.X + 1;
                double offset2 = rp.X + sti.ActualWidth - 1;


                if (double.IsNaN(offset1) || offset1 < corner.TopLeft) offset1 = corner.TopLeft;
                if (double.IsNaN(offset2) || offset2 < corner.TopLeft) offset2 = corner.TopLeft;
                if (offset1 > tcWidth - corner.TopRight) offset1 = tcWidth - corner.TopRight;
                if (offset2 > tcWidth - corner.TopRight) offset2 = tcWidth - corner.TopRight;

                // gap is in top of border
                points = new Point[14] {
                    new Point(offX + offset1, offY + 0),
                    topLeft[0], topLeft[1], topLeft[2],
                    bottomLeft[0], bottomLeft[1], bottomLeft[2],
                    bottomRight[0], bottomRight[1], bottomRight[2],
                    topRight[0], topRight[1], topRight[2],
                    new Point(offX + offset2, offY + 0),
                };
                
            } else if (tcPlace == Dock.Bottom) {
                Point rp = sti.TranslatePoint(new Point(0, 0), tc);
                double offset1 = rp.X + 1;
                double offset2 = rp.X + sti.ActualWidth - 1;

                if (double.IsNaN(offset1) || offset1 < corner.BottomLeft) offset1 = corner.BottomLeft;
                if (double.IsNaN(offset2) || offset2 < corner.BottomLeft) offset2 = corner.BottomLeft;
                if (offset1 > tcWidth - corner.BottomRight) offset1 = tcWidth - corner.BottomRight;
                if (offset2 > tcWidth - corner.BottomRight) offset2 = tcWidth - corner.BottomRight;

                // gap is in bottom of border
                points = new Point[14] {
                    new Point(offX + offset2, offY + tcHeight),
                    bottomRight[0], bottomRight[1], bottomRight[2],
                    topRight[0], topRight[1], topRight[2],
                    topLeft[0], topLeft[1], topLeft[2],
                    bottomLeft[0], bottomLeft[1], bottomLeft[2],
                    new Point(offX + offset1,offY +tcHeight),
                };
            } else if (tcPlace == Dock.Left) {
                Point rp = sti.TranslatePoint(new Point(0, 0), tc);
                double offset1 = rp.Y + 1;
                double offset2 = rp.Y + sti.ActualHeight - 1;

                if (double.IsNaN(offset1) || offset1 < corner.TopLeft) offset1 = corner.TopLeft;
                if (double.IsNaN(offset2) || offset2 < corner.TopLeft) offset2 = corner.TopLeft;
                if (offset1 > tcHeight - corner.BottomLeft) offset1 = tcHeight - corner.BottomLeft;
                if (offset2 > tcHeight - corner.BottomLeft) offset2 = tcHeight - corner.BottomLeft;

                // gap is left of border..
                points = new Point[14] {
                    new Point(offX, offY + offset2),
                    bottomLeft[0], bottomLeft[1], bottomLeft[2],
                    bottomRight[0], bottomRight[1], bottomRight[2],
                    topRight[0], topRight[1], topRight[2],
                    topLeft[0], topLeft[1], topLeft[2],
                    new Point(offX,offY + offset1),
                };
            } else if (tcPlace == Dock.Right) {
                Point rp = sti.TranslatePoint(new Point(0, 0), tc);
                double offset1 = rp.Y + 1;
                double offset2 = rp.Y + sti.ActualHeight - 1;

                if (double.IsNaN(offset1) || offset1 < corner.TopRight) offset1 = corner.TopRight;
                if (double.IsNaN(offset2) || offset2 < corner.TopRight) offset2 = corner.TopRight;
                if (offset1 > tcHeight - corner.BottomRight) offset1 = tcHeight - corner.BottomRight;
                if (offset2 > tcHeight - corner.BottomRight) offset2 = tcHeight - corner.BottomRight;

                // gap is left of border..
                points = new Point[14] {
                    new Point(offX + tcWidth, offY + offset1),
                    topRight[0], topRight[1], topRight[2],
                    topLeft[0], topLeft[1], topLeft[2],
                    bottomLeft[0], bottomLeft[1], bottomLeft[2],
                    bottomRight[0], bottomRight[1], bottomRight[2],
                    new Point(offX + tcWidth,offY + offset2),
                };
            }
            
            
            PathFigure fig = new PathFigure();
            fig.IsClosed = false;
            fig.StartPoint = points[0];
            fig.Segments.Add(new LineSegment(points[1], true));
            fig.Segments.Add(new QuadraticBezierSegment(points[2], points[3], true));
            fig.Segments.Add(new LineSegment(points[4], true));
            fig.Segments.Add(new QuadraticBezierSegment(points[5], points[6], true));
            fig.Segments.Add(new LineSegment(points[7], true));
            fig.Segments.Add(new QuadraticBezierSegment(points[8], points[9], true));
            fig.Segments.Add(new LineSegment(points[10], true));
            fig.Segments.Add(new QuadraticBezierSegment(points[11], points[12], true));
            fig.Segments.Add(new LineSegment(points[13], true));

            PathGeometry geometry = new PathGeometry();
            geometry.Figures.Add(fig);
            return geometry;return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
