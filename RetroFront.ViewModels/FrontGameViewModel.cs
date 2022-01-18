using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroFront.ViewModels
{
    public class FrontGameViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private IThemeService _themeService;
        private IFileJSONService _fileJSONService;
        private IEnumService _enumService;
        private INavigationService _navigationService;

        private Systeme parameter;
        public Systeme CurrentSysteme
        {
            get { return parameter; }
            set { parameter = value; RaisePropertyChanged(); }
        }
        private bool _isDialogDisplayOpen;
        public bool IsDialogDisplayOpen
        {
            get { return _isDialogDisplayOpen; }
            set { _isDialogDisplayOpen = value; RaisePropertyChanged(); }
        }
        private bool _AnimeDetail;
        public bool AnimeDetail
        {
            get { return _AnimeDetail; }
            set { _AnimeDetail = value; RaisePropertyChanged(); }
        }
        private bool _ShowDetail;
        public bool ShowDetail
        {
            get { return _ShowDetail; }
            set { _ShowDetail = value; RaisePropertyChanged(); }
        }
        private RomDisplay _romtmpDisplay;
        public RomDisplay FronttmpDisplay
        {
            get { return _romtmpDisplay; }
            set { _romtmpDisplay = value; RaisePropertyChanged(); }
        }
        private RomDisplay _romDisplay;
        public RomDisplay FrontDisplay
        {
            get { return _romDisplay; }
            set { _romDisplay = value; RaisePropertyChanged(); }
        }

        private List<RomDisplay> _frontDisplayList;
        public List<RomDisplay> FrontDisplayList
        {
            get { return _frontDisplayList; }
            set { _frontDisplayList = value; RaisePropertyChanged(); }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; RaisePropertyChanged(); SetCurrentGameVM(); }
        }
        private GameViewModel _currentgameVM;
        public GameViewModel CurerntGameVM
        {
            get { return _currentgameVM; }
            set { _currentgameVM = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { _games = value; RaisePropertyChanged(); }
        }
        private string _bCKImg;
        public string BCKImg
        {
            get { return _bCKImg; }
            set { _bCKImg = value; RaisePropertyChanged(); }
        }

        private ICommand _goDownCommand;
        private ICommand _goUpCommand;
        private ICommand _goBackCommand;
        private ICommand _openDisplayCommand;
        private ICommand _CloseOrGoCommand;
        private RelayCommand _loadedCommand;
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
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new RelayCommand(GoBack));
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
        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new RelayCommand(Loaded));
                    //() =>
                    //{
                    //    //_navigationService.NavigateTo("Splash", "MainFrame");
                    //    _navigationService.NavigateTo("Systeme", null, string.Empty);
                    //}));
            }
        }
        public FrontGameViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IThemeService themeService, IEnumService enumService, INavigationService navigationService)
        {
            IsDialogDisplayOpen = false;
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _themeService = themeService;
            _enumService = enumService;
            _navigationService = navigationService;
            CurerntGameVM = null;
            AnimeDetail = false;
            SelectedIndex = 0;
            FrontDisplay = _fileJSONService.appSettings.CurrentGameDisplay;
            this.FrontDisplayList = _enumService.GetRomDisplays();
            CurrentSysteme = (Systeme)_navigationService.Parameter;
            LoadData();
            if (File.Exists(_fileJSONService.appSettings.DefaultBCK))
            {
                BCKImg = _fileJSONService.appSettings.DefaultBCK;
            }
        }

        public void LoadData() 
        {
            Games = new ObservableCollection<GameViewModel>();
            if (CurrentSysteme.Shortname == "all")
            {
                foreach (var game in _databaseService.GetGames().OrderBy(x => x.Name))
                {
                    Games.Add(new GameViewModel(game));
                }
            }
            else if (CurrentSysteme.Shortname == "fav")
            {
                foreach (var game in _databaseService.GetGames().Where(x=>x.IsFavorite == true).OrderBy(x => x.Name))
                {
                    Games.Add(new GameViewModel(game));
                }
            }
            else
            {
                foreach (var game in _databaseService.GetGamesForPlateforme(CurrentSysteme.SystemeID).OrderBy(x => x.Name))
                {
                    Games.Add(new GameViewModel(game));
                }
            }
        }
        private void Loaded()
        {
            CurrentSysteme = (Systeme)_navigationService.Parameter;
            LoadData();
        }
        private void GoBack()
        {
            _navigationService.NavigateTo("Systeme",null, string.Empty);
        }
        private void OpenDisplay()
        {
            IsDialogDisplayOpen = true;
        }
        private void CloseOrGo()
        {
            if (IsDialogDisplayOpen == true)
            {
                IsDialogDisplayOpen = false;
                FrontDisplay = FronttmpDisplay;
                FronttmpDisplay = FrontDisplay;
            }
            else
                _navigationService.NavigateTo("Systeme", null, string.Empty);
        }
        private void GoDown()
        {
            try
            {
                if (SelectedIndex > 0)
                {
                    //ShowDetail = false;
                    SelectedIndex--;
                }
            }
            catch (Exception ex) { }
        }
        private void GoUp()
        {
            try
            {
                if (SelectedIndex < Games.Count)
                {
                    //ShowDetail = false;
                    SelectedIndex++; 
                }
            }
            catch (Exception ex) { }
        }
        private void SetCurrentGameVM()
        {
            try
            {
                if (Games != null)
                {
                    AnimeDetail = true;
                    ShowDetail = true;
                    CurerntGameVM = Games[SelectedIndex];
                    AnimeDetail = false;
                }
            }
            catch(Exception ex) { }
        }
        //public Task ActivateAsync(object parameter)
        //{
        //    CurrentSysteme = parameter as Systeme;
        //    LoadData();
        //    return Task.CompletedTask;
        //}
    }
}
