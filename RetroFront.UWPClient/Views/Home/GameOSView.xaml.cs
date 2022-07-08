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

namespace RetroFront.UWPClient.Views.Home
{
    public sealed partial class GameOSView : UserControl
    {
        public HomeViewModel ViewModel => (HomeViewModel)DataContext;
        public GameOSView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomeViewModel>();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                MenuButton.Height = MainGrid.ActualHeight * 0.75;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void SystemGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var grid = sender as GridView;
                ((ItemsWrapGrid)grid.ItemsPanelRoot).ItemWidth = e.NewSize.Width / 6.2;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FirstElement.Focus(FocusState.Programmatic);
        }
    }
}
