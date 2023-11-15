using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukWindowContentVisibilityConverter : IValueConverter{

        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
