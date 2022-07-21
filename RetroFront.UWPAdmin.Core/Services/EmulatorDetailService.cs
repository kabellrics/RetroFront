﻿using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class EmulatorDetailService
    {
        private EmulatorClient emulatorClient = new EmulatorClient();
        public DisplayEmulator GetEmulator(int ID)
        {
            var sys = emulatorClient.EmulatorGet(ID).Result;
            return new DisplayEmulator(sys);
        }
        public async Task UpdateSysteme(DisplayEmulator emulator)
        {
            await emulatorClient.EmulatorPutAsync(emulator.ID, emulator.Emulator);
        }
    }
}
