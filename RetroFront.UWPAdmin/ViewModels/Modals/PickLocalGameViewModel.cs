using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class PickLocalGameViewModel : ObservableObject
    {
        private SystemeModalService service = new SystemeModalService();
        private String _title;
        public String Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }
        private LocalGame _gamestore;
        public LocalGame GameStore
        {
            get => _gamestore;
            set
            {
                SetProperty(ref _gamestore, value);
            }
        }
        private List<GameRom> _foundedgame;
        public List<GameRom> FoundedGame
        {
            get { return _foundedgame; }
            set { SetProperty(ref _foundedgame, value); }
        }
        private List<GameRom> _selectedgame;
        public List<GameRom> SelectedGame
        {
            get { return _selectedgame; }
            set { SetProperty(ref _selectedgame, value); }
        }

    public PickLocalGameViewModel(LocalGame store)
        {
            GameStore = store;
        }
        public PickLocalGameViewModel()
        {
        }

        public async void LoadDataAsync()
        {
            await InitializeAsync();
        }
        public async Task InitializeAsync()
        {
            //Title = $"Recherche de l'Id {Source.ToString()} pour {Name}";
            await Search();
        }
        private async Task Search()
        {
            if(GameStore == LocalGame.Steam) { }
            else if(GameStore == LocalGame.Origin) { }
            else if(GameStore == LocalGame.Epic) { }
        }
    }
}
