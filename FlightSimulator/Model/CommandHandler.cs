using System;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand
    {
        private Action _action;

        public CommandHandler(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
