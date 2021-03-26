using RetroFront.Models;
using RetroFront.Models.ScreenScraper;
using RetroFront.Models.ScreenScraper.GameSearch;
using RetroFront.Models.ScreenScraper.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.Services.Interface
{
    public interface IScreenScraperService
    {
        Task<List<Models.ScreenScraper.System.Systeme>> GetSSCPSystemes();
        IEnumerable<GameRom> SearchGame(string name);
        Jeux GetJeuxDetail(int id);
        void DownloadSteamData(string dllpath, string target);
        string GetSystemeImgDLL(string type, string SCSPSysID);
        string GetSystemeVideoDLL(string SCSPSysID);
    }
}
