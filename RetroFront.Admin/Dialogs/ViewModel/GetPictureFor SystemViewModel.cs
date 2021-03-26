using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class GetPictureForSystemViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ICommand _searchpictureCommand;
        private IDialogService dialogService;
        private IThemeService themeService;
        public string ResultPath { get; set; }
        public ScraperType ScraperType { get; set; }
        private string _imgPath;
        public string ImgPath
        {
            get { return _imgPath; }
            set { _imgPath = value; RaisePropertyChanged(); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }
        public GetPictureForSystemViewModel(Systeme title,string themename,ScraperType scraperType)
        {
            Title = $"Rechercher de {scraperType.ToString()} pour {title.Name}";
            ScraperType = scraperType;
            dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            themeService = App.ServiceProvider.GetRequiredService<IThemeService>();
            var img = themeService.GetBckForTheme(title.Shortname,themename);
            if (img != null)
                ImgPath = img;
        }
        public ICommand SearchpictureCommand
        {
            get
            {
                return _searchpictureCommand ?? (_searchpictureCommand = new RelayCommand<object>(Searchpicture));
            }
        }

        private void Searchpicture(object obj)
        {
            string path;
            if (ScraperType == ScraperType.Logo)
            {
                path = dialogService.OpenUniqueFileDialog($"Fichier Image (*.png)|*.png");
            }
            else
            {
                path = dialogService.OpenUniqueFileDialog($"Fichier Image (*.jpg)|*.jpg");
            }
            if (path != null)
                ImgPath = path;
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
            ResultPath = ImgPath;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
