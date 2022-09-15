using System;

using Microsoft.Toolkit.Uwp.UI.Animations;

using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using RetroFront.UWPAdmin.ViewModels;

using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class SystèmesDetailPage : Page
    {
        public SystèmesDetailViewModel ViewModel { get; } = new SystèmesDetailViewModel();

        public SystèmesDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.LoadDataAsync();
            ViewModel.Initialize(e.Parameter as string, e.NavigationMode);
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            //ViewModel.SaveChangeCommand.Execute(true);
        }
        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    if (e.NavigationMode == NavigationMode.Back)
        //    {
        //        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.SelectedImage);
        //        ImagesNavigationHelper.RemoveImageId(SystèmesViewModel.SystèmesSelectedIdKey);
        //    }
        //}

        private void OnPageKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Escape && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (btTextblock.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                btTextblock.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btTextbox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                btTextblock.Visibility = Windows.UI.Xaml.Visibility.Visible;
                btTextbox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void AdaptiveGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.OnItemSelected(e.ClickedItem as DisplayEmulator);
        }
    }
}
