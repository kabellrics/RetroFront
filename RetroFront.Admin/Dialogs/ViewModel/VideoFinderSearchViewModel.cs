using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Models.SteamGridDB;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class VideoFinderSearchViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ISteamGridDBService steamGridDBService;
        private IIGDBService iGDBService;
        private IDialogService dialogService;

        private ScraperSource _currentsource;
        private ScraperType _currenttype;
        public ScraperSource CurrentScrapeSource
        {
            get { return _currentsource; }
            set { _currentsource = value; RaisePropertyChanged(); }
        }
        public ScraperType CurrentScraperType
        {
            get { return _currenttype; }
            set { _currenttype = value; RaisePropertyChanged(); }
        }

        public string ResultPath { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }
        private KeyValuePair<String, String> _VideoPath;
        public KeyValuePair<String, String> VideoPath
        {
            get { return _VideoPath; }
            set { _VideoPath = value; RaisePropertyChanged(); }
        }
        private int _selectedVideoIndex;
        public int SelectedVideoIndex
        {
            get { return _selectedVideoIndex; }
            set { _selectedVideoIndex = value; RaisePropertyChanged(); }
        }
        private int _nbVIdeo;
        public int NBVideo
        {
            get { return _nbVIdeo; }
            set { _nbVIdeo = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<KeyValuePair<String,String>> _resultVideos;
        public ObservableCollection<KeyValuePair<String, String>> ResultVideos
        {
            get { return _resultVideos; }
            set { _resultVideos = value; RaisePropertyChanged(); }
        }
        public VideoFinderSearchViewModel(GameRom game, ScraperSource currentScrapeSource, ScraperType currentScraperType)
        {
            steamGridDBService = App.ServiceProvider.GetRequiredService<ISteamGridDBService>();
            iGDBService = App.ServiceProvider.GetRequiredService<IIGDBService>();
            dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            Title = $"Recherche de {CurrentScraperType.ToString()} pour {game.Name}";
            ResultVideos = new ObservableCollection<KeyValuePair<String, String>>();
            CurrentScrapeSource = currentScrapeSource;
            CurrentScraperType = currentScraperType;
            LoadingProposal(game);
        }

        public void LoadingProposal(GameRom game)
        {
            Search searchedGame = new Search();

            if (CurrentScrapeSource == ScraperSource.IGDB)
            {
                if (game.IGDBID < 1)
                {
                    searchedGame = dialogService.SearchSteamGridDBByName(game.Name, CurrentScrapeSource);
                    if (searchedGame != null)
                        game.IGDBID = searchedGame.id;
                }
            }
            if (searchedGame != null)
            {
                ResultVideos = new ObservableCollection<KeyValuePair<String, String>>();
                if (CurrentScrapeSource == ScraperSource.IGDB)
                {
                    var detailvideo = iGDBService.GetVideosByGameId(game.IGDBID);
                    if (detailvideo != null)
                    {
                        foreach (var video in detailvideo)
                        {
                            ResultVideos.Add(new KeyValuePair<String, String>(video.name,string.Format("https://www.youtube.com/embed/{0}", video.video_id)));
                        }
                    }
                }
                NBVideo = ResultVideos.Count;
                SelectedVideoIndex = 0;
            }
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
            ResultPath = VideoPath.Value;
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
