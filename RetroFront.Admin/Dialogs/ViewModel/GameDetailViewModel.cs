using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class GameDetailViewModel : ViewModelBase
    {
        private IGameService _gameService;
        public Game GameCurrent { get; set; }
        private ICommand _ScrapeGameCommand;
        public ICommand ScrapeGameCommand
        {
            get
            {
                return _ScrapeGameCommand ?? (_ScrapeGameCommand = new RelayCommand(ScrapeGame));
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
        public string Banner
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
        #endregion
        public GameDetailViewModel(Game game)
        {
            _gameService = App.ServiceProvider.GetRequiredService<IGameService>();
            LoadingGame(game);
        }
        private void LoadingGame(Game game)
        {
            GameCurrent = game;
            Name = game.Name;
            Path = game.Path;
            Desc = game.Desc;
            Year = game.Year;
            Editeur = game.Editeur;
            Dev = game.Dev;
            Boxart = game.Boxart;
            Box3dart = game.Box3dart;
            Banner = game.Banner;
            Fanart = game.Fanart;
        }
        private void ScrapeGame()
        {
            LoadingGame(_gameService.ScrapeGame(GameCurrent));
        }
    }
}
