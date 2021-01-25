using Microsoft.Extensions.DependencyInjection;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront
{
    public class ViewModelLocator
    {
        public FrontSysViewModel MainPageVM
        => App.ServiceProvider.GetRequiredService<FrontSysViewModel>();

    }
}
