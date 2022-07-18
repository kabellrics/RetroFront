using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IHelperModelService
    {
        Emulator AddExplorer(Systeme systeme);
        Emulator CreateEmulateur(Systeme platform, string Name, string Command, string Extension);
        GameRom CreateGame(string gamefile, Emulator emulator);
        Emulator DuplicateEmulator(Emulator emulator);
        GameRom DuplicateGame(GameRom game);
        string FormatExtension(Emulator ext);
        string GetImgPathForGame(GameRom game, int sGDBType);
        IEnumerable<GameRom> ImportGame(string gamelistpath, Emulator emulator);
        GameRom LookForData(GameRom game);
        GameRom ScrapeGamefromGamelist(GameRom game, string filefolder, Game gamedata);
    }
}