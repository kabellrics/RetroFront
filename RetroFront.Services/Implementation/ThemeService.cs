using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RetroFront.Services.Implementation
{
    public class ThemeService : IThemeService
    {
        private FileJSONService FileJSONService = new FileJSONService();

        public IEnumerable<Theme> GetInstalledTheme()
        {
            var themes = Directory.EnumerateDirectories(System.IO.Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes"));
            TextInfo textInfo = new CultureInfo("fr-FR", false).TextInfo;
            foreach (var theme in themes)
            {
                Theme th = new Theme();
                th.FolderName = new DirectoryInfo(theme).Name;
                th.Name = textInfo.ToTitleCase(th.FolderName.Replace("_", " "));
                yield return th;
            }
        }
        public void LoadDefaultBckForSysteme(Systeme systeme)
        {
            try
            {
                var defaulttheme = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", "Space", systeme.Shortname);
                Directory.CreateDirectory(defaulttheme);
                if (systeme.Type == SysType.Arcade)
                {
                    File.Copy(@"default_BCK.jpg", Path.Combine(defaulttheme, "bck.jpg"), true);
                }
                else if (systeme.Type == SysType.Console)
                {
                    File.Copy(@"default_BCK2.jpg", Path.Combine(defaulttheme, "bck.jpg"), true);
                }
                else if (systeme.Type == SysType.ConsolePortable)
                {
                    File.Copy(@"default_BCK3.jpg", Path.Combine(defaulttheme, "bck.jpg"), true);
                }
                else if (systeme.Type == SysType.GameStore)
                {
                    File.Copy(@"default_BCK4.jpg", Path.Combine(defaulttheme, "bck.jpg"), true);
                }
                else if (systeme.Type == SysType.Collection)
                {
                    File.Copy(@"default_BCK5.jpg", Path.Combine(defaulttheme, "bck.jpg"), true);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public void LoadThemesForDefaultCollection()
        {
            try
            {
                var allthemes = Directory.EnumerateDirectories(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes"));
                foreach (string theme in allthemes)
                {
                    Directory.CreateDirectory(Path.Combine(theme, "all"));
                    Directory.CreateDirectory(Path.Combine(theme, "fav"));
                    Directory.CreateDirectory(Path.Combine(theme, "last"));
                    Directory.CreateDirectory(Path.Combine(theme, "most"));
                    try
                    {
                        File.Copy(@"retro-game.jpg", Path.Combine(Path.Combine(theme, "all"), "bck.jpg"), true);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                    try
                    {
                        File.Copy(@"retro-game.jpg", Path.Combine(Path.Combine(theme, "fav"), "bck.jpg"), true);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                    try
                    {
                        File.Copy(@"retro-game.jpg", Path.Combine(Path.Combine(theme, "last"), "bck.jpg"), true);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                    try
                    {
                        File.Copy(@"retro-game.jpg", Path.Combine(Path.Combine(theme, "most"), "bck.jpg"), true);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                }
                Directory.CreateDirectory(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "all"));
                Directory.CreateDirectory(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "fav"));
                Directory.CreateDirectory(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "last"));
                Directory.CreateDirectory(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "most"));
                //File.Create(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "all", $"logo.png"));
                //File.Create(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "fav", $"logo.png"));
                try
                {
                    File.Copy(@"AllGames.png", Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "all", $"logo.png"), true);
                }
                catch (Exception ex)
                {
                    //throw;
                }
                try
                {
                    File.Copy(@"AllGames.png", Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "most", $"logo.png"), true);
                }
                catch (Exception ex)
                {
                    //throw;
                }
                try
                {
                    File.Copy(@"star.png", Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "fav", $"logo.png"), true);
                }
                catch (Exception ex)
                {
                    //throw;
                }
                try
                {
                    File.Copy(@"time.png", Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "last", $"logo.png"), true);
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public void LoadLogoForSysteme(Systeme systeme, string themename, string imgpath)
        {
            try
            {
                var defaulttheme = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", themename, systeme.Shortname);
                Directory.CreateDirectory(defaulttheme);
                File.Copy(imgpath, Path.Combine(defaulttheme, "logo.png"), true);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public void LoadBckForSysteme(Systeme systeme, string themename, string imgpath)
        {
            try
            {
                var defaulttheme = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", themename, systeme.Shortname);
                Directory.CreateDirectory(defaulttheme);
                File.Copy(imgpath, Path.Combine(defaulttheme, "bck.jpg"), true);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public string GetLogoForTheme(string plateforme, string theme)
        {
            try
            {
                if (File.Exists(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "logo.png")))
                    return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "logo.png");
                else
                    return GetLogoForTheme(plateforme);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetBckForTheme(string plateforme, string theme)
        {
            try
            {
                if (File.Exists(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "bck.png")))
                    return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "bck.png");
                else if (File.Exists(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "bck.jpg")))
                    return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "bck.jpg");
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetLogoForTheme(string plateforme)
        {
            try
            {
                if (File.Exists(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "logo.png")))
                    return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "logo.png");
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetLogoPathForTheme(string plateforme)
        {
            try
            {
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, $"{Guid.NewGuid().ToString()}.png");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetVidéoForTheme(string plateforme)
        {
            try
            {
                if (File.Exists(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "video.mp4")))
                    return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "video.mp4");
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetVidéoPathForTheme(string plateforme)
        {
            try
            {
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, $"{Guid.NewGuid().ToString()}.mp4");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetImagePathForTheme(string plateforme)
        {
            try
            {
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, $"{Guid.NewGuid().ToString()}.png");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetImageForTheme(string plateforme)
        {
            try
            {
                if (File.Exists(Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "img.png")))
                    return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "img.png");
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public void DownloadSteamData(string dllpath, string target)
        {
            using (var file = File.Create(target))
            {
                using (WebClient client = new WebClient())
                {
                    file.Write(client.DownloadData(dllpath));
                }
            }
        }

        public void ChangeBCK(string bcppath)
        {
            string fileext = Path.GetExtension(bcppath);
            string oldtarget = FileJSONService.appSettings.DefaultBCK;
            string target = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, string.Format($"{Guid.NewGuid()}{fileext}"));
            File.Copy(bcppath, target, true);
            FileJSONService.appSettings.DefaultBCK = target;
            FileJSONService.UpdateSettings();
        }

    }
}
