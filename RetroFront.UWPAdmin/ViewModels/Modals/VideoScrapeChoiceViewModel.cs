using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class VideoScrapeChoiceViewModel : ObservableObject
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
        private string _VideoToChange;
        public string VideoToChange
        {
            get => _VideoToChange;
            set
            {
                SetProperty(ref _VideoToChange, value);
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
        private ObservableCollection<String> _videoproposals;
        public ObservableCollection<String> VideoProposal
        {
            get { return _videoproposals; }
            set { SetProperty(ref _videoproposals, value); }
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

        private ICommand _ScrapeSNSPCommand;
        public ICommand ScrapeSNSPCommand => _ScrapeSNSPCommand ?? (_ScrapeSNSPCommand = new RelayCommand(ScrapeSNSP));
        private ICommand _ChooseImgCommand;
        public ICommand ChooseImgCommand => _ChooseImgCommand ?? (_ChooseImgCommand = new RelayCommand(ChooseImg));

        public VideoScrapeChoiceViewModel(DisplayGame Game)
        {
            service = new GameDetailModalService();
            DialogService = Ioc.Default.GetService<DialogService>();
            CurrentGame = Game;
            EnableIGDB = true;
            EnableSCSP = true;
            EnableSGDB = false;
            VideoToChange = CurrentGame.Video;
            if (CurrentGame.ScreenScraperID < 1)
            {
                EnableSCSP = false;
            }                
            if (CurrentGame.IGDBID < 1)
            {
                EnableIGDB = false;
            }
        }
        public VideoScrapeChoiceViewModel()
        {
        }
        private async void ScrapeSNSP()
        {
            await SearchVideo(ScraperSource.Screenscraper);
        }

        private async Task SearchVideo(ScraperSource CurrentScrapeSource)
        {
            if (CurrentScrapeSource == ScraperSource.IGDB)
                VideoProposal = new ObservableCollection<string>(await service.GetVideoProposal(CurrentGame.IGDBID, CurrentScrapeSource));
            else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                VideoProposal = new ObservableCollection<string>(await service.GetVideoProposal(CurrentGame.ScreenScraperID, CurrentScrapeSource));
        }

        private async void ScrapeIGDB()
        {
            await SearchVideo(ScraperSource.IGDB);
        }
        private async void ScrapeLocal()
        {
            this.VideoToChange = await DialogService.FilePicker(new System.Collections.Generic.List<string>() { ".avi", ".mkv", ".mp4" });
        }
        private void ChooseImg()
        {
            VideoToChange = Result;
        }
    }
}
