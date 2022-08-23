using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMLibrary
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase? ParentViewModel { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModelBase(ViewModelBase? parentViewModel = null) 
        {
            ParentViewModel = parentViewModel;
        }
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
        protected virtual void InvalidateRequeryChanged() => UIContext.Current?.Post(x => CommandManager.InvalidateRequerySuggested(), null);

    }
}
