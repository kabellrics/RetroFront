using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class EmulatorsService
    {
        private EmulatorClient emulatorClient = new EmulatorClient();
        public IEnumerable<DisplayEmulator> GetEmulators()
        {
            foreach (var sys in emulatorClient.EmulatorGet().Result.OrderBy(x => x.Name).ThenBy(x => x.Command))
            {
                yield return new DisplayEmulator(sys);
            }
        }
    }
}
