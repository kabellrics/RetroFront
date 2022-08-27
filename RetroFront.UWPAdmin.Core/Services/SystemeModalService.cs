using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemeModalService : BaseService
    {

        public async Task<IEnumerable<DisplayGame>> GetInstalledSteamGame()
        {
            Emulator emu = new Emulator();
            var result = await GameAppClient.GetSteamGameAsync(emu);
            var installedsteamgames = result.Result.ToList();
            var resultcurrentsteamgames = await gameClient.GameGetAsync();
            var currentsteamgames = resultcurrentsteamgames.Result.ToList();
            var distinctgames = installedsteamgames.GroupBy(p => p.SteamID).Select(g => g.First()).ToList();
            var InstallGames = distinctgames.Select(x => new DisplayGame(x)).ToList();
            InstallGames.ForEach(game => 
            {
                if(currentsteamgames.Any(x=> x.SteamID == game.SteamID))
                {
                    game.IsSelected = true;
                }
                else
                {
                    game.IsSelected = false;
                }
            });
            return InstallGames;
        }
        public async Task<IEnumerable<DisplayGame>> GetInstalledEpicGame()
        {
            Emulator emu = new Emulator();
            var result = await GameAppClient.GetEpicGameAsync(emu);
            var installedsteamgames = result.Result.ToList();
            var resultcurrentsteamgames = await gameClient.GameGetAsync();
            var currentsteamgames = resultcurrentsteamgames.Result.ToList();
            var distinctgames = installedsteamgames.GroupBy(p => p.EpicID).Select(g => g.First()).ToList();
            var InstallGames = distinctgames.Select(x => new DisplayGame(x)).ToList();
            InstallGames.ForEach(game => 
            {
                if(currentsteamgames.Any(x=> x.EpicID == game.Game.EpicID))
                {
                    game.IsSelected = true;
                }
                else
                {
                    game.IsSelected = false;
                }
            });
            return InstallGames;
        }
        public async Task<IEnumerable<DisplayGame>> GetInstalledOriginGame()
        {
            Emulator emu = new Emulator();
            var result = await GameAppClient.GetOriginGameAsync(emu);
            var installedsteamgames = result.Result.ToList();
            var resultcurrentsteamgames = await gameClient.GameGetAsync();
            var currentsteamgames = resultcurrentsteamgames.Result.ToList();
            var distinctgames = installedsteamgames.GroupBy(p => p.OriginID).Select(g => g.First()).ToList();
            var InstallGames = distinctgames.Select(x => new DisplayGame(x)).ToList();
            InstallGames.ForEach(game => 
            {
                if(currentsteamgames.Any(x=> x.OriginID == game.Game.OriginID))
                {
                    game.IsSelected = true;
                }
                else
                {
                    game.IsSelected = false;
                }
            });
            return InstallGames;
        }
    }
}
