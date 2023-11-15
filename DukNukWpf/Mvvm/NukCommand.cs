using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace DukNuk.Wpf.Mvvm {
    
    /// <summary>
    /// Simple command with a bool CanExec property that when changed, automatically raises <see cref="CanExecuteChanged"/>.
    /// The <see cref="CanExecuteChanged"/> can be also manually invoked.
    /// </summary>
    public class NukCommand : ICommand, INotifyPropertyChanged {
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<TField>(ref TField field, TField value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<TField>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return this._exec != null && this.CanExec;
        }
        public void Execute(object parameter) => this._exec?.Invoke();
        
        private readonly Action? _exec;

        /// <summary>
        /// Set this and <see cref="CanExecuteChanged"/> will be invoked when the value changes
        /// </summary>
        public bool CanExec {
            get => this._canExec;
            set {
                if (this.SetField(ref this._canExec, value)) {
                    this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private bool _canExec = true;
        
        /// <summary>
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public NukCommand(Action execute) {
            this._exec = execute;
        }


        /// <summary>
        /// Call this to invoke the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void OnCanExecuteChanged() {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
