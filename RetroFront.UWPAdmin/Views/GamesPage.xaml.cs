using System;
using Microsoft.UI.Xaml.Controls;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class GamesPage : Page
    {
        public GamesViewModel ViewModel { get; } = new GamesViewModel();

        public GamesPage()
        {
            InitializeComponent();
            //Loaded += GamesPage_Loaded;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.LoadDataAsync(e.Parameter as string);
            //ViewModel.Initialize(e.Parameter as string, e.NavigationMode);
        }
        //private async void GamesPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    await ViewModel.LoadDataAsync();
        //}
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


    }
}
