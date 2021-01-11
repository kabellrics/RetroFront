using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface IDialogService
    {
        bool ShowMessageOk(string title, string message);
        bool ShowMessageOkCancel(string title, string message);
        bool showMessageYesNoCancel(string title, string message);
        bool showMessageYesNo(string title, string message);
        string OpenUniqueFileDialog(string filter);
        IEnumerable<string> OpenMultiFileDialog(string filter);
        string CreateJsonEmu();
        string CreateRetroarchCore();
    }
}
