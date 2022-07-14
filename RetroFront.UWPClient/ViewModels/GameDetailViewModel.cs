using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using RetroFront.UWPClient.Core;
using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.ViewModels
{
    public class GameDetailViewModel : ObservableObject
    {
        public bool True;
        public bool False;
        private int _displayType;
        private GameDetailService gameDetailService;
        public int DisplayType
        {
            get { return _displayType; }
            set { SetProperty(ref _displayType, value); }
        }
        public string _BCK;
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
        private GameRom _currentGame;
        public GameRom CurrenGame
        {
            get { return _currentGame; }
            set { SetProperty(ref _currentGame, value); }
        }
        public GameDetailViewModel()
        {
            True = true; False = false;
            gameDetailService = new GameDetailService();
            DisplayType = (int)GameDetailDisplay._0;
            BCK = @"http://localhost:34322/api/Img/GetAppBackground";
            CurrenGame = null;
        }
        public async void LoadDataAsync(GameRom game)
        {
            //CurrenGame = await gameDetailService.GetGame(game);
            CurrenGame = game;
            DisplayType = (int)await gameDetailService.GetCurrentGameDisplay();
            CurrentSys = await gameDetailService.GetTruePlateforme(CurrenGame);
            WeakReferenceMessenger.Default.Send(new BckChangeMessage(CurrenGame));
        }
}
}
