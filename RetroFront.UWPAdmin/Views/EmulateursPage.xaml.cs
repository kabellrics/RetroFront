using System;

using RetroFront.UWPAdmin.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class EmulateursPage : Page
    {
        public EmulateursViewModel ViewModel { get; } = new EmulateursViewModel();

        public EmulateursPage()
        {
            InitializeComponent();
            Loaded += EmulateursPage_Loaded;
        }

        private async void EmulateursPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync();
        }
    }
}
