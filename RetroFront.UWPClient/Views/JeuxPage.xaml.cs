using System;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.Core.Models;
using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class JeuxPage : Page
    {
        //public JeuxViewModel ViewModel { get; } = new JeuxViewModel();
        public JeuxViewModel ViewModel => (JeuxViewModel)DataContext;

        public JeuxPage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<JeuxViewModel>();
            //ViewModel.LoadDataAsync();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Systeme plateforme)
            {
                ViewModel.LoadDataAsync(plateforme);
            }
        }
    }
}
