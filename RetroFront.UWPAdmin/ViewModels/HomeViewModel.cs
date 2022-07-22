using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp.UI.Animations;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using RetroFront.UWPAdmin.Views;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private HomeService homeService;
        public ObservableCollection<DisplayGame> NoIGDBSource { get; } = new ObservableCollection<DisplayGame>();
        public ObservableCollection<DisplayGame> NoSGDBSource { get; } = new ObservableCollection<DisplayGame>();
        public ObservableCollection<DisplayGame> NoSSDBSource { get; } = new ObservableCollection<DisplayGame>();
        public HomeViewModel()
        {
            homeService = new HomeService();
            WeakReferenceMessenger.Default.Register<ReloadBckMessage>(this, (r, m) =>
            {
                try
                {
                    Load();
                }
                catch (Exception ex)
                {
                }
            });
        }
        public async void Load()
        {
            await Windows.UI.Core.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                await LoadDataAsync();
            });
        }
        public async Task LoadDataAsync()
        {
            NoIGDBSource.Clear();
            NoSGDBSource.Clear();
            NoSSDBSource.Clear();
            var dataigdb = await homeService.GetGameWithoutIGDB();
            foreach (var raw in dataigdb)
            {
                NoIGDBSource.Add(raw);
            }
            var datasgdb = await homeService.GetGameWithoutSGDB();
            foreach (var raw in datasgdb)
            {
                NoSGDBSource.Add(raw);
            }
            var datassdb = await homeService.GetGameWithoutSSDB();
            foreach (var raw in datassdb)
            {
                NoSSDBSource.Add(raw);
            }
        }
        public void OnItemSelected(DisplayGame args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(GamesSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<GamesDetailPage>(selected.ID.ToString());
        }
    }
}
