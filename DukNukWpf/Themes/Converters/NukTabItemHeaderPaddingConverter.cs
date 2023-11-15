using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukTabItemHeaderPaddingConverter : IMultiValueConverter {
        

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            try {
                double size = (double)values[0];
                Thickness pad = (Thickness)values[1];
                Dock tabStripPalcement = (Dock)values[2];
                switch (tabStripPalcement) {
                    case Dock.Left:
                    case Dock.Right: return new Thickness(pad.Left, pad.Top + size, pad.Right, pad.Bottom + size);
                    case Dock.Top:
                    case Dock.Bottom: return new Thickness(pad.Left + size, pad.Top, pad.Right + size, pad.Bottom);
                }
                return pad;

            } catch (Exception) {
                return DependencyProperty.UnsetValue;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
