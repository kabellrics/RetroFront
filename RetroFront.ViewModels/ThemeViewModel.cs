using GalaSoft.MvvmLight;
using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.ViewModels
{
    public class ThemeViewModel : ViewModelBase
    {
        private Theme _Theme;
        public Theme Theme
        {
            get { return _Theme; }
            set { _Theme = value; RaisePropertyChanged(); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private string _folder;
        public string Folder
        {
            get { return _folder; }
            set { _folder = value; RaisePropertyChanged(); }
        }
        public ThemeViewModel(Theme th)
        {
            Theme = th;
            Name = th.Name;
            Folder = th.FolderName;
        }
    }
}
