using RetroFront.Models;
using RetroFront.Models.StandaloneEmulator;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IFileJSONService
    {
        AppSettings appSettings { get; set; }

        void ChangeCurrentTheme(Theme th);
        IEnumerable<RetroFront.Models.ScreenScraper.System.Systeme> GetAllSysFromJSON();
        ThemePath GetCurrentTheme();
        IEnumerable<StandaloneEmulator> GetStandaloneEmulators();
        RetroFront.Models.ScreenScraper.System.Systeme GetSysByCode(string shortname);
        void UpdateSettings();
        void UpdateSettings(AppSettings apps);
    }
}