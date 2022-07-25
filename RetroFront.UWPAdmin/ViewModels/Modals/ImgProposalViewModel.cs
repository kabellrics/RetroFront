using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class ImgProposalViewModel : ObservableObject
    {
        private int _gameid;
        public int GameId
        {
            get => _gameid;
            set
            {
                SetProperty(ref _gameid, value);
            }
        }
        private ScraperSource _source;
        public ScraperSource Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }
        private ScraperType _type;
        public ScraperType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
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
        private GameDetailModalService service;
        public ImgProposalViewModel(int gameId, ScraperSource CurrentScrapeSource, ScraperType CurrentScraperType)
        {
            service = new GameDetailModalService();
            GameId = gameId;
            Source = CurrentScrapeSource;
            Type = CurrentScraperType;
        }

        public ImgProposalViewModel()
        {
        }

        public async void LoadDataAsync()
        {
            await InitializeAsync();
        }
        public async Task InitializeAsync()
        {
            IMGProposal = new ObservableCollection<string>(await service.GetImgProposal(GameId, Source, Type));
        }
    }
}
