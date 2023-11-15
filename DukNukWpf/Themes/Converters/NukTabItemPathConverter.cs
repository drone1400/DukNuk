using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukTabItemPathConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            try {
                double width = (double)values[0];         // tab item width
                double height = (double)values[1];        // tab item height
                double bezSize = (double)values[2];       // tab item height
                bool isSelected = values[3] is true;      // tab item is selected
                bool isEnabled = values[4] is true;       // tab item is enabled
                bool isMouseOver = values[5] is true;     // tab item is being mouse overed
                Dock tabStrip = (Dock)values[6];          // tab control dock placement

               
                PathFigure fig = new PathFigure();

                if (tabStrip == Dock.Top || tabStrip == Dock.Bottom) {
                    double left = 0;
                    double right = width;
                    double top = 0;
                    double bottom = height;

                    Point A0 = tabStrip == Dock.Bottom ? new Point(left , top) : new Point(left , bottom);
                    Point A1 = tabStrip == Dock.Bottom ? new Point(left + bezSize / 2, top) : new Point(left + bezSize / 2, bottom);
                    Point A2 = tabStrip == Dock.Bottom ? new Point(left + bezSize / 2, bottom) : new Point(left + bezSize / 2, top);
                    Point A3 = tabStrip == Dock.Bottom ? new Point(left + bezSize, bottom) : new Point(left + bezSize, top);

                    Point B0 = tabStrip == Dock.Bottom ? new Point(right - bezSize, bottom) : new Point(right - bezSize, top);
                    Point B1 = tabStrip == Dock.Bottom ? new Point(right - bezSize / 2, bottom) : new Point(right - bezSize / 2, top);
                    Point B2 = tabStrip == Dock.Bottom ? new Point(right - bezSize / 2 , top) : new Point(right - bezSize / 2 , bottom);
                    Point B3 = tabStrip == Dock.Bottom ? new Point(right, top) : new Point(right, bottom);

                    fig.IsClosed = false;
                    fig.StartPoint = A0;
                    fig.Segments.Add(new BezierSegment(A1, A2, A3, true));
                    fig.Segments.Add(new LineSegment(B0, true));
                    fig.Segments.Add(new BezierSegment(B1, B2, B3, true));
                } else if (tabStrip == Dock.Left || tabStrip == Dock.Right) {
                    double left = 0;
                    double right = width;
                    double top = 0;
                    double bottom = height;

                    Point A0 = tabStrip == Dock.Left ? new Point(right, bottom) : new Point(left, bottom);
                    Point A1 = tabStrip == Dock.Left ? new Point(right, bottom - bezSize / 2) : new Point(left, bottom - bezSize / 2);
                    Point A2 = tabStrip == Dock.Left ? new Point(right - bezSize / 2, bottom - bezSize / 2) : new Point(left + bezSize / 2, bottom - bezSize / 2);

                    Point B0 = tabStrip == Dock.Left ? new Point(left + bezSize / 2, bottom - bezSize / 2) : new Point(right - bezSize / 2, bottom - bezSize / 2);
                    Point B1 = tabStrip == Dock.Left ? new Point(left, bottom - bezSize / 2) : new Point(right, bottom - bezSize / 2);
                    Point B2 = tabStrip == Dock.Left ? new Point(left, bottom - bezSize) : new Point(right, bottom - bezSize);
                    
                    Point C0 = tabStrip == Dock.Left ? new Point(left, top + bezSize) : new Point(right, top + bezSize);
                    Point C1 = tabStrip == Dock.Left ? new Point(left, top + bezSize / 2) : new Point(right, top + bezSize / 2);
                    Point C2 = tabStrip == Dock.Left ? new Point(left + bezSize / 2, top + bezSize / 2) : new Point(right - bezSize / 2, top + bezSize / 2);
                    
                    Point D0 = tabStrip == Dock.Left ? new Point(right - bezSize / 2, top + bezSize / 2) : new Point(left + bezSize / 2, top + bezSize / 2);
                    Point D1 = tabStrip == Dock.Left ? new Point(right, top + bezSize / 2) : new Point(left, top + bezSize / 2);
                    Point D2 = tabStrip == Dock.Left ? new Point(right, top) : new Point(left, top);

                    fig.IsClosed = false;
                    fig.StartPoint = A0;
                    fig.Segments.Add(new QuadraticBezierSegment(A1, A2, true));
                    fig.Segments.Add(new LineSegment(B0, true));
                    fig.Segments.Add(new QuadraticBezierSegment(B1, B2, true));
                    fig.Segments.Add(new LineSegment(C0, true));
                    fig.Segments.Add(new QuadraticBezierSegment(C1, C2, true));
                    fig.Segments.Add(new LineSegment(D0, true));
                    fig.Segments.Add(new QuadraticBezierSegment(D1, D2, true));
                    //fig.Segments.Add(new LineSegment(A0, true));
                }

                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(fig);

                return geometry;
            } catch (Exception ex) {
                return DependencyProperty.UnsetValue;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
