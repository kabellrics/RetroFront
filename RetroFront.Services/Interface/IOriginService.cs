using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface IOriginService
    {
        List<GameRom> GetOriginGame(Emulator emu);
    }
}
