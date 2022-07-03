using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class GameService: BaseService
    {
        public GameService()
        {

        }
        public async Task<RomDisplay> GetCurrentGameDisplay()
        {
            var appsetting = await SettingsServiceAPI.SettingsGetAsync();
            return appsetting.CurrentGameDisplay;
        }
        public async Task<IEnumerable<GameRom>> GetGames(Systeme plateforme)
        {
            if (plateforme == null) 
            {
                var games = await ApiServiceAPI.GameGetAsync();
                return games.Where(x => x.IsFavorite == true && x.IsDuplicate==false).OrderBy(x=>x.Name);
            }
            else if (plateforme.Shortname == "last")
            {
                var games = await ApiServiceAPI.GameGetAsync();
                return games.Where(x => x.IsDuplicate == false).OrderByDescending(x => x.LastStart).Take(18);
            }
            else if (plateforme.Shortname == "all")
            {
                return await ApiServiceAPI.GameGetAsync(); 
            }
            else if (plateforme.Shortname == "fav")
            {
                var games = await ApiServiceAPI.GameGetAsync();
                var favgames = games.Where(x => x.IsFavorite && x.IsDuplicate == false).OrderBy(x => x.Name);
                return favgames.OrderBy(arg => Guid.NewGuid()).Take(6).OrderBy(x => x.Name);
            }
            else if (plateforme.Shortname == "most") 
            {
                var games = await ApiServiceAPI.GameGetAsync();
                return games.Where(x => x.IsDuplicate == false).OrderByDescending(x => x.NbTimeStarted).Take(18);
            }
            else if (plateforme.Shortname == "pc")
            {
                List<GameRom> pcgames = new List<GameRom>();
                var games = await ApiServiceAPI.GameGetAsync();
                var steam = await SystemeServiceAPI.GetSystemeByNameAsync("steam");
                var origin = await SystemeServiceAPI.GetSystemeByNameAsync("origin");
                var epic = await SystemeServiceAPI.GetSystemeByNameAsync("epic");
                pcgames.AddRange(await SystemeServiceAPI.GetGamesForPlateformeAsync(steam.SystemeID));
                pcgames.AddRange(await SystemeServiceAPI.GetGamesForPlateformeAsync(origin.SystemeID));
                pcgames.AddRange(await SystemeServiceAPI.GetGamesForPlateformeAsync(epic.SystemeID));
                return pcgames.OrderBy(arg => Guid.NewGuid());
            }
            else
            {
                var games = await SystemeServiceAPI.GetGamesForPlateformeAsync(plateforme.SystemeID);
                return games.OrderBy(x => x.Name);
            }
        }
    }
}
