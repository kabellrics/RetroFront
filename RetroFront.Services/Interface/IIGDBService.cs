using RetroFront.Models;
using RetroFront.Models.IGDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.Services.Interface
{
    public interface IIGDBService
    {
        string GetArtWorkLink(string hash);
        string GetCoverLink(string hash);
        IGDBGame GetDetailsGame(int id);
        IEnumerable<Search> GetGameByName(string name);
        IEnumerable<Search> GetPlatformByName(string name);
        IEnumerable<Models.IGDB.Artwork> GetArtworksByGameId(int id);
        IEnumerable<Models.IGDB.Genre> GetGenresByGameId(int id);
        IEnumerable<Models.IGDB.Screenshot> GetScreenshotsByGameId(int id);
        IEnumerable<Models.IGDB.Video> GetVideosByGameId(int id);
        IEnumerable<Models.IGDB.Theme> GetThemesByGameId(int id);
    }
}
