using GalaSoft.MvvmLight;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace RetroFront.ViewModels
{
    public class StartGameViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private INavigationService _navigationService;
        private GameRom _currentgameVM;
        public GameRom Game
        {
            get { return _currentgameVM; }
            set { _currentgameVM = value; RaisePropertyChanged(); }
        }
        public StartGameViewModel(IDatabaseService databaseService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            Game = (GameRom)_navigationService.Parameter;
            StartingGame();
        }

        private void StartingGame()
        {
            string execcommand = null;
            if (Game.SteamID < 0 || !string.IsNullOrEmpty(Game.OriginID) || !string.IsNullOrEmpty(Game.EpicID) )
            {
                execcommand = Game.Path;
            }
            else
            {
                var emulator = _databaseService.GetEmulator(Game.EmulatorID);
                execcommand = emulator.Command.Replace("{ImagePath}", Game.Path);
            }
            Game.NbTimeStarted++;
            Game.LastStart = DateTime.Now;
            _databaseService.SaveUpdate();
            if(!string.IsNullOrEmpty(execcommand))
            {
                Process.Start(execcommand);
                Thread.Sleep(30000);
                _navigationService.GoBack();
            }
        }
    }
}
