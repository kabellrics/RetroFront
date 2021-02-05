using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        #region Properties

        private ScraperSource _currentsource;
        private ScraperType _currenttype;
        public ScraperSource CurrentScrapeSource
        {
            get { return _currentsource; }
            set { _currentsource = value;RaisePropertyChanged(); }
        }
        public ScraperType CurrentScraperType
        {
            get { return _currenttype; }
            set { _currenttype = value;RaisePropertyChanged(); }
        }

        private int _selectedIndexSys;
        private int _selectedIndexEmu;
        private int _selectedIndexGame;

        public int SelectedIndexSys
        {
            get { return _selectedIndexSys; }
            set { _selectedIndexSys = value; RaisePropertyChanged(); }
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
            set { _currentTheme = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<ThemeViewModel> _themes;
        public ObservableCollection<ThemeViewModel> Themes
        {
            get { return _themes; }
            set { _themes = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _allsystemes;
        public ObservableCollection<SystemeViewModel> AllSystemes
        {
            get { return _allsystemes; }
            set { _allsystemes = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _systemes;
        public ObservableCollection<SystemeViewModel> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _oldsystemes;
        public ObservableCollection<SystemeViewModel> OldSystemes
        {
            get { return _oldsystemes; }
            set { _oldsystemes = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _Onlysystemes;
        public ObservableCollection<SystemeViewModel> OnlySystemes
        {
            get { return _Onlysystemes; }
            set { _Onlysystemes = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _customList;
        public ObservableCollection<SystemeViewModel> CustomList
        {
            get { return _customList; }
            set { _customList = value; RaisePropertyChanged(); }
        }
        private SystemeViewModel _selectedsystemeViewModel;
        public SystemeViewModel SelectedSystemeViewModel
        {
            get { return _selectedsystemeViewModel; }
            set { _selectedsystemeViewModel = value; RaisePropertyChanged(); /*LoadEmulator();*/ }
        }
        private ObservableCollection<EmulatorViewModel> _emulators;
        public ObservableCollection<EmulatorViewModel> Emulators
        {
            get { return _emulators; }
            set { _emulators = value; RaisePropertyChanged(); }
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
            set { _selectedEmulator = value; RaisePropertyChanged();/* LoadGames();*/ }
        }
        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { _games = value; RaisePropertyChanged(); }
        }
        private GameViewModel _selectedgames;
        public GameViewModel SelectedGames
        {
            get { return _selectedgames; }
            set { _selectedgames = value; RaisePropertyChanged(); }
        } 
        #endregion

        #region Command
        private ICommand _reloadCommand;
        private ICommand _OpenRetroarchCommand;
        private ICommand _LoadDefaultBCKCommand;
        private ICommand _AddEmuCommand;
        private ICommand _AddCoreCommand;
        private ICommand _AddCustomListCommand;
        private ICommand _AddGameCustomCommand;
        private ICommand _AddPlateformeCommand;
        private ICommand _AddSteamPlateformeCommand;
        private ICommand _AddExplorerCommand;
        private ICommand _AddSingleGameCommand;
        private ICommand _CreateBckPackCommand;
        private ICommand _AddGamelistCommand;
        private ICommand _ShowDetailSystemeGameCommand;
        private ICommand _ShowDetailGameGameCommand;
        private ICommand _ShowDetailEmulatorCommand;
        private ICommand _ShowSettingsCommand;
        private ICommand _CHangeCurrentThemeCommand;
        private ICommand _ScrapeSystemeCommand;
        private ICommand _ChangeScrapeSourceCommand;
        private ICommand _ChangeScrapeTypeCommand;
        public ICommand ChangeScrapeSourceCommand
        {
            get
            {
                return _ChangeScrapeSourceCommand ?? (_ChangeScrapeSourceCommand = new RelayCommand<string>(ChangeScrapeSource));
            }
        }
        public ICommand ChangeScrapeTypeCommand
        {
            get
            {
                return _ChangeScrapeTypeCommand ?? (_ChangeScrapeTypeCommand = new RelayCommand<string>(ChangeScrapeType));
            }
        }
        public ICommand ReloadCommand
        {
            get
            {
                return _reloadCommand ?? (_reloadCommand = new RelayCommand(ReloadData));
            }
        }
        public ICommand OpenRetroarchCommand
        {
            get
            {
                return _OpenRetroarchCommand ?? (_OpenRetroarchCommand = new RelayCommand(OpenRetroarch));
            }
        }
        public ICommand LoadDefaultBCKCommand
        {
            get
            {
                return _LoadDefaultBCKCommand ?? (_LoadDefaultBCKCommand = new RelayCommand(LoadDefaultBCK));
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
                return _AddPlateformeCommand ?? (_AddPlateformeCommand = new RelayCommand<SystemeViewModel>(AddPlateforme));
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
        public ICommand AddGameCustomCommand
        {
            get
            {
                return _AddGameCustomCommand ?? (_AddGameCustomCommand = new RelayCommand<SystemeViewModel>(AddGameCustom));
            }
        }
        public ICommand AddSingleGameCommand
        {
            get
            {
                return _AddSingleGameCommand ?? (_AddSingleGameCommand = new RelayCommand<EmulatorViewModel>(AddSingleGame));
            }
        }
        public ICommand CreateBckPackCommand
        {
            get
            {
                return _CreateBckPackCommand ?? (_CreateBckPackCommand = new RelayCommand<string>(CreateBckPack));
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
        public ICommand ScrapeSystemeCommand
        {
            get
            {
                return _ScrapeSystemeCommand ?? (_ScrapeSystemeCommand = new RelayCommand<SystemeViewModel>(ScrapeSysteme));
            }
        }

        #endregion
        public MainPageViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IRetroarchService retroarchService, IEmulateurService emulateurService, IDialogService dialogService, IGameService gameService, IThemeService themeService, ISteamService steamService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _retroarchService = retroarchService;
            _emulateurService = emulateurService;
            _dialogService = dialogService;
            _gameService = gameService;
            _steamService = steamService;
            _themeService = themeService;
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
                    //if (th.FolderName != CurrentTheme.Folder)
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
            ReloadData();
        }
        private void ChangeScrapeSource(string obj)
        {
            CurrentScrapeSource = (ScraperSource)int.Parse(obj);
        }
        private void ChangeScrapeType(string obj)
        {
            CurrentScraperType = (ScraperType)int.Parse(obj);
        }
        private void CreateBckPack(string newtheme = null)
        {
            if (newtheme == null)
                newtheme = _dialogService.showInputDialog();
            if (newtheme != null)
            {
                var sysInPack = Systemes;
                foreach (var sys in sysInPack)
                {
                    var imgpath = _dialogService.showImgPickerForPlateformeDialog(sys.Systeme, newtheme);
                    if (imgpath != null)
                    {
                        _themeService.LoadBckForSysteme(sys.Systeme, newtheme, imgpath);
                    }
                }
            }
            LoadThemeSettings();
            ReloadData();
        }
        private void ReloadFullEmulators()
        {
            FullEmulators = new ObservableCollection<EmulatorViewModel>();
            var emus = _databaseService.GetEmulators();
            foreach (var emu in emus.Where(x => x.IsDuplicate == false))
            {
                FullEmulators.Add(new EmulatorViewModel(emu));
            }
        }
        private void ReloadData()
        {
            AllSystemes = new ObservableCollection<SystemeViewModel>();
            foreach (var sys in _fileJSONService.GetAllSysFromJSON().OrderBy(x => x.Name))
            {
                AllSystemes.Add(new SystemeViewModel(sys));
            }
            Systemes = new ObservableCollection<SystemeViewModel>();
            CustomList = new ObservableCollection<SystemeViewModel>();
            foreach (var sys in _databaseService.GetSystemes().OrderBy(x => x.Name))
            {
                var sysvm = new SystemeViewModel(sys);
                sysvm.Bck = _themeService.GetBckForTheme(sys.Shortname, _fileJSONService.GetCurrentTheme());
                var logopath = _themeService.GetLogoForTheme(sys.Shortname);
                if(File.Exists(logopath))
                {
                    sysvm.Logo = logopath;
                    sysvm.HasLogo = true;
                }
                sysvm.NBEmu = $"{_databaseService.GetNbEmulatorForSysteme(sys.SystemeID)} Emulateurs";
                sysvm.NBGame = $"{_databaseService.GetNbGamesForPlateforme(sys.SystemeID)} Jeux";
                var emus = _databaseService.GetEmulatorsForSysteme(sys.SystemeID);
                foreach (var emu in emus.OrderBy(x => x.Name))
                {
                    var emuvm = new EmulatorViewModel(emu);
                    emuvm.NBGame = $"{_databaseService.GetNbGamesForemulator(emu.EmulatorID)} Jeux";
                    var games = _databaseService.GetGamesForemulator(emu.EmulatorID);
                    foreach (var game in games.OrderBy(x => x.Name))
                    {
                        emuvm.Games.Add(new GameViewModel(game));
                    }
                    sysvm.Emulators.Add(emuvm);
                }
                Systemes.Add(sysvm);
            }
            Systemes = new ObservableCollection<SystemeViewModel>(Systemes.OrderBy(x => x.Systeme.Type).ThenBy(x => x.Name));
            CustomList = new ObservableCollection<SystemeViewModel>(AllSystemes.Where(x => x.Systeme.Type == SysType.Collection).OrderBy(x=>x.Name));
            OldSystemes = new ObservableCollection<SystemeViewModel>(Systemes.Where(x=>x.Systeme.Type != SysType.Collection && x.Systeme.Type != SysType.GameStore && x.Systeme.Type != SysType.Standalone).OrderBy(x=>x.Name));
            OnlySystemes = new ObservableCollection<SystemeViewModel>(Systemes.Where(x => x.Systeme.Type != SysType.Collection && x.Systeme.Type != SysType.Standalone).OrderBy(x => x.Name));
            ReloadFullEmulators();
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
            if (!string.IsNullOrEmpty(steamexepath))
            {
                Systeme sys = _databaseService.GetSystemeByName("steam");
                if (sys == null)
                {
                    sys = new Systeme();
                    sys.Name = "Steam";
                    sys.Shortname = "steam";
                    sys = _databaseService.AddSystem(sys);
                }
                var emu = _databaseService.GetEmulatorsForSysteme(sys.SystemeID).FirstOrDefault(x => x.Chemin.Contains("Windows\\explorer.exe"));
                if(emu == null)
                {
                    emu = _databaseService.AddEmulator(_emulateurService.AddExplorer(sys));
                }
                var findedsteamgames = _steamService.GetSteamGame(steamexepath, emu);
                var games = _databaseService.GetGames();
                findedsteamgames = findedsteamgames.Where(x => !games.Any(g => g.SteamID == x.SteamID)).ToList();
                var validategames = _dialogService.ShowSteamGamesFounded(findedsteamgames);
                if (validategames != null)
                {
                    foreach (var game in validategames)
                    {
                        _databaseService.AddGame(_steamService.GetSteamInfos(game, emu));
                    }
                }
                ReloadData();
            }
        }
        private void AddPlateforme(SystemeViewModel sysvm = null)
        {
            if (sysvm == null)
            {
                var sysjson = _dialogService.CreateJsonSys();
                if (sysjson != null)
                {
                    sysvm = new SystemeViewModel(JsonConvert.DeserializeObject<Systeme>(sysjson));
                }
            }
            var sys = _databaseService.AddSystem(sysvm.Systeme);
            _themeService.LoadDefaultBckForSysteme(sys);
            ReloadData();
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
        private void LoadDefaultBCK()
        {
            var plateformesbcks = AllSystemes.Where(x => x.Emulators.SelectMany(g => g.Games).Count() > 0);
            foreach (var plateform in plateformesbcks)
            {
                _themeService.LoadDefaultBckForSysteme(plateform.Systeme);
            }
            ReloadData();
        }
        private void OpenRetroarch()
        {
            var retroarchpath = $"{_fileJSONService.appSettings.RetroarchPath}\\retroarch.exe";
            Process.Start(retroarchpath);
        }
        private void ShowDetailSystemeGame(SystemeViewModel sys)
        {
            _dialogService.ShowSystemeDetail(sys.Systeme);
        }
        private void ShowDetailGameGame(GameViewModel obj)
        {
            var game = _dialogService.ShowGameDetail(obj.Game);
            if (game != null)
            {
                if (game.Boxart.StartsWith("http"))
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Boxart);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Boxart)}";
                    _gameService.DownloadImgData(game.Boxart, targetfile);
                    game.Boxart = targetfile;
                }
                if (game.Fanart.StartsWith("http"))
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Banner);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Fanart)}";
                    _gameService.DownloadImgData(game.Fanart, targetfile);
                    game.Fanart = targetfile;
                }
                if (game.Screenshoot.StartsWith("http"))
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.ArtWork);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Screenshoot)}";
                    _gameService.DownloadImgData(game.Screenshoot, targetfile);
                    game.Screenshoot = targetfile;
                }
                if (game.Logo.StartsWith("http"))
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Logo);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Logo)}";
                    _gameService.DownloadImgData(game.Logo, targetfile);
                    game.Logo = targetfile;
                }
                var duplicates = _databaseService.GetGames().Where(x => x.Path == game.Path);
                foreach(var duplicate in duplicates)
                {
                    duplicate.Boxart = game.Boxart;
                    duplicate.Fanart = game.Fanart;
                    duplicate.Screenshoot = game.Screenshoot;
                    duplicate.Logo = game.Logo;
                    duplicate.SGDBID = game.SGDBID;
                    duplicate.SteamID = game.SteamID;
                    duplicate.IGDBID = game.IGDBID;
                    duplicate.RAWGID = game.RAWGID;
                }
                _databaseService.SaveUpdate();
                ReloadData();
            }
        }

        private void ScrapeSysteme(SystemeViewModel sys)
        {
            var gamestoscrape = _databaseService.GetGamesForPlateforme(sys.Systeme.SystemeID).OrderBy(x => x.Name);
            foreach(var games in gamestoscrape)
            {
               if(CurrentScraperType == ScraperType.ArtWork)
               {
                    var result = _dialogService.SearchImgInSteamGridDB(games, CurrentScraperType, CurrentScrapeSource);
                    if (result != null)
                    {
                        games.Screenshoot = result;
                        var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.ArtWork);
                        var targetfile = $"{targetfolder}{Path.GetExtension(games.Screenshoot)}";
                        _gameService.DownloadImgData(games.Screenshoot, targetfile);
                        games.Screenshoot = targetfile; 
                    }
                }
               else if(CurrentScraperType == ScraperType.Banner)
               {
                    var result = _dialogService.SearchImgInSteamGridDB(games, CurrentScraperType, CurrentScrapeSource);
                    if (result != null)
                    {
                        games.Fanart = result;
                        var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Banner);
                        var targetfile = $"{targetfolder}{Path.GetExtension(games.Fanart)}";
                        _gameService.DownloadImgData(games.Fanart, targetfile);
                        games.Fanart = targetfile; 
                    }
                }
               else if(CurrentScraperType == ScraperType.Boxart)
               {
                    var result = _dialogService.SearchImgInSteamGridDB(games, CurrentScraperType, CurrentScrapeSource);
                    if (result != null)
                    {
                        games.Boxart = result;
                        var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Boxart);
                        var targetfile = $"{targetfolder}{Path.GetExtension(games.Boxart)}";
                        _gameService.DownloadImgData(games.Boxart, targetfile);
                        games.Boxart = targetfile; 
                    }
                }
               else if(CurrentScraperType == ScraperType.Logo)
               {
                    var result = _dialogService.SearchImgInSteamGridDB(games, CurrentScraperType, CurrentScrapeSource);
                    if (result != null)
                    {
                        games.Logo = result;
                        var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Logo);
                        var targetfile = $"{targetfolder}{Path.GetExtension(games.Logo)}";
                        _gameService.DownloadImgData(games.Logo, targetfile);
                        games.Logo = targetfile; 
                    }
                }
                _databaseService.SaveUpdate();
                ReloadData();
            }
            
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
        private void AddGameCustom(SystemeViewModel obj)
        {
            var allgames = _databaseService.GetGames().Where(x => x.IsDuplicate == false);
            var gamesalreadyIn = _databaseService.GetGamesForPlateforme(obj.Systeme.SystemeID);
            if (gamesalreadyIn != null)
            {
                var gameschoosing = allgames.Where(x => !gamesalreadyIn.Any(g => g.Path == x.Path));
                var gamestoadd = _dialogService.AddGamesToCollection(obj.Systeme.Name, gameschoosing);
                if (gamestoadd != null)
                {
                    var groupedGames = from game in gamestoadd
                                       group game by game.EmulatorID into groupedgame
                                       orderby groupedgame.Key
                                       select groupedgame;

                    foreach (var grp in groupedGames)
                    {
                        var newemu = _emulateurService.DuplicateEmulator(_databaseService.GetEmulator(grp.Key));
                        newemu.SystemeID = obj.Systeme.SystemeID;
                        _databaseService.AddEmulator(newemu);
                        foreach (var game in grp)
                        {
                            var dupligame = _gameService.DuplicateGame(game);
                            dupligame.EmulatorID = newemu.EmulatorID;
                            _databaseService.AddGame(dupligame);
                        }
                    }
                }
            }
        }
        private void AddCustomList()
        {
            var sysjson = _dialogService.CreateJsonSys();
            if (sysjson != null)
            {
                var newsys = JsonConvert.DeserializeObject<Systeme>(sysjson);
                newsys.Type = SysType.Collection;
                newsys = _databaseService.AddSystem(newsys);
                var allgames = _databaseService.GetGames();
                var gamestoadd = _dialogService.AddGamesToCollection(newsys.Name, allgames.Where(x => x.IsDuplicate == false));
                if (gamestoadd != null)
                {
                    var groupedGames = from game in gamestoadd
                                       group game by game.EmulatorID into groupedgame
                                       orderby groupedgame.Key
                                       select groupedgame;

                    foreach (var grp in groupedGames)
                    {
                        var newemu = _emulateurService.DuplicateEmulator(_databaseService.GetEmulator(grp.Key));
                        newemu.SystemeID = newsys.SystemeID;
                        _databaseService.AddEmulator(newemu);
                        foreach (var game in grp)
                        {
                            var dupligame = _gameService.DuplicateGame(game);
                            dupligame.EmulatorID = newemu.EmulatorID;
                            _databaseService.AddGame(dupligame);
                        }
                    }
                }

                ReloadData();
            }
        }
    }
}
