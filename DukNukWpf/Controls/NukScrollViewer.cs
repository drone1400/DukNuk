using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace DukNuk.Wpf.Controls {

    /// <summary>
    /// Inherits ScrollViewer and provides horizontal mouse wheel scrolling when holding SHIFT
    /// Also allows disabling Keyboard Scrolling through a property
    /// </summary>
    public class NukScrollViewer : ScrollViewer {
        #region MouseWheel stuff
        
        public static readonly DependencyProperty InvertMouseWheelAxesProperty = DependencyProperty.Register(nameof(InvertMouseWheelAxes),
            typeof(bool),
            typeof(NukScrollViewer),
            new PropertyMetadata(false)
            );

        /// <summary>
        /// If true, inverts mouse wheel axes - normal scroll will scroll horizontally, and SHIFT scroll will scroll vertically
        /// </summary>
        public bool InvertMouseWheelAxes {
            get => (bool)this.GetValue(InvertMouseWheelAxesProperty);
            set => this.SetValue(InvertMouseWheelAxesProperty, value);
        }

        public static readonly DependencyProperty InvertMouseWheelHorizontalAxisDirectionProperty = DependencyProperty.Register(nameof(InvertMouseWheelHorizontalAxisDirection),
            typeof(bool),
            typeof(NukScrollViewer),
            new PropertyMetadata(false)
            );

        /// <summary>
        /// If true, inverts the mouse wheel scroll direction for the Horizontal axis
        /// </summary>
        public bool InvertMouseWheelHorizontalAxisDirection {
            get => (bool)this.GetValue(InvertMouseWheelHorizontalAxisDirectionProperty);
            set => this.SetValue(InvertMouseWheelHorizontalAxisDirectionProperty, value);
        }

        public static readonly DependencyProperty InvertMouseWheelVerticalAxisDirectionProperty = DependencyProperty.Register(nameof(InvertMouseWheelVerticalAxisDirection),
            typeof(bool),
            typeof(NukScrollViewer),
            new PropertyMetadata(false)
            );

        /// <summary>
        /// If true, inverts the mouse wheel scroll direction for the Vertical axis
        /// </summary>
        public bool InvertMouseWheelVerticalAxisDirection {
            get => (bool)this.GetValue(InvertMouseWheelVerticalAxisDirectionProperty);
            set => this.SetValue(InvertMouseWheelVerticalAxisDirectionProperty, value);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e) {
            // we can't access this._flags or this.HandlesMouseWheelScrolling so just assume true, whatever

            if (e.Handled)
                return;

            if (this.ScrollInfo != null) {
                void MouseWheelDown() {
                    if (this.InvertMouseWheelVerticalAxisDirection)
                        this.ScrollInfo.MouseWheelUp();
                    else
                        this.ScrollInfo.MouseWheelDown();
                }
                void MouseWheelUp() {
                    if (this.InvertMouseWheelVerticalAxisDirection)
                        this.ScrollInfo.MouseWheelDown();
                    else
                        this.ScrollInfo.MouseWheelUp();
                }
                void MouseWheelLeft() {
                    if (this.InvertMouseWheelHorizontalAxisDirection)
                        this.ScrollInfo.MouseWheelRight();
                    else
                        this.ScrollInfo.MouseWheelLeft();
                }
                void MouseWheelRight() {
                    if (this.InvertMouseWheelHorizontalAxisDirection)
                        this.ScrollInfo.MouseWheelLeft();
                    else
                        this.ScrollInfo.MouseWheelRight();
                }


                if (Keyboard.Modifiers == ModifierKeys.Shift) {
                    if (e.Delta < 0)
                        if (this.InvertMouseWheelAxes)
                            MouseWheelDown();
                        else
                            MouseWheelRight();
                    else if (this.InvertMouseWheelAxes)
                        MouseWheelUp();
                    else
                        MouseWheelLeft();
                } else {
                    if (e.Delta < 0)
                        if (this.InvertMouseWheelAxes)
                            MouseWheelRight();
                        else
                            MouseWheelDown();
                    else if (this.InvertMouseWheelAxes)
                        MouseWheelLeft();
                    else
                        MouseWheelUp();
                }
            }
            e.Handled = true;
        }
        
        #endregion

        #region Keyboard stuff

        public static readonly DependencyProperty IgnoreKeyboardScrollingProperty = DependencyProperty.Register(nameof(IgnoreKeyboardScrolling),
            typeof(bool),
            typeof(NukScrollViewer),
            new PropertyMetadata(false)
            );

        /// <summary>
        /// If true, ignores scrolling with the Left/Right/Up/Down keys
        /// </summary>
        public bool IgnoreKeyboardScrolling {
            get => (bool)this.GetValue(IgnoreKeyboardScrollingProperty);
            set => this.SetValue(IgnoreKeyboardScrollingProperty, value);
        }
        
        
        protected override void OnKeyDown(KeyEventArgs e) {
            if (this.IgnoreKeyboardScrolling
                    && (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down)) {
                return;
            }
            base.OnKeyDown(e);
        }
        
        #endregion
    }
}
