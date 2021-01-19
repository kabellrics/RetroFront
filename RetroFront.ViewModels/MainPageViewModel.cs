﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace RetroFront.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private IFileJSONService _fileJSONService;
        private IRetroarchService _retroarchService;
        private ISteamService _steamService;
        private IEmulateurService _emulateurService;
        private IDialogService _dialogService;
        private IGameService _gameService;
        private IThemeService _themeService;
        private ThemeViewModel _currentTheme;

        private int _selectedIndexSys;
        private int _selectedIndexEmu;
        private int _selectedIndexGame;
        private bool _showOnlyWithamesGame;

        public bool ShowOnlyWithamesGame
        {
            get { return _showOnlyWithamesGame; }
            set { _showOnlyWithamesGame = value;RaisePropertyChanged();LoadSpecific(); }
        }

        public int SelectedIndexSys
        {
            get { return _selectedIndexSys; }
            set { _selectedIndexSys = value;RaisePropertyChanged(); }
        }
        public int SelectedIndexEmu
        {
            get { return _selectedIndexEmu; }
            set { _selectedIndexEmu = value; RaisePropertyChanged(); }
        }
        public int SelectedIndexGame
        {
            get { return _selectedIndexGame; }
            set { _selectedIndexGame = value; RaisePropertyChanged(); }
        }
        public ThemeViewModel CurrentTheme
        {
            get { return _currentTheme; }
            set { _currentTheme = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<ThemeViewModel> _themes;
        public ObservableCollection<ThemeViewModel> Themes
        {
            get { return _themes; }
            set { _themes = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _allsystemes;
        public ObservableCollection<SystemeViewModel> AllSystemes
        {
            get { return _allsystemes; }
            set { _allsystemes = value;RaisePropertyChanged(); }
        }
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
            set { _selectedsystemeViewModel = value;RaisePropertyChanged(); /*LoadEmulator();*/ }
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
            set { _selectedEmulator = value;RaisePropertyChanged();/* LoadGames();*/ }
        }
        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { _games = value;RaisePropertyChanged(); }
        }
        private GameViewModel _selectedgames;
        public GameViewModel SelectedGames
        {
            get { return _selectedgames; }
            set { _selectedgames = value;RaisePropertyChanged(); }
        }
        private ICommand _reloadCommand;
        private ICommand _AddEmuCommand;
        private ICommand _AddCoreCommand;
        private ICommand _AddCustomListCommand;
        private ICommand _AddPlateformeCommand;
        private ICommand _AddSteamPlateformeCommand;
        private ICommand _AddExplorerCommand;
        private ICommand _AddSingleGameCommand;
        private ICommand _AddGamelistCommand;
        private ICommand _ShowDetailSystemeGameCommand;
        private ICommand _ShowDetailGameGameCommand;
        private ICommand _ShowDetailEmulatorCommand;
        private ICommand _ShowSettingsCommand;
        private ICommand _CHangeCurrentThemeCommand;
        public ICommand ReloadCommand
        {
            get
            {
                return _reloadCommand ?? (_reloadCommand = new RelayCommand(ReloadData));
            }
        }
        public ICommand ShowSettingsCommand
        {
            get
            {
                return _ShowSettingsCommand ?? (_ShowSettingsCommand = new RelayCommand(ShowSettings));
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
        public ICommand AddCustomListCommand
        {
            get
            {
                return _AddCustomListCommand ?? (_AddCustomListCommand = new RelayCommand(AddCustomList));
            }
        }

        public ICommand AddPlateformeCommand
        {
            get
            {
                return _AddPlateformeCommand ?? (_AddPlateformeCommand = new RelayCommand(AddPlateforme));
            }
        }
        public ICommand AddSteamPlateformeCommand
        {
            get
            {
                return _AddSteamPlateformeCommand ?? (_AddSteamPlateformeCommand = new RelayCommand(AddSteamPlateforme));
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
        public ICommand AddGamelistCommand
        {
            get
            {
                return _AddGamelistCommand ?? (_AddGamelistCommand = new RelayCommand<EmulatorViewModel>(AddGamelist));
            }
        }
        public ICommand ShowDetailSystemeGameCommand
        {
            get
            {
                return _ShowDetailSystemeGameCommand ?? (_ShowDetailSystemeGameCommand = new RelayCommand<SystemeViewModel>(ShowDetailSystemeGame));
            }
        } 
        public ICommand ShowDetailGameGameCommand
        {
            get
            {
                return _ShowDetailGameGameCommand ?? (_ShowDetailGameGameCommand = new RelayCommand<GameViewModel>(ShowDetailGameGame));
            }
        }
        public ICommand ShowDetailEmulatorCommand
        {
            get
            {
                return _ShowDetailEmulatorCommand ?? (_ShowDetailEmulatorCommand = new RelayCommand<EmulatorViewModel>(ShowDetailEmulator));
            }
        }
        public ICommand CHangeCurrentThemeCommand
        {
            get
            {
                return _CHangeCurrentThemeCommand ?? (_CHangeCurrentThemeCommand = new RelayCommand<ThemeViewModel>(CHangeCurrentTheme));
            }
        }
        public MainPageViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IRetroarchService retroarchService,IEmulateurService emulateurService, IDialogService dialogService, IGameService gameService, IThemeService themeService, ISteamService steamService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _retroarchService = retroarchService;
            _emulateurService = emulateurService;
            _dialogService = dialogService;
            _gameService = gameService;
            _steamService = steamService;
            _themeService = themeService;
            ShowOnlyWithamesGame = false;
            LoadThemeSettings();
            ReloadData();
        }

        private void LoadThemeSettings()
        {
            try
            {
                Themes = new ObservableCollection<ThemeViewModel>();
                var ths = _themeService.GetInstalledTheme();
                var currentthemefolder = _fileJSONService.GetCurrentTheme();
                CurrentTheme = new ThemeViewModel(ths.FirstOrDefault(x => x.FolderName == currentthemefolder));
                foreach (var th in ths)
                {
                    if (th.FolderName != CurrentTheme.Folder)
                        Themes.Add(new ThemeViewModel(th));
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ShowSettings()
        {
            _dialogService.ShowParameters();
        }

        private void ReloadFullEmulators()
        {
            FullEmulators = new ObservableCollection<EmulatorViewModel>();
            var emus = _databaseService.GetEmulators();
            foreach(var emu in emus)
            {
                FullEmulators.Add(new EmulatorViewModel(emu));
            }
        }
        private void ReloadData()
        {
            AllSystemes = new ObservableCollection<SystemeViewModel>();
            Systemes = new ObservableCollection<SystemeViewModel>();
            foreach (var sys in _databaseService.GetSystemes().OrderBy(x=> x.Name))
            {
                var sysvm = new SystemeViewModel(sys);
                sysvm.Bck = _themeService.GetBckForTheme(sys.Shortname, _fileJSONService.GetCurrentTheme());
                sysvm.NBEmu = $"{_databaseService.GetNbEmulatorForSysteme(sys.SystemeID)} Emulateurs";
                sysvm.NBGame = $"{_databaseService.GetNbGamesForPlateforme(sys.SystemeID)} Jeux";
                var emus = _databaseService.GetEmulatorsForSysteme(sys.SystemeID);
                foreach(var emu in emus.OrderBy(x=>x.Name))
                {
                    var emuvm = new EmulatorViewModel(emu);
                    emuvm.NBGame = $"{_databaseService.GetNbGamesForemulator(emu.EmulatorID)} Jeux";
                    var games = _databaseService.GetGamesForemulator(emu.EmulatorID);
                    foreach(var game in games.OrderBy(x => x.Name))
                    {
                        emuvm.Games.Add(new GameViewModel(game));
                    }
                    sysvm.Emulators.Add(emuvm);
                }
                Systemes.Add(sysvm);
                AllSystemes.Add(sysvm);
            }
            LoadSpecific();
            ReloadFullEmulators();
        }
        private void LoadSpecific()
        {
            if(ShowOnlyWithamesGame)
            {
                Systemes = new ObservableCollection<SystemeViewModel>(AllSystemes.Where(x => x.Emulators.SelectMany(g => g.Games).Count() > 0));
            }
            else
            {
                Systemes = AllSystemes;
            }
        }
        private void AddCore()
        {
            var emujson = _dialogService.CreateRetroarchCore();
            if (emujson != null)
            {
                var newemu = JsonConvert.DeserializeObject<Emulator>(emujson);
                _databaseService.AddEmulator(newemu);
                ReloadFullEmulators();
                ReloadData();
            }
        }
        private void AddSteamPlateforme()
        {
            var steamexepath = _dialogService.OpenUniqueFileDialog($"Steam Executable (*steam.exe)|*steam.exe");
            if(!string.IsNullOrEmpty(steamexepath))
            {
                Systeme sys = _databaseService.GetSystemeByName("steam");
                if(sys == null)
                {
                    sys = new Systeme();
                    sys.Name = "Steam";
                    sys.Shortname = "steam";
                    sys = _databaseService.AddSystem(sys);
                }
                var emu = _databaseService.AddEmulator(_emulateurService.AddExplorer(sys));
                var findedsteamgames = _steamService.GetSteamGame(steamexepath,emu);
                var games = _databaseService.GetGames();
                findedsteamgames = findedsteamgames.Where(x => !games.Any(g => g.SteamID == x.SteamID)).ToList();
                var validategames = _dialogService.ShowSteamGamesFounded(findedsteamgames);
                if(validategames != null)
                {
                    foreach(var game in validategames)
                    {
                        _databaseService.AddGame(_steamService.GetSteamInfos(game, emu));
                    }
                }
                ReloadData();
            }
        }
        private void AddPlateforme()
        {
           var sysjson = _dialogService.CreateJsonSys();
            if (sysjson != null)
            {
                var newsys = JsonConvert.DeserializeObject<Systeme>(sysjson);
                _databaseService.AddSystem(newsys);
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
                ReloadFullEmulators();
            }
            ReloadData();
        }
        private void AddExplorer(SystemeViewModel obj)
        {
            _databaseService.AddEmulator(_emulateurService.AddExplorer(obj.Systeme));

            ReloadFullEmulators();
            ReloadData();
        }
        private void AddSingleGame(EmulatorViewModel obj)
        {
            var gamefiles = _dialogService.OpenMultiFileDialog(_emulateurService.FormatExtension(obj.Emulator));
            if (gamefiles != null)
            {
                foreach (var gamefile in gamefiles)
                {
                    _databaseService.AddGame(_gameService.CreateGame(gamefile, obj.Emulator));
                }
                ReloadData(); 
            }
        }
        private void AddGamelist(EmulatorViewModel obj)
        {
            var gamelistfile = _dialogService.OpenUniqueFileDialog($"Fichier Gamelist (*.xml)|*.xml");
            if (gamelistfile != null)
            {
                var gamelist = _gameService.ImportGame(gamelistfile, obj.Emulator);
                foreach (var gamefile in gamelist)
                {
                    _databaseService.AddGame(gamefile);
                }
                ReloadData(); 
            }
        }
        private void ShowDetailSystemeGame(SystemeViewModel sys)
        {
            _dialogService.ShowSystemeDetail(sys.Systeme);
        }
        private void ShowDetailGameGame(GameViewModel obj)
        {
            _dialogService.ShowGameDetail(obj.Game);
        }
        private void ShowDetailEmulator(EmulatorViewModel obj)
        {
            _dialogService.OpenDetailEmu(obj.Emulator);
        }
        private void CHangeCurrentTheme(ThemeViewModel obj)
        {
            _fileJSONService.ChangeCurrentTheme(obj.Theme);
            LoadThemeSettings();
            ReloadData();
        }
        private void AddCustomList()
        {
            throw new NotImplementedException();
        }
    }
}
