namespace RC.Common.Infrastructure
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// An implementation of ICommand. 
    /// To be used for executing actions triggered from the UI controls via Command Binding.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Action to Execute
        /// </summary>
        private Action<object> execute;

        /// <summary>
        /// A condition to check if the command can be executed
        /// </summary>
        private Predicate<object> canExecute;

        /// <summary>
        /// Can Execute Changed handler
        /// </summary>
        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        /// Initializes a new instance of the RelayCommand
        /// </summary>
        /// <param name="execute">Action to execute</param>
        public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand
        /// </summary>
        /// <param name="execute">Action to execute</param>
        /// <param name="canExecute">A condition to check if the command can be executed</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("Execute Action is Null.");
            this.canExecute = canExecute ?? throw new ArgumentNullException("CanExecute is Null.");
        }

        /// <summary>
        /// Event handler for CanExecuteChanged event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        /// <summary>
        /// Triggers the can execute predicate based on the parameter
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        /// <summary>
        /// Executes the action based on the parameter
        /// </summary>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        /// <summary>
        /// Invokes the handler for the CanExecuteChanged event
        /// </summary>
        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Resets the Can Execute and Execute properties
        /// </summary>
        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        /// <summary>
        /// Default value of the CanExecute
        /// </summary>
        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
