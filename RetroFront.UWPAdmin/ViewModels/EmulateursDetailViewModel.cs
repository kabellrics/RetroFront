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
    public class EmulateursDetailViewModel : ObservableObject
    {
        private EmulatorDetailService emulatorDetailService;
        private DisplayEmulator _source;
        public DisplayEmulator Source
        {
            get => _source;
            set
            {
                SetProperty(ref _source, value);
            }
        }

        public EmulateursDetailViewModel()
        {
            emulatorDetailService = new EmulatorDetailService();
        }

        public async Task LoadDataAsync()
        {
            //Source.Clear();

            //Replace this with your actual data
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
                Source = emulatorDetailService.GetEmulator(int.Parse(selectedsysID));
            }
        }
    }
}
