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

namespace RetroFront.UWPClient.Views.Game
{
    public sealed partial class BoxWallView : UserControl
    {
        public JeuxViewModel ViewModel => (JeuxViewModel)DataContext;
        public BoxWallView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<JeuxViewModel>();
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var grid = sender as GridView;
                ((ItemsWrapGrid)grid.ItemsPanelRoot).ItemWidth = e.NewSize.Width / 5.2;
                ((ItemsWrapGrid)grid.ItemsPanelRoot).ItemHeight = e.NewSize.Height / 2.2;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void datagrid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Space || e.Key == Windows.System.VirtualKey.GamepadA || e.Key == Windows.System.VirtualKey.GamepadX)
                ViewModel.GameDetailCommand.Execute(null);
        }
    }
}
