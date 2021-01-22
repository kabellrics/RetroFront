using RetroFront.Models.SteamGridDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface ISteamGridDBService
    {
        IEnumerable<DataSearch> SearchByName(string name);
        IEnumerable<DataSearch> GetGameSteamId(int steamId);
        IEnumerable<ImgResult> GetHeroesForId(int gameId);
        IEnumerable<ImgResult> GetLogoForId(int gameId);
        IEnumerable<ImgResult> GetGridsForId(int gameId);
    }
}
