using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models.SteamGridDB;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class SteamGridDBGameFinderViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ICommand _searchCommand;
        private List<DataSearch> _foundedgame;
        private DataSearch _resultgame;
        private string _name;
        private ISteamGridDBService steamGridDBService;
        public List<DataSearch> FoundedGame
        {
            get { return _foundedgame; }
            set { _foundedgame = value; RaisePropertyChanged(); }
        }
        public DataSearch Resultgame
        {
            get { return _resultgame; }
            set { _resultgame = value; RaisePropertyChanged(); }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged();}
        }
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand<object>(Search));
            }
        }
        public SteamGridDBGameFinderViewModel(string name)
        {
            steamGridDBService = App.ServiceProvider.GetRequiredService<ISteamGridDBService>();
            Name = name;
            SearchByNameResult();
        }
        private void Search(object obj)
        {
            SearchByNameResult();
        }
        private void SearchByNameResult()
        {
            FoundedGame = steamGridDBService.SearchByName(Name).ToList();
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
        {            ;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
