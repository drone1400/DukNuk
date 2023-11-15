using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace DukNuk.Wpf.Themes.Converters {
    public class NukBoolToVisibilityConverter : IValueConverter {
        private bool _negate = false;
        public bool Negate {
            get => this._negate;
            set => this._negate = value;
        }

        private bool _hide = false;
        public bool Hide {
            get => this._hide;
            set => this._hide = value;
        }

        private object BoolToVisbility(object value) {
            if (value is not bool boolVal) return DependencyProperty.UnsetValue;
            if (this._negate) boolVal = !boolVal;
            return this._hide
                ? boolVal 
                    ? Visibility.Visible 
                    : Visibility.Hidden
                : boolVal
                    ? Visibility.Visible
                    : Visibility.Collapsed;

        }
        
        private object VisibilityToBool(object value) {
            if (value is not Visibility visVal) return DependencyProperty.UnsetValue;
            bool boolVal = visVal == Visibility.Visible;
            return this._negate
                ? !boolVal
                : boolVal;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return this.BoolToVisbility(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return this.VisibilityToBool(value);
        }
    }
}
