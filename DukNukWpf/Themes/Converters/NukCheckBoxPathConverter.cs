using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukCheckBoxPathConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            try {
                double width = (double)values[0];         // checkbox border width
                double height = (double)values[1];        // checkbox border height
                
                PathFigure fig = new PathFigure();

                Point A = new Point(0.1 * width, 0.3 * height);
                Point B = new Point(0.5 * width, 0.5 * height);
                Point C = new Point(1.0 * width, -0.3 * height);
                Point D = new Point(1.2 * width, 0.1 * height);
                Point E = new Point(0.5 * width, 0.85 * height);
                Point F = new Point(0.05 * width, 0.55 * height);
                
                fig.IsClosed = true;
                fig.StartPoint = A;
                fig.Segments.Add(new LineSegment(B, true));
                fig.Segments.Add(new LineSegment(C, true));
                fig.Segments.Add(new LineSegment(D, true));
                fig.Segments.Add(new LineSegment(E, true));
                fig.Segments.Add(new LineSegment(F, true));
                

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
