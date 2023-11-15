using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace DukNuk.Wpf.Mvvm {
    
    /// <summary>
    /// Simple command with a bool CanExec property that when changed, automatically raises <see cref="CanExecuteChanged"/>.
    /// Variant that takes a parameter of a user provided type.
    /// The <see cref="CanExecuteChanged"/> can be also manually invoked.
    /// </summary>
    public class NukCommand<TParameter> : ICommand, INotifyPropertyChanged {
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<TField>(ref TField field, TField value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<TField>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        
        public event EventHandler? CanExecuteChanged;

        private Action<TParameter>? _exec;
        private Func<TParameter,bool>? _validateParameter;

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

        public bool CanExecute(object parameter) {
            return parameter is TParameter myT 
                && this._exec != null 
                && this.CanExec
                && (this._validateParameter?.Invoke(myT) ?? true);
        }
        public void Execute(object parameter) {
            if (parameter is TParameter myT) this._exec?.Invoke(myT);
        }

        /// <summary>
        /// </summary>
        /// <param name="execute">Action to execute. Takes a parameter of the type TParameter.</param>
        /// <param name="validateParameter">
        /// Parameter validation callback function. Takes a parameter of the type TParameter.
        /// If null or not provided, CanExecute will return true if the passed parameter is of the type TParameter and the <see cref="CanExec"/> property is true.
        /// </param>
        public NukCommand(Action<TParameter> execute, Func<TParameter,bool>? validateParameter = null) {
            this._exec = execute;
            this._validateParameter = validateParameter;
        }

        /// <summary>
        /// Call this to invoke the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void OnCanExecuteChanged() {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
