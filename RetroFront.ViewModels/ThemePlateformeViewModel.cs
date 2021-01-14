using GalaSoft.MvvmLight;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.ViewModels
{
    public class ThemePlateformeViewModel : ViewModelBase
    {
        private string _logo;
        public string Logo
        {
            get { return _logo; }
            set { _logo = value; RaisePropertyChanged(); }
        }
        private string _Bck;
        public string Bck
        {
            get { return _Bck; }
            set { _Bck = value; RaisePropertyChanged(); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private Theme _currentTheme;
        public Theme CurrentTheme
        {
            get { return _currentTheme; }
            set { _currentTheme = value; RaisePropertyChanged(); }
        }
        public ThemePlateformeViewModel(string plateforme, Theme th)
        {
            CurrentTheme = th;
            Name = th.Name;
        }
    }
}
