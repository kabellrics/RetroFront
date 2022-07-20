using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class EmulatorsService
    {
        private EmulatorClient emulatorClient = new EmulatorClient();
        public async Task<IEnumerable<DisplayEmulator>> GetEmulators()
        {
            List<DisplayEmulator> displayEmus = new List<DisplayEmulator>();
            var result = await emulatorClient.EmulatorGetAsync();
            foreach (var sys in result.Result.OrderBy(x => x.Name).ThenBy(x => x.Command))
            {
                displayEmus.Add(new DisplayEmulator(sys));
            }
            return displayEmus;
        }
    }
}
