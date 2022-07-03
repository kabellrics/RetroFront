using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.Core.Models;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace RetroFront.UWPClient
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GameDetailPage : Page
    {
        public GameDetailViewModel ViewModel => (GameDetailViewModel)DataContext;
        public GameDetailPage()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<GameDetailViewModel>();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is GameRom rom)
            {
                ViewModel.LoadDataAsync(rom);
            }
        }
    }
}
