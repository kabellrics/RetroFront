using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class MetadataScrapeChooseViewModel : ObservableObject
    {
        private GameDetailModalService service;
        private DisplayGame _currentgame;
        public DisplayGame CurrentGame
        {
            get => _currentgame;
            set
            {
                SetProperty(ref _currentgame, value);
            }
        }
        private bool _showigdb;
        public bool EnableIGDB
        {
            get => _showigdb;
            set
            {
                SetProperty(ref _showigdb, value);
            }
        }
        private bool _showSCSP;
        public bool EnableSCSP
        {
            get => _showSCSP;
            set
            {
                SetProperty(ref _showSCSP, value);
            }
        }
        private ICommand _ScrapeIGDBCommand;
        public ICommand ScrapeIGDBCommand => _ScrapeIGDBCommand ?? (_ScrapeIGDBCommand = new RelayCommand(ScrapeIGDB));
        private ICommand _ScrapeSNSPCommand;
        public ICommand ScrapeSNSPCommand => _ScrapeSNSPCommand ?? (_ScrapeSNSPCommand = new RelayCommand(ScrapeSNSP));
        public MetadataScrapeChooseViewModel(DisplayGame Game)
        {
            service = new GameDetailModalService();
            EnableIGDB = true;
            EnableSCSP = true;
            CurrentGame = Game;
            if (CurrentGame.ScreenScraperID < 1)
            {
                EnableSCSP = false;
            }
            if (CurrentGame.IGDBID < 1)
            {
                EnableIGDB = false;
            }
        }
        public MetadataScrapeChooseViewModel()
        {
            service = new GameDetailModalService();
        }
        private async void ScrapeSNSP()
        {
            var data = await service.GetMetadataScreenscraper(CurrentGame.ScreenScraperID);
            CurrentGame.Genre = data.Genres;
            CurrentGame.Editeur = data.Editeur;
            CurrentGame.Developpeur = data.Developpeur;
            CurrentGame.Description = data.Synopsis;
            CurrentGame.Year = data.Year;
        }
        private async void ScrapeIGDB()
        {
            try
            {
                var data = await service.GetMetadataIGDB(CurrentGame.IGDBID);
                CurrentGame.Genre = data.Genres;
                CurrentGame.Editeur = data.Editeur;
                CurrentGame.Developpeur = data.Developpeur;
                CurrentGame.Description = data.Synopsis;
                CurrentGame.Year = data.Year;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
