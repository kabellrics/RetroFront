using System;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class PlateformePage : Page
    {
        //public PlateformeViewModel ViewModel { get; } = new PlateformeViewModel();
        public PlateformeViewModel ViewModel => (PlateformeViewModel)DataContext;
        public PlateformePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<PlateformeViewModel>();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is bool parameter)
            {
                ViewModel.LoadDataAsync(parameter);
            }
            //ViewModel.LoadDataAsync();
        }
    }
}
