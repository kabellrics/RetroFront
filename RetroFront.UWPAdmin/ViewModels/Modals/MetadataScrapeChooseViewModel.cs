using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class MetadataScrapeChooseViewModel : ObservableObject
    {
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
        public MetadataScrapeChooseViewModel(DisplayGame Game)
        {
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
        }
    }
}
