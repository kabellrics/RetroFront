﻿using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class GameDetailService: BaseService
    {
        public async Task<DisplayGame> GetGame(int ID)
        {
            var sys = await gameClient.GameGetAsync(ID);
            return new DisplayGame(sys.Result);
        }
        public async Task UpdateGame(DisplayGame game)
        {
            await gameClient.GamePutAsync(game.ID, game.Game);
        }
        public async Task<String> GetNewImgPath(DisplayGame game, int sGDBType)
        {
            var result = await helperModelClient.GetImgPathForGameAsync(sGDBType, game.Game);
            return result.Result.Path;
        }
    }
}
