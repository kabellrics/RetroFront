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

        private ICommand _resolveGameCommand;
        public ICommand ResolveGameCommand
        {
            get
            {
                return _resolveGameCommand ?? (_resolveGameCommand = new RelayCommand<ScraperSource>(ResolveGame));
            }
        }
        private ICommand _SGDBBoxFinderCommand;
        public ICommand SGDBBoxFinderCommand
        {
            get
            {
                return _SGDBBoxFinderCommand ?? (_SGDBBoxFinderCommand = new RelayCommand<ScraperSource>(SGDBBoxFinder));
            }
        }
        private ICommand _SGDBLogoFinderCommand;
        public ICommand SGDBLogoFinderCommand
        {
            get
            {
                return _SGDBLogoFinderCommand ?? (_SGDBLogoFinderCommand = new RelayCommand<ScraperSource>(SGDBLogoFinder));
            }
        }
        private ICommand _SGDBScreenFinderCommand;
        public ICommand SGDBScreenFinderCommand
        {
            get
            {
                return _SGDBScreenFinderCommand ?? (_SGDBScreenFinderCommand = new RelayCommand<ScraperSource>(SGDBScreenFinder));
            }
        }
        private ICommand _SGDBFanartFinderCommand;
        public ICommand SGDBFanartFinderCommand
        {
            get
            {
                return _SGDBFanartFinderCommand ?? (_SGDBFanartFinderCommand = new RelayCommand<ScraperSource>(SGDBFanartFinder));
            }
        }
        private ICommand _SGDBVideoFinderCommand;
        public ICommand SGDBVideoFinderCommand
        {
            get
            {
                return _SGDBVideoFinderCommand ?? (_SGDBVideoFinderCommand = new RelayCommand<ScraperSource>(SGDBVideoFinder));
            }
        }

        #region Properties
        private int _IsResolveSGDB;
        public int IsResolveSGDB
        {
            get { return _IsResolveSGDB; }
            set { _IsResolveSGDB = value; RaisePropertyChanged(); }
        }
        private int _IsResolveIGDB;
        public int IsResolveIGDB
        {
            get { return _IsResolveIGDB; }
            set { _IsResolveIGDB = value; RaisePropertyChanged(); }
        }
        private int _IsResolveSCSP;
        public int IsResolveSCSP
        {
            get { return _IsResolveSCSP; }
            set { _IsResolveSCSP = value; RaisePropertyChanged(); }
        }
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
        private void LoadingGame(GameRom game = null)
        {
            if(game != null)
                GameCurrent = game;

            Name = GameCurrent.Name;
            Path = GameCurrent.Path;
            Desc = GameCurrent.Desc;
            Year = GameCurrent.Year;
            Editeur = GameCurrent.Editeur;
            Dev = GameCurrent.Dev;
            Genre = GameCurrent.Genre;
            Boxart = GameCurrent.Boxart;
            Screenshoot = GameCurrent.Screenshoot;
            Logo = GameCurrent.Logo;
            RecalView = GameCurrent.RecalView;
            TitleScreen = GameCurrent.TitleScreen;
            Fanart = GameCurrent.Fanart;
            Video = GameCurrent.Video;
            IsResolveIGDB = GameCurrent.IGDBID;
            IsResolveSCSP = GameCurrent.ScreenScraperID;
            IsResolveSGDB = GameCurrent.SGDBID;
            if (GameCurrent.Fanart != null)
            {
                if (File.Exists(GameCurrent.Fanart) == false)
                {
                    if (File.Exists(GameCurrent.Fanart.Replace(".jpg", ".png")))
                    {
                        Fanart = GameCurrent.Fanart.Replace(".jpg", ".png");
                    }
                    //    Fanart = TitleScreen;
                    //    if (File.Exists(game.TitleScreen) == false)
                    //    {
                    //        Fanart = RecalView;
                    //    }
                } 
            }
            if (GameCurrent.Screenshoot != null)
            {
                if (File.Exists(GameCurrent.Screenshoot) == false)
                {
                    if (File.Exists(GameCurrent.Screenshoot.Replace(".png", ".jpg")))
                    {
                        Screenshoot = GameCurrent.Screenshoot.Replace(".png", ".jpg");
                    }
                    else
                    {
                        Screenshoot = TitleScreen;
                        if (File.Exists(GameCurrent.TitleScreen) == false)
                        {
                            Screenshoot = RecalView;
                        }
                    }
                }
                else
                {
                    Screenshoot = GameCurrent.Screenshoot;
                } 
            }
        }

        private void ResolveGame(ScraperSource CurrentScrapeSource)
        {
            var searchedGame = dialogService.SearchSteamGridDBByName(GameCurrent.Name, CurrentScrapeSource);
            if(searchedGame != null)
            {
                if(CurrentScrapeSource == ScraperSource.IGDB)
                {
                    GameCurrent.IGDBID = searchedGame.id;
                }
                else if(CurrentScrapeSource == ScraperSource.SGDB)
                {
                    GameCurrent.SGDBID = searchedGame.id;
                }
                else if(CurrentScrapeSource == ScraperSource.Screenscraper) 
                {
                    GameCurrent.ScreenScraperID = searchedGame.id;
                }
                LoadingGame();
            }
            //throw new NotImplementedException();
        }
        private void SGDBBoxFinder(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.Local)
            {
                var strimg = dialogService.OpenUniqueFileDialog($"Fichier Image (*.png;*.jpg)|*.png;*.jpg");
                if (strimg != null)
                    Boxart = strimg;
            }
            else
            {
                var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, ScraperType.Boxart, CurrentScrapeSource);
                if (resultimg != null)
                {
                    Boxart = resultimg;
                }
            }
        }
        private void SGDBLogoFinder(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.Local)
            {
                var strimg = dialogService.OpenUniqueFileDialog($"Fichier Image (*.png)|*.png");
                if (strimg != null)
                    Logo = strimg;
            }
            else
            {
                var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, ScraperType.Logo, CurrentScrapeSource);
                if (resultimg != null)
                {
                    Logo = resultimg;
                }
            }
        }
        private void SGDBScreenFinder(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.Local)
            {
                var strimg = dialogService.OpenUniqueFileDialog($"Fichier Image (*.png;*.jpg)|*.png;*.jpg");
                if (strimg != null)
                    Screenshoot = strimg;
            }
            else
            {
                var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, ScraperType.ArtWork, CurrentScrapeSource);
                if (resultimg != null)
                {
                    Screenshoot = resultimg;
                }
            }
        }
        private void SGDBFanartFinder(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.Local)
            {
                var strimg = dialogService.OpenUniqueFileDialog($"Fichier Image (*.png;*.jpg)|*.png;*.jpg");
                if (strimg != null)
                    Fanart = strimg;
            }
            else
            {
                var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, ScraperType.Banner, CurrentScrapeSource);
                if (resultimg != null)
                {
                    Fanart = resultimg;
                }
            }
        }
        private void SGDBVideoFinder(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.Local)
            {
                var strimg = dialogService.OpenUniqueFileDialog($"Fichier Vidéo (*.mp4;*.avi)|*.mp4;*.avi");
                if (strimg != null)
                    Video = strimg;
            }
            else if (CurrentScrapeSource == ScraperSource.Screenscraper) { }
            {
                var resultimg = dialogService.SearchImgInSteamGridDB(GameCurrent, ScraperType.Video, CurrentScrapeSource);
                if (resultimg != null)
                {
                    Video = resultimg;
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
            GameCurrent.Boxart = Boxart;
            GameCurrent.Fanart = Fanart;
            GameCurrent.Logo = Logo;
            GameCurrent.Screenshoot = Screenshoot;
            GameCurrent.Name = Name;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
