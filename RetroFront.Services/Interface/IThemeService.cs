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
        void LoadThemesForDefaultCollection();
        void LoadBckForSysteme(Systeme systeme, string themename, string imgpath);
        void LoadLogoForSysteme(Systeme systeme, string themename, string imgpath);
        string GetLogoForTheme(string plateforme);
        string GetLogoPathForTheme(string plateforme);
        string GetVidéoForTheme(string plateforme);
        string GetVidéoPathForTheme(string plateforme);
        string GetImagePathForTheme(string plateforme);
        string GetImageForTheme(string plateforme);
        void DownloadSteamData(string dllpath, string target);
        void ChangeBCK(string bcppath);
    }
}