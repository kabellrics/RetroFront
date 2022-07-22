using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
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
        private GameDataProviderClient gameDataProvider;
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(SearchGame));
            }
        }

        public ScrapeResolverViewModel(String name, ScraperSource scraper)
        {
            gameDataProvider = new GameDataProviderClient();
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
            FoundedGame = new List<Search>();
            if (Source == ScraperSource.SGDB)
            {
                var games = await gameDataProvider.SearchByNameAsync(Name);
                FoundedGame = games.Result.ToList();
            }
            else if (Source == ScraperSource.IGDB)
            {
                var games = await gameDataProvider.GetGameByNameAsync(Name);
                FoundedGame = games.Result.ToList();
            }
            else if (Source == ScraperSource.Screenscraper)
            {
                var games = await gameDataProvider.SearchGameAsync(Name);
                var SCSPFoundedGame = games.Result.ToList();
                foreach (var SCSPgame in SCSPFoundedGame)
                {
                    FoundedGame.Add(new Search() { Id = SCSPgame.ScreenScraperID, Name = SCSPgame.Name });
                }
            }
        }
    }
}
