using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class FindScreenScraperSystemeViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ICommand _searchCommand;
        private string _name;
        private RetroFront.Models.ScreenScraper.System.Systeme _resultgame;
        private ObservableCollection<RetroFront.Models.ScreenScraper.System.Systeme> _foundedSystem;

        public ObservableCollection<RetroFront.Models.ScreenScraper.System.Systeme> FoundedSystem
        {
            get { return _foundedSystem; }
            set { _foundedSystem = value; RaisePropertyChanged(); }
        }
        private List<RetroFront.Models.ScreenScraper.System.Systeme> _allSystem;

        public List<RetroFront.Models.ScreenScraper.System.Systeme> AllSystem
        {
            get { return _allSystem; }
            set { _allSystem = value; RaisePropertyChanged(); }
        }
        public RetroFront.Models.ScreenScraper.System.Systeme ResultSystem
        {
            get { return _resultgame; }
            set { _resultgame = value; RaisePropertyChanged(); }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(Search));
            }
        }
        public FindScreenScraperSystemeViewModel(string name)
        {
            var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\SystemListSCSP.json");
            JObject rss = JObject.Parse(jsonString);
            JArray categories = (JArray)rss["response"]["systemes"];
            AllSystem = categories.ToObject<List<RetroFront.Models.ScreenScraper.System.Systeme>>();
            FoundedSystem = new ObservableCollection<Models.ScreenScraper.System.Systeme>();
            Name = name;
            SearchByNameResult();
        }
        private void Search()
        {
            SearchByNameResult();
        }
        private void SearchByNameResult()
        {
            FoundedSystem.Clear();
            var searchtext = Name.Split(' ').ToList();
            searchtext.AddRange(Name.Split('_'));
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomEU)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomUS)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomJP)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomRecalbox)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomRetropie)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomLaunchbox)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomCommun)))
                FoundedSystem.Add(sys);

            FoundedSystem.Distinct();
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
            ;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
