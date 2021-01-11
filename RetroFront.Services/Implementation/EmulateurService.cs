using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class EmulateurService : IEmulateurService
    {
        public Emulator CreateEmulateur(Systeme platform, string Name, string Command, string Extension)
        {
            Emulator emu = new Emulator();
            emu.Systeme = platform;
            emu.Name = Name;
            emu.Command = Command;
            emu.Extension = Extension;
            return emu;
        }
    }
}
