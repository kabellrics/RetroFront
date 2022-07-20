using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class GameDetailService
    {
        private GameClient gameClient = new GameClient();
        public async Task<DisplayGame> GetGame(int ID)
        {
            var sys = await gameClient.GameGetAsync(ID);
            return new DisplayGame(sys.Result);
        }
    }
}
