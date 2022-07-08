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
    public sealed partial class ArtworkfView : UserControl
    {
        public GameDetailViewModel ViewModel => (GameDetailViewModel)DataContext;
        public ArtworkfView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<GameDetailViewModel>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FirstElement.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
    }
}
