using System.ComponentModel;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Timer = System.Timers.Timer;

namespace DukNuk.Wpf.Controls {
    public class NukCounterLong : NukCounterBaseHex<long> {

        protected override long MultiplyIncrement(long increment, long multiplier) {
            return increment * multiplier;
        }
        protected override long IncrementValue(long value, long increment) {
            return value + increment;
        }
        protected override long DecrementValue(long value, long increment) {
            return value - increment;
        }
        protected override bool TryParse(string text, out long value) {
            if (this.IsHexaDecimalMode) {
                string cleanText = text.Replace("0x", "");
                return long.TryParse(cleanText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
            } else {
                return long.TryParse(text, out value);
            }
        }

        static NukCounterLong() {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (NukCounterLong), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (NukCounterLong)));
            NukCounterLong.IncrementProperty.OverrideMetadata(typeof(NukCounterLong), new FrameworkPropertyMetadata((long)1));
            NukCounterLong.AutoIncrementAccelerationMultiplierProperty.OverrideMetadata(typeof(NukCounterLong), new FrameworkPropertyMetadata((long)2));
            NukCounterLong.ValueProperty.OverrideMetadata(typeof(NukCounterLong), new FrameworkPropertyMetadata((long)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValuePropertyChangedCallback));
            NukCounterLong.ValueMinProperty.OverrideMetadata(typeof(NukCounterLong), new FrameworkPropertyMetadata(long.MinValue));
            NukCounterLong.ValueMaxProperty.OverrideMetadata(typeof(NukCounterLong), new FrameworkPropertyMetadata(long.MaxValue));
            NukCounterLong.ToStringFormatProperty.OverrideMetadata(typeof(NukCounterLong), new FrameworkPropertyMetadata("{0:0}"));
        }
    }
}
