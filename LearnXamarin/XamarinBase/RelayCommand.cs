using System;
using System.Windows.Input;

namespace LearnXamarin.XamarinBase
{
    public class RelayCommand : ICommand
    {
        // variables
        private readonly Action executeAction;
        private readonly Func<bool> canExecuteFunc;

        // events
        public event EventHandler CanExecuteChanged;

        // constructors
        public RelayCommand(Action execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            executeAction = execute;
            if (canExecute != null)
                canExecuteFunc = canExecute;
        }

        // public methods
        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public bool CanExecute()
        {
            if (canExecuteFunc == null)
                return true;

            var canExecute = canExecuteFunc.Invoke();
            return canExecute;
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public void Execute()
        {
            var canExecute = CanExecute();
            if (canExecute)
                executeAction.Invoke();
        }

    }
}
