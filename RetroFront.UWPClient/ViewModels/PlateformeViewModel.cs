using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPClient.Core;
using RetroFront.UWPClient.Core.Models;

namespace RetroFront.UWPClient.ViewModels
{
    public class PlateformeViewModel : ObservableObject
    {
        private PlateformeService plateformeService;
        private SysDisplay _displayType;
        public SysDisplay DisplayType
        {
            get { return _displayType; }
            set { SetProperty(ref _displayType, value); }
        }
        public ObservableCollection<Systeme> Systems { get; set; }
        public PlateformeViewModel()
        {
            plateformeService = new PlateformeService();
            DisplayType = SysDisplay._0;
        }
        public async void LoadDataAsync()
        {
            Systems = new ObservableCollection<Systeme>();
            await Init();
        }
        private async Task Init()
        {
            foreach (var system in await plateformeService.GetPlateformes())
            {
                Systems.Add(system);
            }
        }
    }
}
