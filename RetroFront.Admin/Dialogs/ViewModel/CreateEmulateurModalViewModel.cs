using GalaSoft.MvvmLight;
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
using System.Linq;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class CreateEmulateurModalViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ICommand _lookForExeCommand;
        public ICommand LookForExeCommand
        {
            get
            {
                return _lookForExeCommand ?? (_lookForExeCommand = new RelayCommand(LookForExe));
            }
        }
        public string ResultJson { get; set; }
        private Emulator _result;
        public Emulator Result
        {
            get { return _result; }
            set { _result = value; RaisePropertyChanged(); }
        }
        private IDatabaseService _databaseService;
        private IDialogService _dialogService;
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

        private ObservableCollection<SystemeDetailViewModel> _systemes;
        private SystemeDetailViewModel _Selectedsysteme;
        public SystemeDetailViewModel Selectedsysteme
        {
            get { return _Selectedsysteme; }
            set { _Selectedsysteme = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<SystemeDetailViewModel> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
        }

        public CreateEmulateurModalViewModel()
        {
            _databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            _dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            var plateformes = _databaseService.GetSystemes();
            Systemes = new ObservableCollection<SystemeDetailViewModel>();
            foreach(var sys in plateformes.OrderBy(x => x.Name))
            {
                Systemes.Add(new SystemeDetailViewModel(sys));
            }
        } 
        public CreateEmulateurModalViewModel(Emulator emu)
        {
            _databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            var plateformes = _databaseService.GetSystemes();
            Systemes = new ObservableCollection<SystemeDetailViewModel>();
            foreach (var sys in plateformes.OrderBy(x => x.Name))
            {
                Systemes.Add(new SystemeDetailViewModel(sys));
            }
            Result = emu;
            Name = emu.Name;
            Chemin = emu.Chemin;
            Command = emu.Command;
            Extension = emu.Extension;
            Selectedsysteme = new SystemeDetailViewModel(_databaseService.GetSysteme(emu.SystemeID));
        }

        private void LookForExe()
        {
            var result = _dialogService.OpenUniqueFileDialog($"Fichier Executable (*.exe)|*.exe");
            if (result != null)
                Chemin = result;
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
            GetJsonEmu();
            CloseDialogWithResult(parameter as Window, true);
        }

        private void GetJsonEmu()
        {
            Result.Chemin = Chemin;
            Result.Command = Command;
            Result.Extension = Extension;
            Result.Name = $"{Selectedsysteme.Name} - {Name}";
            Result.SystemeID = Selectedsysteme.Sys.SystemeID;
        }
    }
}
