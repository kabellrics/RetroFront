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
        private ICommand _GroupedSystemeCommand;
        private ICommand _GroupedEmulatorCommand;
        private ICommand _GroupedYearCommand;
        private ICommand _GroupedGenreCommand;
        private ICommand _GroupedDevCommand;
        private ICommand _GroupedEditeurCommand;

        public ObservableCollection<DisplayGroupedGame> Source { get; } = new ObservableCollection<DisplayGroupedGame>();
        public ObservableCollection<DisplayGame> RawSource { get; } = new ObservableCollection<DisplayGame>();

        //public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnItemSelected));
        //public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<DisplayGame>(OnItemSelected));
        //public ICommand GroupedSystemeCommand => _GroupedSystemeCommand ?? (_GroupedSystemeCommand = new RelayCommand(GroupedSysteme));
        //public ICommand GroupedEmulatorCommand => _GroupedEmulatorCommand ?? (_GroupedEmulatorCommand = new RelayCommand(GroupedEmulator));
        //public ICommand GroupedYearCommand => _GroupedYearCommand ?? (_GroupedYearCommand = new RelayCommand(GroupedYear));
        //public ICommand GroupedGenreCommand => _GroupedGenreCommand ?? (_GroupedGenreCommand = new RelayCommand(GroupedGenre));
        //public ICommand GroupedDevCommand => _GroupedDevCommand ?? (_GroupedDevCommand = new RelayCommand(GroupedDev));
        //public ICommand GroupedEditeurCommand => _GroupedEditeurCommand ?? (_GroupedEditeurCommand = new RelayCommand(GroupedEditeur));

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
            //var data = await gamesService.GetGroupedBySystemes();
            //foreach (var item in data)
            //{
            //    Source.Add(item);
            //}
        }
        public async void InitRawData()
        {
            RawSource.Clear();
            var dataraw = await gamesService.GetGame();
            foreach (var raw in dataraw)
            {
                RawSource.Add(raw);
            }
        }
        public void OnItemSelected(DisplayGame args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(GamesSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<GamesDetailPage>(selected.ID.ToString());
        }
        public async void GroupedSysteme()
        {
            Source.Clear();
            var data = await gamesService.GetGroupedBySystemes();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
        public async void GroupedEmulator()
        {
            Source.Clear();
            var data = await gamesService.GetGroupedByEmulators();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
        public async void GroupedYear()
        {
            Source.Clear();
            var data = await gamesService.GetGroupedByYears();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
        public async void GroupedGenre()
        {
            Source.Clear();
            var data = await gamesService.GetGroupedByGenres();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
        public async void GroupedDev()
        {
            Source.Clear();
            var data = await gamesService.GetGroupedByDevs();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
        public async void GroupedEditeur()
        {
            Source.Clear();
            var data = await gamesService.GetGroupedByEditeurs();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
