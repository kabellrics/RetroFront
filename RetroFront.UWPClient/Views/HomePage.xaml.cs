using System;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel => (HomeViewModel)DataContext;
        

        public HomePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomeViewModel>();
            ViewModel.LoadDataAsync();
        }
        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    ViewModel.LoadDataAsync();
        //}
    }
}
