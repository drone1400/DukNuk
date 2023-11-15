using System.ComponentModel;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Timer = System.Timers.Timer;

namespace DukNuk.Wpf.Controls {
    public class NukCounterUint : NukCounterBaseHex<uint> {

        protected override uint MultiplyIncrement(uint increment, uint multiplier) {
            return increment * multiplier;
        }
        protected override uint IncrementValue(uint value, uint increment) {
            return value + increment;
        }
        protected override uint DecrementValue(uint value, uint increment) {
            return value - increment;
        }
        protected override bool TryParse(string text, out uint value) {
            if (this.IsHexaDecimalMode) {
                string cleanText = text.Replace("0x", "");
                return uint.TryParse(cleanText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
            } else {
                return uint.TryParse(text, out value);
            }
        }

        static NukCounterUint() {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (NukCounterUint), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (NukCounterUint)));
            NukCounterUint.IncrementProperty.OverrideMetadata(typeof(NukCounterUint), new FrameworkPropertyMetadata((uint)1));
            NukCounterUint.AutoIncrementAccelerationMultiplierProperty.OverrideMetadata(typeof(NukCounterUint), new FrameworkPropertyMetadata((uint)2));
            NukCounterUint.ValueProperty.OverrideMetadata(typeof(NukCounterUint), new FrameworkPropertyMetadata((uint)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValuePropertyChangedCallback));
            NukCounterUint.ValueMinProperty.OverrideMetadata(typeof(NukCounterUint), new FrameworkPropertyMetadata(uint.MinValue));
            NukCounterUint.ValueMaxProperty.OverrideMetadata(typeof(NukCounterUint), new FrameworkPropertyMetadata(uint.MaxValue));
            NukCounterUint.ToStringFormatProperty.OverrideMetadata(typeof(NukCounterUint), new FrameworkPropertyMetadata("{0:0}"));
        }
    }
}
