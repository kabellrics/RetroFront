using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<DisplayGame> _foundedgame;
        public ObservableCollection<DisplayGame> FoundedGame
        {
            get { return _foundedgame; }
            set { SetProperty(ref _foundedgame, value); }
        }
        private ObservableCollection<DisplayGame> _selectedgame;
        public ObservableCollection<DisplayGame> SelectedGame
        {
            get { return _selectedgame; }
            set { SetProperty(ref _selectedgame, value); }
        }

    public PickLocalGameViewModel(LocalGame store)
        {
            GameStore = store;
            FoundedGame = new ObservableCollection<DisplayGame>();
            SelectedGame = new ObservableCollection<DisplayGame>();
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
            FoundedGame.Clear();
            if(GameStore == LocalGame.Steam)
            {
                foreach(var game = await service.GetInstalledSteamGame())
                {
                    FoundedGame.Add(game);
                }
            }
            else if(GameStore == LocalGame.Origin)
            {
                foreach (var game = await service.GetInstalledOriginGame())
                {
                    FoundedGame.Add(game);
                }
            }
            else if(GameStore == LocalGame.Epic)
            {
                foreach (var game = await service.GetInstalledEpicGame())
                {
                    FoundedGame.Add(game);
                }
            }
        }
    }
}
