using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPClient.Core;
using RetroFront.UWPClient.Core.Models;
using RetroFront.UWPClient.Services;

namespace RetroFront.UWPClient.ViewModels
{
    public class ParametreViewModel : ObservableObject
    {
        private ICommand _validateChenge;
        public ICommand ValidateChangeCommand => _validateChenge ?? (_validateChenge = new RelayCommand(ValidateChange));
        private ParametreService parametreService;
        private AppSettings _settings;
        public AppSettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }
        private string _BCK;
        public string BCK
        {
            get { return _BCK; }
            set { SetProperty(ref _BCK, value); }
        }
        private KeyValuePair<int, string> _selectedHomeDisplay;
        public KeyValuePair<int, string> SelectedHomeDisplay
        {
            get { return _selectedHomeDisplay; }
            set { SetProperty(ref _selectedHomeDisplay, value); }
        }
        private KeyValuePair<int, string> _selectedPlateFormeDisplay;
        public KeyValuePair<int, string> SelectedPlateFormeDisplay
        {
            get { return _selectedPlateFormeDisplay; }
            set { SetProperty(ref _selectedPlateFormeDisplay, value); }
        }
        private KeyValuePair<int, string> _selectedGameDisplay;
        public KeyValuePair<int, string> SelectedGameDisplay
        {
            get { return _selectedGameDisplay; }
            set { SetProperty(ref _selectedGameDisplay, value); }
        }
        private KeyValuePair<int, string> _selectedGameDetailDisplay;
        public KeyValuePair<int, string> SelectedGameDetailDisplay
        {
            get { return _selectedGameDetailDisplay; }
            set { SetProperty(ref _selectedGameDetailDisplay, value); }
        }
        public ObservableCollection<KeyValuePair<int, string>> HomeDisplays { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> PlateformeDisplays { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> GameDisplays { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> GameDetailDisplays { get; set; }
        public ParametreViewModel()
        {
            parametreService = new ParametreService();
            HomeDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            PlateformeDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            GameDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            GameDetailDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            BCK = @"http://localhost:34322/api/Img/GetAppBackground";
        }
        public async void LoadDataAsync()
        {
            //HomeDisplays.Clear();
            //GameDisplays.Clear();
            //PlateformeDisplays.Clear();

            HomeDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            PlateformeDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            GameDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            GameDetailDisplays = new ObservableCollection<KeyValuePair<int, string>>();
            Settings = await parametreService.GetSettingsAsync();
            foreach (var pair in await parametreService.GetHomeDisplay())
            {
                HomeDisplays.Add(pair);
            }
            foreach (var pair in await parametreService.GetPlateformeDisplay())
            {
                PlateformeDisplays.Add(pair);
            }
            foreach (var pair in await parametreService.GetGameDisplay())
            {
                GameDisplays.Add(pair);
            }
            foreach (var pair in await parametreService.GetGameDetailDisplay())
            {
                GameDetailDisplays.Add(pair);
            }
            SelectedHomeDisplay = HomeDisplays.First(x=>x.Key == (int)Settings.CurrentHomeDisplay);
            SelectedGameDisplay = GameDisplays.First(x => x.Key == (int)Settings.CurrentGameDisplay);
            SelectedGameDetailDisplay = GameDetailDisplays.First(x => x.Key == (int)Settings.CurrentGameDetailDisplay);
            SelectedPlateFormeDisplay = PlateformeDisplays.First(x => x.Key == (int)Settings.CurrentSysDisplay);
        }
        private async void ValidateChange()
        {
            Settings.CurrentGameDisplay = (RomDisplay)SelectedGameDisplay.Key;
            Settings.CurrentHomeDisplay = (HomeDisplay)SelectedHomeDisplay.Key;
            Settings.CurrentSysDisplay = (SysDisplay)SelectedPlateFormeDisplay.Key;
            Settings.CurrentGameDetailDisplay = (GameDetailDisplay)SelectedGameDetailDisplay.Key;
            await parametreService.SaveSettingsAsync(Settings);
            NavigationService.GoBack();
        }
    }
}
