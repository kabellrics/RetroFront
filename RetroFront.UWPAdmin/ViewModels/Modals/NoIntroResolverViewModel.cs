using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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
    public class NoIntroResolverViewModel : ObservableObject
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
        private String _plateforme;
        public String Plateforme
        {
            get => _plateforme;
            set
            {
                SetProperty(ref _plateforme, value);
            }
        }
        private List<NoIntroSearchResult> _foundedgame;
        private NoIntroSearchResult _resultgame;
        public List<NoIntroSearchResult> FoundedGame
        {
            get { return _foundedgame; }
            set { SetProperty(ref _foundedgame, value); }
        }
        public NoIntroSearchResult Resultgame
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
        public NoIntroResolverViewModel(String name, String plateforme)
        {
            service = new GameDetailModalService();
            Name = name;
            Plateforme = plateforme;
            Title = $"Recherche du NoIntro  pour {Name}";
        }

        public NoIntroResolverViewModel()
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
            FoundedGame = (List<NoIntroSearchResult>)await service.GetNoIntroProposal(Name, Plateforme);
        }
    }
}
