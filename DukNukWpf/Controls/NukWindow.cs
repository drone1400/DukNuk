using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace DukNuk.Wpf.Controls {
    
    // background graphics
    [TemplatePart(Name = "PART_backgroundLayer1", Type = typeof(Path))]
    [TemplatePart(Name = "PART_backgroundLayer2", Type = typeof(Path))]
    // title bar buttons
    [TemplatePart(Name = "PART_focusButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_minimizeButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_maximizeButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_closeButton", Type = typeof(Button))]
    // the title control
    [TemplatePart(Name = "PART_windowTitle", Type = typeof(FrameworkElement))]
    // the container for the title bar buttons
    [TemplatePart(Name = "PART_windowTitleBarButtons", Type = typeof(FrameworkElement))]
    // the container for the main window content
    [TemplatePart(Name = "PART_windowContent", Type = typeof(FrameworkElement))]
    public class NukWindow : Window {
        public enum TitlePositionValue {
            Left,
            Top
        }
        
        public enum MenuPositionValue {
            OnTitleBar,
            UnderTitleBar
        }
        
        public static readonly DependencyProperty StatusBarContentProperty = DependencyProperty.Register(nameof(StatusBarContent),
            typeof(object),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(null)
            );
        
        public static readonly DependencyProperty MenuContentProperty = DependencyProperty.Register(nameof(MenuContent),
            typeof(object),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(null)
            );
        
        public static readonly DependencyProperty TitlePositionProperty = DependencyProperty.Register(nameof(TitlePosition),
            typeof(TitlePositionValue),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(TitlePositionValue.Top, OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty MenuPositionProperty = DependencyProperty.Register(nameof(MenuPosition),
            typeof(MenuPositionValue),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(MenuPositionValue.OnTitleBar, OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty TitleMenuZoneHeightProperty = DependencyProperty.Register(nameof(TitleMenuZoneHeight),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(24.0, OnWindowShapeChanged)
            );

        public static readonly DependencyProperty TitleZoneHeightProperty = DependencyProperty.Register(nameof(TitleZoneHeight),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(40.0, OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty TitleGapLeftProperty = DependencyProperty.Register(nameof(TitleGapLeft),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(double.NaN, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleBezierLeftWidthProperty = DependencyProperty.Register(nameof(TitleBezierLeftWidth),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(60.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleBezierLeftHeightProperty = DependencyProperty.Register(nameof(TitleBezierLeftHeight),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(16.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleBezierLeftOffsetProperty = DependencyProperty.Register(nameof(TitleBezierLeftOffset),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(20.0, OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty TitleGapRightProperty = DependencyProperty.Register(nameof(TitleGapRight),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(160.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleBezierRightWidthProperty = DependencyProperty.Register(nameof(TitleBezierRightWidth),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(60.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleBezierRightHeightProperty = DependencyProperty.Register(nameof(TitleBezierRightHeight),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(36.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleBezierRightOffsetProperty = DependencyProperty.Register(nameof(TitleBezierRightOffset),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(20.0, OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty TitleCornerRightWidthProperty = DependencyProperty.Register(nameof(TitleCornerRightWidth),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(40.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleCornerRightHeightProperty = DependencyProperty.Register(nameof(TitleCornerRightHeight),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(40.0, OnWindowShapeChanged)
            );
        public static readonly DependencyProperty TitleCornerRightQuadBezierRadiusProperty = DependencyProperty.Register(nameof(TitleCornerRightQuadBezierRadius),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(5.0, OnWindowShapeChanged)
            );

        public static readonly DependencyProperty QuadBezierRadiusProperty = DependencyProperty.Register(nameof(QuadBezierRadius),
            typeof(double),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(10.0, OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty SecondLayerPaddingProperty = DependencyProperty.Register(nameof(SecondLayerPadding),
            typeof(Thickness),
            typeof(NukWindow),
            new FrameworkPropertyMetadata( new Thickness(0,0,12,0), OnWindowShapeChanged)
            );
        
        public static readonly DependencyProperty FirstLayerPaddingProperty = DependencyProperty.Register(nameof(FirstLayerPadding),
            typeof(Thickness),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(new Thickness(8,8,0,8), OnWindowShapeChanged)
            );
        public static readonly DependencyProperty ContentPaddingProperty = DependencyProperty.Register(nameof(ContentPadding),
            typeof(Thickness),
            typeof(NukWindow),
            new FrameworkPropertyMetadata(new Thickness(8,8,8,8), OnWindowShapeChanged)
            );
        
        [Category("Custom Content")]
        public object StatusBarContent {
            get => (object)this.GetValue(NukWindow.StatusBarContentProperty);
            set => this.SetValue(NukWindow.StatusBarContentProperty, value);
        }
        
        [Category("Custom Content")]
        public object MenuContent {
            get => (object)this.GetValue(NukWindow.MenuContentProperty);
            set => this.SetValue(NukWindow.MenuContentProperty, value);
        }
        
        [Category("Custom Look - General")]
        public TitlePositionValue TitlePosition {
            get => (TitlePositionValue)this.GetValue(NukWindow.TitlePositionProperty);
            set => this.SetValue(NukWindow.TitlePositionProperty, value);
        }
        
        [Category("Custom Look - General")]
        public MenuPositionValue MenuPosition {
            get => (MenuPositionValue)this.GetValue(NukWindow.MenuPositionProperty);
            set => this.SetValue(NukWindow.MenuPositionProperty, value);
        }
                
        [Category("Custom Look - General")]
        public Thickness SecondLayerPadding {
            get => (Thickness)this.GetValue(NukWindow.SecondLayerPaddingProperty);
            set => this.SetValue(NukWindow.SecondLayerPaddingProperty, value);
        }
        
        [Category("Custom Look - General")]
        public Thickness FirstLayerPadding {
            get => (Thickness)this.GetValue(NukWindow.FirstLayerPaddingProperty);
            set => this.SetValue(NukWindow.FirstLayerPaddingProperty, value);
        }
        
        [Category("Custom Look - General")]
        public Thickness ContentPadding {
            get => (Thickness)this.GetValue(NukWindow.ContentPaddingProperty);
            set => this.SetValue(NukWindow.ContentPaddingProperty, value);
        }
        
        [Category("Custom Look - General")]
        public double TitleMenuZoneHeight {
            get => (double)this.GetValue(NukWindow.TitleMenuZoneHeightProperty);
            set => this.SetValue(NukWindow.TitleMenuZoneHeightProperty, value);
        }
        
        [Category("Custom Look - General")]
        public double TitleZoneHeight {
            get => (double)this.GetValue(NukWindow.TitleZoneHeightProperty);
            set => this.SetValue(NukWindow.TitleZoneHeightProperty, value);
        }
        
        [Category("Custom Look - Title Bar Left")]
        public double TitleGapLeft {
            get => (double)this.GetValue(NukWindow.TitleGapLeftProperty);
            set => this.SetValue(NukWindow.TitleGapLeftProperty, value);
        }
        
        [Category("Custom Look - Title Bar Left")]
        public double TitleBezierLeftHeight {
            get => (double)this.GetValue(NukWindow.TitleBezierLeftHeightProperty);
            set => this.SetValue(NukWindow.TitleBezierLeftHeightProperty, value);
        }
        
        [Category("Custom Look - Title Bar Left")]
        public double TitleBezierLeftWidth {
            get => (double)this.GetValue(NukWindow.TitleBezierLeftWidthProperty);
            set => this.SetValue(NukWindow.TitleBezierLeftWidthProperty, value);
        }
        
        [Category("Custom Look - Title Bar Left")]
        public double TitleBezierLeftOffset {
            get => (double)this.GetValue(NukWindow.TitleBezierLeftOffsetProperty);
            set => this.SetValue(NukWindow.TitleBezierLeftOffsetProperty, value);
        }
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleGapRight {
            get => (double)this.GetValue(NukWindow.TitleGapRightProperty);
            set => this.SetValue(NukWindow.TitleGapRightProperty, value);
        }
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleBezierRightHeight {
            get => (double)this.GetValue(NukWindow.TitleBezierRightHeightProperty);
            set => this.SetValue(NukWindow.TitleBezierRightHeightProperty, value);
        }
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleBezierRightWidth {
            get => (double)this.GetValue(NukWindow.TitleBezierRightWidthProperty);
            set => this.SetValue(NukWindow.TitleBezierRightWidthProperty, value);
        }
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleBezierRightOffset {
            get => (double)this.GetValue(NukWindow.TitleBezierRightOffsetProperty);
            set => this.SetValue(NukWindow.TitleBezierRightOffsetProperty, value);
        }

        [Category("Custom Look - Title Bar Right")]
        public double QuadBezierRadius {
            get => (double)this.GetValue(NukWindow.QuadBezierRadiusProperty);
            set => this.SetValue(NukWindow.QuadBezierRadiusProperty, value);
        }
        
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleCornerRightWidth {
            get => (double)this.GetValue(NukWindow.TitleCornerRightWidthProperty);
            set => this.SetValue(NukWindow.TitleCornerRightWidthProperty, value);
        }
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleCornerRightHeight {
            get => (double)this.GetValue(NukWindow.TitleCornerRightHeightProperty);
            set => this.SetValue(NukWindow.TitleCornerRightHeightProperty, value);
        }
        
        [Category("Custom Look - Title Bar Right")]
        public double TitleCornerRightQuadBezierRadius {
            get => (double)this.GetValue(NukWindow.TitleCornerRightQuadBezierRadiusProperty);
            set => this.SetValue(NukWindow.TitleCornerRightQuadBezierRadiusProperty, value);
        }

        private Path? part_backgroundLayer1 = null;
        private Path? part_backgroundLayer2 = null;

        private Button? part_focusButton = null;
        private Button? part_minimizeButton = null;
        private Button? part_maximizeButton = null;
        private Button? part_closeButton = null;

        
        private FrameworkElement? part_windowContent = null;
        private FrameworkElement? part_windowTitleBarButtons = null;
        private FrameworkElement? part_windowTitle = null;
        
        
        private const string _StylesSource = "pack://application:,,,/DukNukWpf;component/Themes/Styles.xaml";
        
        static NukWindow(){  
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NukWindow), new FrameworkPropertyMetadata(typeof(NukWindow)));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            base.OnPropertyChanged(e);
            string name = e.Property.Name;
            if (name == nameof(this.Padding) ||
                name == nameof(this.ActualWidth) ||
                name == nameof(this.ActualHeight) ||
                name == nameof(this.WindowState) ||
                name == nameof(this.Margin)) {
                this.RecalculatePaths();
            }
        }
        
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (this.Template == null) { return; }

            if (this.Template.FindName("PART_focusButton", this) is Button focusButton) {
                this.part_focusButton = focusButton;
            } else {
                this.part_focusButton = null;
            }
            
            if (this.Template.FindName("PART_minimizeButton", this) is Button minimizeButton) {
                if (this.part_minimizeButton != minimizeButton) {
                    if (this.part_minimizeButton != null) {
                        this.part_minimizeButton.Click -= this.MinimizeWindow;
                    }
                    this.part_minimizeButton = minimizeButton;
                    this.part_minimizeButton.Click += this.MinimizeWindow;
                }
            } else {
                if (this.part_minimizeButton != null) {
                    this.part_minimizeButton.Click -= this.MinimizeWindow;
                }
                this.part_minimizeButton = null;
            }
            
            if (this.Template.FindName("PART_maximizeButton", this) is Button maximizeButton) {
                if (this.part_maximizeButton != maximizeButton) {
                    if (this.part_maximizeButton != null) {
                        this.part_maximizeButton.Click -= this.MaximizeWindow;
                    }
                    this.part_maximizeButton = maximizeButton;
                    this.part_maximizeButton.Click += this.MaximizeWindow;
                }
            } else {
                if (this.part_maximizeButton != null) {
                    this.part_maximizeButton.Click -= this.MaximizeWindow;
                }
                this.part_maximizeButton = null;
            }
            
            if (this.Template.FindName("PART_closeButton", this) is Button closeButton) {
                if (this.part_closeButton != closeButton) {
                    if (this.part_closeButton != null) {
                        this.part_closeButton.Click -= this.CloseWindow;
                    }
                    this.part_closeButton = closeButton;
                    this.part_closeButton.Click += this.CloseWindow;
                }
            } else {
                if (this.part_closeButton != null) {
                    this.part_closeButton.Click -= this.CloseWindow;
                }
                this.part_closeButton = null;
            }
            
            if (this.Template.FindName("PART_backgroundLayer1", this) is Path layer0) {
                this.part_backgroundLayer1 = layer0;
            } else {
                this.part_backgroundLayer1 = null;
            }
            
            if (this.Template.FindName("PART_backgroundLayer2", this) is Path layer1) {
                this.part_backgroundLayer2 = layer1;
            } else {
                this.part_backgroundLayer2 = null;
            }
            
            if (this.Template.FindName("PART_windowTitle", this) is FrameworkElement windowTitle) {
                this.part_windowTitle = windowTitle;
            } else {
                this.part_windowTitle = null;
            }
            
            if (this.Template.FindName("PART_windowTitleBarButtons", this) is FrameworkElement windowTitleBar) {
                this.part_windowTitleBarButtons = windowTitleBar;
            } else {
                this.part_windowTitleBarButtons = null;
            }
            
            if (this.Template.FindName("PART_windowContent", this) is FrameworkElement windowContent) {
                this.part_windowContent = windowContent;
            } else {
                this.part_windowContent = null;
            }
            
            this.RecalculatePaths();
        }
        
        private static void OnWindowShapeChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e) {
            if (d is not NukWindow win) return;
            win.RecalculatePaths();
        }

        private void CloseWindow(object sender, RoutedEventArgs e) {
            this.part_focusButton?.Focus();
            this.Close();
        }
        
        private void MaximizeWindow(object sender, RoutedEventArgs e) {
            this.part_focusButton?.Focus();
            switch (this.WindowState) {
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    break;
            }
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e) {
            this.part_focusButton?.Focus();
            this.WindowState = WindowState.Minimized;
        }

        //int debug = 0;

        private void RecalculatePaths() {
            //this.debug++;
            //this.Title = $"{this.debug} - {this.ActualWidth}x{this.ActualHeight}";
            
            if (this.ActualWidth == 0 || this.ActualHeight == 0 ||
                double.IsNaN(this.ActualWidth) || double.IsNaN(this.ActualHeight)) {
                if (this.part_backgroundLayer1 != null) {
                    this.part_backgroundLayer1.Data = Geometry.Empty;
                }
                if (this.part_backgroundLayer2 != null) {
                    this.part_backgroundLayer2.Data = Geometry.Empty;
                }
                return;
            }

            double width = this.ActualWidth - this.Padding.Left - this.Padding.Right;
            double height = this.ActualHeight - this.Padding.Top - this.Padding.Bottom;
            
            double gapLeft = double.IsNaN(this.TitleGapLeft)
                ? (width - this.TitleGapRight) / 3.0
                : this.TitleGapLeft;
            double gapRight = double.IsNaN(this.TitleGapRight) 
                ? this.TitleBezierRightWidth * 2
                : this.TitleGapRight;

            if (this.part_backgroundLayer1 != null) {
                if (this.WindowState == WindowState.Maximized) {
                    DrawPathLayer1Maximized(this.part_backgroundLayer1, width, height);
                } else {
                    DrawPathLayer1(this.part_backgroundLayer1,
                        this.FirstLayerPadding,
                        width, height,
                        this.QuadBezierRadius,
                        gapLeft,
                        this.TitleBezierLeftWidth, this.TitleBezierLeftHeight, this.TitleBezierLeftOffset,
                        this.TitleCornerRightWidth, this.TitleCornerRightHeight, this.TitleCornerRightQuadBezierRadius);
                }
            }

            if (this.part_backgroundLayer2 != null) {
                if (this.WindowState == WindowState.Maximized) {
                    DrawPathLayer2Maximized(this.part_backgroundLayer2,
                        this.SecondLayerPadding,
                        width, height, this.QuadBezierRadius,
                        gapLeft, this.TitleBezierLeftWidth, this.TitleBezierLeftHeight, this.TitleBezierLeftOffset,
                        gapRight, this.TitleBezierRightWidth, this.TitleBezierRightHeight, this.TitleBezierRightOffset
                        );
                } else {
                    DrawPathLayer2(this.part_backgroundLayer2,
                        this.SecondLayerPadding,
                        width, height, this.QuadBezierRadius,
                        gapLeft, this.TitleBezierLeftWidth, this.TitleBezierLeftHeight, this.TitleBezierLeftOffset,
                        gapRight, this.TitleBezierRightWidth, this.TitleBezierRightHeight, this.TitleBezierRightOffset
                        );
                }
            }

            TitlePositionValue tpv = this.TitlePosition; 
            MenuPositionValue mpv = this.MenuPosition;

            if (mpv == MenuPositionValue.OnTitleBar) tpv = TitlePositionValue.Top;

            if (this.part_windowTitle != null) {
                if (tpv == TitlePositionValue.Left) {
                    double ah = this.part_windowTitle.ActualHeight;
                    double centeringOffset = (this.TitleZoneHeight - this.TitleBezierLeftHeight - ah) / 2;
                    this.part_windowTitle.Margin = new Thickness(
                        this.SecondLayerPadding.Left + this.ContentPadding.Left, this.SecondLayerPadding.Top + this.TitleBezierLeftHeight + centeringOffset, 0, 0);
                } else {
                    double ah = this.part_windowTitle.ActualHeight;
                    double leftOffset = gapLeft + this.TitleBezierLeftWidth / 2;
                    double topOff = 5;
                    double centeringOffset = (this.TitleZoneHeight - this.TitleBezierLeftHeight - ah) / 2;
                    this.part_windowTitle.Margin = new Thickness(
                        this.SecondLayerPadding.Left + leftOffset, this.SecondLayerPadding.Top + topOff, 0, 0);
                }
            }
            
            if (this.part_windowTitleBarButtons != null) {
                this.part_windowTitleBarButtons.Margin = new Thickness(
                    0, this.FirstLayerPadding.Top, this.FirstLayerPadding.Right + this.TitleCornerRightWidth, 0);
            }
            if (this.part_windowContent != null) {
                this.part_windowContent.Margin = new Thickness(
                    this.ContentPadding.Left + this.SecondLayerPadding.Left, 
                    this.ContentPadding.Top + this.SecondLayerPadding.Top + this.TitleZoneHeight, 
                    this.ContentPadding.Right + this.SecondLayerPadding.Right, 
                    this.ContentPadding.Bottom + this.SecondLayerPadding.Bottom);
            }
        }

        public static void DrawPathLayer1(Path target, Thickness pad, double width, double height, double quadRadius, 
            double gapLeft, double bezLeftWidth, double bezLeftHeight, double bezLeftCurveOffset, 
            double cornerRightWidth, double cornerRightHeight, double cornerQuadRadius) {
            
            double wadj = width - pad.Left - pad.Right;
            double hadj = height - pad.Top - pad.Bottom;

            if (gapLeft < bezLeftWidth / 2) gapLeft = bezLeftWidth / 2;

            Point tl = new Point(pad.Left, pad.Top);
            Point tr = new Point(pad.Left + wadj, pad.Top);
            Point bl = new Point( pad.Left, pad.Top + hadj);
            Point br = new Point( pad.Left + wadj, pad.Top + hadj);


            // quadratic bezier top left small
            Point A0 = new Point(tl.X, tl.Y + bezLeftHeight + quadRadius);
            Point A1 = new Point(tl.X, tl.Y + bezLeftHeight);
            Point A2 = new Point(tl.X + quadRadius, tl.Y + bezLeftHeight);
            // bezier top left
            Point B0 = new Point(tl.X + gapLeft - bezLeftWidth / 2, tl.Y + bezLeftHeight);
            Point B1 = new Point(tl.X + gapLeft + bezLeftCurveOffset, tl.Y + bezLeftHeight);
            Point B2 = new Point(tl.X + gapLeft - bezLeftCurveOffset, tl.Y);
            Point B3 = new Point(tl.X + gapLeft + bezLeftWidth / 2, tl.Y );
            // corner right - quadratic bezier 1
            Point C0 = new Point(tr.X - cornerRightWidth - cornerQuadRadius, tr.Y);
            Point C1 = new Point(tr.X - cornerRightWidth, tr.Y);
            Point C2 = new Point(tr.X - cornerRightWidth + cornerQuadRadius, tr.Y + cornerQuadRadius);
            // corner right - quadratic bezier 2
            Point D0 = new Point(tr.X - cornerQuadRadius, tr.Y + cornerRightHeight - cornerQuadRadius);
            Point D1 = new Point(tr.X, tr.Y + cornerRightHeight);
            Point D2 = new Point(tr.X, tr.Y + cornerRightHeight + cornerQuadRadius);
            // quadratic bezier bottom right
            Point E0 = new Point(br.X, br.Y - quadRadius);
            Point E1 = new Point(br.X, br.Y);
            Point E2 = new Point(br.X - quadRadius, br.Y);
            // quadratic bezier bottom left
            Point F0 = new Point(bl.X + quadRadius, bl.Y);
            Point F1 = new Point(bl.X, bl.Y);
            Point F2 = new Point(bl.X, bl.Y - quadRadius);

            PathFigure fig = new PathFigure();
            fig.StartPoint = A0;
            fig.Segments.Add(new QuadraticBezierSegment(A1, A2, true));
            fig.Segments.Add(new LineSegment(B0, true));
            fig.Segments.Add(new BezierSegment(B1, B2, B3, true));
            fig.Segments.Add(new LineSegment(C0, true));
            fig.Segments.Add(new QuadraticBezierSegment(C1, C2,true));
            fig.Segments.Add(new LineSegment(D0, true));
            fig.Segments.Add(new QuadraticBezierSegment(D1, D2, true));
            fig.Segments.Add(new LineSegment(E0, true));
            fig.Segments.Add(new QuadraticBezierSegment(E1, E2, true));
            fig.Segments.Add(new LineSegment(F0, true));
            fig.Segments.Add(new QuadraticBezierSegment(F1, F2, true));
            fig.Segments.Add(new LineSegment(A0, true));

            PathGeometry geometry0 = new PathGeometry();
            geometry0.Figures.Add(fig);
            target.Data = geometry0;
        }
        
        public static void DrawPathLayer1Maximized(Path target, double width, double height) {
            PathFigure fig = new PathFigure();
            fig.StartPoint = new Point(0,0);
            fig.Segments.Add(new LineSegment(new Point(width,0), true));
            fig.Segments.Add(new LineSegment(new Point(width,height), true));
            fig.Segments.Add(new LineSegment(new Point(0,height), true));
            fig.Segments.Add(new LineSegment(new Point(0,0), true));

            PathGeometry geometry0 = new PathGeometry();
            geometry0.Figures.Add(fig);
            target.Data = geometry0;
        }

        public static void DrawPathLayer2(Path target, Thickness pad, double width, double height, double quadRadius, 
            double gapLeft, double bezLeftWidth, double bezLeftHeight, double bezLeftCurveOffset,
            double gapRight, double bezRightWidth, double bezRightHeight, double bezRightCurveOffset) {
            double wadj = width - pad.Left - pad.Right;
            double hadj = height - pad.Top - pad.Bottom;

            if (gapLeft < bezLeftWidth / 2) gapLeft = bezLeftWidth / 2;
            if (gapRight < bezRightWidth / 2) gapRight = bezRightWidth / 2;

            Point tl = new Point(pad.Left, pad.Top);
            Point tr = new Point(pad.Left + wadj, pad.Top);
            Point bl = new Point( pad.Left, pad.Top + hadj);
            Point br = new Point( pad.Left + wadj, pad.Top + hadj);


            // quadratic bezier top left small
            Point A0 = new Point(tl.X, tl.Y + bezLeftHeight + quadRadius);
            Point A1 = new Point(tl.X, tl.Y + bezLeftHeight);
            Point A2 = new Point(tl.X + quadRadius, tl.Y + bezLeftHeight);
            // bezier top left
            Point B0 = new Point(tl.X + gapLeft - bezLeftWidth / 2, tl.Y + bezLeftHeight);
            Point B1 = new Point(tl.X + gapLeft + bezLeftCurveOffset, tl.Y + bezLeftHeight);
            Point B2 = new Point(tl.X + gapLeft - bezLeftCurveOffset, tl.Y);
            Point B3 = new Point(tl.X + gapLeft + bezLeftWidth / 2, tl.Y );
            // bezier top right
            Point C0 = new Point(tr.X - gapRight - bezRightWidth / 2, tr.Y);
            Point C1 = new Point(tr.X - gapRight + bezRightCurveOffset, tr.Y);
            Point C2 = new Point(tr.X - gapRight - bezRightCurveOffset, tr.Y + bezRightHeight);
            Point C3 = new Point(tr.X - gapRight + bezRightWidth / 2, tr.Y + bezRightHeight);
            // quadratic bezier top right
            Point D0 = new Point(tr.X - quadRadius, tr.Y + bezRightHeight);
            Point D1 = new Point(tr.X, tr.Y + bezRightHeight);
            Point D2 = new Point(tr.X, tr.Y + bezRightHeight + quadRadius);
            // quadratic bezier bottom right
            Point E0 = new Point(br.X, br.Y - quadRadius);
            Point E1 = new Point(br.X, br.Y);
            Point E2 = new Point(br.X - quadRadius, br.Y);
            // quadratic bezier bottom left
            Point F0 = new Point(bl.X + quadRadius, bl.Y);
            Point F1 = new Point(bl.X, bl.Y);
            Point F2 = new Point(bl.X, bl.Y - quadRadius);


            PathFigure fig = new PathFigure();
            fig.StartPoint = A0;
            fig.Segments.Add(new QuadraticBezierSegment(A1, A2, true));
            fig.Segments.Add(new LineSegment(B0, true));
            fig.Segments.Add(new BezierSegment(B1, B2, B3, true));
            fig.Segments.Add(new LineSegment(C0, true));
            fig.Segments.Add(new BezierSegment(C1, C2, C3, true));
            fig.Segments.Add(new LineSegment(D0, true));
            fig.Segments.Add(new QuadraticBezierSegment(D1, D2, true));
            fig.Segments.Add(new LineSegment(E0, true));
            fig.Segments.Add(new QuadraticBezierSegment(E1, E2, true));
            fig.Segments.Add(new LineSegment(F0, true));
            fig.Segments.Add(new QuadraticBezierSegment(F1, F2, true));
            fig.Segments.Add(new LineSegment(A0, true));

            PathGeometry geometry0 = new PathGeometry();
            geometry0.Figures.Add(fig);
            target.Data = geometry0;
        }
        
        public static void DrawPathLayer2Maximized(Path target, Thickness pad, double width, double height, double quadRadius, 
            double gapLeft, double bezLeftWidth, double bezLeftHeight, double bezLeftCurveOffset,
            double gapRight, double bezRightWidth, double bezRightHeight, double bezRightCurveOffset) {
            double wadj = width - pad.Left - pad.Right;
            double hadj = height - pad.Top - pad.Bottom;

            if (gapLeft < bezLeftWidth / 2) gapLeft = bezLeftWidth / 2;
            if (gapRight < bezRightWidth / 2) gapRight = bezRightWidth / 2;

            Point tl = new Point(pad.Left, pad.Top);
            Point tr = new Point(pad.Left + wadj, pad.Top);
            Point bl = new Point( pad.Left, pad.Top + hadj);
            Point br = new Point( pad.Left + wadj, pad.Top + hadj);


            // top left
            Point A0 = new Point(tl.X, tl.Y + bezLeftHeight);
            // bezier top left
            Point B0 = new Point(tl.X + gapLeft - bezLeftWidth / 2, tl.Y + bezLeftHeight);
            Point B1 = new Point(tl.X + gapLeft + bezLeftCurveOffset, tl.Y + bezLeftHeight);
            Point B2 = new Point(tl.X + gapLeft - bezLeftCurveOffset, tl.Y);
            Point B3 = new Point(tl.X + gapLeft + bezLeftWidth / 2, tl.Y );
            // bezier top right
            Point C0 = new Point(tr.X - gapRight - bezRightWidth / 2, tr.Y);
            Point C1 = new Point(tr.X - gapRight + bezRightCurveOffset, tr.Y);
            Point C2 = new Point(tr.X - gapRight - bezRightCurveOffset, tr.Y + bezRightHeight);
            Point C3 = new Point(tr.X - gapRight + bezRightWidth / 2, tr.Y + bezRightHeight);
            // top right
            Point D0 = new Point(tr.X, tr.Y + bezRightHeight);
            // bottom right
            Point E0 = new Point(br.X, br.Y);
            // bottom left
            Point F0 = new Point(bl.X, bl.Y);


            PathFigure fig = new PathFigure();
            fig.StartPoint = A0;
            fig.Segments.Add(new LineSegment(B0, true));
            fig.Segments.Add(new BezierSegment(B1, B2, B3, true));
            fig.Segments.Add(new LineSegment(C0, true));
            fig.Segments.Add(new BezierSegment(C1, C2, C3, true));
            fig.Segments.Add(new LineSegment(D0, true));
            fig.Segments.Add(new LineSegment(E0, true));
            fig.Segments.Add(new LineSegment(F0, true));
            fig.Segments.Add(new LineSegment(A0, true));

            PathGeometry geometry0 = new PathGeometry();
            geometry0.Figures.Add(fig);
            target.Data = geometry0;
        }

    }
}
