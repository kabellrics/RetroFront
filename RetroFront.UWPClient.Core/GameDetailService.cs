using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class GameDetailService : BaseService
    {
        public async Task<GameDetailDisplay> GetCurrentGameDisplay()
        {
            var appsetting = await SettingsServiceAPI.SettingsGetAsync();
            return appsetting.CurrentGameDetailDisplay;
        }
        public async Task<GameRom> GetGame(GameRom game)
        {
                return await ApiServiceAPI.GameGetAsync(game.Id);
        }
        public async Task<Systeme> GetTruePlateforme(GameRom game)
        {
                var dbgame = await ApiServiceAPI.GameGetAsync(game.Id);
            var DirectEmu = await ApiServiceAPI.EmulatorGetAsync(game.EmulatorID);
            if (DirectEmu.IsDuplicate == true)
            {
                var emus = await ApiServiceAPI.EmulatorGetAsync();
                DirectEmu = emus.First(x=>x.Chemin == DirectEmu.Chemin && x.IsDuplicate == false);
            }
            return await ApiServiceAPI.SystemeGetAsync(DirectEmu.SystemeID);
        }
    }
}
