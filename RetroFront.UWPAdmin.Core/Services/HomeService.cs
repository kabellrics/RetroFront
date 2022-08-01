using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class HomeService: BaseService
    {

        public async Task<IEnumerable<DisplayGame>> GetGameWithoutIGDB()
        {
            var result = await gameClient.GameGetAsync();
            return result.Result.Where(x=>x.Igdbid < 1).Select(x => new DisplayGame(x));
        }
        public async Task<IEnumerable<DisplayGame>> GetGameWithoutSGDB()
        {
            var result = await gameClient.GameGetAsync();
            return result.Result.Where(x => x.Sgdbid < 1).Select(x => new DisplayGame(x));
        }
        public async Task<IEnumerable<DisplayGame>> GetGameWithoutSSDB()
        {
            var result = await gameClient.GameGetAsync();
            return result.Result.Where(x => x.ScreenScraperID < 1).Select(x => new DisplayGame(x));
        }
    }
}
