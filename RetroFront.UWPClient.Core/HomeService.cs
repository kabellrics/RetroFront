using RetroFront.UWPClient.Core.APIService;
using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class HomeService: BaseService
    {
        public HomeService()
        {

        }
        public async Task<IEnumerable<Systeme>> GetRandomPlateforme()
        {
            var systemes = await ApiServiceAPI.SystemeGetAsync();
            return systemes.Where(x => x.Type != SysType._4).OrderBy(arg => Guid.NewGuid()).Take(6);
        }
        public async Task<HomeDisplay> GetCurrentHomeDisplay()
        {
            var appsetting = await SettingsServiceAPI.SettingsGetAsync();
            return appsetting.CurrentHomeDisplay;
        }
        public async Task<IEnumerable<GameRom>> GetLastPlayedGames()
        {
            var games = await ApiServiceAPI.GameGetAsync();
            return games.Where(x => x.IsDuplicate == false).OrderByDescending(x => x.LastStart).Take(6);
        }
        public async Task<IEnumerable<GameRom>> GetMostPlayedGames()
        {
            var games = await ApiServiceAPI.GameGetAsync();
            return games.Where(x => x.IsDuplicate == false).OrderByDescending(x => x.NbTimeStarted).Take(6);
        }
        public async Task<IEnumerable<GameRom>> GetFavGames()
        {
            var games = await ApiServiceAPI.GameGetAsync();
            var favgames = games.Where(x => x.IsFavorite && x.IsDuplicate == false).OrderBy(x=>x.Name);
            return favgames.OrderBy(arg=> Guid.NewGuid()).Take(6).OrderBy(x => x.Name);
        }
        public async Task<IEnumerable<Systeme>> GetRandomCustomCollection()
        {
            var systemes = await ApiServiceAPI.SystemeGetAsync();
            return systemes.Where(x=>x.Type == SysType._4).OrderBy(arg => Guid.NewGuid()).Take(6);
        }
        public async Task<IEnumerable<GameRom>> GetRandomPCGames()
        {
            List<GameRom> pcgames = new List<GameRom>();
            var games = await ApiServiceAPI.GameGetAsync();
            var steam = await SystemeServiceAPI.GetSystemeByNameAsync("steam");
            var origin = await SystemeServiceAPI.GetSystemeByNameAsync("origin");
            var epic = await SystemeServiceAPI.GetSystemeByNameAsync("epic");
            pcgames.AddRange(await SystemeServiceAPI.GetGamesForPlateformeAsync(steam.SystemeID));
            pcgames.AddRange(await SystemeServiceAPI.GetGamesForPlateformeAsync(origin.SystemeID));
            pcgames.AddRange(await SystemeServiceAPI.GetGamesForPlateformeAsync(epic.SystemeID));
            return pcgames.OrderBy(arg => Guid.NewGuid()).Take(6);
        }
    }
}
