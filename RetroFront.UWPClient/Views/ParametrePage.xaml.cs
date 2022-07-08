using System;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class ParametrePage : Page
    {
        //public ParametreViewModel ViewModel { get; } = new ParametreViewModel();
        public ParametreViewModel ViewModel => (ParametreViewModel)DataContext;

        public ParametrePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ParametreViewModel>();
            ViewModel.LoadDataAsync();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FirstElement.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    ViewModel.LoadDataAsync();
        //}
    }
}
