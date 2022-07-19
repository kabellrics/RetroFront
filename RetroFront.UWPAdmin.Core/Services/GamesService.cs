using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class GamesService
    {
        private GameClient gameClient = new GameClient();
        private EmulatorClient emulatorClient = new EmulatorClient();
        private SystemeClient systemeClient = new SystemeClient();
        public IEnumerable<DisplayGame> GetGame()
        {
            return gameClient.GameGet().Result.Select(x => new DisplayGame(x));
        }
        public IEnumerable<DisplayGroupedGame> GetGroupedBySystemes()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            IEnumerable<GameRom> games = gameClient.GameGet().Result;
            IEnumerable<Systeme> systemes = systemeClient.SystemeGet().Result;
            foreach(var sys in systemes)
            {
                var gamesinSysteme = systemeClient.GetGamesForPlateforme(sys.SystemeID).Result;
                groupedGames.Add(new DisplayGroupedGame(sys.Name, gamesinSysteme.Select(x => new DisplayGame(x))));
            }
            //var results = from game in games
            //              group game by game.Plateforme into grp
            //              orderby grp.Key
            //              select new DisplayGroupedGame(grp.Key,grp.ToList().Select(x=>new DisplayGame(x)));
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.OrderBy(x=>x.Name).Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public IEnumerable<DisplayGroupedGame> GetGroupedByEmulators()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            IEnumerable<GameRom> games = gameClient.GameGet().Result;
            var results = from game in games
                          group game by game.EmulatorID into grp
                          orderby grp.Key
                          select new {Key = grp.Key,Items = grp.ToList().OrderBy(x => x.Name) };
            foreach(var element in results)
            {
                var emulator = emulatorClient.EmulatorGet(element.Key).Result;
                groupedGames.Add(new DisplayGroupedGame(emulator.Name, element.Items.Select(x => new DisplayGame(x))));
            }

            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public IEnumerable<DisplayGroupedGame> GetGroupedByGenres()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            IEnumerable<GameRom> games = gameClient.GameGet().Result;
            var genresstr = string.Join(",", games.Select(x => x.Genre));
            var genres = genresstr.Split(new Char[] { ',', '/',' ' });
            foreach(var g in genres.OrderBy(x=>x))
            {
                var gamesIngenres = games.Where(x => genres.Any(s => x.Genre.Contains(s)));
                groupedGames.Add(new DisplayGroupedGame(g, gamesIngenres.OrderBy(x => x.Name).Select(x => new DisplayGame(x))));
            }
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public IEnumerable<DisplayGroupedGame> GetGroupedByYears()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            IEnumerable<GameRom> games = gameClient.GameGet().Result;
            var results = from game in games
                          group game by game.Year into grp
                          orderby grp.Key
                          select new DisplayGroupedGame(grp.Key, grp.ToList().OrderBy(x => x.Name).Select(x => new DisplayGame(x)));
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public IEnumerable<DisplayGroupedGame> GetGroupedByDevs()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            IEnumerable<GameRom> games = gameClient.GameGet().Result;
            var devSTR = string.Join(",", games.Select(x => x.Dev));
            var devs = devSTR.Split(new Char[] { ',', '/', ' ' });
            foreach (var g in devs.OrderBy(x => x))
            {
                var gamesInDev = games.Where(x => devs.Any(s => x.Dev.Contains(s)));
                groupedGames.Add(new DisplayGroupedGame(g, gamesInDev.OrderBy(x => x.Name).Select(x => new DisplayGame(x))));
            }
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public IEnumerable<DisplayGroupedGame> GetGroupedByEditeurs()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            IEnumerable<GameRom> games = gameClient.GameGet().Result;
            var editeurSTR = string.Join(",", games.Select(x => x.Editeur));
            var editeurs = editeurSTR.Split(new Char[] { ',', '/', ' ' });
            foreach (var g in editeurs.OrderBy(x => x))
            {
                var gamesInEditeur = games.Where(x => editeurs.Any(s => x.Editeur.Contains(s)));
                groupedGames.Add(new DisplayGroupedGame(g, gamesInEditeur.OrderBy(x => x.Name).Select(x => new DisplayGame(x))));
            }
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.Select(x => new DisplayGame(x))));
            return groupedGames;
        }
    }
}
