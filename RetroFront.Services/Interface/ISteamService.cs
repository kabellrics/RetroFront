﻿using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface ISteamService
    {
        List<GameRom> GetSteamGame(Emulator emu);
        GameRom GetSteamInfos(GameRom game, Emulator emu);
        void DownloadSteamData(string dllpath, string target);
    }
}
