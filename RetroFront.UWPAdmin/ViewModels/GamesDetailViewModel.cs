using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
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

        private ICommand _ScrapeBoxartCommand;
        public ICommand ScrapeBoxartCommand => _ScrapeBoxartCommand ?? (_ScrapeBoxartCommand = new RelayCommand(ScrapeBoxart));

        private ICommand _ScrapeBannerCommand;
        public ICommand ScrapeBannerCommand => _ScrapeBannerCommand ?? (_ScrapeBannerCommand = new RelayCommand(ScrapeBanner));
        private ICommand _ScrapeScreenshootCommand;
        public ICommand ScrapeScreenshootCommand => _ScrapeScreenshootCommand ?? (_ScrapeScreenshootCommand = new RelayCommand(ScrapeScreenshoot));
        private ICommand _ScrapeMetadataCommand;
        public ICommand ScrapeMetadataCommand => _ScrapeMetadataCommand ?? (_ScrapeMetadataCommand = new RelayCommand(ScrapeMetadata));

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
            }
        }
        private async void ScrapeIGDB()
        {
            var findgame = await dialogService.SearchSteamGridDBByName(Source.Name, Core.APIHelper.ScraperSource.IGDB);
            if(findgame != null)
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
            if(findgame != null)
            {
            }
        }
        private async void ScrapeBoxart()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.Boxart);
            if (findgame != null)
            {
            }
        }
        private async void ScrapeBanner()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.Banner);
            if (findgame != null)
            {
            }
        }
        private async void ScrapeScreenshoot()
        {
            var findgame = await dialogService.ShowImgScrapeChoice(Source, Core.APIHelper.ScraperType.ArtWork);
            if (findgame != null)
            {
            }
        }
        private async void ScrapeMetadata()
        {
            var findgame = await dialogService.ShowMetadataScrapeChoice(Source);
            if (findgame != null)
            {
            }
        }
    }
}
