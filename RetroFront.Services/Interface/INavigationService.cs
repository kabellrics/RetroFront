using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.Services.Interface
{
    public interface INavigationService
    {
        object Parameter { get; }
        void GoBack();
        void NavigateTo(string pageKey, object parameter, string frameName);
    }
}
