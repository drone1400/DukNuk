using System.Windows.Input;
namespace DukNuk.Wpf.Mvvm {
    
    /// <summary>
    /// Simple command whose "CanExecuteChanged" event can be manually invoked.
    /// Uses an external CanExecute callback function function for validation (Optional).
    /// </summary>
    public class DukCommand : ICommand {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return this._exec != null && (this._canExec?.Invoke() ?? true);
        }
        public void Execute(object parameter) => this._exec?.Invoke();
        
        private readonly Action? _exec;
        private readonly Func<bool>? _canExec;

        /// <summary>
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Execution validation callback function. If null or not provided, CanExecute will always return true.</param>
        public DukCommand(Action execute, Func<bool>? canExecute = null) {
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
