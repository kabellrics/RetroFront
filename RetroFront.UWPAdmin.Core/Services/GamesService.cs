using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class GamesService
    {
        private GameClient gameClient = new GameClient();
        private EmulatorClient emulatorClient = new EmulatorClient();
        private SystemeClient systemeClient = new SystemeClient();
        public async Task<IEnumerable<DisplayGame>> GetGame()
        {
            var result = await gameClient.GameGetAsync();
            return result.Result.Select(x => new DisplayGame(x));
        }
        public async Task<IEnumerable<DisplayGroupedGame>> GetGroupedBySystemes()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            var gamesSW = await gameClient.GameGetAsync();
            IEnumerable<GameRom> games = gamesSW.Result;
            var systemesSW = await systemeClient.SystemeGetAsync();
            IEnumerable<Systeme> systemes = systemesSW.Result;
            foreach(var sys in systemes)
            {
                var gamesinSystemeSW = await systemeClient.GetGamesForPlateformeAsync(sys.SystemeID);
                var gamesinSysteme = gamesinSystemeSW.Result;
                groupedGames.Add(new DisplayGroupedGame(sys.Name, gamesinSysteme.Select(x => new DisplayGame(x))));
            }
            //var results = from game in games
            //              group game by game.Plateforme into grp
            //              orderby grp.Key
            //              select new DisplayGroupedGame(grp.Key,grp.ToList().Select(x=>new DisplayGame(x)));
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.OrderBy(x=>x.Name).Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public async Task<IEnumerable<DisplayGroupedGame>> GetGroupedByEmulators()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            var gamesSW = await gameClient.GameGetAsync();
            IEnumerable<GameRom> games = gamesSW.Result;
            var results = from game in games
                          group game by game.EmulatorID into grp
                          orderby grp.Key
                          select new {Key = grp.Key,Items = grp.ToList().OrderBy(x => x.Name) };
            foreach(var element in results)
            {
                var emulatorSW = await emulatorClient.EmulatorGetAsync(element.Key);
                var emulator = emulatorSW.Result;
                groupedGames.Add(new DisplayGroupedGame(emulator.Name, element.Items.Select(x => new DisplayGame(x))));
            }

            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games.Select(x => new DisplayGame(x))));
            return groupedGames;
        }
        public async Task<IEnumerable<DisplayGroupedGame>> GetGroupedByGenres()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            var gamesSW = await gameClient.GameGetAsync();
            IEnumerable<DisplayGame> games = gamesSW.Result.Select(x => new DisplayGame(x));
            var genresstr = string.Join(",", games.Select(x => x.Genre));
            var genres = genresstr.Split(new Char[] { ',', '/'}).Distinct();
            var genresUpper = genres.Select(x => x.Trim()).Distinct();
            foreach (var g in genresUpper.OrderBy(x => x))
            {
                //var gamesIngenres = games.Where(x => genresUpper.Any(s => x.Genre.Contains(s)));
                var gamesIngenres = games.Where(x => x.Genre.Contains(g));
                groupedGames.Add(new DisplayGroupedGame(g, gamesIngenres.OrderBy(x => x.Name)));
            }
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games));
            return groupedGames;
        }
        public async Task<IEnumerable<DisplayGroupedGame>> GetGroupedByYears()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            var gamesSW = await gameClient.GameGetAsync();
            IEnumerable<DisplayGame> games = gamesSW.Result.Select(x => new DisplayGame(x));
            var results = from game in games
                          group game by game.Year into grp
                          orderby grp.Key
                          select new DisplayGroupedGame(grp.Key, grp.ToList().OrderBy(x => x.Name));
            groupedGames.AddRange(results);
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games));
            return groupedGames;
        }
        public async Task<IEnumerable<DisplayGroupedGame>> GetGroupedByDevs()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            var gamesSW = await gameClient.GameGetAsync();
            IEnumerable<DisplayGame> games = gamesSW.Result.Select(x => new DisplayGame(x));
            var results = from game in games
                          group game by game.Developpeur into grp
                          orderby grp.Key
                          select new DisplayGroupedGame(grp.Key, grp.ToList().OrderBy(x => x.Name));
            groupedGames.AddRange(results);
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games));
            return groupedGames;
        }
        public async Task<IEnumerable<DisplayGroupedGame>> GetGroupedByEditeurs()
        {
            List<DisplayGroupedGame> groupedGames = new List<DisplayGroupedGame>();
            var gamesSW = await gameClient.GameGetAsync();
            IEnumerable<DisplayGame> games = gamesSW.Result.Select(x => new DisplayGame(x));
            var results = from game in games
                          group game by game.Editeur into grp
                          orderby grp.Key
                          select new DisplayGroupedGame(grp.Key, grp.ToList().OrderBy(x => x.Name));
            groupedGames.AddRange(results);
            groupedGames.Add(new DisplayGroupedGame("Tous les jeux", games));
            return groupedGames;
        }
        public async Task UpdateGame(DisplayGame game)
        {
            await gameClient.GamePutAsync(game.ID, game.Game);
        }
    }
}
