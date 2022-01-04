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
using VideoLibrary;

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
        private IIGDBService _iGDBService;
        private IOriginService _originService;
        private IEpicService _epicService;
        private IScreenScraperService _screenScraperService;

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
        private ObservableCollection<SCSPSystemeViewModel> _allsystemes;
        public ObservableCollection<SCSPSystemeViewModel> AllSystemes
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

        private string _bCKImg;
        public string BCKImg
        {
            get { return _bCKImg; }
            set { _bCKImg = value; RaisePropertyChanged(); }
        }
        #endregion

        #region Command
        private ICommand _reloadCommand;
        private ICommand _OpenRetroarchCommand;
        private ICommand _LoadDefaultBCKCommand;
        private ICommand _AddEmuCommand;
        private ICommand _AddStandaloneCommand;
        private ICommand _AddCoreCommand;
        private ICommand _AddCustomListCommand;
        private ICommand _AddGameCustomCommand;
        private ICommand _AddPlateformeCommand;
        private ICommand _AddSteamPlateformeCommand;
        private ICommand _AddOriginPlateformeCommand;
        private ICommand _AddEpicPlateformeCommand;
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
        private ICommand _GetSystemeIMGCommand;
        private ICommand _SetBckCommand;
        public ICommand SetBckCommand
        {
            get
            {
                return _SetBckCommand ?? (_SetBckCommand = new RelayCommand(SetBck));
            }
        }
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
                return _AddPlateformeCommand ?? (_AddPlateformeCommand = new RelayCommand<SCSPSystemeViewModel>(AddPlateforme));
            }
        }
        public ICommand AddSteamPlateformeCommand
        {
            get
            {
                return _AddSteamPlateformeCommand ?? (_AddSteamPlateformeCommand = new RelayCommand(AddSteamPlateforme));
            }
        }
        public ICommand AddOriginPlateformeCommand
        {
            get
            {
                return _AddOriginPlateformeCommand ?? (_AddOriginPlateformeCommand = new RelayCommand(AddOriginPlateforme));
            }
        }
        public ICommand AddEpicPlateformeCommand
        {
            get
            {
                return _AddEpicPlateformeCommand ?? (_AddEpicPlateformeCommand = new RelayCommand(AddEpicPlateforme));
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
        public ICommand AddStandaloneCommand
        {
            get
            {
                return _AddStandaloneCommand ?? (_AddStandaloneCommand = new RelayCommand(AddStandalone));
            }
        }
        public ICommand GetSystemeIMGCommand
        {
            get
            {
                return _GetSystemeIMGCommand ?? (_GetSystemeIMGCommand = new RelayCommand(GetSystemeIMG));
            }
        }
        #endregion
        public MainPageViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IRetroarchService retroarchService, IEmulateurService emulateurService, IDialogService dialogService, IGameService gameService, IThemeService themeService, ISteamService steamService, IIGDBService iGDBService, IOriginService originService, IEpicService epicService, IScreenScraperService screenScraperService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _retroarchService = retroarchService;
            _emulateurService = emulateurService;
            _dialogService = dialogService;
            _gameService = gameService;
            _steamService = steamService;
            _themeService = themeService;
            _iGDBService = iGDBService;
            _originService = originService;
            _epicService = epicService;
            _screenScraperService = screenScraperService;
            LoadThemeSettings();
            ReloadData();
        }
        private void LoadThemeSettings()
        {
            try 
            {
                if (File.Exists(_fileJSONService.appSettings.DefaultBCK))
                {
                    BCKImg = _fileJSONService.appSettings.DefaultBCK; 
                }
            }
            catch (Exception ex)
            {
            }
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
            bool addlogo = _dialogService.showMessageYesNo("Pack Style", "Voulez vous ajouter également les logos ?");
            if (newtheme == null)
                newtheme = _dialogService.showInputDialog();
            if (newtheme != null)
            {
                var sysInPack = Systemes;
                foreach (var sys in sysInPack)
                {
                    if (addlogo)
                    {
                        var logopath = _dialogService.showImgPickerForPlateformeDialog(sys.Systeme, newtheme, ScraperType.Logo);
                        if (logopath != null)
                        {
                            _themeService.LoadLogoForSysteme(sys.Systeme, newtheme, logopath);
                        } 
                    }
                    var imgpath = _dialogService.showImgPickerForPlateformeDialog(sys.Systeme, newtheme,ScraperType.ArtWork);
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
            AllSystemes = new ObservableCollection<SCSPSystemeViewModel>();
            foreach (var sys in _fileJSONService.GetAllSysFromJSON().OrderBy(x => x.noms.noms_commun))
            {
                var sysSCSPvm = new SCSPSystemeViewModel(sys);
                if (!string.IsNullOrEmpty(sysSCSPvm.ShortName))
                    AllSystemes.Add(sysSCSPvm);
            }
            AllSystemes = new ObservableCollection<SCSPSystemeViewModel>(AllSystemes.OrderBy(x=>x.Name));
            Systemes = new ObservableCollection<SystemeViewModel>();
            CustomList = new ObservableCollection<SystemeViewModel>();
            foreach (var sys in _databaseService.GetSystemes().OrderBy(x => x.Name))
            {
                var sysvm = new SystemeViewModel(sys);
                sysvm.Bck = _themeService.GetBckForTheme(sys.Shortname, _fileJSONService.GetCurrentTheme());
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
            CustomList = new ObservableCollection<SystemeViewModel>(Systemes.Where(x => x.Systeme.Type == SysType.Collection).OrderBy(x=>x.Name));
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
                var findedsteamgames = _steamService.GetSteamGame(emu);
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
        private void AddEpicPlateforme()
        {
            Systeme sys = _databaseService.GetSystemeByName("epic");
            if (sys == null)
            {
                sys = new Systeme();
                sys.Name = "Epic Game Store";
                sys.Shortname = "epic";
                sys = _databaseService.AddSystem(sys);
            }
            var emu = _databaseService.GetEmulatorsForSysteme(sys.SystemeID).FirstOrDefault(x => x.Chemin.Contains("Windows\\explorer.exe"));
            if (emu == null)
            {
                emu = _databaseService.AddEmulator(_emulateurService.AddExplorer(sys));
            }
            var findedorigingames = _epicService.GetEpicGame(emu);
            var games = _databaseService.GetGames();
            findedorigingames = findedorigingames.Where(x => !games.Any(g => g.Path == x.Path)).ToList();
            var validategames = _dialogService.ShowSteamGamesFounded(findedorigingames);
            if (validategames != null)
            {
                foreach (var game in validategames)
                {
                    _databaseService.AddGame(_steamService.GetSteamInfos(game, emu));
                }
            }
            ReloadData();
        }
        private void AddOriginPlateforme()
        {
            Systeme sys = _databaseService.GetSystemeByName("origin");
            if (sys == null)
            {
                sys = new Systeme();
                sys.Name = "Origin";
                sys.Shortname = "origin";
                sys = _databaseService.AddSystem(sys);
            }
            var emu = _databaseService.GetEmulatorsForSysteme(sys.SystemeID).FirstOrDefault(x => x.Chemin.Contains("Windows\\explorer.exe"));
            if (emu == null)
            {
                emu = _databaseService.AddEmulator(_emulateurService.AddExplorer(sys));
            }
            var findedorigingames = _originService.GetOriginGame(emu);
            var games = _databaseService.GetGames();
            findedorigingames = findedorigingames.Where(x => !games.Any(g => g.Path == x.Path)).ToList();
            var validategames = _dialogService.ShowSteamGamesFounded(findedorigingames);
            if (validategames != null)
            {
                foreach (var game in validategames)
                {
                    _databaseService.AddGame(_steamService.GetSteamInfos(game, emu));
                }
            }
            ReloadData();
        }
        private void AddPlateforme(SCSPSystemeViewModel sysSCSP)
        {
            var sys = new Systeme();
            sys.Name = sysSCSP.Name;
            sys.Shortname = sysSCSP.ShortName;
            sys.SystemeSCSPID = sysSCSP.SCSPSysteme.id;
            sys = _databaseService.AddSystem(sys);
            GetSysMedia(sys, sysSCSP.SCSPSysteme);
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
        private void AddStandalone()
        {
            var sysjson = _dialogService.CreateStandalone();
            if (sysjson != null)
            {
                Systeme sys = new Systeme();
                sys.Name = sysjson[0];
                sys.Shortname = sysjson[1];
                sys.Type = SysType.Standalone;
                sys = _databaseService.AddSystem(sys);
                var emu = _databaseService.AddEmulator(_emulateurService.AddExplorer(sys));
                var game = new GameRom();
                game.Name = sysjson[0];
                game.Path = sysjson[2];
                game.EmulatorID = emu.EmulatorID;
                _databaseService.AddGame(game);
                _themeService.LoadDefaultBckForSysteme(sys);
                ReloadData();
                var currentthemefolder = _fileJSONService.GetCurrentTheme();
                var logopath = _dialogService.showImgPickerForPlateformeDialog(sys, currentthemefolder, ScraperType.Logo);
                if (logopath != null)
                {
                    _themeService.LoadLogoForSysteme(sys, currentthemefolder, logopath);
                }
            }
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
                var exts = obj.Emulator.Extension.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                List<String> romWithExt = new List<String>();
                foreach(var ext in exts)
                {
                    romWithExt.AddRange(gamefiles.Where(x => x.EndsWith(ext)));
                }
                foreach (var gamefile in romWithExt)
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
            var plateformesbcks = Systemes.Where(x => x.Emulators.SelectMany(g => g.Games).Count() > 0);
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
        private void SetBck()
        {
            var strimg = _dialogService.OpenUniqueFileDialog($"Fichier Image (*.png;*.jpg)|*.png;*.jpg");
            if (strimg != null)
            {
                _themeService.ChangeBCK(strimg);
                LoadThemeSettings();
            }
        }
        private void ShowDetailSystemeGame(SystemeViewModel sys)
        {
            var oldsyslogo = sys.Systeme.Logo;
            var oldsysScreen = sys.Systeme.Screenshoot;
            var oldsysVideo = sys.Systeme.Video;
            var sysmodified = _dialogService.ShowSystemeDetail(sys.Systeme);
            if(sysmodified != null)
            {
                var targetfolder = _themeService.GetImagePathForTheme(sysmodified.Shortname);
                if (sysmodified.Logo != oldsyslogo)
                {
                    var targetfile = $"{targetfolder}{Path.GetExtension(sysmodified.Logo)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                    var fstream = File.Create(targetfile);
                    fstream.Close();
                    fstream.Dispose();
                    _themeService.DownloadSteamData(sysmodified.Logo, targetfile);
                }
                if(sysmodified.Screenshoot != oldsysScreen)
                {
                    var targetfile = $"{targetfolder}{Path.GetExtension(sysmodified.Screenshoot)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                    var fstream = File.Create(targetfile);
                    fstream.Close();
                    fstream.Dispose();
                    _themeService.DownloadSteamData(sysmodified.Logo, targetfile);
                }
                if(sysmodified.Video != oldsysVideo)
                {
                    var targetfile = $"{targetfolder}{Path.GetExtension(sysmodified.Video)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                    var fstream = File.Create(targetfile);
                    fstream.Close();
                    fstream.Dispose();
                    _themeService.DownloadSteamData(sysmodified.Logo, targetfile);
                }
                _databaseService.SaveUpdate();
                ReloadData();
            }
        }
        private async void ShowDetailGameGame(GameViewModel obj)
        {
            var oldgamebox = obj.Game.Boxart;
            var oldgamefanart = obj.Game.Fanart;
            var oldgamescreen = obj.Game.Screenshoot;
            var oldgamelogo = obj.Game.Logo;
            var oldgamevideo = obj.Game.Video;
            var game = _dialogService.ShowGameDetail(obj.Game);
            if (game != null)
            {
                try
                {
                    if (game.Boxart != oldgamebox)
                    {
                        var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Boxart);
                        var targetfile = $"{targetfolder}{Path.GetExtension(game.Boxart)}";
                        Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                        var fstream = File.Create(targetfile);
                        fstream.Close();
                        fstream.Dispose();
                        _gameService.DownloadImgData(game.Boxart, targetfile);
                        game.Boxart = targetfile;
                    }
                }
                catch (Exception ex)
                {
                 //   throw;
                }
                try { 
                if (game.Fanart!= oldgamefanart)
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Banner);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Fanart)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                    var fstream = File.Create(targetfile);
                    fstream.Close();
                    fstream.Dispose();
                    _gameService.DownloadImgData(game.Fanart, targetfile);
                    game.Fanart = targetfile;
                    }
                }
                catch (Exception ex)
                {
                    //   throw;
                }
                try { 
                if (game.Screenshoot != oldgamescreen)
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.ArtWork);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Screenshoot)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                    var fstream = File.Create(targetfile);
                    fstream.Close();
                    fstream.Dispose();
                    _gameService.DownloadImgData(game.Screenshoot, targetfile);
                    game.Screenshoot = targetfile;
                    }
                }
                catch (Exception ex)
                {
                    //   throw;
                }
                try { 
                if (game.Logo != oldgamelogo)
                {
                    var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Logo);
                    var targetfile = $"{targetfolder}{Path.GetExtension(game.Logo)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                    var fstream = File.Create(targetfile);
                    fstream.Close();
                    fstream.Dispose();
                    _gameService.DownloadImgData(game.Logo, targetfile);
                    game.Logo = targetfile;
                    }
                }
                catch (Exception ex)
                {
                    //   throw;
                }
                try 
                {
                    if(game.Video != oldgamevideo)
                    {
                        if (game.Video.Contains("youtube"))
                        {
                            var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Video);
                            var targetfile = $"{targetfolder}{Path.GetExtension(".mp4")}";
                            var youTube = YouTube.Default;
                            var video = youTube.GetVideo(game.Video.Replace("embed/", "watch?v="));
                            File.WriteAllBytes(targetfile, video.GetBytes());
                            //await youtube.Videos.DownloadAsync(, targetfile);
                            game.Video = targetfile;
                        }
                        else if (File.Exists(game.Video))
                        {
                            var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Video);
                            var targetfile = $"{targetfolder}{Path.GetExtension(game.Video)}";
                            Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                            var fstream = File.Create(targetfile);
                            fstream.Close();
                            fstream.Dispose();
                            _gameService.DownloadImgData(game.Video, targetfile);
                            game.Video = targetfile;
                        }
                        else
                        {
                            var targetfolder = _gameService.GetImgPathForGame(game, ScraperType.Video);
                            var targetfile = $"{targetfolder}{Path.GetExtension(".mp4")}";
                            _gameService.DownloadImgData(game.Video, targetfile);
                            game.Video = targetfile; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    //   throw;
                }
                var duplicates = _databaseService.GetGames().Where(x => x.Path == game.Path);
                foreach(var duplicate in duplicates)
                {
                    duplicate.Video = game.Video;
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
                if(CurrentScraperType == ScraperType.Full)
                {
                    if(CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        ScrapeScreenScraperMetadata(games);
                        ScrapeArtwork(games, ScraperType.ArtWork, ScraperSource.Screenscraper);
                        ScrapeBanner(games, ScraperType.Banner, ScraperSource.Screenscraper);
                        ScrapeBoxart(games, ScraperType.Boxart, ScraperSource.Screenscraper);
                        ScrapeLogo(games, ScraperType.Logo, ScraperSource.Screenscraper);
                        ScrapeVideoScreenScraper(games);
                    }
                    else if(CurrentScrapeSource == ScraperSource.SGDB)
                    {
                        ScrapeArtwork(games, ScraperType.ArtWork, ScraperSource.SGDB);
                        ScrapeBanner(games, ScraperType.Banner, ScraperSource.SGDB);
                        ScrapeBoxart(games, ScraperType.Boxart, ScraperSource.SGDB);
                        ScrapeLogo(games, ScraperType.Logo, ScraperSource.SGDB);
                    }
                    else if(CurrentScrapeSource == ScraperSource.IGDB)
                    {
                        ScrapeArtwork(games, ScraperType.ArtWork, ScraperSource.IGDB);
                        ScrapeBoxart(games, ScraperType.Boxart, ScraperSource.IGDB);
                        ScrapeIGDBMetadata(games);
                    }

                }
               else if(CurrentScraperType == ScraperType.ArtWork)
                {
                    ScrapeArtwork(games, CurrentScraperType, CurrentScrapeSource);
                }
                else if(CurrentScraperType == ScraperType.Banner)
                {
                    ScrapeBanner(games, CurrentScraperType, CurrentScrapeSource);
                }
                else if(CurrentScraperType == ScraperType.Boxart)
                {
                    ScrapeBoxart(games, CurrentScraperType, CurrentScrapeSource);
                }
                else if(CurrentScraperType == ScraperType.Logo)
                {
                    ScrapeLogo(games, CurrentScraperType, CurrentScrapeSource);
                }
                else if(CurrentScraperType == ScraperType.Metadata)
                {
                   if(CurrentScrapeSource == ScraperSource.IGDB)
                    {
                        ScrapeIGDBMetadata(games);
                    }
                    if (CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        ScrapeScreenScraperMetadata(games);
                    }
                }
                else if(CurrentScraperType == ScraperType.Video)
                {
                   if(CurrentScrapeSource == ScraperSource.IGDB)
                    {
                        //ScrapeIGDBMetadata(games);
                    }
                    if (CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        ScrapeVideoScreenScraper(games);
                    }
                }
                _databaseService.SaveUpdate();
            }
            ReloadData();
        }

        private void ScrapeScreenScraperMetadata(GameRom games)
        {
            if (games.ScreenScraperID < 1)
            {
                var resultsearch = _dialogService.SearchSteamGridDBByName(games.Name, ScraperSource.Screenscraper);
                if (resultsearch != null)
                {
                    games.ScreenScraperID = resultsearch.id;
                    games.Name = resultsearch.name;
                }
            }
            if (games.ScreenScraperID > 0)
            {
                var SCSPdata = _screenScraperService.GetJeuxDetail(games.ScreenScraperID);
                /*games.Name*/ var name = SCSPdata.noms.FirstOrDefault(x => x.region == "eu" || x.region == "fr");
                if (name != null)
                    games.Name = name.text;
                else
                    games.Name = SCSPdata.noms.FirstOrDefault(x => x.region == "wor").text;
                /*games.Year*/
                if (SCSPdata.dates != null)
                {
                    var year = SCSPdata.dates.FirstOrDefault(x => x.region == "eu" || x.region == "fr");
                    if (year != null)
                        games.Year = year.text;
                    else
                        games.Year = SCSPdata.dates.FirstOrDefault(x => x.region == "wor").text; 
                }
                if (SCSPdata.editeur != null)
                {
                    games.Editeur = SCSPdata.editeur.text; 
                }
                if (SCSPdata.developpeur != null)
                {
                    games.Dev = SCSPdata.developpeur.text; 
                }
                if (SCSPdata.synopsis != null)
                {
                    games.Desc = SCSPdata.synopsis.FirstOrDefault(x => x.langue == "fr").text; 
                }
                if (SCSPdata.genres != null)
                {
                    var genresoffgame = SCSPdata.genres.FirstOrDefault();
                    if (genresoffgame.noms != null)
                    {
                        games.Genre = genresoffgame.noms.FirstOrDefault(x => x.langue == "fr").text;
                    }
                }
            }
        }
        private void ScrapeVideoScreenScraper(GameRom games)
        {
            if (games.ScreenScraperID < 1)
            {
                var resultsearch = _dialogService.SearchSteamGridDBByName(games.Name, ScraperSource.Screenscraper);
                if (resultsearch != null)
                {
                    games.ScreenScraperID = resultsearch.id;
                    games.Name = resultsearch.name;
                }
            }
            if (games.ScreenScraperID > 0)
            {
                var SCSPdata = _screenScraperService.GetJeuxDetail(games.ScreenScraperID);
                var SCSPVideo = SCSPdata.medias.FirstOrDefault(x => x.type == "video");
                if(SCSPVideo != null)
                {
                    var videoURL = SCSPVideo.url;
                    games.Video = videoURL;
                    var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Video);
                    var targetfile = $"{targetfolder}{Path.GetExtension(".mp4")}";
                    _gameService.DownloadImgData(games.Video, targetfile);
                    games.Video = targetfile;
                }
            }
        }
        private void ScrapeIGDBVideo(GameRom games)
        {

        }
        private void ScrapeIGDBMetadata(GameRom games)
        {
            if (games.IGDBID < 1)
            {
                var resultsearch = _dialogService.SearchSteamGridDBByName(games.Name, ScraperSource.IGDB);
                if (resultsearch != null)
                {
                    games.IGDBID = resultsearch.id;
                    games.Name = resultsearch.name;
                }
            }
            if (games.IGDBID > 0)
            {
                var igdbData = _iGDBService.GetDetailsGame(games.IGDBID);
                games.Name = igdbData.name;
                if (!string.IsNullOrEmpty(igdbData.storyline))
                    games.Desc = igdbData.storyline;
                else if (!string.IsNullOrEmpty(igdbData.summary))
                    games.Desc = igdbData.summary;

                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(igdbData.first_release_date).ToLocalTime();
                games.Year = dtDateTime.Year.ToString();

                var igdbgenres = _iGDBService.GetGenresByGameId(games.IGDBID);
                games.Genre = string.Join(", ", igdbgenres.Select(x => x.name));

                var involved = _iGDBService.GetInvolvedCompanyByGameId(games.IGDBID);
                var devs = _iGDBService.GetDevByGameId(involved);
                var publiser = _iGDBService.GetPublishersByGameId(involved);

                games.Dev = string.Join(", ", devs.Select(x => x.name));
                games.Editeur = string.Join(", ", publiser.Select(x => x.name));
            }
        }

        private void ScrapeLogo(GameRom games, ScraperType _currentScraperType, ScraperSource _currentScrapeSource)
        {
            var result = _dialogService.SearchImgInSteamGridDB(games, _currentScraperType, _currentScrapeSource);
            if (result != null)
            {
                games.Logo = result;
                var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Logo);
                var targetfile = $"{targetfolder}{Path.GetExtension(games.Logo)}";
                _gameService.DownloadImgData(games.Logo, targetfile);
                games.Logo = targetfile;
            }
        }

        private void ScrapeBoxart(GameRom games, ScraperType _currentScraperType, ScraperSource _currentScrapeSource)
        {
            var result = _dialogService.SearchImgInSteamGridDB(games, _currentScraperType, _currentScrapeSource);
            if (result != null)
            {
                games.Boxart = result;
                var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Boxart);
                var targetfile = $"{targetfolder}{Path.GetExtension(games.Boxart)}";
                _gameService.DownloadImgData(games.Boxart, targetfile);
                games.Boxart = targetfile;
            }
        }

        private void ScrapeBanner(GameRom games, ScraperType _currentScraperType, ScraperSource _currentScrapeSource)
        {
            var result = _dialogService.SearchImgInSteamGridDB(games, _currentScraperType, _currentScrapeSource);
            if (result != null)
            {
                games.Fanart = result;
                var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.Banner);
                var targetfile = $"{targetfolder}{Path.GetExtension(games.Fanart)}";
                _gameService.DownloadImgData(games.Fanart, targetfile);
                games.Fanart = targetfile;
            }
        }

        private void ScrapeArtwork(GameRom games, ScraperType _currentScraperType, ScraperSource _currentScrapeSource)
        {
            var result = _dialogService.SearchImgInSteamGridDB(games, _currentScraperType, _currentScrapeSource);
            if (result != null)
            {
                games.Screenshoot = result;
                var targetfolder = _gameService.GetImgPathForGame(games, ScraperType.ArtWork);
                var targetfile = $"{targetfolder}{Path.GetExtension(games.Screenshoot)}";
                _gameService.DownloadImgData(games.Screenshoot, targetfile);
                games.Screenshoot = targetfile;
            }
        }

        private void GetSystemeIMG()
        {
            //var sysSCSPList = await _screenScraperService.GetSSCPSystemes();
            var EmulSys = Systemes.Where(x => x.Systeme.Type != SysType.GameStore && x.Systeme.Type != SysType.Standalone && x.Systeme.Type != SysType.Collection);
            foreach ( var sys in EmulSys)
            {
                var sysSCSP = AllSystemes.FirstOrDefault(x => x.SCSPSysteme.id == sys.Systeme.SystemeSCSPID);
                if(sysSCSP != null)
                {
                    GetSysMedia(sys.Systeme, sysSCSP.SCSPSysteme);
                }
            }
            _dialogService.ShowMessageOk("Fin", "Fin de la récupération des Médias systèmes");
        }
        private void GetSysMedia(Systeme sys, Models.ScreenScraper.System.Systeme sysSCSP)
        {
            var logopath = _themeService.GetLogoPathForTheme(sys.Shortname);
            Directory.CreateDirectory(Path.GetDirectoryName(logopath));
            _themeService.DownloadSteamData(_screenScraperService.GetSystemeImgDLL("wheel", sysSCSP.id.ToString()), logopath);
            sys.Logo = logopath;
            var videopath = _themeService.GetVidéoPathForTheme(sys.Shortname);
            Directory.CreateDirectory(Path.GetDirectoryName(videopath));
            _themeService.DownloadSteamData(_screenScraperService.GetSystemeVideoDLL(sysSCSP.id.ToString()), videopath);
            sys.Video = videopath;
            var imgpath = _themeService.GetImagePathForTheme(sys.Shortname);
            Directory.CreateDirectory(Path.GetDirectoryName(imgpath));
            _themeService.DownloadSteamData(_screenScraperService.GetSystemeImgDLL("photo", sysSCSP.id.ToString()), imgpath);
            sys.Screenshoot = imgpath;
            _databaseService.SaveUpdate();
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
