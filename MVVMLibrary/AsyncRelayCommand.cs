using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLibrary
{
    public class AsyncRelayCommand<T> : RelayCommand<T>
    {
        public AsyncRelayCommand(Action<T> execute, Func<T, bool> canExecute = null): base(execute, canExecute) { }
        public override async void Execute(object? parameter) => await Task.Run(() => base.Execute(parameter));
    }
}
