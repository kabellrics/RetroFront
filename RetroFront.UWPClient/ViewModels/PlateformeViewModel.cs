using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using RetroFront.UWPClient.Core;
using RetroFront.UWPClient.Core.Models;
using RetroFront.UWPClient.Services;
using RetroFront.UWPClient.Views;

namespace RetroFront.UWPClient.ViewModels
{
    public class PlateformeViewModel : ObservableObject
    {
        private PlateformeService plateformeService;
        private ICommand _gamelistCommand;
        public ICommand GamelistCommand => _gamelistCommand ?? (_gamelistCommand = new RelayCommand(GotoGamePage));
        private int _displayType;
        public int DisplayType
        {
            get { return _displayType; }
            set { SetProperty(ref _displayType, value); }
        }
        private string _BCK;
        public string BCK
        {
            get { return _BCK; }
            set { SetProperty(ref _BCK, value); }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value);SendNewBck(); }
        }

        public ObservableCollection<Systeme> Systems { get; set; }
        public PlateformeViewModel()
        {
            plateformeService = new PlateformeService();
            DisplayType = (int)SysDisplay._0;
            BCK = string.Empty;
        }
        private void SendNewBck()
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new BckChangeMessage(Systems[SelectedIndex]));
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public async void LoadDataAsync(bool onlyCustomCollec = false)
        {
            Systems = new ObservableCollection<Systeme>();
            DisplayType = (int)await plateformeService.GetCurrentPlateformeDisplay();
            SelectedIndex = 0;
            await Init(onlyCustomCollec);
        }
        private async Task Init(bool onlyCustomCollec = false)
        {
            foreach (var system in await plateformeService.GetPlateformes(onlyCustomCollec))
            {
                Systems.Add(system);
            }
        }
        private void GotoGamePage()
        {
            var plateforme = Systems[SelectedIndex];
            NavigationService.Navigate<JeuxPage>(plateforme);
        }
    }
}
