using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemeDetailService: BaseService
    {
        public DisplaySysteme GetSysteme(int ID)
        {
            var sys = systemeClient.SystemeGet(ID).Result;
            return new DisplaySysteme(sys);
        }
        public IEnumerable<DisplayEmulator> GetEmus(int ID)
        {
            var result = emulatorClient.EmulatorGet();
            var emus = result.Result;
            return emus.Where(x=>x.SystemeID == ID).Select(x=>new DisplayEmulator(x));
        }
        public async Task UpdateSysteme(DisplaySysteme system)
        {
            await systemeClient.SystemePutAsync(system.ID, system.Systeme);
        }
        public async Task<String> GetNewLogoPath(DisplaySysteme system)
        {
            var result = await themeClient.GetLogoPathForThemeAsync(system.Systeme.Shortname);
            return result.Result.Path;
        }
        public async Task<String> GetNewScreenshootPath(DisplaySysteme system)
        {
            var result = await themeClient.GetImagePathForThemeAsync(system.Systeme.Shortname);
            return result.Result.Path;
        }
        public async Task ExportToPegasus(DisplaySysteme system)
        {
            await externalAppClient.ExportToPegasusAsync(system.ID);
        }
    }
}
