using System;

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
    }
}
