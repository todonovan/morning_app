using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MorningApp
{
    public sealed class RelayCommand : ICommand
    {
        private Action _targetExecuteMethod;
        private Func<bool> _targetCanExecuteMethod;

        public RelayCommand(Action executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChange()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
            {
                return _targetCanExecuteMethod();
            }
            return _targetExecuteMethod != null;
        }

        void ICommand.Execute(object parameter)
        {
            _targetExecuteMethod?.Invoke();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }

    public sealed class RelayCommand<T> : ICommand
    {
        private Action<T> _targetExecuteMethod;
        private Func<T, bool> _targetCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
            {
                T param = (T)parameter;
                return _targetCanExecuteMethod(param);
            }
            return _targetExecuteMethod != null;
        }

        void ICommand.Execute(object parameter)
        {
            _targetExecuteMethod?.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
