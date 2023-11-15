using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukTabItemZIndexReverseConverter : IMultiValueConverter{

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            try {
                ItemCollection tabs = (ItemCollection)values[0];
                TabItem theTab = (TabItem)values[1];
                
                int index = tabs.IndexOf(theTab);
                return -index;
            } catch (Exception) {
                return DependencyProperty.UnsetValue;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
