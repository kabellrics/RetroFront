using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class ImgScrapeChoiceViewModel : ObservableObject
    {
        private DialogService dialogService;
        private String _title;
        public String Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }
        private DisplayGame _currentgame;
        public DisplayGame CurrentGame
        {
            get => _currentgame;
            set
            {
                SetProperty(ref _currentgame, value);
            }
        }
        private string _ImgToChange;
        public string ImgToChange
        {
            get => _ImgToChange;
            set
            {
                SetProperty(ref _ImgToChange, value);
            }
        }
        private ScraperType _typeimg;
        public ScraperType TypeImg
        {
            get => _typeimg;
            set
            {
                SetProperty(ref _typeimg, value);
            }
        }
        private int _imgheight;
        public int ImgHeight
        {
            get => _imgheight;
            set
            {
                SetProperty(ref _imgheight, value);
            }
        }
        private int _imgwidht;
        public int ImgWidth
        {
            get => _imgwidht;
            set
            {
                SetProperty(ref _imgwidht, value);
            }
        }
        //private int _ctdgheight;
        //public int CTDGHeight
        //{
        //    get => _ctdgheight;
        //    set
        //    {
        //        SetProperty(ref _ctdgheight, value);
        //    }
        //}
        //private int _ctdgwidht;
        //public int CTDGWidth
        //{
        //    get => _ctdgwidht;
        //    set
        //    {
        //        SetProperty(ref _ctdgwidht, value);
        //    }
        //}
        private bool _showigdb;
        public bool EnableIGDB
        {
            get => _showigdb;
            set
            {
                SetProperty(ref _showigdb, value);
            }
        }
        private bool _showSGDB;
        public bool EnableSGDB
        {
            get => _showSGDB;
            set
            {
                SetProperty(ref _showSGDB, value);
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

        private ICommand _ScrapeLocalCommand;
        public ICommand ScrapeLocalCommand => _ScrapeLocalCommand ?? (_ScrapeLocalCommand = new RelayCommand(ScrapeLocal));

        private ICommand _ScrapeIGDBCommand;
        public ICommand ScrapeIGDBCommand => _ScrapeIGDBCommand ?? (_ScrapeIGDBCommand = new RelayCommand(ScrapeIGDB));

        private ICommand _ScrapeSGDBCommand;
        public ICommand ScrapeSGDBCommand => _ScrapeSGDBCommand ?? (_ScrapeSGDBCommand = new RelayCommand(ScrapeSGDB));

        private ICommand _ScrapeSNSPCommand;
        public ICommand ScrapeSNSPCommand => _ScrapeSNSPCommand ?? (_ScrapeSNSPCommand = new RelayCommand(ScrapeSNSP));

        public ImgScrapeChoiceViewModel(DisplayGame Game, ScraperType typeImg)
        {
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
            CurrentGame = Game;
            TypeImg = typeImg;
            EnableIGDB = true;
            EnableSCSP = true;
            EnableSGDB = true;
            if (TypeImg == ScraperType.Boxart)
            {
                ImgHeight = 900;
                ImgWidth = 600;
                //CTDGHeight = 950;
                //CTDGWidth = 900;
                ImgToChange = CurrentGame.Boxart;
            }
            else if (TypeImg == ScraperType.Banner)
            {
                ImgHeight = 600;
                ImgWidth = 900;
                //CTDGHeight = 650;
                //CTDGWidth = 1200;
                EnableIGDB = false;
                ImgToChange = CurrentGame.Banner;
            }
            else if (TypeImg == ScraperType.Logo)
            {
                ImgHeight = 500;
                ImgWidth = 500;
                //CTDGHeight = 550;
                //CTDGWidth = 700;
                ImgToChange = CurrentGame.Logo;
            }
            else if (TypeImg == ScraperType.ArtWork)
            {
                ImgHeight = 540;
                ImgWidth = 960;
                //CTDGHeight = 600;
                //CTDGWidth = 1200;
                ImgToChange = CurrentGame.Screenshoot;
            }
            if (CurrentGame.ScreenScraperID < 1)
            {
                EnableSCSP = false;
            }
            if (CurrentGame.SteamGridDBID < 1)
            {
                EnableSGDB = false;
            }
            if (CurrentGame.IGDBID < 1)
            {
                EnableIGDB = false;
            }
        }

        public ImgScrapeChoiceViewModel()
        {
        }
        private async void ScrapeSNSP()
        {
            var result = await dialogService.ShowIMGProposal(CurrentGame.ScreenScraperID, ScraperSource.Screenscraper, TypeImg);
            if(!string.IsNullOrEmpty(result))
            {
                ImgToChange = result;
            }
        }
        private async void ScrapeSGDB()
        {
            var result = await dialogService.ShowIMGProposal(CurrentGame.SteamGridDBID, ScraperSource.SGDB, TypeImg);
            if (!string.IsNullOrEmpty(result))
            {
                ImgToChange = result;
            }
        }
        private async void ScrapeIGDB()
        {
            var result = await dialogService.ShowIMGProposal(CurrentGame.IGDBID, ScraperSource.IGDB, TypeImg);
            if (!string.IsNullOrEmpty(result))
            {
                ImgToChange = result;
            }
        }
        private async void ScrapeLocal()
        {
            var result = await dialogService.FilePicker(new System.Collections.Generic.List<string>() { ".jpg", ".jpeg", ".png" }) ;
            if (!string.IsNullOrEmpty(result))
            {
                ImgToChange = result;
            }
        }

    }
}
