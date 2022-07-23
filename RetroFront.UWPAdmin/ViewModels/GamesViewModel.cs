using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp.UI.Animations;

using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using RetroFront.UWPAdmin.Views;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class GamesViewModel : ObservableObject
    {
        public const string GamesSelectedIdKey = "GamesSelectedIdKey";
        private GamesService gamesService;
        private DialogService dialogService;
        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SaveChange));

        private async void SaveChange()
        {
            if (Source.Any(x => x.HasChanged == true))
            {
                var result = await dialogService.ConfirmationDialogAsync("Voulez vous sauvegarder les changements effectués ?", "Oui", "Non");
                if (result == true)
                {
                    foreach (var row in Source.SelectMany(x=>x.Items).Where(x => x.HasChanged == true))
                    {
                        await gamesService.UpdateGame(row);
                    }
                }
            }
        }
        public ObservableCollection<DisplayGroupedGame> Source { get; } = new ObservableCollection<DisplayGroupedGame>();
        public ObservableCollection<DisplayGame> RawSource { get; } = new ObservableCollection<DisplayGame>();

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
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
            ToggleRaw = false;
            WeakReferenceMessenger.Default.Register<ReloadBckMessage>(this, async (r, m) =>
            {
                try
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => {
                        //Load();
                    });
                }
                catch (Exception ex)
                {
                }
            });
        }
        //public async void Load()
        //{
        //    await LoadDataAsync();
        //}

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
