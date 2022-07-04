using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Models.SteamGridDB;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class ImgFinderSearchViewModel : ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private ISteamGridDBService steamGridDBService;
        private IIGDBService iGDBService;
        private IScreenScraperService ScreenScraperService;
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
        private String _ImgPath;
        public String ImgPath
        {
            get { return _ImgPath; }
            set { _ImgPath = value; RaisePropertyChanged(); }
        }
        private int _selectedImgIndex;
        public int SelectedImgIndex
        {
            get { return _selectedImgIndex; }
            set { _selectedImgIndex = value; RaisePropertyChanged(); }
        }
        private int _nbImg;
        public int NBImg
        {
            get { return _nbImg; }
            set { _nbImg = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<String> _resultImgs;


        public ObservableCollection<String> ResultImgs
        {
            get { return _resultImgs; }
            set { _resultImgs = value; RaisePropertyChanged(); }
        }
        public ImgFinderSearchViewModel(GameRom game, ScraperSource currentScrapeSource, ScraperType currentScraperType)
        {
            steamGridDBService = App.ServiceProvider.GetRequiredService<ISteamGridDBService>();
            iGDBService = App.ServiceProvider.GetRequiredService<IIGDBService>();
            ScreenScraperService = App.ServiceProvider.GetRequiredService<IScreenScraperService>();
            dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            Title = $"Recherche de {CurrentScraperType.ToString()} pour {game.Name}";
            ResultImgs = new ObservableCollection<string>();
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
            else if (CurrentScrapeSource == ScraperSource.SGDB)
            {
                if (game.SGDBID < 1)
                {
                    searchedGame = dialogService.SearchSteamGridDBByName(game.Name, CurrentScrapeSource);
                    if (searchedGame != null)
                        game.SGDBID = searchedGame.id;
                }
            }
            
            else if (CurrentScrapeSource == ScraperSource.Screenscraper)
            {
                if (game.ScreenScraperID < 1)
                {
                    searchedGame = dialogService.SearchSteamGridDBByName(game.Name, CurrentScrapeSource);
                    if (searchedGame != null)
                        game.ScreenScraperID = searchedGame.id;
                }
            }

            if (searchedGame != null)
            {
                ResultImgs = new ObservableCollection<string>();
                if (CurrentScraperType == ScraperType.Logo)
                {
                    if (CurrentScrapeSource == ScraperSource.SGDB)
                    {
                        var result = steamGridDBService.GetLogoForId(game.SGDBID);
                        if (result != null)
                        {
                            foreach (var img in result)
                            {
                                ResultImgs.Add(img.url);
                            }
                        } 
                    }
                    else if(CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        var result = ScreenScraperService.GetJeuxDetail(game.ScreenScraperID);
                        if (result != null)
                        {
                            foreach(var img in result.medias.Where(x => x.type == "wheel"))
                            {
                                ResultImgs.Add(img.url);
                            }
                        }
                    }
                }
                else if (CurrentScraperType == ScraperType.ArtWork)
                {
                    IEnumerable<ImgResult> result;
                    if (CurrentScrapeSource == ScraperSource.SGDB)
                    {
                        result = steamGridDBService.GetHeroesForId(game.SGDBID);
                        if (result != null)
                        {
                            foreach (var img in result)
                            {
                                ResultImgs.Add(img.url);
                            }
                        }
                    }
                    else if (CurrentScrapeSource == ScraperSource.IGDB)
                    {
                        var detailart = iGDBService.GetArtworksByGameId(game.IGDBID);
                        var detailsch = iGDBService.GetScreenshotsByGameId(game.IGDBID);
                        if (detailart != null)
                        {
                            var resultartigdb = detailart.Select(x => iGDBService.GetArtWorkLink(x.image_id));
                            foreach (var img in resultartigdb)
                            {
                                ResultImgs.Add(img);
                            }
                        }
                        if (detailsch != null)
                        {
                            var resultschigdb = detailsch.Select(x => iGDBService.GetArtWorkLink(x.image_id));
                            foreach (var img in resultschigdb)
                            {
                                ResultImgs.Add(img);
                            }
                        }
                    }
                    else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        var arts = ScreenScraperService.GetJeuxDetail(game.ScreenScraperID);
                        if (arts != null)
                        {
                            foreach (var img in arts.medias.Where(x => x.type == "fanart" || x.type == "ss"|| x.type == "sstitle"|| x.type == "themehs"))
                            {
                                ResultImgs.Add(img.url);
                            }
                        }
                    }
                }
                else if (CurrentScraperType == ScraperType.Banner)
                {
                    if (CurrentScrapeSource == ScraperSource.SGDB)
                    {
                        var result = steamGridDBService.GetGridBannerForId(game.SGDBID);
                        if (result != null)
                        {
                            foreach (var img in result)
                            {
                                ResultImgs.Add(img.url);
                            }
                        } 
                    }
                    else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        var arts = ScreenScraperService.GetJeuxDetail(game.ScreenScraperID);
                        if (arts != null)
                        {
                            foreach (var img in arts.medias.Where(x => x.type == "marquee" || x.type == "screenmarquee" || x.type == "steamgrid"))
                            {
                                ResultImgs.Add(img.url);
                            }
                        }
                    }
                }
                else if (CurrentScraperType == ScraperType.Boxart)
                {
                    if (CurrentScrapeSource == ScraperSource.SGDB)
                    {
                        var result = steamGridDBService.GetGridBoxartForId(game.SGDBID);
                        if (result != null)
                        {
                            foreach (var img in result)
                            {
                                ResultImgs.Add(img.url);
                            }
                        }
                    }
                    else if (CurrentScrapeSource == ScraperSource.IGDB)
                    {
                        var detail = iGDBService.GetDetailsGame(game.IGDBID);
                        ResultImgs.Add(iGDBService.GetCoverLink(detail.cover.image_id));
                    }
                    else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                    {
                        var arts = ScreenScraperService.GetJeuxDetail(game.ScreenScraperID);
                        if (arts != null)
                        {
                            foreach (var img in arts.medias.Where(x => x.type == "box-2D"))
                            {
                                ResultImgs.Add(img.url);
                            }
                        }
                    }
                }
                NBImg = ResultImgs.Count;
                SelectedImgIndex = 0;
            }
        }


        #region MyRegion
        //if (game.SteamID == 0)
        //{
        //    steamgridgame = dialogService.SearchSteamGridDBByName(game.Name,CurrentScrapeSource);
        //}
        //else if (game.SteamID != -1)
        //{
        //    steamgridgame = steamGridDBService.GetGameSteamId(game.SteamID.ToString());
        //}
        //if (searchedGame != null)
        //{
        //    if (CurrentScraperType == ScraperType.Logo)
        //    {
        //        var result = steamGridDBService.GetLogoForId(searchedGame.id);
        //        if (result != null)
        //        {
        //            foreach (var img in result)
        //            {
        //                ResultImgs.Add(img.url);
        //            }
        //        }
        //    }
        //    else if (CurrentScraperType == ScraperType.ArtWork)
        //    {
        //        var result = steamGridDBService.GetHeroesForId(searchedGame.id); if (result != null)
        //        {
        //            foreach (var img in result)
        //            {
        //                ResultImgs.Add(img.url);
        //            }
        //        }
        //    }
        //    else if (CurrentScraperType == ScraperType.Banner)
        //    {
        //        var result = steamGridDBService.GetGridBannerForId(searchedGame.id); if (result != null)
        //        {
        //            foreach (var img in result)
        //            {
        //                ResultImgs.Add(img.url);
        //            }
        //        }
        //    }
        //    else if (CurrentScraperType == ScraperType.Boxart)
        //    {
        //        var result = steamGridDBService.GetGridBoxartForId(searchedGame.id); if (result != null)
        //        {
        //            foreach (var img in result)
        //            {
        //                ResultImgs.Add(img.url);
        //            }
        //        }
        //    } 
        #endregion


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
