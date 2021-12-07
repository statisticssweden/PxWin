using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PX.Desktop.Interfaces
{
    public interface IToolWindow
    {
        string Title { get; }
        string Id { get; }
        Control View { get; }

        void Reload();
        bool CanClose();

        event EventHandler TitleChanged;
    }
}
