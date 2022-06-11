using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using RetroFront.Models;
using RetroFront.Models.Messenger;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetroFront.ViewModels
{
    public class StartGameViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private INavigationService _navigationService;
        private GameViewModel _currentgameVM;
        private RelayCommand _loadedCommand;
        private Process gameprocess;
        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new RelayCommand(Loaded));
            }
        }
        private Systeme parameter;
        public Systeme CurrentSysteme
        {
            get { return parameter; }
            set { parameter = value; RaisePropertyChanged(); }
        }
        public GameViewModel Game
        {
            get { return _currentgameVM; }
            set { _currentgameVM = value; RaisePropertyChanged(); }
        }
        public StartGameViewModel(IDatabaseService databaseService, INavigationService navigationService)
        {
            Messenger.Default.Register<KillGameMessage>(this, KillGameNotificationMessage);
            _databaseService = databaseService;
            _navigationService = navigationService;
            var param = (ParameterLaunchGame)_navigationService.Parameter;
            Game = param.CurrentGameVM;
            CurrentSysteme = param.CurrentSysteme;
        }

        private void KillGameNotificationMessage(KillGameMessage obj)
        {
            gameprocess.Kill(true); 
            Task.Delay(3000);
            _navigationService.NavigateTo("Games", CurrentSysteme, string.Empty);
        }

        private void Loaded()
        {
            Game = null;
            CurrentSysteme = null;
            var param = (ParameterLaunchGame)_navigationService.Parameter;
            Game = param.CurrentGameVM;
            CurrentSysteme = param.CurrentSysteme;
            StartingGame();
        }
        private async void StartingGame()
        {
            await Task.Delay(5000);
            string execcommand = string.Empty;
            string paramcommand = string.Empty;
            if (Game.Game.SteamID > 0 || !string.IsNullOrEmpty(Game.Game.OriginID) || !string.IsNullOrEmpty(Game.Game.EpicID) )
            {
                execcommand = Game.Path;
            }
            else
            {
                var emulator = _databaseService.GetEmulator(Game.Game.EmulatorID);
                execcommand = emulator.Chemin;
                //execcommand = "cmd.exe";
                paramcommand = emulator.Command.Replace("{ImagePath}", Game.Path);
                //paramcommand = string.Format($"\"{ emulator.Chemin }\" { emulator.Command.Replace("{ImagePath}", Game.Path) }"); 
            }
            Game.Game.NbTimeStarted++;
            Game.Game.LastStart = DateTime.Now;
            _databaseService.SaveUpdate();
            if(!string.IsNullOrEmpty(execcommand))
            {
                //Process.Start(execcommand);
                var startInfo = new ProcessStartInfo(execcommand, paramcommand)
                {
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal                   
                };
                gameprocess = new Process();
                gameprocess.StartInfo = startInfo;
                gameprocess.Start();
                //gameprocess = Process.Start(startInfo);
                //await Task.Delay(30000);
                //gameprocess.WaitForExit();
                //await Task.Delay(3000);
                //_navigationService.NavigateTo("Games", CurrentSysteme, string.Empty);
            }
        }
    }
}
