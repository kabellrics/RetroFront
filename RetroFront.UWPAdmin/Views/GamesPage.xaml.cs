using System;
using Microsoft.UI.Xaml.Controls;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class GamesPage : Page
    {
        public GamesViewModel ViewModel { get; } = new GamesViewModel();

        public GamesPage()
        {
            InitializeComponent();
            Loaded += GamesPage_Loaded;
        }

        private async void GamesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync();
        }
        private void toggleRawBT_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ToggleRaw)
            {
                toggleRawBT.Content = "Présenter les données";
                ViewModel.InitRawData();
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

        private void AdaptiveGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.OnItemSelected(e.ClickedItem as DisplayGame);
        }

        private void RadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is RadioButtons rb)
            {
                string groupedType = rb.SelectedItem as string;
                switch (groupedType)
                {
                    case "Système":
                        ViewModel.GroupedSysteme();
                        break;
                    case "Emulateur":
                        ViewModel.GroupedEmulator();
                        break;
                    case "Genre":
                        ViewModel.GroupedGenre();
                        break;
                    case "Année":
                        ViewModel.GroupedYear();
                        break;
                    case "Editeur":
                        ViewModel.GroupedEditeur();
                        break;
                    case "Devellopeur":
                        ViewModel.GroupedDev();
                        break;
                }
            }

        }
    }
}
