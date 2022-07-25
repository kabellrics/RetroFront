using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class ScrapeResolverViewModel : ObservableObject
    {
        private String _title;
        public String Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }
        private String _name;
        public String Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        private ScraperSource _source;
        private List<Search> _foundedgame;
        private Search _resultgame;
        public ScraperSource Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }
        public List<Search> FoundedGame
        {
            get { return _foundedgame; }
            set { SetProperty(ref _foundedgame, value); }
        }
        public Search Resultgame
        {
            get { return _resultgame; }
            set { SetProperty(ref _resultgame, value); }
        }
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(SearchGame));
            }
        }

        private GameDetailModalService service;
        public ScrapeResolverViewModel(String name, ScraperSource scraper)
        {
            service = new GameDetailModalService();
            Name = name;
            Source = scraper;
            Title = $"Recherche de l'Id {Source.ToString()} pour {Name}";
        }

        public ScrapeResolverViewModel()
        {
        }

        public async void LoadDataAsync()
        {
            await InitializeAsync();
        }
        public async Task InitializeAsync()
        {
            //Title = $"Recherche de l'Id {Source.ToString()} pour {Name}";
            await SearchByNameResult();
        }
        private async void SearchGame()
        {
            await SearchByNameResult();
        }
        private async Task SearchByNameResult()
        {
            FoundedGame = await service.ResolveByName(Name, Source);
        }
    }
}
