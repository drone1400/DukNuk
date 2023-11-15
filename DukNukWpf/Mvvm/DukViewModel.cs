using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace DukNuk.Wpf.Mvvm {
    
    /// <summary>
    /// Simple view model base object that implements <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public class DukViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
