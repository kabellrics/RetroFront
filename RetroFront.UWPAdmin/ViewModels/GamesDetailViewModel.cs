﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class GamesDetailViewModel : ObservableObject
    {
        private GameDetailService gameDetailService;
        private DialogService dialogService;
        private DisplayGame _source;
        public DisplayGame Source
        {
            get => _source;
            set
            {
                SetProperty(ref _source, value);
            }
        }
        private String _newLogo;
        public String NewLogo
        {
            get => _newLogo;
            set
            {
                SetProperty(ref _newLogo, value);
            }
        }
        private String _newBoxart;
        public String NewBoxart
        {
            get => _newBoxart;
            set
            {
                SetProperty(ref _newBoxart, value);
            }
        }
        private String _newArtwork;
        public String NewArtwork
        {
            get => _newArtwork;
            set
            {
                SetProperty(ref _newArtwork, value);
            }
        }
        private String _newVideo;
        public String NewVideo
        {
            get => _newVideo;
            set
            {
                SetProperty(ref _newVideo, value);
            }
        }
        private String _newBanner;
        public String NewBanner
        {
            get => _newBanner;
            set
            {
                SetProperty(ref _newBanner, value);
            }
        }
        private String _newBezel;
        public String NewBezel
        {
            get => _newBezel;
            set
            {
                SetProperty(ref _newBezel, value);
            }
        }
        private ICommand _nointrosearchCommand;
        public ICommand NoIntroCommand => _nointrosearchCommand ?? (_nointrosearchCommand = new RelayCommand(NoIntroSearch));

        private ICommand _CfgRetroarchCommand;
        public ICommand CfgRetroarchCommand => _CfgRetroarchCommand ?? (_CfgRetroarchCommand = new RelayCommand(CfgRetroarch));

        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteElement));

        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SavingChange));

        private ICommand _ScrapeIGDBCommand;
        public ICommand ScrapeIGDBCommand => _ScrapeIGDBCommand ?? (_ScrapeIGDBCommand = new RelayCommand(ScrapeIGDB));

        private ICommand _ScrapeSGDBCommand;
        public ICommand ScrapeSGDBCommand => _ScrapeSGDBCommand ?? (_ScrapeSGDBCommand = new RelayCommand(ScrapeSGDB));

        private ICommand _ScrapeSNSPCommand;
        public ICommand ScrapeSNSPCommand => _ScrapeSNSPCommand ?? (_ScrapeSNSPCommand = new RelayCommand(ScrapeSNSP));

        private ICommand _ScrapeLogoCommand;
        public ICommand ScrapeLogoCommand => _ScrapeLogoCommand ?? (_ScrapeLogoCommand = new RelayCommand(ScrapeLogo));

        private ICommand _ScrapeBezelCommand;
        public ICommand ScrapeBezelCommand => _ScrapeBezelCommand ?? (_ScrapeBezelCommand = new RelayCommand(ScrapeBezel));

        private ICommand _ScrapeBoxartCommand;
        public ICommand ScrapeBoxartCommand => _ScrapeBoxartCommand ?? (_ScrapeBoxartCommand = new RelayCommand(ScrapeBoxart));

        private ICommand _ScrapeBannerCommand;
        public ICommand ScrapeBannerCommand => _ScrapeBannerCommand ?? (_ScrapeBannerCommand = new RelayCommand(ScrapeBanner));
        private ICommand _ScrapeVideoCommand;
        public ICommand ScrapeVideoCommand => _ScrapeVideoCommand ?? (_ScrapeVideoCommand = new RelayCommand(ScrapeVideo));
        private ICommand _ScrapeScreenshootCommand;
        public ICommand ScrapeScreenshootCommand => _ScrapeScreenshootCommand ?? (_ScrapeScreenshootCommand = new RelayCommand(ScrapeScreenshoot));
        private ICommand _ScrapeMetadataCommand;
        public ICommand ScrapeMetadataCommand => _ScrapeMetadataCommand ?? (_ScrapeMetadataCommand = new RelayCommand(ScrapeMetadata));
        private ICommand _SaveAndDLLCommand;
        public ICommand SaveAndDLLCommand => _SaveAndDLLCommand ?? (_SaveAndDLLCommand = new RelayCommand(SaveChangeAndDLLImg));

        private async void SavingChange()
        {
            await SaveChange();
        }
        public async Task SaveChange()
        {
            if (Source.HasChanged == true)
            {
                var result = await dialogService.ConfirmationDialogAsync("Voulez vous sauvegarder les changements effectués ?", "Oui", "Non");
                if (result == true)
                {
                    await gameDetailService.UpdateGame(Source).ContinueWith(task => SendNewBck());
                }
            }
        }

        private async void NoIntroSearch()
        {
            var emus = await gameDetailService.GetEmulators();
            var emu = emus.FirstOrDefault(x => x.ID == Source.EmulatorID);
            if(emu != null)
            {
                var syss = await gameDetailService.GetSystemes();
                var sys = syss.FirstOrDefault(x => x.ID == emu.SystemeID);
                if(sys != null)
                {
                    var result = await dialogService.SearchNoIntroName(Source.Name,sys.ShortName);
                    if (result != null)
                    {
                        try
                        {
                            var filename = Path.GetFileNameWithoutExtension(Source.Path);
                            var newpath = Source.Path.Replace(filename, result.Value);
                            await gameDetailService.MoveFile(Source.Path, newpath);
                            Source.Path = newpath;
                        }
                        catch (Exception ex)
                        {
                            //throw;
                        }
                    }
                }
            }
        }
        private async void CfgRetroarch()
        {
           await Task.Run(async () => await gameDetailService.GenerateBezelCFGForRetroarch(Source.ID));
           await dialogService.ConfirmationDialogAsync("Fichiers générés");
        }
        private async void DeleteElement()
        {
            var dialogresult = await dialogService.ConfirmationDialogAsync("Etes vous sure de vouloir supprimer ce jeu ?");
            if (dialogresult.HasValue && dialogresult == true)
            {
                var t = Task.Run(async () => await gameDetailService.DeleteGame(Source.Game));
                await dialogService.ConfirmationDialogAsync($"Jeu {Source.Name} Supprimé");
                NavigationService.GoBack();
            }
        }
        private void SendNewBck()
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new ReloadBckMessage(null));
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public GamesDetailViewModel()
        {
            gameDetailService = new GameDetailService();
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
        }

        public async Task LoadDataAsync()
        {
        }

        public async void Initialize(string selectedGameID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedGameID))
            {
                Source = await gameDetailService.GetGame(int.Parse(selectedGameID));
                NewLogo = Source.LogoPath;
                NewBoxart = Source.BoxartPath;
                NewBanner = Source.BannerPath;
                NewArtwork = Source.ScreenshootPath;
                NewVideo = Source.VideoPath;
                NewBezel = Source.BezelPath;
            }
        }
        public async void Initialize(string selectedGameID)
        {
            if (!string.IsNullOrEmpty(selectedGameID))
            {
                Source = await gameDetailService.GetGame(int.Parse(selectedGameID));
                NewLogo = Source.LogoPath;
                NewBoxart = Source.BoxartPath;
                NewBanner = Source.BannerPath;
                NewArtwork = Source.ScreenshootPath;
                NewVideo = Source.VideoPath;
                NewBezel = Source.BezelPath;
            }
        }
        private async void ScrapeIGDB()
        {
            var findgame = await dialogService.SearchSteamGridDBByName(Source.Name, Core.APIHelper.ScraperSource.IGDB);
            if (findgame != null)
            {
                Source.IGDBID = findgame.Id;
            }
        }
        private async void ScrapeSGDB()
        {
            var findgame = await dialogService.SearchSteamGridDBByName(Source.Name, Core.APIHelper.ScraperSource.SGDB);
            if (findgame != null)
            {
                Source.SteamGridDBID = findgame.Id;
            }
        }
        private async void ScrapeSNSP()
        {
            var findgame = await dialogService.SearchSteamGridDBByName(Source.Name, Core.APIHelper.ScraperSource.Screenscraper);
            if (findgame != null)
            {
                Source.ScreenScraperID = findgame.Id;
            }
        }
        private async void ScrapeLogo()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.Logo);
            if (findgame != null)
            {
                NewLogo = findgame;
            }
        }
        private async void ScrapeBezel()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.Bezel);
            if (findgame != null)
            {
                NewBezel = findgame;
            }
        }
        private async void ScrapeBoxart()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.Boxart);
            if (findgame != null)
            {
                NewBoxart = findgame;
            }
        }
        private async void ScrapeBanner()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.Banner);
            if (findgame != null)
            {
                NewBanner = findgame;
            }
        }
        private async void ScrapeScreenshoot()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.ArtWork);
            if (findgame != null)
            {
                NewArtwork = findgame;
            }
        }
        private async void ScrapeVideo()
        {
            var findgame = await dialogService.ShowVideoScrapeChoice(Source);
            if (findgame != null)
            {
                NewVideo = findgame;
            }
        }
        private async void SaveChangeAndDLLImg()
        {
            if (Source.LogoPath != NewLogo)
            {
                var path = await gameDetailService.GetNewImgPath(Source, (int)ScraperType.Logo);
                if (NewLogo.Contains("screenscraper"))
                    path += ".png";
                else if (NewLogo.Contains("png"))
                    path += ".png";
                else if (NewLogo.Contains("jpg"))
                    path += ".jpg";
                else if (NewLogo.Contains("jpeg"))
                    path += ".jpeg";
                await dialogService.DLLFile(NewLogo, path, "Logo", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        Source.LogoPath = path;
                    });
                });
            }
            if (Source.BezelPath != NewBezel)
            {
                var path = await gameDetailService.GetNewImgPath(Source, (int)ScraperType.Bezel);
                if (NewLogo.Contains("screenscraper"))
                    path += ".png";
                else if (NewLogo.Contains("png"))
                    path += ".png";
                else if (NewLogo.Contains("jpg"))
                    path += ".jpg";
                else if (NewLogo.Contains("jpeg"))
                    path += ".jpeg";
                await dialogService.DLLFile(NewBezel, path, "Bezel", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        Source.BezelPath = path;
                    });
                });
            }
            if (Source.BoxartPath != NewBoxart)
            {
                var path = await gameDetailService.GetNewImgPath(Source, (int)ScraperType.Boxart);
                if (NewBoxart.Contains("screenscraper"))
                    path += ".jpg";
                else if (NewBoxart.Contains("png"))
                    path += ".png";
                else if (NewBoxart.Contains("jpg"))
                    path += ".jpg";
                else if (NewBoxart.Contains("jpeg"))
                    path += ".jpeg";
                await dialogService.DLLFile(NewBoxart, path, "Boxart", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        Source.BoxartPath = path;
                    });
                });
            }
            if (Source.BannerPath != NewBanner)
            {
                var path = await gameDetailService.GetNewImgPath(Source, (int)ScraperType.Banner);
                if (NewBanner.Contains("screenscraper"))
                    path += ".jpg";
                else if (NewBanner.Contains("png"))
                    path += ".png";
                else if (NewBanner.Contains("jpg"))
                    path += ".jpg";
                else if (NewBanner.Contains("jpeg"))
                    path += ".jpeg";
                await dialogService.DLLFile(NewBanner, path, "Banner", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        Source.BannerPath = path;
                    });
                });
            }
            if (Source.ScreenshootPath != NewArtwork)
            {
                var path = await gameDetailService.GetNewImgPath(Source, (int)ScraperType.ArtWork);
                if (NewArtwork.Contains("screenscraper"))
                    path += ".jpg";
                else if (NewArtwork.Contains("png"))
                    path += ".png";
                else if (NewArtwork.Contains("jpg"))
                    path += ".jpg";
                else if (NewArtwork.Contains("jpeg"))
                    path += ".jpeg";
                await dialogService.DLLFile(NewArtwork, path, "Artwork", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        Source.ScreenshootPath = path;
                    });
                });
            }
            if (Source.VideoPath != NewVideo)
            {
                var path = await gameDetailService.GetNewImgPath(Source, (int)ScraperType.Video);
                if (NewVideo.Contains("youtube"))
                    path += ".mp4";
                if (NewVideo.Contains("screenscraper"))
                    path += ".mp4";
                else if (NewVideo.Contains("avi"))
                    path += ".avi";
                else if (NewVideo.Contains("mp4"))
                    path += ".mp4";
                else if (NewVideo.Contains("mkv"))
                    path += ".mkv";
                    await dialogService.DLLFile(NewVideo, path, "Video", Source.Name).ContinueWith(async task =>
                    {
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                        {
                            Source.VideoPath = path;
                        });
                    });
                 
            }
            await SaveChange();
            var saveID = Source.ID;
            Source = null;
            Initialize(saveID.ToString());

        }
        private async void ScrapeMetadata()
        {
            var findgame = await dialogService.ShowMetadataScrapeChoice(Source);
            if (findgame != null)
            {
                Source.Year = findgame.Year;
                Source.Genre = findgame.Genre;
                Source.Editeur = findgame.Editeur;
                Source.Developpeur = findgame.Developpeur;
                Source.Description = findgame.Description;
            }
        }
    }
}
