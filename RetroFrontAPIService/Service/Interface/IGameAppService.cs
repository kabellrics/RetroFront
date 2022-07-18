using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IGameAppService
    {
        List<GameRom> GetEpicGame(Emulator emu);
        List<GameRom> GetOriginGame(Emulator emu);
        List<GameRom> GetSteamGame(Emulator emu);
        GameRom GetSteamInfos(GameRom game);
    }
}