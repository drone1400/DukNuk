using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace DukNuk.Wpf.Themes.Converters {
    
    // just like the MenuScrollVisibilityConverter but hides instead of collapsing
    public class NukTabItemScrollVisibilityConverter : IMultiValueConverter{

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            bool AreClose(double value1, double value2)
            {
                if (value1 == value2)
                    return true;
                double num1 = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.220446049250313E-16;
                double num2 = value1 - value2;
                return -num1 < num2 && num1 > num2;
            }
            
            Type type1 = typeof (double);
            if (parameter == null || values == null || values.Length != 4 || values[0] == null || values[1] == null || values[2] == null || values[3] == null || !typeof (Visibility).IsAssignableFrom(values[0].GetType()) || !type1.IsAssignableFrom(values[1].GetType()) || !type1.IsAssignableFrom(values[2].GetType()) || !type1.IsAssignableFrom(values[3].GetType()))
                return DependencyProperty.UnsetValue;
            Type type2 = parameter.GetType();
            if (!type1.IsAssignableFrom(type2) && !typeof (string).IsAssignableFrom(type2))
                return DependencyProperty.UnsetValue;
            if ((Visibility) values[0] != Visibility.Visible)
                return (object) Visibility.Hidden;
            double num1 = !(parameter is string) ? (double) parameter : double.Parse((string) parameter, (IFormatProvider) NumberFormatInfo.InvariantInfo);
            double num2 = (double) values[1];
            double num3 = (double) values[2];
            double num4 = (double) values[3];
            return num3 != num4 && AreClose(Math.Min(100.0, Math.Max(0.0, num2 * 100.0 / (num3 - num4))), num1) ? (object) Visibility.Hidden : (object) Visibility.Visible;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
