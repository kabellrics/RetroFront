using RetroFront.UWPClient.Core.APIService;
using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class PlateformeService: BaseService
    {

        public PlateformeService()
        {

        }
        public async Task<IEnumerable<Systeme>> GetPlateformes(bool onlyCustomCollec = false)
        {
            if (!onlyCustomCollec)
            {
                var systemes = await ApiServiceAPI.SystemeGetAsync();
                return systemes.OrderBy(x => x.Type).ThenBy(x=>x.Name);
            }
            else
            {
                var systemes = await ApiServiceAPI.SystemeGetAsync();
                return systemes.Where(x=>x.Type == SysType._4).OrderBy(x => x.Name);
            }
        }
        public async Task<SysDisplay> GetCurrentPlateformeDisplay()
        {
            var appsetting = await SettingsServiceAPI.SettingsGetAsync();
            return appsetting.CurrentSysDisplay;
        }
    }
}
