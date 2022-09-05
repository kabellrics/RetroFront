using RetroFront.UWPAdmin.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Boîte de dialogue de contenu, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace RetroFront.UWPAdmin.Views.Modals
{
    public sealed partial class VideoScrapeChoiceContentDialog : ContentDialog
    {
        public VideoScrapeChoiceViewModel ViewModel { get; } = new VideoScrapeChoiceViewModel();
        public VideoScrapeChoiceContentDialog(VideoScrapeChoiceViewModel vm)
        {
            ViewModel = vm;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            player.MediaPlayer.Pause();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            player.MediaPlayer.Pause();
        }
        private void playpauseBT_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (player.MediaPlayer.CurrentState == MediaPlayerState.Playing)
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProposalGrid.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProposalGrid.Visibility = Visibility.Collapsed;
        }
    }
}
