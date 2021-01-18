﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private IFileJSONService _FileJson;
        private AppSettings settings;
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
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
             settings.CurrentTheme = CurrentTheme;
             settings.ScreenScraperID = ScreenScraperID;
             settings.ScreenScraperPWD = ScreenScraperPWD;
             settings.AppSettingsLocation = AppSettingsLocation;
             settings.AppSettingsFolder = AppSettingsFolder;
             settings.RetroarchCMD = RetroarchCMD;
             settings.RetroarchPath = RetroarchPath;
            _FileJson.UpdateSettings(settings);
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
