using System.Windows;
using System.Windows.Controls;
namespace DukNuk.Wpf.Controls {
    public static class TabItemHelper {
        public static readonly DependencyProperty HeaderBezierSizeProperty = DependencyProperty.RegisterAttached(
            "HeaderBezierSize", typeof(double), typeof(TabItemHelper), new PropertyMetadata(5.0));

        // Declare a get accessor method.
        public static double GetHeaderBezierSize(UIElement target) =>
            (double)target.GetValue(ScrollHelper.UseCustomScrollProperty);

        // Declare a set accessor method.
        public static void SetHeaderBezierSize(UIElement target, double value) =>
            target.SetValue(ScrollHelper.UseCustomScrollProperty, value);
    }
}
