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
        private bool _ShowSteamId;
        public bool ShowSteamId
        {
            get => _ShowSteamId;
            set
            {
                SetProperty(ref _ShowSteamId, value);
            }
        }
        private bool _ShowOriginId;
        public bool ShowOriginId
        {
            get => _ShowOriginId;
            set
            {
                SetProperty(ref _ShowOriginId, value);
            }
        }
        private bool _ShowEpicId;
        public bool ShowEpicId
        {
            get => _ShowEpicId;
            set
            {
                SetProperty(ref _ShowEpicId, value);
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
        private IEnumerable<DisplayGame> _selectedgame;
        public IEnumerable<DisplayGame> SelectedGame
        {
            get { return _selectedgame; }
            set { SetProperty(ref _selectedgame, value); }
        }

    public PickLocalGameViewModel(LocalGame store)
        {
            GameStore = store;
            if(GameStore == LocalGame.Steam)
            {
                ShowSteamId = true;
                ShowOriginId = false;
                ShowEpicId = false;
            }
            else if(GameStore == LocalGame.Epic)
            {
                ShowSteamId = false;
                ShowOriginId = false;
                ShowEpicId = true;
            }
            else if(GameStore != LocalGame.Origin)
            {
                ShowSteamId = false;
                ShowOriginId = true;
                ShowEpicId = false;
            }
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
                foreach(var game in await service.GetInstalledSteamGame())
                {
                    FoundedGame.Add(game);
                }
            }
            else if(GameStore == LocalGame.Origin)
            {
                foreach (var game in await service.GetInstalledOriginGame())
                {
                    FoundedGame.Add(game);
                }
            }
            else if(GameStore == LocalGame.Epic)
            {
                foreach (var game in await service.GetInstalledEpicGame())
                {
                    FoundedGame.Add(game);
                }
            }
        }
        public void SetUSerChoice()
        {
            SelectedGame = FoundedGame.Where(x => x.IsSelected);
        }
    }
}
