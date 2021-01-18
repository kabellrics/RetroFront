using GalaSoft.MvvmLight;
using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RetroFront.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private GameRom _game;
        public GameRom Game
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
        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; RaisePropertyChanged(); }
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
        public string Sxreenshoot
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
        private string _logo;
        public string Logo
        {
            get { return _logo; }
            set { _logo = value;RaisePropertyChanged(); }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;RaisePropertyChanged(); }
        }
        public GameViewModel(GameRom game)
        {
            Game = game;
            Name = game.Name;
            Path = game.Path;
            Desc = game.Desc;
            Year = game.Year;
            Editeur = game.Editeur;
            Dev = game.Dev;
            Genre = game.Genre;
            Boxart = game.Boxart;
            Sxreenshoot = game.Screenshoot;
            Logo = game.Logo;
            RecalView = game.RecalView;
            TitleScreen = game.TitleScreen;
            Fanart = game.Fanart;
            if (File.Exists(game.Fanart) == false)
            {
                Fanart = TitleScreen;
                if (File.Exists(game.TitleScreen) == false)
                {
                    Fanart = RecalView;
                }
            }
        }
    }
}
