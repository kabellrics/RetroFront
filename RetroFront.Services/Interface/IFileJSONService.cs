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
        IEnumerable<Models.ScreenScraper.System.Systeme> GetAllSysFromJSON();
    }
}