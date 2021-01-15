using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class SystemeDetailViewModel : ViewModelBase
    {
        private IThemeService _themeService;
        #region Properties
        private ThemePlateformeViewModel _currentTheme;
        public ThemePlateformeViewModel CurrentTheme
        {
            get { return _currentTheme; }
            set { _currentTheme = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<ThemePlateformeViewModel> _themes;
        public ObservableCollection<ThemePlateformeViewModel> Themes
        {
            get { return _themes; }
            set { _themes = value; RaisePropertyChanged(); }
        }
        private int _selectedThemeIndex;
        public int SelectedThemeIndex
        {
            get { return _selectedThemeIndex; }
            set { _selectedThemeIndex = value; RaisePropertyChanged(); }
        }

        public Systeme Sys { get; set; }
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
        private string _shortname;
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; RaisePropertyChanged(); }
        }

        #endregion

        //private ICommand _previousthemeCommand;
        //private ICommand _nextthemeCommand;
        //public ICommand PreviousthemeCommand
        //{
        //    get
        //    {
        //        return _previousthemeCommand ?? (_previousthemeCommand = new RelayCommand(Previoustheme));
        //    }
        //}

        //public ICommand NextthemeCommand
        //{
        //    get
        //    {
        //        return _nextthemeCommand ?? (_nextthemeCommand = new RelayCommand(Nexttheme));
        //    }
        //}

        //private void Nexttheme()
        //{
        //    try
        //    {
        //        CurrentTheme = Themes[SelectedThemeIndex++];
        //        ChangeImg();
        //    }
        //    catch (Exception ex)
        //    {
        //        SelectedThemeIndex = 0;
        //        CurrentTheme = Themes[SelectedThemeIndex];
        //        ChangeImg();
        //    }
        //}
        //private void Previoustheme()
        //{
        //    try
        //    {
        //        CurrentTheme = Themes[SelectedThemeIndex--];
        //        ChangeImg();
        //    }
        //    catch (Exception ex)
        //    {
        //        SelectedThemeIndex = Themes.Count-1;
        //        CurrentTheme = Themes[SelectedThemeIndex];
        //        ChangeImg();
        //    }
        //}
        public SystemeDetailViewModel(Systeme sys)
        {
            Sys = sys;
            Name = sys.Name;
            ShortName = sys.Shortname;
            _themeService = App.ServiceProvider.GetRequiredService<IThemeService>();
            Themes = new ObservableCollection<ThemePlateformeViewModel>();
            foreach(var th in _themeService.GetInstalledTheme())
            {
                ThemePlateformeViewModel thvm = new ThemePlateformeViewModel(ShortName, th);
                thvm.Bck = ChargeBck(th.FolderName);
                thvm.Logo = ChargeLogo(th.FolderName);
                Themes.Add(thvm);
            }
            SelectedThemeIndex = 0;
            //CurrentTheme = Themes[SelectedThemeIndex];
            //ChangeImg();
        }
        public string ChargeLogo(string foldername)
        {
            return _themeService.GetLogoForTheme(ShortName, foldername);
        }
        public string ChargeBck(string foldername)
        {
            return _themeService.GetBckForTheme(ShortName, foldername);
        }
    }
}
