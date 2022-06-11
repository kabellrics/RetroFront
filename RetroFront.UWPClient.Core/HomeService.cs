using RetroFront.UWPClient.Core.APIService;
using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class HomeService
    {
        ThemeServiceAPI ThemeServiceAPI = new ThemeServiceAPI();
        SystemeServiceAPI SystemeServiceAPI = new SystemeServiceAPI();
        ImgServiceAPI ImgServiceAPI = new ImgServiceAPI();
        GameServiceAPI GameServiceAPI = new GameServiceAPI();
        ApiServiceAPI ApiServiceAPI = new ApiServiceAPI();
        EmulatorServiceAPI EmulatorServiceAPI = new EmulatorServiceAPI();
        SettingsServiceAPI SettingsServiceAPI = new SettingsServiceAPI();
        public HomeService()
        {

        }
        public async Task<IEnumerable<Systeme>> GetRandomPlateforme()
        {
            var systemes = await ApiServiceAPI.SystemeGetAsync();
            return systemes.OrderBy(arg => Guid.NewGuid()).Take(6);
        }
        public async Task<HomeDisplay> GetCurrentHomeDisplay()
        {
            var appsetting = await SettingsServiceAPI.SettingsGetAsync();
            return appsetting.CurrentHomeDisplay;
        }
        public async Task<IEnumerable<GameRom>> GetLastPlayedGames()
        {
            var games = await ApiServiceAPI.GameGetAsync();
            return games.OrderByDescending(x => x.LastStart).Take(6);
        }
        public async Task<IEnumerable<GameRom>> GetMostPlayedGames()
        {
            var games = await ApiServiceAPI.GameGetAsync();
            return games.OrderByDescending(x => x.NbTimeStarted).Take(6);
        }
        public async Task<IEnumerable<GameRom>> GetFavGames()
        {
            var games = await ApiServiceAPI.GameGetAsync();
            var favgames = games.Where(x => x.IsFavorite).OrderBy(x=>x.Name);
            return favgames.OrderBy(arg=> Guid.NewGuid()).Take(6).OrderBy(x => x.Name);
        }
    }
}
