using RetroFront.UWPClient.Core.APIService;
using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class PlateformeService
    {
        ThemeServiceAPI ThemeServiceAPI = new ThemeServiceAPI();
        SystemeServiceAPI SystemeServiceAPI = new SystemeServiceAPI();
        ImgServiceAPI ImgServiceAPI = new ImgServiceAPI();
        GameServiceAPI GameServiceAPI = new GameServiceAPI();
        ApiServiceAPI ApiServiceAPI = new ApiServiceAPI();
        EmulatorServiceAPI EmulatorServiceAPI = new EmulatorServiceAPI();
        SettingsServiceAPI SettingsServiceAPI = new SettingsServiceAPI();
        public PlateformeService()
        {

        }
        public async Task<IEnumerable<Systeme>> GetPlateformes()
        {
            var systemes = await ApiServiceAPI.SystemeGetAsync();
            return systemes.OrderBy(x=>x.Name);
        }
    }
}
