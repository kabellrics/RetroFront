using RetroFront.Models;
using RetroFront.Models.IGDB;
using RetroFront.Models.ScreenScraper.GameSearch;
using RetroFront.Models.SteamGridDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IGameDataProviderService
    {
        void DownloadSteamData(string dllpath, string target);
        string GetArtWorkLink(string hash);
        IEnumerable<Artwork> GetArtworksByGameId(int id);
        string GetCoverLink(string hash);
        IGDBGame GetDetailsGame(int id);
        IEnumerable<Company> GetDevByGameId(int id);
        IEnumerable<Search> GetGameByName(string name);
        Search GetGameSteamId(string steamId);
        IEnumerable<RetroFront.Models.IGDB.Genre> GetGenresByGameId(int id);
        IEnumerable<ImgResult> GetGridBannerForId(int gameId);
        IEnumerable<ImgResult> GetGridBoxartForId(int gameId);
        IEnumerable<ImgResult> GetHeroesForId(int gameId);
        IEnumerable<InvolvedCompany> GetInvolvedCompanyByGameId(int id);
        Jeux GetJeuxDetail(int id);
        IEnumerable<ImgResult> GetLogoForId(int gameId);
        IEnumerable<Search> GetPlatformByName(string name);
        IEnumerable<Company> GetPublishersByGameId(int id);
        IEnumerable<RetroFront.Models.IGDB.Screenshot> GetScreenshotsByGameId(int id);
        Task<List<RetroFront.Models.ScreenScraper.System.Systeme>> GetSSCPSystemes();
        string GetSystemeImgDLL(string type, string SCSPSysID);
        string GetSystemeVideoDLL(string SCSPSysID);
        IEnumerable<RetroFront.Models.IGDB.Theme> GetThemesByGameId(int id);
        IEnumerable<Video> GetVideosByGameId(int id);
        IEnumerable<Search> SearchByName(string name);
        IEnumerable<GameRom> SearchGame(string name);
    }
}