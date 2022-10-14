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
    public class EmulatorsService: BaseService
    {

        public async Task UpdateEmulator(DisplayEmulator emulator)
        {
            await emulatorClient.EmulatorPutAsync(emulator.ID, emulator.Emulator);
        }
        public async Task<Emulator> CreateEmulator(Emulator emulator)
        {
            var result = await emulatorClient.EmulatorPostAsync(emulator);
            return result.Result;
        }
        public async Task<Systeme> CreateSysteme(Systeme sys)
        {
            var result = await systemeClient.SystemePostAsync(sys);
            return result.Result;
        }

    }
}
