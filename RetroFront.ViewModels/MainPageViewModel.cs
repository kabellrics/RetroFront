using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace RetroFront.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private IFileJSONService _fileJSONService;
        private IMainService _mainService;
        private IRetroarchService _retroarchService;
        private IEmulateurService _emulateurService;
        private IDialogService _dialogService;
        private ObservableCollection<SystemeViewModel> _systemes;
        public ObservableCollection<SystemeViewModel> Systemes
        {
            get { return _systemes; }
            set { _systemes = value;RaisePropertyChanged(); }
        }
        private SystemeViewModel _selectedsystemeViewModel;
        public SystemeViewModel SelectedSystemeViewModel
        {
            get { return _selectedsystemeViewModel; }
            set { _selectedsystemeViewModel = value;RaisePropertyChanged(); LoadEmulator(); }
        }
        private ObservableCollection<EmulatorViewModel> _emulators;
        public ObservableCollection<EmulatorViewModel> Emulators
        {
            get { return _emulators; }
            set { _emulators = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<EmulatorViewModel> _fullemulators;
        public ObservableCollection<EmulatorViewModel> FullEmulators
        {
            get { return _fullemulators; }
            set { _fullemulators = value; RaisePropertyChanged(); }
        }
        private EmulatorViewModel _selectedEmulator;
        public EmulatorViewModel SelectedEmulator
        {
            get { return _selectedEmulator; }
            set { _selectedEmulator = value;RaisePropertyChanged(); LoadGames(); }
        }
        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { _games = value;RaisePropertyChanged(); }
        }
        private ICommand _reloadCommand;
        private ICommand _AddEmuCommand;
        private ICommand _AddCoreCommand;
        private ICommand _AddExplorerCommand;
        private ICommand _AddSingleGameCommand;
        public ICommand ReloadCommand
        {
            get
            {
                return _reloadCommand ?? (_reloadCommand = new RelayCommand(ReloadData));
            }
        }
        public ICommand AddEmuCommand
        {
            get
            {
                return _AddEmuCommand ?? (_AddEmuCommand = new RelayCommand(AddEmu));
            }
        }
        public ICommand AddCoreCommand
        {
            get
            {
                return _AddCoreCommand ?? (_AddCoreCommand = new RelayCommand(AddCore));
            }
        }
        public ICommand AddExplorerCommand
        {
            get
            {
                return _AddExplorerCommand ?? (_AddExplorerCommand = new RelayCommand<SystemeViewModel>(AddExplorer));
            }
        }
        public ICommand AddSingleGameCommand
        {
            get
            {
                return _AddSingleGameCommand ?? (_AddSingleGameCommand = new RelayCommand<EmulatorViewModel>(AddSingleGame));
            }
        }

        public MainPageViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IMainService mainService, IRetroarchService retroarchService,IEmulateurService emulateurService, IDialogService dialogService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _mainService = mainService;
            _retroarchService = retroarchService;
            _emulateurService = emulateurService;
            _dialogService = dialogService;
            FullEmulators = new ObservableCollection<EmulatorViewModel>();
            var emus = _databaseService.GetEmulators();
            foreach(var emu in emus)
            {
                FullEmulators.Add(new EmulatorViewModel(emu));
            }
            ReloadData();
        }
        private void ReloadData()
        {
            Systemes = new ObservableCollection<SystemeViewModel>();

            foreach (var sys in _databaseService.GetSystemes().OrderBy(x=> x.Name))
            {
                var sysvm = new SystemeViewModel(sys);
                sysvm.NBEmu = $"{_databaseService.GetNbEmulatorForSysteme(sys.SystemeID)} Emulateurs";
                sysvm.NBGame = $"{_databaseService.GetNbGamesForPlateforme(sys.SystemeID)} Jeux";
                //sysvm.NBGame = $"{Systeme?.Emulators.SelectMany(x => x.Games).Count()} Jeux";
                Systemes.Add(sysvm);
            }
        }
        private void LoadEmulator()
        {
            if (SelectedSystemeViewModel != null)
            {
                Emulators = new ObservableCollection<EmulatorViewModel>();
                foreach (var emu in _databaseService.GetEmulatorsForSysteme(SelectedSystemeViewModel.Systeme.SystemeID).OrderBy(x => x.Name))
                {
                    var emuvm = new EmulatorViewModel(emu);
                    emuvm.NBGame = $"{_databaseService.GetNbGamesForemulator(emu.EmulatorID)} Jeux";
                    Emulators.Add(emuvm);
                } 
            }
        }
        private void LoadGames()
        {
            if(SelectedEmulator != null)
            {
                Games = new ObservableCollection<GameViewModel>();
                foreach(var game in _databaseService.GetGamesForemulator(SelectedEmulator.Emulator.EmulatorID).OrderBy(XMLSystem=>XMLSystem.Name))
                {
                    Games.Add(new GameViewModel(game));
                }
            }
        }
        private void AddCore()
        {
            var emujson = _dialogService.CreateRetroarchCore();
            if (emujson != null)
            {
                var newemu = JsonConvert.DeserializeObject<Emulator>(emujson);
                _databaseService.AddEmulator(newemu);
                ReloadData();
            }
        }
        private void AddEmu()
        {
            var emujson = _dialogService.CreateJsonEmu();
            if (emujson != null)
            {
                var newemu = JsonConvert.DeserializeObject<Emulator>(emujson);
                _databaseService.AddEmulator(newemu);
                ReloadData(); 
            }
        }
        private void AddExplorer(SystemeViewModel obj)
        {
            Emulator emuexe = new Emulator();
            emuexe.Name = "Explorer.exe";
            emuexe.Chemin = @"C:\Windows\explorer.exe";
            emuexe.Command = "%ROMPATH% --fullscreen";
            emuexe.SystemeID = obj.Systeme.SystemeID;
            emuexe.Extension = ".exe .EXE .lnk .url";
            _databaseService.AddEmulator(emuexe);
            ReloadData();
        }
        private void AddSingleGame(EmulatorViewModel obj)
        {
            var gamefiles = _dialogService.OpenMultiFileDialog(FormatExtension(obj));
            foreach (var gamefile in gamefiles)
            {
                Game game = new Game();
                game.Path = gamefile;
                game.Name = System.IO.Path.GetFileNameWithoutExtension(gamefile);
                game.EmulatorID = obj.Emulator.EmulatorID;
                _databaseService.AddGame(game); 
            }
        }

        private string FormatExtension(EmulatorViewModel ext)
        {
            //. Exemple : \"Fichiers image (*.bmp, *.jpg)|*.bmp;*.jpg|Tous les fichiers (*.*)|*.*\"'
            string str = ext.Emulator.Extension.Replace(".","*.");
            var extlist = str.Split(" ").ToList();
            var exext = $"({string.Join(", ", extlist)})|{string.Join("; ", extlist)}";
            return $"Fichier pour {ext.Name} {exext} ";
        }
    }
}
