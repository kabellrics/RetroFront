using GalaSoft.MvvmLight;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private IFileJSONService _FileJson;
        private AppSettings settings;

        private string _CurrentTheme;
        public string CurrentTheme
        {
            get { return _CurrentTheme; }
            set { _CurrentTheme = value; RaisePropertyChanged(); } 
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

        public SettingsViewModel()
        {
            _FileJson = App.ServiceProvider.GetRequiredService<IFileJSONService>();
            settings = _FileJson.appSettings;
            CurrentTheme = settings.CurrentTheme;
            ScreenScraperID = settings.ScreenScraperID;
            ScreenScraperPWD = settings.ScreenScraperPWD;
            AppSettingsLocation = settings.AppSettingsLocation;
            AppSettingsFolder = settings.AppSettingsFolder;
            RetroarchCMD = settings.RetroarchCMD;
            RetroarchPath = settings.RetroarchPath;
        }
    }
}
