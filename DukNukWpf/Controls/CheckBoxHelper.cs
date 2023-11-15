using System.Windows;
using System.Windows.Input;
namespace DukNuk.Wpf.Controls {
    public static class CheckBoxHelper {
        public static readonly DependencyProperty RightSideCheckMarkProperty = DependencyProperty.RegisterAttached(
            "RightSideCheckMark", typeof(bool), typeof(CheckBoxHelper), new PropertyMetadata(false));
        public static bool GetRightSideCheckMark(UIElement target) =>
            (bool)target.GetValue(CheckBoxHelper.RightSideCheckMarkProperty);
        public static void SetRightSideCheckMark(UIElement target, bool value) =>
            target.SetValue(CheckBoxHelper.RightSideCheckMarkProperty, value);
    }
}
