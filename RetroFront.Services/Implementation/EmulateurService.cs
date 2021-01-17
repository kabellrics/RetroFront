using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class EmulateurService : IEmulateurService
    {
        public Emulator CreateEmulateur(Systeme platform, string Name, string Command, string Extension)
        {
            Emulator emu = new Emulator();
            emu.Name = Name;
            emu.Command = Command;
            emu.Extension = Extension;
            return emu;
        }

        public Emulator AddExplorer(Systeme systeme)
        {
            Emulator emuexe = new Emulator();
            emuexe.Name = $"{systeme.Name} Explorer.exe";
            emuexe.Chemin = @"C:\Windows\explorer.exe";
            emuexe.Command = "%ROMPATH% --fullscreen";
            emuexe.SystemeID = systeme.SystemeID;
            emuexe.Extension = ".exe .EXE .lnk .url";
            return emuexe;
        }

        public string FormatExtension(Emulator ext)
        {
            string str = ext.Extension.Replace(".", "*.");
            var extlist = str.Split(" ").ToList();
            var exext = $"({string.Join(", ", extlist)})|{string.Join("; ", extlist)}";
            return $"Fichier pour {ext.Name} {exext} ";
        }
    }
}
