using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private IFileJSONService _FileJson;
        private IThemeService themeService;
        private AppSettings settings;
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ThemeViewModel _CurrentTheme;
        public ThemeViewModel CurrentTheme
        {
            get { return _CurrentTheme; }
            set { _CurrentTheme = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<ThemeViewModel> _themes;
        public ObservableCollection<ThemeViewModel> Themes
        {
            get { return _themes; }
            set { _themes = value; RaisePropertyChanged(); }
        }
        private string _ScreenScraperID;
        public string ScreenScraperID
        {
            get { return _ScreenScraperID; }
            set { _ScreenScraperID = value; RaisePropertyChanged(); }
        }
        private string _ScreenScraperPWD;
        public string ScreenScraperPWD
        {
            get { return _ScreenScraperPWD; }
            set { _ScreenScraperPWD = value; RaisePropertyChanged(); }
        }
        private string _AppSettingsLocation;
        public string AppSettingsLocation
        {
            get { return _AppSettingsLocation; }
            set { _AppSettingsLocation = value; RaisePropertyChanged(); }
        }
        private string _AppSettingsFolder;
        public string AppSettingsFolder
        {
            get { return _AppSettingsFolder; }
            set { _AppSettingsFolder = value; RaisePropertyChanged(); }
        }
        private string _RetroarchPath;
        public string RetroarchPath
        {
            get { return _RetroarchPath; }
            set { _RetroarchPath = value; RaisePropertyChanged(); }
        }
        private string _RetroarchCMD;
        public string RetroarchCMD
        {
            get { return _RetroarchCMD; }
            set { _RetroarchCMD = value; RaisePropertyChanged(); }
        }
        private string _SGDBKey;
        public string SGDBKey
        {
            get { return _SGDBKey; }
            set { _SGDBKey = value; RaisePropertyChanged(); }
        }
        private SysDisplay _sysDisplay;
        public SysDisplay SysDisplay
        {
            get { return _sysDisplay; }
            set { _sysDisplay = value;RaisePropertyChanged(); }
        }
    
        private List<SysDisplay> _sysDisplayList;
        public List<SysDisplay> SysDisplayList
        {
            get { return _sysDisplayList; }
            set { _sysDisplayList = value;RaisePropertyChanged(); }
        }
        private RomDisplay _romDisplay;
        public RomDisplay RomDisplay
        {
            get { return _romDisplay; }
            set { _romDisplay = value; RaisePropertyChanged(); }
        }
        private List<RomDisplay> _romDisplayList;
        public List<RomDisplay> RomDisplayList
        {
            get { return _romDisplayList; }
            set { _romDisplayList = value; RaisePropertyChanged(); }
        }
        public SettingsViewModel()
        {
            _FileJson = App.ServiceProvider.GetRequiredService<IFileJSONService>();
            themeService = App.ServiceProvider.GetRequiredService<IThemeService>();
            Getthemes();
            settings = _FileJson.appSettings;
            ScreenScraperID = settings?.ScreenScraperID;
            ScreenScraperPWD = settings?.ScreenScraperPWD;
            AppSettingsLocation = settings.AppSettingsLocation;
            AppSettingsFolder = settings.AppSettingsFolder;
            RetroarchCMD = settings.RetroarchCMD;
            RetroarchPath = settings?.RetroarchPath;
            SGDBKey = settings?.SGDBKey;
            SysDisplay = settings.CurrentSysDisplay;
            RomDisplay = settings.CurrentGameDisplay;
            this.SysDisplayList = new List<SysDisplay>
            {
                    SysDisplay.BigLogo,
                    SysDisplay.LogoBanner,
                    SysDisplay.CarrouselLogo,
                    SysDisplay.WheelLeftLogo,
                    SysDisplay.WheelRightLogo
            };
            this.RomDisplayList = new List<RomDisplay>
            {
                    RomDisplay.WallBox,
                    RomDisplay.WallBanner,
                    RomDisplay.ListLogo,
                    RomDisplay.ListBanner,
                    RomDisplay.Screenshot,
                    RomDisplay.Fanart
            };
        }

        private void Getthemes()
        {
            Themes = new ObservableCollection<ThemeViewModel>();
            var ths = themeService.GetInstalledTheme();
            foreach (var th in ths)
            {
                //if (th.FolderName != CurrentTheme.Folder)
                Themes.Add(new ThemeViewModel(th));
            }
            var currentthemefolder = _FileJson.GetCurrentTheme();
            CurrentTheme = Themes.FirstOrDefault(x => x.Folder == currentthemefolder);
        }

        public void CloseDialogWithResult(Window dialog, bool result)
        {
            if (dialog != null)
                dialog.DialogResult = result;
        }
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(CancelClick));
            }
        }
        private void CancelClick(object parameter)
        {
            CloseDialogWithResult(parameter as Window, false);
        }
        public ICommand YesCommand
        {
            get
            {
                return _yesCommand ?? (_yesCommand = new RelayCommand<object>(ValidateClick));
            }
        }
        private void ValidateClick(object parameter)
        {
            settings.CurrentTheme = CurrentTheme.Folder;
            settings.ScreenScraperID = ScreenScraperID;
            settings.ScreenScraperPWD = ScreenScraperPWD;
            settings.AppSettingsLocation = AppSettingsLocation;
            settings.AppSettingsFolder = AppSettingsFolder;
            settings.RetroarchCMD = RetroarchCMD;
            settings.RetroarchPath = RetroarchPath;
            settings.SGDBKey = SGDBKey;
            settings.CurrentSysDisplay= SysDisplay;
            settings.CurrentGameDisplay= RomDisplay;
            _FileJson.UpdateSettings(settings);
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
