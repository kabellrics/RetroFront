using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MDIXDialogHost = MaterialDesignThemes.Wpf.DialogHost;

namespace RetroFront.ViewModels
{
    public class FrontSysViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private IThemeService _themeService;
        private IFileJSONService _fileJSONService;
        private IEnumService _enumService;
        private INavigationService _navigationService;
        private bool _isDialogDisplayOpen;
        public bool IsDialogDisplayOpen
        {
            get { return _isDialogDisplayOpen; }
            set { _isDialogDisplayOpen = value;RaisePropertyChanged(); }
        }
        private SysDisplay _sysDisplay;
        public SysDisplay SysDisplay
        {
            get { return _sysDisplay; }
            set { _sysDisplay = value; RaisePropertyChanged(); }
        }
        private SysDisplay _systmpDisplay;
        public SysDisplay SystmpDisplay
        {
            get { return _systmpDisplay; }
            set { _systmpDisplay = value; RaisePropertyChanged(); }
        }

        private List<SysDisplay> _sysDisplayList;
        public List<SysDisplay> SysDisplayList
        {
            get { return _sysDisplayList; }
            set { _sysDisplayList = value; RaisePropertyChanged(); }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SystemeViewModel> _systemes;
        public ObservableCollection<SystemeViewModel> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
        }
        private string _bCKImg;
        public string BCKImg
        {
            get { return _bCKImg; }
            set { _bCKImg = value; RaisePropertyChanged(); }
        }
        private ICommand _goDownCommand;
        private ICommand _goUpCommand;
        private ICommand _openDisplayCommand;
        private ICommand _CloseOrGoCommand;
        public ICommand GoDownCommand
        {
            get
            {
                return _goDownCommand ?? (_goDownCommand = new RelayCommand(GoDown));
            }
        }
        public ICommand GoUpCommand
        {
            get
            {
                return _goUpCommand ?? (_goUpCommand = new RelayCommand(GoUp));
            }
        }
        public ICommand OpenDisplayCommand
        {
            get
            {
                return _openDisplayCommand ?? (_openDisplayCommand = new RelayCommand(OpenDisplay));
            }
        }
        public ICommand CloseOrGoCommand
        {
            get
            {
                return _CloseOrGoCommand ?? (_CloseOrGoCommand = new RelayCommand(CloseOrGo));
            }
        }

        public FrontSysViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IThemeService themeService,IEnumService enumService, INavigationService navigationService)
        {
            IsDialogDisplayOpen = false;
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _themeService = themeService;
            _enumService = enumService;
            _navigationService = navigationService;
            SysDisplay = _fileJSONService.appSettings.CurrentSysDisplay;
            SelectedIndex = 0;
            ReloadData();
            this.SysDisplayList = _enumService.GetSysDisplays();
            if (File.Exists(_fileJSONService.appSettings.DefaultBCK))
            {
                BCKImg = _fileJSONService.appSettings.DefaultBCK;
            }
        }

        private void ReloadData()
        {
            Systemes = new ObservableCollection<SystemeViewModel>();
            foreach (var sys in _databaseService.GetSystemes().OrderBy(x => x.Name))
            {
                var sysvm = new SystemeViewModel(sys);
                sysvm.Bck = _themeService.GetBckForTheme(sys.Shortname, _fileJSONService.GetCurrentTheme());
                var logopath = _themeService.GetLogoForTheme(sys.Shortname);
                if (File.Exists(logopath))
                {
                    sysvm.Logo = logopath;
                    sysvm.HasLogo = true;
                }
                sysvm.NBEmu = $"{_databaseService.GetNbEmulatorForSysteme(sys.SystemeID)} Emulateurs";
                sysvm.NBGame = $"{_databaseService.GetNbGamesForPlateforme(sys.SystemeID)} Jeux";
                Systemes.Add(sysvm);
            }
            Systemes = new ObservableCollection<SystemeViewModel>(Systemes.OrderBy(x => x.Name));
            AddLastPlayed();
            AddMostPlayed();
            AddFavoriteGame();
            AddAllGames();
            //Systemes = new ObservableCollection<SystemeViewModel>(Systemes.OrderBy(x => x.Systeme.Type).ThenBy(x => x.Name));
        }
        private void AddFavoriteGame()
        {
            if (_fileJSONService.appSettings.ShowFav == true)
            {
                var newsys = new Systeme();
                newsys.Name = "Jeux Favoris";
                newsys.Shortname = "fav";
                newsys.Type = SysType.Collection;
                SystemeViewModel sysvm = new SystemeViewModel(newsys);
                sysvm.Bck = _themeService.GetBckForTheme(newsys.Shortname, _fileJSONService.GetCurrentTheme());
                var logopath = _themeService.GetLogoForTheme(newsys.Shortname);
                if (File.Exists(logopath))
                {
                    sysvm.Logo = logopath;
                    sysvm.HasLogo = true;
                }
                Systemes.Insert(0, sysvm); 
            }
        }
        private void AddAllGames()
        {
            if (_fileJSONService.appSettings.ShowAll == true)
            {
                var newsys = new Systeme();
                newsys.Name = "Tous les Jeux";
                newsys.Shortname = "all";
                newsys.Type = SysType.Collection;
                SystemeViewModel sysvm = new SystemeViewModel(newsys);
                sysvm.Bck = _themeService.GetBckForTheme(newsys.Shortname, _fileJSONService.GetCurrentTheme());
                var logopath = _themeService.GetLogoForTheme(newsys.Shortname);
                if (File.Exists(logopath))
                {
                    sysvm.Logo = logopath;
                    sysvm.HasLogo = true;
                }
                Systemes.Insert(0, sysvm); 
            }
        }
        private void AddMostPlayed()
        {
            if (_fileJSONService.appSettings.ShowMostPlayed == true)
            {
                var newsys = new Systeme();
                newsys.Name = "Jeux les plus joués";
                newsys.Shortname = "most";
                newsys.Type = SysType.Collection;
                SystemeViewModel sysvm = new SystemeViewModel(newsys);
                sysvm.Bck = _themeService.GetBckForTheme(newsys.Shortname, _fileJSONService.GetCurrentTheme());
                var logopath = _themeService.GetLogoForTheme(newsys.Shortname);
                if (File.Exists(logopath))
                {
                    sysvm.Logo = logopath;
                    sysvm.HasLogo = true;
                }
                Systemes.Insert(0, sysvm); 
            }
        }
        private void AddLastPlayed()
        {
            if (_fileJSONService.appSettings.ShowLastPlayed == true)
            {
                var newsys = new Systeme();
                newsys.Name = "Derniers jeux joués";
                newsys.Shortname = "last";
                newsys.Type = SysType.Collection;
                SystemeViewModel sysvm = new SystemeViewModel(newsys);
                sysvm.Bck = _themeService.GetBckForTheme(newsys.Shortname, _fileJSONService.GetCurrentTheme());
                var logopath = _themeService.GetLogoForTheme(newsys.Shortname);
                if (File.Exists(logopath))
                {
                    sysvm.Logo = logopath;
                    sysvm.HasLogo = true;
                }
                Systemes.Insert(0, sysvm); 
            }
        }
        private void OpenDisplay()
        {
            IsDialogDisplayOpen = true;
        }
        private void CloseOrGo()
        {
            if(IsDialogDisplayOpen == true)
            {
                IsDialogDisplayOpen = false;
                SysDisplay = SystmpDisplay;
                SystmpDisplay = SysDisplay;
            }
            else if (IsDialogDisplayOpen == false)
            {
                try
                {
                    var currentSys = Systemes[SelectedIndex];
                    _navigationService.NavigateTo("Games", currentSys.Systeme, string.Empty);
                }
                catch (Exception ex)
                {
                 //   throw;
                }
            }
        }
        private void GoDown()
        {
            try
            { 
                if(SelectedIndex >0)
                    SelectedIndex--; 
            }
            catch(Exception ex) { }
        }
        private void GoUp()
        {
            try 
            {
                if(SelectedIndex< Systemes.Count)
                SelectedIndex++;
            }
            catch (Exception ex) { }
        }
    }
}
