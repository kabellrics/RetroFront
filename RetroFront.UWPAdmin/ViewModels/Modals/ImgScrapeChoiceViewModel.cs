using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class ImgScrapeChoiceViewModel : ObservableObject
    {
        private GameDetailModalService service;
        private DialogService DialogService;
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
        private ObservableCollection<String> _imgproposals;
        public ObservableCollection<String> IMGProposal
        {
            get { return _imgproposals; }
            set { SetProperty(ref _imgproposals, value); }
        }
        private String _result;
        public String Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }
        private ICommand _ScrapeLocalCommand;
        public ICommand ScrapeLocalCommand => _ScrapeLocalCommand ?? (_ScrapeLocalCommand = new RelayCommand(ScrapeLocal));

        private ICommand _ScrapeIGDBCommand;
        public ICommand ScrapeIGDBCommand => _ScrapeIGDBCommand ?? (_ScrapeIGDBCommand = new RelayCommand(ScrapeIGDB));

        private ICommand _ScrapeSGDBCommand;
        public ICommand ScrapeSGDBCommand => _ScrapeSGDBCommand ?? (_ScrapeSGDBCommand = new RelayCommand(ScrapeSGDB));

        private ICommand _ScrapeSNSPCommand;
        public ICommand ScrapeSNSPCommand => _ScrapeSNSPCommand ?? (_ScrapeSNSPCommand = new RelayCommand(ScrapeSNSP));
        private ICommand _ChooseImgCommand;
        public ICommand ChooseImgCommand => _ChooseImgCommand ?? (_ChooseImgCommand = new RelayCommand(ChooseImg));

        public ImgScrapeChoiceViewModel(DisplayGame Game, ScraperType typeImg)
        {
            service = new GameDetailModalService();
            DialogService = Ioc.Default.GetService<DialogService>();
            CurrentGame = Game;
            TypeImg = typeImg;
            EnableIGDB = true;
            EnableSCSP = true;
            EnableSGDB = true;
            if (TypeImg == ScraperType.Boxart)
            {
                ImgToChange = CurrentGame.Boxart;
            }
            else if (TypeImg == ScraperType.Banner)
            {
                EnableIGDB = false;
                ImgToChange = CurrentGame.Banner;
            }
            else if (TypeImg == ScraperType.Logo)
            {
                ImgToChange = CurrentGame.Logo;
            }
            else if (TypeImg == ScraperType.ArtWork)
            {
                ImgToChange = CurrentGame.Screenshoot;
            }
            else if (TypeImg == ScraperType.Bezel)
            {
                EnableIGDB = false;
                EnableSGDB = false;
                ImgToChange = CurrentGame.Bezel;
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
            await SearchImg(ScraperSource.Screenscraper);
        }
        private async void ScrapeSGDB()
        {
            await SearchImg(ScraperSource.SGDB);
        }
        private async void ScrapeIGDB()
        {
            await SearchImg(ScraperSource.IGDB);
        }
        private async void ScrapeLocal()
        {
            this.ImgToChange = await DialogService.FilePicker(new System.Collections.Generic.List<string>() { ".jpg", ".jpeg", ".png" });
        }
        public async Task SearchImg(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.IGDB)
                IMGProposal = new ObservableCollection<string>(await service.GetImgProposal(CurrentGame.IGDBID, CurrentScrapeSource, TypeImg));
            else if (CurrentScrapeSource == ScraperSource.SGDB)
                IMGProposal = new ObservableCollection<string>(await service.GetImgProposal(CurrentGame.SteamGridDBID, CurrentScrapeSource, TypeImg));
            else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                IMGProposal = new ObservableCollection<string>(await service.GetImgProposal(CurrentGame.ScreenScraperID, CurrentScrapeSource, TypeImg));
        }
        private void ChooseImg()
        {
            ImgToChange = Result;
        }
    }
}
