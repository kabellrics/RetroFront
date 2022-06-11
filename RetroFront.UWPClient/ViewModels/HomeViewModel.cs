using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI;
using RetroFront.UWPClient.Core;
using RetroFront.UWPClient.Core.Models;

namespace RetroFront.UWPClient.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private HomeDisplay _displayType;
        public HomeDisplay DisplayType
        {
            get { return _displayType; }
            set { SetProperty(ref _displayType, value); }
        }
        private HomeService homeService;
        #region LastPlayedGame
        public ObservableCollection<GameRom> LastPlayedGames { get; set; }
        private GameRom _LPGame1;
        public GameRom LPGame1
        {
            get { return _LPGame1; }
            set
            {
                SetProperty(ref _LPGame1, value);
            }
        }
        private GameRom _LPGame2;
        public GameRom LPGame2
        {
            get { return _LPGame2; }
            set
            {
                SetProperty(ref _LPGame2, value);
            }
        }
        private GameRom _LPGame3;
        public GameRom LPGame3
        {
            get { return _LPGame3; }
            set
            {
                SetProperty(ref _LPGame3, value);
            }
        }
        private GameRom _LPGame4;
        public GameRom LPGame4
        {
            get { return _LPGame4; }
            set
            {
                SetProperty(ref _LPGame4, value);
            }
        }
        private GameRom _LPGame5;
        public GameRom LPGame5
        {
            get { return _LPGame5; }
            set
            {
                SetProperty(ref _LPGame5, value);
            }
        }
        private GameRom _LPGame6;
        public GameRom LPGame6
        {
            get { return _LPGame6; }
            set
            {
                SetProperty(ref _LPGame6, value);
            }
        }  
        #endregion
        #region MostPlayedGame
        public ObservableCollection<GameRom> MostPlayedGames { get; set; }
        private GameRom _MPGame1;
        public GameRom MPGame1
        {
            get { return _MPGame1; }
            set { SetProperty(ref _MPGame1, value); }
        }
        private GameRom _MPGame2;
        public GameRom MPGame2
        {
            get { return _MPGame2; }
            set { SetProperty(ref _MPGame2, value); }
        }
        private GameRom _MPGame3;
        public GameRom MPGame3
        {
            get { return _MPGame3; }
            set { SetProperty(ref _MPGame3, value); }
        }
        private GameRom _MPGame4;
        public GameRom MPGame4
        {
            get { return _MPGame4; }
            set { SetProperty(ref _MPGame4, value); }
        }
        private GameRom _MPGame5;
        public GameRom MPGame5
        {
            get { return _MPGame5; }
            set { SetProperty(ref _MPGame5, value); }
        }
        private GameRom _MPGame6;
        public GameRom MPGame6
        {
            get { return _MPGame6; }
            set { SetProperty(ref _MPGame6, value); }
        } 
        #endregion
        #region Fav Games
        public ObservableCollection<GameRom> FavGames { get; set; }
        private GameRom _FavGame1;
        public GameRom FavGame1
        {
            get { return _FavGame1; }
            set { SetProperty(ref _FavGame1, value); }
        }
        private GameRom _FavGame2;
        public GameRom FavGame2
        {
            get { return _FavGame2; }
            set { SetProperty(ref _FavGame2, value); }
        }
        private GameRom _FavGame3;
        public GameRom FavGame3
        {
            get { return _FavGame3; }
            set { SetProperty(ref _FavGame3, value); }
        }
        private GameRom _FavGame4;
        public GameRom FavGame4
        {
            get { return _FavGame4; }
            set { SetProperty(ref _FavGame4, value); }
        }
        private GameRom _FavGame5;
        public GameRom FavGame5
        {
            get { return _FavGame5; }
            set { SetProperty(ref _FavGame5, value); }
        }
        private GameRom _FavGame6;
        public GameRom FavGame6
        {
            get { return _FavGame6; }
            set { SetProperty(ref _FavGame6, value); }
        } 
        #endregion
        #region Systemes
        public ObservableCollection<Systeme> RandomSystems { get; set; }
        private Systeme _System1;
        public Systeme System1
        {
            get { return _System1; }
            set { SetProperty(ref _System1, value); }
        }
        private Systeme _System3;
        public Systeme System3
        {
            get { return _System3; }
            set { SetProperty(ref _System3, value); }
        }
        private Systeme _System2;
        public Systeme System2
        {
            get { return _System2; }
            set { SetProperty(ref _System2, value); }
        }
        private Systeme _System4;
        public Systeme System4
        {
            get { return _System4; }
            set { SetProperty(ref _System4, value); }
        }
        private Systeme _System6;
        public Systeme System6
        {
            get { return _System6; }
            set { SetProperty(ref _System6, value); }
        }
        private Systeme _System5;
        public Systeme System5
        {
            get { return _System5; }
            set { SetProperty(ref _System5, value); }
        } 
        #endregion
        public HomeViewModel()
        {
            homeService = new HomeService();
            DisplayType = HomeDisplay._0;
        }
        public async void LoadDataAsync()
        {
            LastPlayedGames = new ObservableCollection<GameRom>();
            MostPlayedGames = new ObservableCollection<GameRom>();
            FavGames = new ObservableCollection<GameRom>();
            RandomSystems = new ObservableCollection<Systeme>();
            await Init();
        }

        private async Task Init()
        {
            DisplayType = await homeService.GetCurrentHomeDisplay();
            await InitLastPlayed();
            await InitMostPlayed();
            await InitFavGames();
            await InitRandomSystems();
        }

        private async Task InitRandomSystems()
        {
            foreach(var system in await homeService.GetRandomPlateforme())
            {
                RandomSystems.Add(system);
            }
            System1 = RandomSystems.ElementAt(0);
            System2 = RandomSystems.ElementAt(1);
            System3 = RandomSystems.ElementAt(2);
            System4 = RandomSystems.ElementAt(3);
            System5 = RandomSystems.ElementAt(4);
            System6 = RandomSystems.ElementAt(5);
        }
        private async Task InitFavGames()
        {
            foreach(var game in await homeService.GetFavGames())
            {
                FavGames.Add(game);
            }
            FavGame1 = FavGames.ElementAt(0);
            FavGame2 = FavGames.ElementAt(1);
            FavGame3 = FavGames.ElementAt(2);
            FavGame4 = FavGames.ElementAt(3);
            FavGame5 = FavGames.ElementAt(4);
            FavGame6 = FavGames.ElementAt(5);
        }

        private async Task InitMostPlayed()
        {
            foreach (var game in await homeService.GetMostPlayedGames())
            {
                MostPlayedGames.Add(game);
            }
            MPGame1 = MostPlayedGames.ElementAt(0);
            MPGame2 = MostPlayedGames.ElementAt(1);
            MPGame3 = MostPlayedGames.ElementAt(2);
            MPGame4 = MostPlayedGames.ElementAt(3);
            MPGame5 = MostPlayedGames.ElementAt(4);
            MPGame6 = MostPlayedGames.ElementAt(5);
        }

        private async Task InitLastPlayed()
        {
            foreach (var game in await homeService.GetLastPlayedGames())
            {
                LastPlayedGames.Add(game);
            }
            LPGame1 = LastPlayedGames.ElementAt(0);
            LPGame2 = LastPlayedGames.ElementAt(1);
            LPGame3 = LastPlayedGames.ElementAt(2);
            LPGame4 = LastPlayedGames.ElementAt(3);
            LPGame5 = LastPlayedGames.ElementAt(4);
            LPGame6 = LastPlayedGames.ElementAt(5);
        }
    }
}
