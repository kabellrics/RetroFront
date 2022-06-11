using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IThemeService
    {
        void ChangeBCK(string bcppath);
        void DownloadSteamData(string dllpath, string target);
        ThemePath GetBckForTheme(string plateforme, string theme);
        ThemePath GetImageForTheme(string plateforme);
        ThemePath GetImagePathForTheme(string plateforme);
        IEnumerable<Theme> GetInstalledTheme();
        ThemePath GetLogoForTheme(string plateforme);
        ThemePath GetLogoForTheme(string plateforme, string theme);
        ThemePath GetLogoPathForTheme(string plateforme);
        ThemePath GetVidéoForTheme(string plateforme);
        ThemePath GetVidéoPathForTheme(string plateforme);
        void LoadBckForSysteme(Systeme systeme, string themename, string imgpath);
        void LoadDefaultBckForSysteme(Systeme systeme);
        void LoadLogoForSysteme(Systeme systeme, string themename, string imgpath);
        void LoadThemesForDefaultCollection();
    }
}