using System.Windows.Input;
namespace DukNuk.Wpf.Mvvm {
    
    /// <summary>
    /// Simple command whose "CanExecuteChanged" event can be manually invoked.
    /// Variant that takes a parameter of a user provided type.
    /// Uses an external CanExecute callback function function for validation (Optional).
    /// </summary>
    public class DukCommand<TParameter> : ICommand {
        public event EventHandler? CanExecuteChanged;
        
        public bool CanExecute(object parameter) {
            return parameter is TParameter myT 
                && this._exec != null 
                && (this._canExec?.Invoke(myT) ?? true); 
        }
        public void Execute(object parameter) {
            if (parameter is TParameter myT) this._exec?.Invoke(myT);
        }

        private readonly Action<TParameter>? _exec;
        private readonly Func<TParameter,bool>? _canExec;
        
        /// <summary>
        /// </summary>
        /// <param name="execute">Action to execute. Takes a parameter of the type TParameter.</param>
        /// <param name="canExecute">Execution validation callback function. Takes a parameter of the type TParameter. If null or not provided, CanExecute will return true if the passed parameter is of the type TParameter.</param>
        public DukCommand(Action<TParameter> execute, Func<TParameter,bool>? canExecute = null) {
            this._exec = execute;
            this._canExec = canExecute;
        }

        /// <summary>
        /// Call this to invoke the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void OnCanExecuteChanged() {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
