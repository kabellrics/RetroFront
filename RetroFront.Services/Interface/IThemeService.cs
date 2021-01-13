using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IThemeService
    {
        IEnumerable<Theme> GetInstalledTheme();
        Theme GetCurrentTheme();
        string GetLogoForTheme(string plateforme, string theme);
        string GetBckForTheme(string plateforme, string theme);
    }
}