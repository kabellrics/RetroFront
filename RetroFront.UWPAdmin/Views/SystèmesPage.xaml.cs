using System;

using RetroFront.UWPAdmin.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class SystèmesPage : Page
    {
        public SystèmesViewModel ViewModel { get; } = new SystèmesViewModel();

        public SystèmesPage()
        {
            InitializeComponent();
            Loaded += SystèmesPage_Loaded;
        }

        private async void SystèmesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync();
        }
    }
}
