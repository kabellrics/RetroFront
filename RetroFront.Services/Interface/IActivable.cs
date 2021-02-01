using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.Services.Interface
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }
}
