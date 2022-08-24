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
    public class EmulatorDetailService: BaseService
    {
        public DisplayEmulator GetEmulator(int ID)
        {
            var sys = emulatorClient.EmulatorGet(ID).Result;
            return new DisplayEmulator(sys);
        }
        public async Task UpdateEmulator(DisplayEmulator emulator)
        {
            await emulatorClient.EmulatorPutAsync(emulator.ID, emulator.Emulator);
        }
        public async Task<GameRom> CreateGame(GameRom game)
        {
            return (await gameClient.GamePostAsync(game)).Result;
        }
    }
}
