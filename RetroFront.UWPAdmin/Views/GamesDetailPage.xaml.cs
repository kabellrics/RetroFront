using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Animations;

using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using RetroFront.UWPAdmin.ViewModels;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class GamesDetailPage : Page
    {
        public GamesDetailViewModel ViewModel { get; } = new GamesDetailViewModel();

        public GamesDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.LoadDataAsync();
            ViewModel.Initialize(e.Parameter as string, e.NavigationMode);
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //ViewModel.SaveChange();
            base.OnNavigatingFrom(e);
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    if (e.NavigationMode == NavigationMode.Back)
        //    {
        //        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.SelectedImage);
        //        ImagesNavigationHelper.RemoveImageId(GamesViewModel.GamesSelectedIdKey);
        //    }
        //}

        private void OnPageKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Escape && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                e.Handled = true;
            }
        }

        private void playpauseBT_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(player.MediaPlayer.CurrentState == MediaPlayerState.Playing)
            {
                player.MediaPlayer.Pause();
                playpauseBT.Content = "Play";
            }
            else
            {
                player.MediaPlayer.Play();
                playpauseBT.Content = "Pause";
            }
        }

        private void btshowIntel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            if (btshowIntel.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                btshowIntel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btEditIntel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                btshowIntel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                btEditIntel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void ImageEx_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ScrapeBezelCommand.Execute(true);
        }
    }
}
