﻿using GalaSoft.MvvmLight;
using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RetroFront.ViewModels
{
    public class SystemeViewModel : ViewModelBase
    {
        private Systeme _systeme;
        public Systeme Systeme
        {
            get { return _systeme; }
            set { _systeme = value;RaisePropertyChanged(); }
        }

        public SystemeViewModel(Systeme systeme)
        {
            if (systeme != null)
            {
                Systeme = systeme;
                Name = Systeme.Name;
                Theme = Systeme.Theme;               
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;RaisePropertyChanged(); }
        }
        private string _theme;
        public string Theme
        {
            get { return _theme; }
            set { _theme = value; RaisePropertyChanged(); }
        }
        private string _nBEmu;
        public string NBEmu
        {
            get { return _nBEmu; }
            set { _nBEmu = value; RaisePropertyChanged(); }
            //get { return $"{Systeme?.Emulators?.Count} Emulateurs"; }
        }

        private string _nBGame;
        public string NBGame
        {
            get { return _nBGame; }
            set { _nBGame = value; RaisePropertyChanged(); }
            //get { return $"{Systeme?.Emulators.SelectMany(x => x.Games).Count()} Jeux"; }
        }
    }
}