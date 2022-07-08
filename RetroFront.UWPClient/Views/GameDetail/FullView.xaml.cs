using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace RetroFront.UWPClient.Views.GameDetail
{
    public sealed partial class FullView : UserControl
    {
        public GameDetailViewModel ViewModel => (GameDetailViewModel)DataContext;
        public FullView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<GameDetailViewModel>();
            player.MediaPlayer.IsMuted = true;
            player.Visibility = Visibility.Visible;
            player.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private async void MediaPlayer_MediaEnded(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                player.Visibility = Visibility.Collapsed;
            });
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.Pause();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.IsMuted = !player.MediaPlayer.IsMuted;
        }

        private void AppBarToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            favbt.Icon = new SymbolIcon(Symbol.Favorite);
            favbt.Label = "Favoris";
        }

        private void AppBarToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            favbt.Icon = new SymbolIcon(Symbol.UnFavorite);
            favbt.Label = string.Empty;
        }

        private void favbt_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrenGame.IsFavorite == true)
            {
                favbt.Icon = new SymbolIcon(Symbol.Favorite);
                favbt.Label = "Favoris";
            }
            else
            {
                favbt.Icon = new SymbolIcon(Symbol.UnFavorite);
                favbt.Label = string.Empty;
            }

        }

        private void mutebt_Checked(object sender, RoutedEventArgs e)
        {
            mutebt.Icon = new SymbolIcon(Symbol.Volume);
            mutebt.Label = "Sonore"; player.MediaPlayer.IsMuted = !player.MediaPlayer.IsMuted;
        }

        private void mutebt_Unchecked(object sender, RoutedEventArgs e)
        {
            mutebt.Icon = new SymbolIcon(Symbol.Mute);
            mutebt.Label = "Muet"; player.MediaPlayer.IsMuted = !player.MediaPlayer.IsMuted;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FirstElement.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
    }
}
