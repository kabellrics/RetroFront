using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Toolkit.Mvvm.ComponentModel;

using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;

using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class GamesDetailViewModel : ObservableObject
    {
        private GameDetailService gameDetailService;
        private DisplayGame _source;
        public DisplayGame Source
        {
            get => _source;
            set
            {
                SetProperty(ref _source, value);
            }
        }

        public GamesDetailViewModel()
        {
            gameDetailService = new GameDetailService();
        }

        public async Task LoadDataAsync()
        {
            //Source.Clear();

            //// Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");

            //foreach (var item in data)
            //{
            //    Source.Add(item);
            //}
        }

        public async void Initialize(string selectedGameID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedGameID))
            {
                Source = await gameDetailService.GetGame(int.Parse(selectedGameID));
            }
        }
    }
}
