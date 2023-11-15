using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Timer = System.Timers.Timer;

namespace DukNuk.Wpf.Controls {
    public class NukCounterDouble : NukCounterBase<double> {

        protected override double MultiplyIncrement(double increment, double multiplier) {
            return increment * multiplier;
        }
        protected override double IncrementValue(double value, double increment) {
            return value + increment;
        }
        protected override double DecrementValue(double value, double increment) {
            return value - increment;
        }
        protected override bool TryParse(string text, out double value) {
            return double.TryParse(text, out value);
        }

        static NukCounterDouble() {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (NukCounterDouble), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (NukCounterDouble)));
            NukCounterDouble.IncrementProperty.OverrideMetadata(typeof(NukCounterDouble), new FrameworkPropertyMetadata((double)1.0));
            NukCounterDouble.AutoIncrementAccelerationMultiplierProperty.OverrideMetadata(typeof(NukCounterDouble), new FrameworkPropertyMetadata((double)2.0));
            NukCounterDouble.ValueProperty.OverrideMetadata(typeof(NukCounterDouble), new FrameworkPropertyMetadata((double)0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValuePropertyChangedCallback));
            NukCounterDouble.ValueMinProperty.OverrideMetadata(typeof(NukCounterDouble), new FrameworkPropertyMetadata(double.MinValue));
            NukCounterDouble.ValueMaxProperty.OverrideMetadata(typeof(NukCounterDouble), new FrameworkPropertyMetadata(double.MaxValue));
            NukCounterDouble.ToStringFormatProperty.OverrideMetadata(typeof(NukCounterDouble), new FrameworkPropertyMetadata("{0:0.0###}"));
        }
    }
}
