using System;

using RetroFront.UWPAdmin.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            ViewModel.SaveChangeCommand.Execute(true);
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
