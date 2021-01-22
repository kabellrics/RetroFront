using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
                if(systeme.Type == SysType.Arcade)
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
        public void LoadBckForSysteme(Systeme systeme,string themename,string imgpath)
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

        public string GetLogoForTheme(string plateforme,string theme)
        {
            try
            {
                var xmltheme = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "theme.xml");
                var doc = XDocument.Load(xmltheme);
                var results = doc.Element("theme")
                    .Elements("view")
                    .First(e => (string)e.Attribute("name") == "system");
                var logonode = results.Elements("image")
                    .First(e => (string)e.Attribute("name") == "logo");
                var logopath = logonode.Element("path").Value;
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, logopath.Substring(2).Replace("/", "\\"));
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
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme,"bck.jpg");
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
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "logo.png");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetWheelCarbonForTheme(string plateforme)
        {
            try
            {
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "wheelcarbon.png");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetWheelSteelForTheme(string plateforme)
        {
            try
            {
                return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme, "wheelsteel.png");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
