using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp.UI;
using RetroFront.UWPClient.Core;
using RetroFront.UWPClient.Core.Models;
using RetroFront.UWPClient.Services;
using RetroFront.UWPClient.Views;

namespace RetroFront.UWPClient.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private ICommand _plateformelistCommand;
        private ICommand _customcolleclistCommand;
        public ICommand PlateformelistCommand => _plateformelistCommand ?? (_plateformelistCommand = new RelayCommand<bool>(GotoPlateformePage));
        public ICommand CustomcolleclistCommand => _customcolleclistCommand ?? (_customcolleclistCommand = new RelayCommand<bool>(GotoPlateformePage));
        private ICommand _gamelistCommand;
        private ICommand _gamedetailCommand;
        private ICommand _gamelistspecificCommand;
        public ICommand GamelistCommand => _gamelistCommand ?? (_gamelistCommand = new RelayCommand<Systeme>(GotoGamePage));
        public ICommand GameDetailCommand => _gamedetailCommand ?? (_gamedetailCommand = new RelayCommand<GameRom>(GotoGameDetailPage));
        public ICommand GamelistSpecificCommand => _gamelistspecificCommand ?? (_gamelistspecificCommand = new RelayCommand<string>(GotoGamespecificPage));
        public ObservableCollection<FlipRotateElement> FlipRotateList { get; set; }

        private int _displayType;
        public int DisplayType
        {
            get { return _displayType; }
            set { SetProperty(ref _displayType, value); }
        }
        private int _flipSelectedIndex;
        public int FlipSelectedIndex
        {
            get { return _flipSelectedIndex; }
            set { SetProperty(ref _flipSelectedIndex, value); }
        }
        public bool True;
        public bool False;
        public string _BCK;
            public string BCK
        {
            get { return _BCK; }
            set { SetProperty(ref _BCK, value); }
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
        #region PC Games
        public ObservableCollection<GameRom> PCGames { get; set; }
        private GameRom _PCGame1;
        public GameRom PCGame1
        {
            get { return _PCGame1; }
            set { SetProperty(ref _PCGame1, value); }
        }
        private GameRom _PCGame2;
        public GameRom PCGame2
        {
            get { return _PCGame2; }
            set { SetProperty(ref _PCGame2, value); }
        }
        private GameRom _PCGame3;
        public GameRom PCGame3
        {
            get { return _PCGame3; }
            set { SetProperty(ref _PCGame3, value); }
        }
        private GameRom _PCGame4;
        public GameRom PCGame4
        {
            get { return _PCGame4; }
            set { SetProperty(ref _PCGame4, value); }
        }
        private GameRom _PCGame5;
        public GameRom PCGame5
        {
            get { return _PCGame5; }
            set { SetProperty(ref _PCGame5, value); }
        }
        private GameRom _PCGame6;
        public GameRom PCGame6
        {
            get { return _PCGame6; }
            set { SetProperty(ref _PCGame6, value); }
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
        #region CollecSystemes
        public ObservableCollection<Systeme> CollecSystems { get; set; }
        private Systeme _CollecSystem1;
        public Systeme CollecSystem1
        {
            get { return _CollecSystem1; }
            set { SetProperty(ref _CollecSystem1, value); }
        }
        private Systeme _CollecSystem3;
        public Systeme CollecSystem3
        {
            get { return _CollecSystem3; }
            set { SetProperty(ref _CollecSystem3, value); }
        }
        private Systeme _CollecSystem2;
        public Systeme CollecSystem2
        {
            get { return _CollecSystem2; }
            set { SetProperty(ref _CollecSystem2, value); }
        }
        private Systeme _CollecSystem4;
        public Systeme CollecSystem4
        {
            get { return _CollecSystem4; }
            set { SetProperty(ref _CollecSystem4, value); }
        }
        private Systeme _CollecSystem6;
        public Systeme CollecSystem6
        {
            get { return _CollecSystem6; }
            set { SetProperty(ref _CollecSystem6, value); }
        }
        private Systeme _CollecSystem5;
        public Systeme CollecSystem5
        {
            get { return _CollecSystem5; }
            set { SetProperty(ref _CollecSystem5, value); }
        } 
        #endregion
        public HomeViewModel()
        {
            True = true;False = false;
            homeService = new HomeService();
            DisplayType = (int)HomeDisplay._0;
            BCK = @"http://localhost:34322/api/Img/GetAppBackground";
            FlipSelectedIndex = 0;
        }
        public async void LoadDataAsync()
        {
            LastPlayedGames = new ObservableCollection<GameRom>();
            MostPlayedGames = new ObservableCollection<GameRom>();
            FavGames = new ObservableCollection<GameRom>();
            RandomSystems = new ObservableCollection<Systeme>();
            CollecSystems = new ObservableCollection<Systeme>();
            PCGames = new ObservableCollection<GameRom>();
            FlipRotateList = new ObservableCollection<FlipRotateElement>();
            await Init();
            WeakReferenceMessenger.Default.Send(new BckChangeMessage(null));
        }

        private async Task Init()
        {
            DisplayType = (int)await homeService.GetCurrentHomeDisplay();
            await InitLastPlayed();
            await InitMostPlayed();
            await InitFavGames();
            await InitRandomSystems();
            await InitPCGames();
            await InitRandomCollection();
            try
            {
                var favItem = new FlipRotateElement("Systemes", "sys", "plateforme", new ObservableCollection<IConvertedID>(RandomSystems));
                FlipRotateList.Add(favItem);
                var collecitem = new FlipRotateElement("Collections", "collec", "plateforme", new ObservableCollection<IConvertedID>(CollecSystems));
                FlipRotateList.Add(collecitem);
                var favitem = new FlipRotateElement("Favoris", "fav", "games", new ObservableCollection<IConvertedID>(FavGames));
                FlipRotateList.Add(favitem);
                var lastitem = new FlipRotateElement("Derniers jeux lancés", "last", "games", new ObservableCollection<IConvertedID>(LastPlayedGames));
                FlipRotateList.Add(lastitem);
                var mostitem = new FlipRotateElement("Jeux les plus lancés", "most", "games", new ObservableCollection<IConvertedID>(LastPlayedGames));
                FlipRotateList.Add(mostitem);
                var pcitem = new FlipRotateElement("Jeux PC", "pc", "games", new ObservableCollection<IConvertedID>(PCGames));
                FlipRotateList.Add(pcitem);
                foreach (var sys in RandomSystems)
                {
                    var item = new FlipRotateElement(sys.Name, sys.Shortname, "games", new ObservableCollection<IConvertedID>(await homeService.GetGamesForPlateforme(sys)));
                    FlipRotateList.Add(item);
                }
                foreach (var sys in CollecSystems)
                {
                    var item = new FlipRotateElement(sys.Name, sys.Shortname, "games", new ObservableCollection<IConvertedID>(await homeService.GetGamesForPlateforme(sys)));
                    FlipRotateList.Add(item);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        private void GotoPlateformePage(bool plateforme = false)
        {
            NavigationService.Navigate<PlateformePage>(plateforme);
        }
        private void GotoGamePage(Systeme plateforme = null)
        {
            NavigationService.Navigate<JeuxPage>(plateforme);
        }
        private void GotoGameDetailPage(GameRom game)
        {
            NavigationService.Navigate<GameDetailPage>(game);
        }
        private void GotoGamespecificPage(string plateforme)
        {
                Systeme sys = new Systeme { Shortname = plateforme };
                NavigationService.Navigate<JeuxPage>(sys);
        }
        private async Task InitPCGames()
        {
            foreach(var game in await homeService.GetRandomPCGames())
            {
                PCGames.Add(game);
            }
            PCGame1 = PCGames.ElementAtOrDefault(0);
            PCGame2 = PCGames.ElementAtOrDefault(1);
            PCGame3 = PCGames.ElementAtOrDefault(2);
            PCGame4 = PCGames.ElementAtOrDefault(3);
            PCGame5 = PCGames.ElementAtOrDefault(4);
            PCGame6 = PCGames.ElementAtOrDefault(5);
        }
        private async Task InitRandomCollection()
        {
            foreach(var system in await homeService.GetRandomCustomCollection())
            {
                CollecSystems.Add(system);
            }
            CollecSystem1 = CollecSystems.ElementAtOrDefault(0);
            CollecSystem2 = CollecSystems.ElementAtOrDefault(1);
            CollecSystem3 = CollecSystems.ElementAtOrDefault(2);
            CollecSystem4 = CollecSystems.ElementAtOrDefault(3);
            CollecSystem5 = CollecSystems.ElementAtOrDefault(4);
            CollecSystem6 = CollecSystems.ElementAtOrDefault(5);
        }
        private async Task InitRandomSystems()
        {
            foreach(var system in await homeService.GetRandomPlateforme())
            {
                RandomSystems.Add(system);
            }
            System1 = RandomSystems.ElementAtOrDefault(0);
            System2 = RandomSystems.ElementAtOrDefault(1);
            System3 = RandomSystems.ElementAtOrDefault(2);
            System4 = RandomSystems.ElementAtOrDefault(3);
            System5 = RandomSystems.ElementAtOrDefault(4);
            System6 = RandomSystems.ElementAtOrDefault(5);
        }
        private async Task InitFavGames()
        {
            foreach(var game in await homeService.GetFavGames())
            {
                FavGames.Add(game);
            }
            FavGame1 = FavGames.ElementAtOrDefault(0);
            FavGame2 = FavGames.ElementAtOrDefault(1);
            FavGame3 = FavGames.ElementAtOrDefault(2);
            FavGame4 = FavGames.ElementAtOrDefault(3);
            FavGame5 = FavGames.ElementAtOrDefault(4);
            FavGame6 = FavGames.ElementAtOrDefault(5);
        }
        private async Task InitMostPlayed()
        {
            foreach (var game in await homeService.GetMostPlayedGames())
            {
                MostPlayedGames.Add(game);
            }
            MPGame1 = MostPlayedGames.ElementAtOrDefault(0);
            MPGame2 = MostPlayedGames.ElementAtOrDefault(1);
            MPGame3 = MostPlayedGames.ElementAtOrDefault(2);
            MPGame4 = MostPlayedGames.ElementAtOrDefault(3);
            MPGame5 = MostPlayedGames.ElementAtOrDefault(4);
            MPGame6 = MostPlayedGames.ElementAtOrDefault(5);
        }
        private async Task InitLastPlayed()
        {
            foreach (var game in await homeService.GetLastPlayedGames())
            {
                LastPlayedGames.Add(game);
            }
            LPGame1 = LastPlayedGames.ElementAtOrDefault(0);
            LPGame2 = LastPlayedGames.ElementAtOrDefault(1);
            LPGame3 = LastPlayedGames.ElementAtOrDefault(2);
            LPGame4 = LastPlayedGames.ElementAtOrDefault(3);
            LPGame5 = LastPlayedGames.ElementAtOrDefault(4);
            LPGame6 = LastPlayedGames.ElementAtOrDefault(5);
        }
    }

    public class FlipRotateElement : ObservableObject
    {
        public string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string _shortname;
        public string ShortName
        {
            get { return _shortname; }
            set { SetProperty(ref _shortname, value); }
        }
        public string _type;
        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        private ObservableCollection<IConvertedID> _items;
        public ObservableCollection<IConvertedID> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        public FlipRotateElement(string name,string shortname,string type, ObservableCollection<IConvertedID> items)
        {
            this.Name = name;
            this.Type = type;
            this.ShortName = shortname;
            this.Items = items;
        }
    }
}
