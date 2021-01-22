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
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }
        private String _ImgPath;
        public String ImgPath
        {
            get { return _ImgPath; }
            set { _ImgPath = value; RaisePropertyChanged(); }
        }
        private int _selectedImgIndex;
        public int SelectedImgIndex
        {
            get { return _selectedImgIndex; }
            set { _selectedImgIndex = value; RaisePropertyChanged(); }
        }
        private int _nbImg;
        public int NBImg
        {
            get { return _nbImg; }
            set { _nbImg = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<String> _resultImgs;
        public ObservableCollection<String> ResultImgs
        {
            get { return _resultImgs; }
            set { _resultImgs = value; RaisePropertyChanged(); }
        }
        public SteamGridSearchViewModel(GameRom game, SGDBType type)
        {
            steamGridDBService = App.ServiceProvider.GetRequiredService<ISteamGridDBService>();
            dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            Title = $"Recherche de {type.ToString()} pour {game.Name}";
            ResultImgs = new ObservableCollection<string>();
            DataSearch steamgridgame = new DataSearch(); ;
            if (game.SteamID != -1)
            {
                steamgridgame = steamGridDBService.GetGameSteamId(game.SteamID.ToString());
            }
            else
            {
                steamgridgame = dialogService.SearchSteamGridDBByName(game.Name);
            }
            if (type == SGDBType.logo)
            {
                var result = steamGridDBService.GetLogoForId(steamgridgame.id);
                if (result != null)
                {
                    foreach (var img in result)
                    {
                        ResultImgs.Add(img.url);
                    }
                }
            }
            else if (type == SGDBType.background)
            {
                var result = steamGridDBService.GetHeroesForId(steamgridgame.id); if (result != null)
                {
                    foreach (var img in result)
                    {
                        ResultImgs.Add(img.url);
                    }
                }
            }
            else if (type == SGDBType.fanart)
            {
                var result = steamGridDBService.GetGridFanartForId(steamgridgame.id); if (result != null)
                {
                    foreach (var img in result)
                    {
                        ResultImgs.Add(img.url);
                    }
                }
            }
            else if (type == SGDBType.boxart)
            {
                var result = steamGridDBService.GetGridBoxartForId(steamgridgame.id); if (result != null)
                {
                    foreach (var img in result)
                    {
                        ResultImgs.Add(img.url);
                    }
                }
            }
            NBImg = ResultImgs.Count;
            SelectedImgIndex = 0;
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
