using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IGameService
    {
        GameRom CreateGame(string gamefile, Emulator emulator);
        GameRom ScrapeGame(GameRom game);
        IEnumerable<GameRom> ImportGame(string gamelistpath, Emulator emulator);
        GameRom DuplicateGame(GameRom game);
        string DownloadImgData(string dllpath, string target);
        string GetImgPathForGame(GameRom game, ScraperType sGDBType);
    }
}