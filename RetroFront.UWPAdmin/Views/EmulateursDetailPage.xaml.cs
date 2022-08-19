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
    public sealed partial class EmulateursDetailPage : Page
    {
        public EmulateursDetailViewModel ViewModel { get; } = new EmulateursDetailViewModel();

        public EmulateursDetailPage()
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
            ViewModel.SaveChangeCommand.Execute(true);
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    if (e.NavigationMode == NavigationMode.Back)
        //    {
        //        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.SelectedImage);
        //        ImagesNavigationHelper.RemoveImageId(EmulateursViewModel.EmulateursSelectedIdKey);
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

        private void AdaptiveGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.OnItemSelected(e.ClickedItem as DisplayGame);
        }
    }
}
