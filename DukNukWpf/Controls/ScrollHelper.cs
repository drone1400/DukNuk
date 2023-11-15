using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
namespace DukNuk.Wpf.Controls {
    public static class ScrollHelper {
        public static readonly DependencyProperty UseCustomScrollProperty = DependencyProperty.RegisterAttached(
            "UseCustomScroll", typeof(bool), typeof(ScrollHelper), new PropertyMetadata(false, HorizontalScrollShiftMouseWheelCallback));

        // Declare a get accessor method.
        public static bool GetUseCustomScroll(UIElement target) =>
            (bool)target.GetValue(ScrollHelper.UseCustomScrollProperty);

        // Declare a set accessor method.
        public static void SetUseCustomScroll(UIElement target, bool value) =>
            target.SetValue(ScrollHelper.UseCustomScrollProperty, value);
        
        // private static readonly DependencyProperty TargetScrollViewerProperty = DependencyProperty.RegisterAttached(
        //     "TargetScrollViewer", typeof(ScrollViewer), typeof(ScrollHelper));
        //
        // // Declare a get accessor method.
        // public static ScrollViewer? GetTargetScrollViewer(UIElement target) =>
        //     target.GetValue(ScrollHelper.TargetScrollViewerProperty) as ScrollViewer;
        //
        // // Declare a set accessor method.
        // public static void SetTargetScrollViewer(UIElement target, ScrollViewer? value) =>
        //     target.SetValue(ScrollHelper.TargetScrollViewerProperty, value);
        
        private static void HorizontalScrollShiftMouseWheelCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            if (dependencyObject is not UIElement uiElement)
                throw new ArgumentException("Target is not an UIElement!");
            
            // ScrollViewer? scroller = FindFirstScrollViewer(uiElement);
            // if (scroller == null)
            //     throw new ArgumentException("Target does not have a ScrollViewer!");
            
            //uiElement.SetValue(ScrollHelper.TargetScrollViewerProperty, scroller);

            if ((bool)e.NewValue == true) {
                uiElement.PreviewMouseWheel += OnPreviewMouseWheel;
            } else {
                uiElement.PreviewMouseWheel -= OnPreviewMouseWheel;
            }
        }
        
        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args) {
            if (sender is not DependencyObject dObj)
                return;
            
            ScrollViewer? scroller = dObj.FindFirstScrollViewer();
            
            if (scroller == null)
                return;

            // NOTE: This SUCKS! We can't access the IScrollInfo object so we can't use the MouseWheelLeft / MouseWheelRight / MouseWheelUp / MouseWheelDown methods...
            // because the delta for LineScroll is smaller than MouseWheel scrolling, this makes scrolling slower and more annoying!
            
            // having to look for a scroll viewer each time the event gets called sucks too
            // a better way to do it would be to only do it when the visual tree changes i suppose...

            if (Keyboard.Modifiers == ModifierKeys.Shift) {
                if (args.Delta < 0)
                    scroller.LineRight();
                else
                    scroller.LineLeft();
            } else {
                if (args.Delta < 0)
                    scroller.LineUp();
                else
                    scroller.LineDown();
            }
            args.Handled = true;
        }
        
        private static ScrollViewer? FindFirstScrollViewer(this DependencyObject? dObj)
        {
            if (dObj == null)
                return null;

            Queue<DependencyObject> muhChildren = new Queue<DependencyObject>();
            muhChildren.Enqueue(dObj);

            while (muhChildren.Count > 0) {
                DependencyObject childObj = muhChildren.Dequeue();
                int visualChildren = VisualTreeHelper.GetChildrenCount(childObj);

                for (var i = 0; i < visualChildren; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(childObj, i);

                    if (child is ScrollViewer scroll)
                        return scroll;
                
                    muhChildren.Enqueue(child);
                }
            }

            return null;
        }
    }
}
