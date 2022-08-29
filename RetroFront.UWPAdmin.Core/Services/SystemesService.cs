using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemesService: BaseService
    {
        public async Task UpdateSysteme(DisplaySysteme system)
        {
            await systemeClient.SystemePutAsync(system.ID, system.Systeme);
        }
        public async Task<DisplaySysteme> CreateSysteme(Systeme system)
        {
            return new DisplaySysteme((await systemeClient.SystemePostAsync(system)).Result);
        }
        public async Task<Emulator> CreateEmulator(Emulator emulator)
        {
            return (await emulatorClient.EmulatorPostAsync(emulator)).Result;
        }
        public async Task<DisplaySysteme> GetSystemeByShortname(string shortname)
        {
            var result = await systemeClient.SystemeGetAsync();
            var sys = result.Result;
            var dissys = sys.FirstOrDefault(x=> x.Shortname == shortname);
            if (dissys == null)
                return null;
            else
                return new DisplaySysteme(dissys);
        }
    }
}
