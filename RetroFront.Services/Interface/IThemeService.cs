using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IThemeService
    {
        IEnumerable<Theme> GetInstalledTheme();
        string GetLogoForTheme(string plateforme, string theme);
        string GetBckForTheme(string plateforme, string theme);
        void LoadDefaultBckForSysteme(Systeme systeme);
        void LoadBckForSysteme(Systeme systeme, string themename, string imgpath);
        string GetLogoForTheme(string plateforme);

    }
}