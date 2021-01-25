using GalaSoft.MvvmLight;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace RetroFront.ViewModels
{
    public class FrontSysViewModel : ViewModelBase
    {
        private IDatabaseService _databaseService;
        private IThemeService _themeService;
        private IFileJSONService _fileJSONService;
        private ObservableCollection<SystemeViewModel> _systemes;
        public ObservableCollection<SystemeViewModel> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
        }
        public FrontSysViewModel(IDatabaseService databaseService, IFileJSONService fileJSONService, IThemeService themeService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _themeService = themeService;
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
    }
}
