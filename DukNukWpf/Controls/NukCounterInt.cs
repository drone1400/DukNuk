using System.ComponentModel;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Timer = System.Timers.Timer;

namespace DukNuk.Wpf.Controls {
    public class NukCounterInt : NukCounterBaseHex<int> {

        protected override int MultiplyIncrement(int increment, int multiplier) {
            return increment * multiplier;
        }
        protected override int IncrementValue(int value, int increment) {
            return value + increment;
        }
        protected override int DecrementValue(int value, int increment) {
            return value - increment;
        }
        protected override bool TryParse(string text, out int value) {
            if (this.IsHexaDecimalMode) {
                string cleanText = text.Replace("0x", "");
                return int.TryParse(cleanText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
            } else {
                return int.TryParse(text, out value);
            }
        }

        static NukCounterInt() {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (NukCounterInt), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (NukCounterInt)));
            NukCounterInt.IncrementProperty.OverrideMetadata(typeof(NukCounterInt), new FrameworkPropertyMetadata((int)1));
            NukCounterInt.AutoIncrementAccelerationMultiplierProperty.OverrideMetadata(typeof(NukCounterInt), new FrameworkPropertyMetadata((int)2));
            NukCounterInt.ValueProperty.OverrideMetadata(typeof(NukCounterInt), new FrameworkPropertyMetadata((int)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValuePropertyChangedCallback));
            NukCounterInt.ValueMinProperty.OverrideMetadata(typeof(NukCounterInt), new FrameworkPropertyMetadata(int.MinValue));
            NukCounterInt.ValueMaxProperty.OverrideMetadata(typeof(NukCounterInt), new FrameworkPropertyMetadata(int.MaxValue));
            NukCounterInt.ToStringFormatProperty.OverrideMetadata(typeof(NukCounterInt), new FrameworkPropertyMetadata("{0:0}"));
        }
    }
}
