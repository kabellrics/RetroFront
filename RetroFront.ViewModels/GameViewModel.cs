using GalaSoft.MvvmLight;
using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private Game _game;
        public Game Game
        {
            get { return _game; }
            set { _game = value;RaisePropertyChanged(); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; RaisePropertyChanged(); }
        }
        private string _desc;
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; RaisePropertyChanged(); }
        }
        private string _year;
        public string Year
        {
            get { return _year; }
            set { _year = value; RaisePropertyChanged(); }
        }
        private string _editeur;
        public string Editeur
        {
            get { return _editeur; }
            set { _editeur = value; RaisePropertyChanged(); }
        }
        private string _dev;
        public string Dev
        {
            get { return _dev; }
            set { _dev = value; RaisePropertyChanged(); }
        }
        private string _boxart;
        public string Boxart
        {
            get { return _boxart; }
            set { _boxart = value; RaisePropertyChanged(); }
        }
        private string _box3dart;
        public string Box3dart
        {
            get { return _box3dart; }
            set { _box3dart = value; RaisePropertyChanged(); }
        }
        private string _banner;
        public string Banner
        {
            get { return _banner; }
            set { _banner = value; RaisePropertyChanged(); }
        }
        private string _fanart;
        public string Fanart
        {
            get { return _fanart; }
            set { _fanart = value; RaisePropertyChanged(); }
        }

        public GameViewModel(Game game)
        {
            Game = game;
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
    }
}
