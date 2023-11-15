using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Timer = System.Timers.Timer;
namespace DukNuk.Wpf.Controls {
    
    
    /// <summary>
    /// Counter Control Base class for shared WPF style template
    /// </summary>
    public abstract class NukCounterBase : Control {

        #region DependencyProperties
        
        public static readonly DependencyProperty ValueStringProperty = DependencyProperty.Register(nameof(ValueString),
            typeof(string),
            typeof(NukCounterBase),
            new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public static readonly DependencyProperty ForegroundInvalidValueProperty = DependencyProperty.Register(nameof(ForegroundInvalidValue),
            typeof(Brush),
            typeof(NukCounterBase),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Red))
            );
        public static readonly DependencyProperty IsStringValueValidProperty = DependencyProperty.Register(nameof(IsStringValueValid),
            typeof(bool),
            typeof(NukCounterBase),
            new FrameworkPropertyMetadata(false)
            );

        public static readonly DependencyProperty ShowButtonsProperty = DependencyProperty.Register(nameof(ShowButtons),
            typeof(bool),
            typeof(NukCounterBase),
            new FrameworkPropertyMetadata(true)
            );

        #endregion

        #region Properties

        [Category("Custom Properties")]
        public bool ShowButtons {
            get => (bool)this.GetValue(NukCounterBase.ShowButtonsProperty);
            set => this.SetValue(NukCounterBase.ShowButtonsProperty, value);
        }

        [Category("Custom Properties")]
        public string ValueString {
            get => (string)this.GetValue(NukCounterBase.ValueStringProperty);
            set => this.SetValue(NukCounterBase.ValueStringProperty, value);
        }
        
        public bool IsStringValueValid {
            get => (bool)this.GetValue(NukCounterBase.IsStringValueValidProperty);
            set => this.SetValue(NukCounterBase.IsStringValueValidProperty, value);
        }

        [Category("Custom Properties")]
        public Brush ForegroundInvalidValue {
            get => (Brush)this.GetValue(NukCounterBase.ForegroundInvalidValueProperty);
            set => this.SetValue(NukCounterBase.ForegroundInvalidValueProperty, value);
        }

        #endregion

        #region protected stuff

        // template parts
        protected FrameworkElement? _templateButtonInc = null;
        protected FrameworkElement? _templateButtonDec = null;
        protected TextBox? _templateTextBox = null;

        #endregion

        #region Constructor & Template

        static NukCounterBase() {
            NukCounterBase.DefaultStyleKeyProperty.OverrideMetadata(typeof(NukCounterBase), new FrameworkPropertyMetadata(typeof(NukCounterBase)));
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (this.Template == null) { return; }

            FrameworkElement? btnInc = this.Template.FindName("PART_buttonInc", this) as FrameworkElement;
            if (btnInc != this._templateButtonInc) {
                if (this._templateButtonInc != null) {
                    this._templateButtonInc.MouseDown -= this._templateButtonInc_PreviewMouseDown;
                    this._templateButtonInc.PreviewMouseUp -= this._templateButtonInc_PreviewMouseUp;
                }
                this._templateButtonInc = btnInc;

                if (this._templateButtonInc != null) {
                    this._templateButtonInc.PreviewMouseDown += this._templateButtonInc_PreviewMouseDown;
                    this._templateButtonInc.PreviewMouseUp += this._templateButtonInc_PreviewMouseUp;
                }
            }

            FrameworkElement? btnDec = this.Template.FindName("PART_buttonDec", this) as FrameworkElement;
            if (btnDec != this._templateButtonDec) {
                if (this._templateButtonDec != null) {
                    this._templateButtonDec.MouseDown -= this._templateButtonDec_PreviewMouseDown;
                    this._templateButtonDec.PreviewMouseUp -= this._templateButtonDec_PreviewMouseUp;
                }
                this._templateButtonDec = btnDec;

                if (this._templateButtonDec != null) {
                    this._templateButtonDec.PreviewMouseDown += this._templateButtonDec_PreviewMouseDown;
                    this._templateButtonDec.PreviewMouseUp += this._templateButtonDec_PreviewMouseUp;
                }
            }
            
            TextBox? textBox = this.Template.FindName("PART_theTextBox", this) as TextBox;
            if (textBox != this._templateTextBox) {
                if (this._templateTextBox != null) {
                    this._templateTextBox.TextChanged -= this._templateTextBox_TextChanged;
                }
                this._templateTextBox = textBox;
                if (this._templateTextBox != null) {
                    this._templateTextBox.TextChanged += this._templateTextBox_TextChanged;
                }
            }
        }
        
        #endregion

        #region Methods

        protected abstract void _templateButtonDec_PreviewMouseUp(object sender, MouseButtonEventArgs e);
        protected abstract void _templateButtonDec_PreviewMouseDown(object sender, MouseButtonEventArgs e);
        protected abstract void _templateButtonInc_PreviewMouseUp(object sender, MouseButtonEventArgs e);

        protected abstract void _templateButtonInc_PreviewMouseDown(object sender, MouseButtonEventArgs e);

        protected abstract void _templateTextBox_TextChanged(object sender, TextChangedEventArgs e);

        #endregion
    }
    
    /// <summary>
    /// Counter Control advanced base class with generic numeric data type
    /// </summary>
    /// <typeparam name="T">Some numeric data type like double, int, uint, long, ulong</typeparam>
    public abstract class NukCounterBase<T> : NukCounterBase where T 
        : IComparable
        , IComparable<T>
        , IFormattable
        , IConvertible
        , IEquatable<T> {
        
        public static readonly RoutedCommand IncrementValueStartCommand = new RoutedCommand();
        public static readonly RoutedCommand DecrementValueStartCommand = new RoutedCommand();
        public static readonly RoutedCommand IncrementValueStopCommand = new RoutedCommand();
        public static readonly RoutedCommand DecrementValueStopCommand = new RoutedCommand();
        public static readonly RoutedCommand ValidateTextInputCommand = new RoutedCommand();

        #region DependencyProperties

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value),
            typeof(T),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(default(T), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValuePropertyChangedCallback)
            );

        public static readonly DependencyProperty ValueMinProperty = DependencyProperty.Register(nameof(ValueMin),
            typeof(T),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(default(T))
            );

        public static readonly DependencyProperty ValueMaxProperty = DependencyProperty.Register(nameof(ValueMax),
            typeof(T),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(default(T))
            );

        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(nameof(Increment),
            typeof(T),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(default(T))
            );

        public static readonly DependencyProperty AutoIncrementIntervalProperty = DependencyProperty.Register(nameof(AutoIncrementInterval),
            typeof(int),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(50)
            );

        public static readonly DependencyProperty AutoIncrementDelayProperty = DependencyProperty.Register(nameof(AutoIncrementDelay),
            typeof(int),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(500)
            );

        public static readonly DependencyProperty AutoIncrementAccelerationMultiplierProperty = DependencyProperty.Register(nameof(AutoIncrementAccelerationMultiplier),
            typeof(T),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(default(T))
            );

        public static readonly DependencyProperty AutoIncrementAccelerationTicksProperty = DependencyProperty.Register(nameof(AutoIncrementAccelerationTicks),
            typeof(int),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata(10)
            );

        public static readonly DependencyProperty ToStringFormatProperty = DependencyProperty.Register(nameof(ToStringFormat),
            typeof(string),
            typeof(NukCounterBase<T>),
            new FrameworkPropertyMetadata("{0:0.0###}")
            );

        #endregion

        #region Properties

        [Category("Custom Properties")]
        public T Value {
            get => (T)this.GetValue(NukCounterBase<T>.ValueProperty);
            set {
                if (value.CompareTo(this.ValueMin) >= 0 && 
                    value.CompareTo(this.ValueMax) <= 0) {
                    this.SetValue(NukCounterBase<T>.ValueProperty, value);
                }
            }
        }

        [Category("Custom Properties")]
        public T ValueMin {
            get => (T)this.GetValue(NukCounterBase<T>.ValueMinProperty);
            set {
                if (value.CompareTo(this.ValueMax) <= 0) {
                    this.SetValue(NukCounterBase<T>.ValueMinProperty, value);
                    if (this.Value.CompareTo(value) < 0) {
                        this.SetValue(NukCounterBase<T>.ValueProperty, value);
                    }
                }
            }
        }

        [Category("Custom Properties")]
        public T ValueMax {
            get => (T)this.GetValue(NukCounterBase<T>.ValueMaxProperty);
            set {
                if (value.CompareTo(this.ValueMin) >= 0) {
                    this.SetValue(NukCounterBase<T>.ValueMaxProperty, value);
                    if (this.Value.CompareTo(value) > 0) {
                        this.SetValue(NukCounterBase<T>.ValueProperty, value);
                    }
                }
            }
        }

        [Category("Custom Properties")]
        public T Increment {
            get => (T)this.GetValue(NukCounterBase<T>.IncrementProperty);
            set => this.SetValue(NukCounterBase<T>.IncrementProperty, value);
        }

        [Category("Custom Properties")]
        public int AutoIncrementInterval {
            get => (int)this.GetValue(NukCounterBase<T>.AutoIncrementIntervalProperty);
            set {
                if (value >= 50) {
                    this.SetValue(NukCounterBase<T>.AutoIncrementIntervalProperty, value);
                }
            }
        }

        [Category("Custom Properties")]
        public int AutoIncrementDelay {
            get => (int)this.GetValue(NukCounterBase<T>.AutoIncrementDelayProperty);
            set {
                if (value >= 100) {
                    this.SetValue(NukCounterBase<T>.AutoIncrementDelayProperty, value);
                }
            }
        }

        [Category("Custom Properties")]
        public T AutoIncrementAccelerationMultiplier {
            get => (T)this.GetValue(NukCounterBase<T>.AutoIncrementAccelerationMultiplierProperty);
            set {
                if (value.CompareTo(1) >= 0) {
                    this.SetValue(NukCounterBase<T>.AutoIncrementAccelerationMultiplierProperty, value);
                }
            }
        }

        [Category("Custom Properties")]
        public int AutoIncrementAccelerationTicks {
            get => (int)this.GetValue(NukCounterBase<T>.AutoIncrementAccelerationTicksProperty);
            set {
                if (value >= 2) {
                    this.SetValue(NukCounterBase<T>.AutoIncrementAccelerationTicksProperty, value);
                }
            }
        }

        [Category("Custom Properties")]
        public string ToStringFormat {
            get => (string)this.GetValue(NukCounterBase<T>.ToStringFormatProperty);
            set => this.SetValue(NukCounterBase<T>.ToStringFormatProperty, value);
        }

        #endregion

        #region protected stuff

        protected readonly Timer _incrementCommitDelay;
        protected T _value = default(T);
        protected T _increment = default(T);
        protected bool _incrementPositive = true;
        protected bool _incrementActive = false;
        protected int _incrementTicks = 0;
        protected bool _suppressTextChangeHandler = false;
        
        #endregion

        #region Constructor & Template

        static NukCounterBase() {
            NukCounterBase<T>.ValueStringProperty.OverrideMetadata(typeof(NukCounterBase<T>), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValueStringPropertyChangedCallback));
        }

        public NukCounterBase() {
            this._incrementCommitDelay = new Timer() {
                AutoReset = false,
                Interval = this.AutoIncrementDelay,
            };

            this._incrementCommitDelay.Elapsed += this._incrementCommitDelay_Elapsed;

            this.CommandBindings.Add(new CommandBinding(IncrementValueStartCommand, ExecuteIncrementValue, CanExecuteGenericCommand));
            this.CommandBindings.Add(new CommandBinding(DecrementValueStartCommand, ExecuteDecrementValue, CanExecuteGenericCommand));
            this.CommandBindings.Add(new CommandBinding(IncrementValueStopCommand, ExecuteIncrementValueCommit, CanExecuteGenericCommand));
            this.CommandBindings.Add(new CommandBinding(DecrementValueStopCommand, ExecuteIncrementValueCommit, CanExecuteGenericCommand));
            this.CommandBindings.Add(new CommandBinding(ValidateTextInputCommand, ExecuteValidateTextInput, CanExecuteGenericCommand));

            this.PreviewKeyDown += this.PreviewKeyDownHandler;
            this.PreviewKeyUp += this.PreviewKeyUpHandler;

            this._value = this.Value;
            this.UpdateValueString(this.Value);
            this.RegenerateStringValueValidity(this.ValueString);
        }

        protected override void _templateButtonDec_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            NukCounterBase<T>.DecrementValueStopCommand.Execute(null, this);
        }

        protected override void _templateButtonDec_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            NukCounterBase<T>.DecrementValueStartCommand.Execute(null, this);
        }

        protected override void _templateButtonInc_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            NukCounterBase<T>.IncrementValueStopCommand.Execute(null, this);
        }

        protected override void _templateButtonInc_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            NukCounterBase<T>.IncrementValueStartCommand.Execute(null, this);
        }
        
        protected override void _templateTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox txt = sender as TextBox;
            NukCounterBase<T>.ValidateTextInputCommand.Execute(txt?.Text, this);
        }

        #endregion

        #region button incrementation/decrementation


        protected void PreviewKeyDownHandler(object sender, KeyEventArgs e) {
            switch (e.Key) {
                //case Key.Left:
                case Key.Down: {
                        if (this._incrementActive == false) {
                            NukCounterBase<T>.DecrementValueStartCommand.Execute(this, this);
                        }
                        break;
                    }
                //case Key.Right:
                case Key.Up: {
                        if (this._incrementActive == false) {
                            NukCounterBase<T>.IncrementValueStartCommand.Execute(this, this);
                        }
                        break;
                    }
            }
        }
        protected void PreviewKeyUpHandler(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Left:
                case Key.Down: {
                        NukCounterBase<T>.DecrementValueStopCommand.Execute(this, this);
                        break;
                    }
                case Key.Right:
                case Key.Up: {
                        NukCounterBase<T>.IncrementValueStopCommand.Execute(this, this);
                        break;
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="increment">current increment</param>
        /// <param name="multiplier">increment multiplier</param>
        /// <returns>New increment</returns>
        protected abstract T MultiplyIncrement(T increment, T multiplier);
        
        /// <summary>
        /// Adds increment to value
        /// </summary>
        /// <param name="value">old value</param>
        /// <param name="increment">how much to increment by</param>
        /// <returns>New incremented value</returns>
        protected abstract T IncrementValue(T value, T increment);
        
        /// <summary>
        /// Subtract increment from value
        /// </summary>
        /// <param name="value">old value</param>
        /// <param name="increment">how much to decrement by</param>
        /// <returns>New incremented value</returns>
        protected abstract T DecrementValue(T value, T increment);

        protected abstract bool TryParse(string text, out T value);

        
        /// <summary>
        /// Applies increment to internal <see cref="_value"/>
        /// </summary>
        protected void DoIncrementInternal() {
            try {
                T incVal = this.IncrementValue(this._value, this._increment);
                if (incVal.CompareTo(this.ValueMax) < 0) {
                    this._value = incVal;
                } else {
                    this._value = this.ValueMax;
                }
            } catch (OverflowException) {
                this._value = this.ValueMax;
            }
        }

        /// <summary>
        /// Applies decrement to internal <see cref="_value"/>
        /// </summary>
        protected void DoDecrementInternal() {
            try {
                T decVal = this.DecrementValue(this._value, this._increment);
                if (decVal.CompareTo(this.ValueMin) > 0) {
                    this._value = decVal;
                } else {
                    this._value = this.ValueMin;
                }
            } catch (OverflowException) {
                this._value = this.ValueMin;
            }
        }

        protected void _incrementCommitDelay_Elapsed(object sender, ElapsedEventArgs e) {
            this._incrementCommitDelay.Stop();

            this.Dispatcher.Invoke(new Action(() => {
                // increment acceleration
                this._incrementTicks++;
                if (this._incrementTicks >= this.AutoIncrementAccelerationTicks) {
                    this._increment = this.MultiplyIncrement(this._increment,this.AutoIncrementAccelerationMultiplier);
                    this._incrementTicks = 0;
                }

                if (this._incrementPositive) {
                    this.DoIncrementInternal();
                } else {
                    this.DoDecrementInternal();
                }

                this.UpdateValueString(this._value);
                
                this._incrementCommitDelay.Interval = this.AutoIncrementInterval;
            }));

            this._incrementCommitDelay.Start();
        }

        protected static void ExecuteIncrementValueCommit(object sender, ExecutedRoutedEventArgs e) {
            if (sender is not NukCounterBase<T> target) return;
            
            target._incrementCommitDelay.Stop();
            target._suppressTextChangeHandler = false;
            target._incrementActive = false;
            target.Value = target._value;
        }

        protected static void ExecuteIncrementValue(object sender, ExecutedRoutedEventArgs e) {
            if (sender is not NukCounterBase<T> target) return;
            
            target._suppressTextChangeHandler = true;
            target._increment = target.Increment;
            target._incrementTicks = 0;
            target._incrementPositive = true;
            target._incrementActive = true;
            
            target.DoIncrementInternal();
            target.UpdateValueString(target._value);

            target._incrementCommitDelay.Interval = target.AutoIncrementDelay;
            target._incrementCommitDelay.Start();

            e.Handled = true;
        }
        protected static void ExecuteDecrementValue(object sender, ExecutedRoutedEventArgs e) {
            if (sender is not NukCounterBase<T> target) return;

            target._suppressTextChangeHandler = true;
            target._increment = target.Increment;
            target._incrementTicks = 0;
            target._incrementPositive = false;
            target._incrementActive = true;
            
            target.DoDecrementInternal();
            target.UpdateValueString(target._value);
            
            target._incrementCommitDelay.Interval = target.AutoIncrementDelay;
            target._incrementCommitDelay.Start();

            e.Handled = true;
        }
        protected static void ExecuteValidateTextInput(object sender, ExecutedRoutedEventArgs e) {
            if (sender is not NukCounterBase<T> target ||
                e.Parameter is not string newText) return;

            target.RegenerateStringValueValidity(newText);
        }

        protected static void CanExecuteGenericCommand(object sender, CanExecuteRoutedEventArgs e) {
            NukCounterBase<T>? target = sender as NukCounterBase<T>;
            e.CanExecute = target?.IsEnabled == true;
        }

        #endregion

        #region text value stuff
        public bool IsValueInRange(T newValue) {
            if (newValue.CompareTo(this.ValueMax) > 0) return false;
            if (newValue.CompareTo(this.ValueMin) < 0) return false;
            return true;
        }

        protected static void ValueStringPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is not NukCounterBase<T> target) return;

            if (!target._suppressTextChangeHandler) {
                if (target.TryParse(target.ValueString, out T newValue)) {
                    if (newValue.CompareTo(target.ValueMax) > 0) {
                        newValue = target.ValueMax;
                    }
                    if (newValue.CompareTo(target.ValueMin) < 0) {
                        newValue = target.ValueMin;
                    }
                    target.Value = newValue;
                }

                target.UpdateValueString(target.Value);
                target.RegenerateStringValueValidity(target.ValueString);
            }
        }

        protected static void ValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NukCounterBase<T>? target = d as NukCounterBase<T>;
            if (target == null) return;
            
            target._value = target.Value;
            target.UpdateValueString(target.Value);
            target.RegenerateStringValueValidity(target.ValueString);
            target.FireValueChangedEvent();
        }

        protected virtual void UpdateValueString(T newValue) {
            string str = String.Format(this.ToStringFormat, newValue);
            this.ValueString = str;
        }

        protected virtual void RegenerateStringValueValidity(string newText) {
            if (this.TryParse(newText, out T value) && this.IsValueInRange(value)) {
                this.IsStringValueValid = true;
            } else {
                this.IsStringValueValid = false;
            }
        }

        #endregion

        #region Events

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NukCounterBase<T>));

        public event RoutedEventHandler ValueChanged {
            add { AddHandler(NukCounterBase<T>.ValueChangedEvent, value); }
            remove { RemoveHandler(NukCounterBase<T>.ValueChangedEvent, value); }
        }

        protected void FireValueChangedEvent() {
            RoutedEventArgs args = new RoutedEventArgs(NukCounterBase<T>.ValueChangedEvent);
            this.RaiseEvent(args);
        }

        #endregion
    }

    
    /// <summary>
    /// Counter Control advanced base class with generic numeric data type and helper properties for hexadecimal mode
    /// </summary>
    /// <typeparam name="T">Some numeric data type like double, int, uint, long, ulong</typeparam>
    public abstract class NukCounterBaseHex<T> : NukCounterBase<T> where T
        : IComparable
        , IComparable<T>
        , IFormattable
        , IConvertible
        , IEquatable<T> {
        
        
        #region Dependency Properties
        
        public static readonly DependencyProperty ToStringHexFormatProperty = DependencyProperty.Register(nameof(ToStringHexFormat),
            typeof(string),
            typeof(NukCounterBaseHex<T>),
            new FrameworkPropertyMetadata("0x{0:X4}", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, StringFormatChangedCallback)
            );

        public static readonly DependencyProperty IsHexaDecimalModeProperty = DependencyProperty.Register(nameof(IsHexaDecimalMode),
            typeof(bool),
            typeof(NukCounterBaseHex<T>),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, StringFormatChangedCallback)
            );
        
        #endregion

        #region  Properties
        
        
        [Category("Custom Properties")]
        public string ToStringHexFormat {
            get {
                return (string)this.GetValue(NukCounterBaseHex<T>.ToStringHexFormatProperty);
            }
            set {
                this.SetValue(NukCounterBaseHex<T>.ToStringHexFormatProperty, value);
            }
        }

        [Category("Custom Properties")]
        public bool IsHexaDecimalMode {
            get {
                return (bool)this.GetValue(NukCounterBaseHex<T>.IsHexaDecimalModeProperty);
            }
            set {
                this.SetValue(NukCounterBaseHex<T>.IsHexaDecimalModeProperty, value);
            }
        }

        #endregion
        
        
        #region Methods
        
        protected static void StringFormatChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is not NukCounterBaseHex<T> target) return;
            target._value = target.Value;
            target.UpdateValueString(target.Value);
            target.RegenerateStringValueValidity(target.ValueString);
        }
        
        protected override void UpdateValueString(T newValue) {
            if (this.IsHexaDecimalMode) {
                this.ValueString = String.Format(this.ToStringHexFormat, newValue);;
            } else {
                this.ValueString = String.Format(this.ToStringFormat, newValue);;
            }
        }
        
        #endregion
        
    }
}
