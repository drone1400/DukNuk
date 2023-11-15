using System.ComponentModel;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Timer = System.Timers.Timer;

namespace DukNuk.Wpf.Controls {
    public class NukCounterUlong : NukCounterBaseHex<ulong> {

        protected override ulong MultiplyIncrement(ulong increment, ulong multiplier) {
            return increment * multiplier;
        }
        protected override ulong IncrementValue(ulong value, ulong increment) {
            return value + increment;
        }
        protected override ulong DecrementValue(ulong value, ulong increment) {
            return value - increment;
        }
        protected override bool TryParse(string text, out ulong value) {
            if (this.IsHexaDecimalMode) {
                string cleanText = text.Replace("0x", "");
                return ulong.TryParse(cleanText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
            } else {
                return ulong.TryParse(text, out value);
            }
        }

        static NukCounterUlong() {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (NukCounterUlong), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (NukCounterUlong)));
            NukCounterUlong.IncrementProperty.OverrideMetadata(typeof(NukCounterUlong), new FrameworkPropertyMetadata((ulong)1));
            NukCounterUlong.AutoIncrementAccelerationMultiplierProperty.OverrideMetadata(typeof(NukCounterUlong), new FrameworkPropertyMetadata((ulong)2));
            NukCounterUlong.ValueProperty.OverrideMetadata(typeof(NukCounterUlong), new FrameworkPropertyMetadata((ulong)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValuePropertyChangedCallback));
            NukCounterUlong.ValueMinProperty.OverrideMetadata(typeof(NukCounterUlong), new FrameworkPropertyMetadata(ulong.MinValue));
            NukCounterUlong.ValueMaxProperty.OverrideMetadata(typeof(NukCounterUlong), new FrameworkPropertyMetadata(ulong.MaxValue));
            NukCounterUlong.ToStringFormatProperty.OverrideMetadata(typeof(NukCounterUlong), new FrameworkPropertyMetadata("{0:0}"));
        }
    }
}
