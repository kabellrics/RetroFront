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
        }

        #region Last Played BT
        private void BTLP1_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTLP1.Game);
        }
        private void BTLP2_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTLP2.Game);
        }
        private void BTLP3_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTLP3.Game);
        }
        private void BTLP4_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTLP4.Game);
        }
        private void BTLP5_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTLP5.Game);
        }
        private void BTLP6_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTLP6.Game);
        }
        #endregion
        #region MostPlayed BT
        private void BTMP1_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTMP1.Game);
        }
        private void BTMP2_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTMP2.Game);
        }
        private void BTMP3_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTMP3.Game);
        }
        private void BTMP4_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTMP4.Game);
        }
        private void BTMP5_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTMP5.Game);
        }
        private void BTMP6_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTMP6.Game);
        }
        #endregion
        #region Fav BT
        private void BTFav1_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTFav1.Game);
        }
        private void BTFav2_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTFav2.Game);
        }
        private void BTFav3_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTFav3.Game);
        }
        private void BTFav4_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTFav4.Game);
        }
        private void BTFav5_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTFav5.Game);
        }
        private void BTFav6_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTFav6.Game);
        }
        #endregion
        #region PC BT
        private void BTPC1_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTPC1.Game);
        }
        private void BTPC2_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTPC2.Game);
        }
        private void BTPC3_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTPC3.Game);
        }
        private void BTPC4_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTPC4.Game);
        }
        private void BTPC5_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTPC5.Game);
        }
        private void BTPC6_Click(object sender, EventArgs e)
        {
            ViewModel.GotoGameDetailPage(BTPC6.Game);
        }
        #endregion

    }
}
