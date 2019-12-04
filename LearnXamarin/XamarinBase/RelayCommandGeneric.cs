using System;
using System.Windows.Input;

namespace LearnXamarin.XamarinBase
{
    public class RelayCommand<T> : ICommand
    {
        // variables
        private readonly Action<T> executeAction;
        private readonly Func<T, bool> canExecuteFunc;

        // events
        public event EventHandler CanExecuteChanged;

        // constructors
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            executeAction = execute;
            if (canExecute != null)
                canExecuteFunc = canExecute;
        }

        // public methods
        public bool CanExecute(T parameter)
        {
            if (canExecuteFunc == null)
                return true;

            var canExecute = canExecuteFunc.Invoke(parameter);
            return canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public void Execute(T parameter)
        {
            var canExecute = CanExecute(parameter);
            if (canExecute)
                executeAction.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }

    }
}
