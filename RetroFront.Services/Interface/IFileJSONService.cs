using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IFileJSONService
    {
        AppSettings appSettings { get; set; }
        IEnumerable<Systeme> GetAllSysFromJSON();
        void ChangeCurrentTheme(Theme th);
        string GetCurrentTheme();
        void UpdateSettings(AppSettings apps);
    }
}