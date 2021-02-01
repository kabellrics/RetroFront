using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            get => parameter;
            set => Set(ref parameter, value);
        }
        private bool _isDialogDisplayOpen;
        public bool IsDialogDisplayOpen
        {
            get { return _isDialogDisplayOpen; }
            set { _isDialogDisplayOpen = value; RaisePropertyChanged(); }
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
            set { _selectedIndex = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { _games = value; RaisePropertyChanged(); }
        }

        private ICommand _goDownCommand;
        private ICommand _goUpCommand;
        private ICommand _goBackCommand;
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
        public FrontGameViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IThemeService themeService, IEnumService enumService, INavigationService navigationService)
        {
            IsDialogDisplayOpen = false;
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _themeService = themeService;
            _enumService = enumService;
            _navigationService = navigationService;
            SelectedIndex = 0;
            FrontDisplay = _fileJSONService.appSettings.CurrentGameDisplay;
            this.FrontDisplayList = _enumService.GetRomDisplays();
            CurrentSysteme = (Systeme)_navigationService.Parameter;
            LoadData();
        }
        public void LoadData() 
        {
            Games = new ObservableCollection<GameViewModel>();
            foreach(var game in _databaseService.GetGamesForPlateforme(CurrentSysteme.SystemeID).OrderBy(x=>x.Name))
            {
                Games.Add(new GameViewModel(game));
            }
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
            }
        }
        private void GoDown()
        {
            try
            {
                if (SelectedIndex > 0)
                    SelectedIndex--;
            }
            catch (Exception ex) { }
        }
        private void GoUp()
        {
            try
            {
                if (SelectedIndex < Games.Count)
                    SelectedIndex++;
            }
            catch (Exception ex) { }
        }

        //public Task ActivateAsync(object parameter)
        //{
        //    CurrentSysteme = parameter as Systeme;
        //    LoadData();
        //    return Task.CompletedTask;
        //}
    }
}
