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
    public class SystèmesViewModel : ObservableObject
    {
        public const string SystèmesSelectedIdKey = "SystèmesSelectedIdKey";
        private SystemesService systemesService;
        private ICommand _itemSelectedCommand;

        public ObservableCollection<DisplaySysteme> Source { get; } = new ObservableCollection<DisplaySysteme>();

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<DisplaySysteme>(OnItemSelected));
        //public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnItemSelected));
        private bool _toggleRaw;
        public bool ToggleRaw
        {
            get => _toggleRaw;
            set
            {
                SetProperty(ref _toggleRaw, value);
            }
        }
        public SystèmesViewModel()
        {
            systemesService = new SystemesService();
            ToggleRaw = false;
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");
            var data = systemesService.GetSystemes();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void OnItemSelected(DisplaySysteme args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(SystèmesSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<SystèmesDetailPage>(selected.ID.ToString());
        }
    }
}
