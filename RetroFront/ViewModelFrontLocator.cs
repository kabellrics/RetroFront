using Microsoft.Extensions.DependencyInjection;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront
{
    public class ViewModelFrontLocator
    {
        public FrontSysViewModel MainPageVM
        => App.ServiceProvider.GetRequiredService<FrontSysViewModel>();

    }
}
