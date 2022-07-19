using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Animations;

using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using RetroFront.UWPAdmin.Views;

using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class GamesViewModel : ObservableObject
    {
        public const string GamesSelectedIdKey = "GamesSelectedIdKey";
        private GamesService gamesService;
        private ICommand _itemSelectedCommand;

        public ObservableCollection<DisplayGroupedGame> Source { get; } = new ObservableCollection<DisplayGroupedGame>();
        public ObservableCollection<DisplayGame> RawSource { get; } = new ObservableCollection<DisplayGame>();

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnItemSelected));
        private bool _toggleRaw;
        public bool ToggleRaw
        {
            get => _toggleRaw;
            set
            {
                SetProperty(ref _toggleRaw, value);
            }
        }
        public GamesViewModel()
        {
            gamesService = new GamesService();
            ToggleRaw = false;
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();
            RawSource.Clear();
            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");
            var data = gamesService.GetGroupedBySystemes();
            foreach (var item in data)
            {
                Source.Add(item);
            }
            var dataraw = gamesService.GetGame();
            foreach(var raw in dataraw)
            {
                RawSource.Add(raw);
            }
        }

        private void OnItemSelected(ItemClickEventArgs args)
        {
            var selected = args.ClickedItem as DisplayGame;
            //ImagesNavigationHelper.AddImageId(GamesSelectedIdKey, selected.ID);
            //NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            //NavigationService.Navigate<GamesDetailPage>(selected.ID);
        }
    }
}
