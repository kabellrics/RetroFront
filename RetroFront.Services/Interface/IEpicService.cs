using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface IEpicService
    {
        List<GameRom> GetEpicGame(Emulator emu);
    }
}
