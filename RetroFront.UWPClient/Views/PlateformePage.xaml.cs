using System;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;

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
            ViewModel.LoadDataAsync();
        }
    }
}
