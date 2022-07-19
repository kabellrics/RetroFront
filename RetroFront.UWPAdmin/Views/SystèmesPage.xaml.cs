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

        private void toggleRawBT_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ToggleRaw)
            {
                toggleRawBT.Content = "Présenter les données";
                presentData.Visibility = Visibility.Collapsed;
                rawData.Visibility = Visibility.Visible;
                rawBT.Visibility = Visibility.Visible;
            }
            else
            {
                toggleRawBT.Content = "Voir les données Bruts";
                presentData.Visibility = Visibility.Visible;
                rawData.Visibility = Visibility.Collapsed;
                rawBT.Visibility = Visibility.Collapsed;
            }
        }
    }
}
