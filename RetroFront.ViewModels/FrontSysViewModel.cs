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
using System.Windows.Input;

namespace RetroFront.ViewModels
{
    public class FrontSysViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private IThemeService _themeService;
        private IFileJSONService _fileJSONService;
        private SysDisplay _sysDisplay;
        public SysDisplay SysDisplay
        {
            get { return _sysDisplay; }
            set { _sysDisplay = value; RaisePropertyChanged(); }
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

        private ICommand _goDownCommand;
        private ICommand _goUpCommand;
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
        public FrontSysViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IThemeService themeService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _themeService = themeService;
            SysDisplay = SysDisplay.BigLogo;
            SelectedIndex = 0;
            ReloadData();
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
            Systemes = new ObservableCollection<SystemeViewModel>(Systemes.OrderBy(x => x.Systeme.Type).ThenBy(x => x.Name));
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
