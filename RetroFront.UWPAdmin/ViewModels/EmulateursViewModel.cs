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
    public class EmulateursViewModel : ObservableObject
    {
        public const string EmulateursSelectedIdKey = "EmulateursSelectedIdKey";
        private EmulatorsService emulatorsService;
        private ICommand _itemSelectedCommand;

        public ObservableCollection<DisplayEmulator> Source { get; } = new ObservableCollection<DisplayEmulator>();

        //public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnItemSelected));
        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<DisplayEmulator>(OnItemSelected));
        private bool _toggleRaw;
        public bool ToggleRaw
        {
            get => _toggleRaw;
            set
            {
                SetProperty(ref _toggleRaw, value);
            }
        }
        public EmulateursViewModel()
        {
            emulatorsService = new EmulatorsService();
            ToggleRaw = false;
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");
            var data = await emulatorsService.GetEmulators();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void OnItemSelected(DisplayEmulator args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(EmulateursSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<EmulateursDetailPage>(selected.ID.ToString());
        }
    }
}
