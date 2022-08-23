using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLibrary
{
    public interface IDialogService
    {
        void OpenDialog(ViewModelBase context, object parent, string title, double width, double height);
        void CloseDialog(object dataContext);
    }
}
