﻿using GalaSoft.MvvmLight;
using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.ViewModels
{
    public class EmulatorViewModel : ViewModelBase
    {
        private Emulator _emulator;
        public Emulator Emulator
        {
            get { return _emulator; }
            set { _emulator = value; RaisePropertyChanged(); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private string _nBGame;
        public string NBGame
        {
            get { return _nBGame; }
            set { _nBGame = value; RaisePropertyChanged(); }
            //get { return $"{Systeme?.Emulators.SelectMany(x => x.Games).Count()} Jeux"; }
        }
        public EmulatorViewModel(Emulator emulator)
        {
            Emulator = emulator;
            Name = Emulator.Name;
            NBGame = $"{Emulator?.Games.Count} Jeux";
        }
    }
}