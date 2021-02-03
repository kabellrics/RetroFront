using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.IO;
using System.Windows;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class GameDetailViewModel : ViewModelBase
    {
        private IGameService _gameService;
        private IDialogService dialogService;
        private IDatabaseService databaseService;
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        public GameRom GameCurrent { get; set; }
        private ICommand _ScrapeGameCommand;
        public ICommand ScrapeGameCommand
        {
            get
            {
                return _ScrapeGameCommand ?? (_ScrapeGameCommand = new RelayCommand(ScrapeGame));
            }
        }
        private ICommand _SGDBBoxFinderCommand;
        public ICommand SGDBBoxFinderCommand
        {
            get
            {
                return _SGDBBoxFinderCommand ?? (_SGDBBoxFinderCommand = new RelayCommand(SGDBBoxFinder));
            }
        }
        private ICommand _SGDBLogoFinderCommand;
        public ICommand SGDBLogoFinderCommand
        {
            get
            {
                return _SGDBLogoFinderCommand ?? (_SGDBLogoFinderCommand = new RelayCommand(SGDBLogoFinder));
            }
        }
        private ICommand _SGDBScreenFinderCommand;
        public ICommand SGDBScreenFinderCommand
        {
            get
            {
                return _SGDBScreenFinderCommand ?? (_SGDBScreenFinderCommand = new RelayCommand(SGDBScreenFinder));
            }
        }
        private ICommand _SGDBFanartFinderCommand;
        public ICommand SGDBFanartFinderCommand
        {
            get
            {
                return _SGDBFanartFinderCommand ?? (_SGDBFanartFinderCommand = new RelayCommand(SGDBFanartFinder));
            }
        }

        #region Properties
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged(); }
        }
        private string _Path;
        public string Path
        {
            get { return _Path; }
            set { _Path = value; RaisePropertyChanged(); }
        }
        private string _Desc;
        public string Desc
        {
            get { return _Desc; }
            set { _Desc = value; RaisePropertyChanged(); }
        }
        private string _Year;
        public string Year
        {
            get { return _Year; }
            set { _Year = value; RaisePropertyChanged(); }
        }
        private string _Editeur;
        public string Editeur
        {
            get { return _Editeur; }
            set { _Editeur = value; RaisePropertyChanged(); }
        }
        private string _Dev;
        public string Dev
        {
            get { return _Dev; }
            set { _Dev = value; RaisePropertyChanged(); }
        }
        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; RaisePropertyChanged(); }
        }
        private string _Boxart;
        public string Boxart
        {
            get { return _Boxart; }
            set { _Boxart = value; RaisePropertyChanged(); }
        }
        private string _Box3dart;
        public string Box3dart
        {
            get { return _Box3dart; }
            set { _Box3dart = value; RaisePropertyChanged(); }
        }
        private string _Banner;
        public string Screenshoot
        {
            get { return _Banner; }
            set { _Banner = value; RaisePropertyChanged(); }
        }
        private string _Fanart;
        public string Fanart
        {
            get { return _Fanart; }
            set { _Fanart = value; RaisePropertyChanged(); }
        }
        private string _logo;
        public string Logo
        {
            get { return _logo; }
            set { _logo = value; RaisePropertyChanged(); }
        }
        private string _video;
        public string Video
        {
            get { return _video; }
            set { _video = value; RaisePropertyChanged(); }
        }
        private string _RecalView;
        public string RecalView
        {
            get { return _RecalView; }
            set { _RecalView = value; RaisePropertyChanged(); }
        }
        private string _TitleScreen;
        public string TitleScreen
        {
            get { return _TitleScreen; }
            set { _TitleScreen = value; RaisePropertyChanged(); }
        }
        #endregion
        public GameDetailViewModel(GameRom game)
        {
            _gameService = App.ServiceProvider.GetRequiredService<IGameService>();
            dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            LoadingGame(game);
        }
        private void LoadingGame(GameRom game)
        {
            GameCurrent = game;
            Name = game.Name;
            Path = game.Path;
            Desc = game.Desc;
            Year = game.Year;
            Editeur = game.Editeur;
            Dev = game.Dev;
            Genre = game.Genre;
            Boxart = game.Boxart;
            Screenshoot = game.Screenshoot;
            Logo = game.Logo;
            RecalView = game.RecalView;
            TitleScreen = game.TitleScreen;
            Fanart = game.Fanart;
            Video = game.Video;
            if (File.Exists(game.Fanart) == false)
            {
                if (File.Exists(game.Fanart.Replace(".jpg", ".png")))
                {
                    Fanart = game.Fanart.Replace(".jpg", ".png");
                }
                //    Fanart = TitleScreen;
                //    if (File.Exists(game.TitleScreen) == false)
                //    {
                //        Fanart = RecalView;
                //    }
            }
        }

        private void SGDBBoxFinder()
        {
            var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, SGDBType.boxart);
            if (resultimg != null)
            {
                Boxart = resultimg;
            }
        }
        private void SGDBLogoFinder()
        {
            var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, SGDBType.logo);
            if (resultimg != null)
            {
                Logo = resultimg;
            }
        }
        private void SGDBScreenFinder()
        {
            var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, SGDBType.background);
            if (resultimg != null)
            {
                Screenshoot = resultimg;
            }
        }
        private void SGDBFanartFinder()
        {
            var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, SGDBType.fanart);
            if (resultimg != null)
            {
                Fanart = resultimg;
            }
        }
        private void ScrapeGame()
        {
            LoadingGame(_gameService.ScrapeGame(GameCurrent));
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
            GameCurrent.Boxart = Boxart;
            GameCurrent.Fanart = Fanart;
            GameCurrent.Logo = Logo;
            GameCurrent.Screenshoot = Screenshoot;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
