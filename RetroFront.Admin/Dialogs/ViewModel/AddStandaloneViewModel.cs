using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class AddStandaloneViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private IDialogService _dialogService;
        public string[] ResultJson { get; set; }
        private ICommand _lookForExeCommand;
        public ICommand LookForExeCommand
        {
            get
            {
                return _lookForExeCommand ?? (_lookForExeCommand = new RelayCommand(LookForExe));
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private string _shortname;
        public string Shortname
        {
            get { return _shortname; }
            set { _shortname = value; RaisePropertyChanged(); }
        }
        private string _ExePath;
        public string ExePath
        {
            get { return _ExePath; }
            set { _ExePath = value; RaisePropertyChanged(); }
        }
        public AddStandaloneViewModel()
        {
            _dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
        }
        private void LookForExe()
        {
            var result = _dialogService.OpenUniqueFileDialog($"Fichier Executable (*.exe)|*.exe");
            if(result != null)
                ExePath = result;
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
            ResultJson = GetJsonSys();
            CloseDialogWithResult(parameter as Window, true);
        }
        private string[] GetJsonSys()
        {
            return new string[] { Name, Shortname, ExePath };
        }
    }
}
