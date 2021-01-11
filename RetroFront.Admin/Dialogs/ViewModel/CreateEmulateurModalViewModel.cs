﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RetroFront.Models;
using RetroFront.Services.Implementation;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class CreateEmulateurModalViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        public string ResultJson { get; set; }
        private IDatabaseService _databaseService;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private string _chemin;
        public string Chemin
        {
            get { return _chemin; }
            set { _chemin = value; RaisePropertyChanged(); }
        }
        private string _command;
        public string Command
        {
            get { return _command; }
            set { _command = value; RaisePropertyChanged(); }
        }
        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set { _extension = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Systeme> _systemes;
        private Systeme _Selectedsysteme;
        public Systeme Selectedsysteme
        {
            get { return _Selectedsysteme; }
            set { _Selectedsysteme = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<Systeme> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
        }

        public CreateEmulateurModalViewModel()
        {
            _databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            var plateformes = _databaseService.GetSystemes();
            Systemes = new ObservableCollection<Systeme>(plateformes);
            
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
            ResultJson = GetJsonEmu();
            CloseDialogWithResult(parameter as Window, true);
        }

        private string GetJsonEmu()
        {
            Emulator emu = new Emulator();
            emu.Chemin = Chemin;
            emu.Command = Command;
            emu.Extension = Extension;
            emu.Name = Name;
            emu.SystemeID = Selectedsysteme.SystemeID;
            return JsonConvert.SerializeObject(emu);
        }
    }
}