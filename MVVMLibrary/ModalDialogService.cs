using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMLibrary
{
    public  class ModalDialogService: IDialogService
    {
        private List<Window> dialogs;
        private Type dialogType;
        public ModalDialogService(Type dialogType) {
            if (!(dialogType.IsSubclassOf(typeof(Window))))
            {
                throw new Exception($"Dialog must be type of Window. Type Passed: {dialogType}");
            }
            dialogs = new List<Window>();
            this.dialogType = dialogType;
        }

        public void CloseDialog(object dataContext) => UIContext.Current?.Send(x => dialogs.FirstOrDefault(d => d.DataContext == dataContext)?.Close(), null);

        public void OpenDialog(ViewModelBase context, object parent, string title, double width, double height) => UIContext.Current?.Send(x => setupWindow(context, parent, title, width, height), null);

        private Window? setupWindow(ViewModelBase context, object parent, string title, double width, double height)
        {
            Window? dialog = Activator.CreateInstance(dialogType) as Window;
            if (dialog == null) return null;
            dialog.DataContext = context;
            dialog.Title = title;
            dialog.Width = width;
            dialog.Height = height;
            Window? parentWindow = getParentWindow(parent);
            dialog.Owner = parentWindow;
            dialog.WindowStartupLocation = (parentWindow != null) ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen;
            if (parentWindow != null) parentWindow.Opacity = 0.5;
            dialog.Closed += (sender, args) =>
            {
                if (parentWindow != null) parentWindow.Opacity = 0;
                var dc = dialog.DataContext as IDisposable;
                dc?.Dispose();
            };
            dialogs.Add(dialog);
            return dialog;
        }

        private Window? getParentWindow(object parent)
        {
            foreach(var window in Application.Current.Windows)
            {
                if((window as Window)?.DataContext == parent)
                {
                    return window as Window;
                }
            }
            return null;
        }
    }
}
