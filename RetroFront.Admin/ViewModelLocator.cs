using Microsoft.Extensions.DependencyInjection;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Admin
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageVM
        => App.ServiceProvider.GetRequiredService<MainPageViewModel>();

    }
}
