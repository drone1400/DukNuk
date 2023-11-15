using DukNuk.Wpf.Mvvm;
namespace DukNuk.Wpf.Test.ViewModels {
    public class ListViewTestData : DukViewModel {
        public string Name {
            get => this._name;
            set => this.SetField(ref this._name, value);
        }
        private string _name = "";
        public string Value {
            get => this._value; 
            set => this.SetField(ref this._value, value);
        }
        private string _value = "";
    }
}
