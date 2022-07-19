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
    public class SystèmesDetailViewModel : ObservableObject
    {
        private object _selectedImage;
        private SystemeDetailService systemeDetailService;
        public object SelectedImage
        {
            get => _selectedImage;
            set
            {
                SetProperty(ref _selectedImage, value);
                ImagesNavigationHelper.UpdateImageId(SystèmesViewModel.SystèmesSelectedIdKey, ((SampleImage)SelectedImage)?.ID);
            }
        }

        public DisplaySysteme Source { get; set; }

        public SystèmesDetailViewModel()
        {
            systemeDetailService = new SystemeDetailService();
        }

        public async Task LoadDataAsync()
        {
            //Source.Clear();

            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");

            //foreach (var item in data)
            //{
            //    Source.Add(item);
            //}
        }

        public void Initialize(string selectedsysID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedsysID))
            {
                Source = systemeDetailService.GetSysteme(int.Parse(selectedsysID));
            }
        }
    }
}
