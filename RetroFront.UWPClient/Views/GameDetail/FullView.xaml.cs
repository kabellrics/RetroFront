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
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.Pause();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.MediaPlayer.IsMuted = !player.MediaPlayer.IsMuted;
        }
    }
}
