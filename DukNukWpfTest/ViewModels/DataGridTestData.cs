using DukNuk.Wpf.Mvvm;
namespace DukNuk.Wpf.Test.ViewModels {
    public class DataGridTestData : DukViewModel {
        public string Name {
            get => this._name;
            set => this.SetField(ref this._name, value);
        }
        private string _name = "";

        public string Link {
            get => this._link;
            set => this.SetField(ref this._link, value);
        }
        private string _link = "https://github.com/drone1400/DukNuk";

        public double Value1 {
            get => this._value1;
            set => this.SetField(ref this._value1, value);
        }
        private double _value1 = 0.0;

        public int Value2 {
            get => this._value2;
            set => this.SetField(ref this._value2, value);
        }
        private int _value2 = 0;

        public bool Value3 {
            get => this._value3;
            set => this.SetField(ref this._value3, value);
        }
        private bool _value3 = false;

        public string SelectedOption {
            get => this._selectedOption;
            set => this.SetField(ref this._selectedOption, value);
        }
        private string _selectedOption = "";
    }
}
