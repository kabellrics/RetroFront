using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IFileJSONService
    {
        AppSettings appSettings { get; set; }
        void ChangeCurrentTheme(Theme th);
        string GetCurrentTheme();
        void UpdateSettings(AppSettings apps);
        void UpdateSettings();
        Models.ScreenScraper.System.Systeme GetSysByCode(string shortname);
        IEnumerable<Models.ScreenScraper.System.Systeme> GetAllSysFromJSON();
        IEnumerable<Models.StandaloneEmulator.StandaloneEmulator> GetStandaloneEmulators();
    }
}