using System.Globalization;
using System.Windows.Data;
using DukNuk.Wpf.Controls;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukWindowCaptionHeightConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            try {
                double titleZoneHeight = (double)values[0];
                double titleMenuHeight = (double)values[1];
                NukWindow.MenuPositionValue mpv = (NukWindow.MenuPositionValue)values[2];

                if (mpv == NukWindow.MenuPositionValue.UnderTitleBar) {
                    return titleZoneHeight;
                } else {
                    return titleZoneHeight - titleMenuHeight;
                }
            } catch (Exception) {
                return Binding.DoNothing;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
