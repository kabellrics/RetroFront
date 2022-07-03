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

namespace RetroFront.UWPClient.ViewModels
{
    public class JeuxViewModel : ObservableObject
    {
        private GameService gameservice;
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
        private Systeme _currentSys;
        public Systeme CurrentSys
        {
            get { return _currentSys; }
            set { SetProperty(ref _currentSys, value); }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); SendNewBck(); }
        }
        public ObservableCollection<GameRom> Games { get; set; }
        private ICommand _gamedetailCommand;
        public ICommand GameDetailCommand => _gamedetailCommand ?? (_gamedetailCommand = new RelayCommand(GotoGameDetailPage));
        public JeuxViewModel()
        {
            gameservice = new GameService();
            DisplayType = (int)RomDisplay._0;
            BCK = @"http://localhost:34322/api/Img/GetAppBackground";
        }
        private void SendNewBck()
        {
            WeakReferenceMessenger.Default.Send(new BckChangeMessage(-1));
        }
        public async void LoadDataAsync(Systeme plateforme = null)
        {
            Games = new ObservableCollection<GameRom>();
            DisplayType = (int)await gameservice.GetCurrentGameDisplay();
            CurrentSys = plateforme;
            await Init();
        }
        private async Task Init()
        {
            foreach (var system in await gameservice.GetGames(CurrentSys))
            {
                Games.Add(system);
            }
            SelectedIndex = 0;
        }
        private void GotoGameDetailPage()
        {
            var game = Games[SelectedIndex];
            NavigationService.Navigate<GameDetailPage>(game);
        }
    }
}
