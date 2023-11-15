using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukSliderSelectionConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is double width) {
                if (parameter is bool p && p ||
                    parameter is string s && s.ToLowerInvariant() == "true") {
                    return new Thickness(0,-width, 0, -width );
                } else {
                    return new Thickness(-width,0, -width, 0);
                }
            }
            return new Thickness(0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
