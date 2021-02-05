using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models.SteamGridDB;
using RetroFront.Models;
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
        private List<Search> _foundedgame;
        private Search _resultgame;
        private string _name;
        private ISteamGridDBService steamGridDBService;
        private IIGDBService iGDBService;
        private ScraperSource _source;
        public ScraperSource Source
        {
            get { return _source; }
            set { _source = value;RaisePropertyChanged(); }
        }

        public List<Search> FoundedGame
        {
            get { return _foundedgame; }
            set { _foundedgame = value; RaisePropertyChanged(); }
        }
        public Search Resultgame
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
        public SteamGridDBGameFinderViewModel(string name,ScraperSource scraperSource)
        {
            steamGridDBService = App.ServiceProvider.GetRequiredService<ISteamGridDBService>();
            iGDBService = App.ServiceProvider.GetRequiredService<IIGDBService>();
            Source = scraperSource;
            Name = name;
            SearchByNameResult();
        }
        private void Search(object obj)
        {
            SearchByNameResult();
        }
        private void SearchByNameResult()
        {
            if (Source == ScraperSource.SGDB)
            {
                FoundedGame = steamGridDBService.SearchByName(Name).ToList();
            }
            else if(Source == ScraperSource.IGDB)
            {
                FoundedGame = iGDBService.GetGameByName(Name).ToList();
            }
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
