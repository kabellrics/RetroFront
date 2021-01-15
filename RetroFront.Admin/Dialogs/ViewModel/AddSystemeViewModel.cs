using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class AddSystemeViewModel:ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        public string ResultJson { get; set; }
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
        private string GetJsonSys()
        {
            Systeme sys = new Systeme();
            sys.Name = Name;
            sys.Shortname = Shortname;
            return JsonConvert.SerializeObject(sys);
        }
    }
}
