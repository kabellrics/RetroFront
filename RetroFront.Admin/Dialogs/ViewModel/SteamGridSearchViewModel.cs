using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Models.SteamGridDB;
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
    public class SteamGridSearchViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ISteamGridDBService steamGridDBService;
        private IDialogService dialogService;
        public string ResultPath { get; set; }
        private string _imgPath;
        public string ImgPath
        {
            get { return _imgPath; }
            set { _imgPath = value; RaisePropertyChanged(); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<String> _resultImgs;
        public ObservableCollection<String> ResultImgs
        {
            get { return _resultImgs; }
            set { _resultImgs = value;RaisePropertyChanged(); }
        }
        public SteamGridSearchViewModel(GameRom game, string type, string target = null)
        {
            ImgPath = target;
            steamGridDBService = App.ServiceProvider.GetRequiredService<ISteamGridDBService>();
            dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            ResultImgs = new ObservableCollection<string>();
            DataSearch steamgridgame = new DataSearch(); ;
            if (game.SteamID != -1)
            {
                steamgridgame = steamGridDBService.GetGameSteamId(game.SteamID).FirstOrDefault();
            }else
            {
                steamgridgame = dialogService.SearchSteamGridDBByName(game.Name);
            }
            if(type == "Logo")
            {
                var result = steamGridDBService.GetLogoForId(steamgridgame.id);
                foreach(var img in result)
                {
                    ResultImgs.Add(img.url);
                }
            }
            else if(type == "heroes")
            {
                var result = steamGridDBService.GetHeroesForId(steamgridgame.id);
                foreach (var img in result)
                {
                    ResultImgs.Add(img.url);
                }
            }
            else if(type == "grid")
            {
                var result = steamGridDBService.GetGridsForId(steamgridgame.id);
                foreach (var img in result)
                {
                    ResultImgs.Add(img.url);
                }
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
        {
            ResultPath = ImgPath;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
