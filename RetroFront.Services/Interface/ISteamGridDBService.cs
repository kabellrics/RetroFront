using RetroFront.Models.SteamGridDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface ISteamGridDBService
    {
        IEnumerable<DataSearch> SearchByName(string name);
        DataSearch GetGameSteamId(string steamId);
        IEnumerable<ImgResult> GetHeroesForId(int gameId);
        IEnumerable<ImgResult> GetLogoForId(int gameId);
        IEnumerable<ImgResult> GetGridBoxartForId(int gameId);
        IEnumerable<ImgResult> GetGridFanartForId(int gameId);
    }
}
