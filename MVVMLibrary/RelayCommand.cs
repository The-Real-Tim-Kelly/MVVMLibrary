using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMLibrary
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentException(nameof(execute), "A relay command requires an execute action");
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => canExecute == null || canExecute((T)parameter);

        public virtual void Execute(object? parameter) => execute((T)parameter);
    }
}
