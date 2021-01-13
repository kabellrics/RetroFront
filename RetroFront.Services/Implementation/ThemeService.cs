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

        public Theme GetCurrentTheme()
        {
            var themes = GetInstalledTheme().ToList();
            var currenttheme = FileJSONService.appSettings.CurrentTheme;
            return themes.FirstOrDefault(x => x.FolderName == currenttheme);
        }

        public string GetLogoForTheme(string plateforme,string theme)
        {
            var xmltheme = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "theme.xml");
            var doc = XDocument.Load(xmltheme);
            var results = doc.Element("theme")
                .Elements("view")
                .First(e => (string)e.Attribute("name") == "system");               
            var logonode = results.Elements("image")
                .First(e => (string)e.Attribute("name") == "logo");
            var logopath = logonode.Element("path").Value;
            return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme,logopath.Substring(2).Replace("/","\\"));
        }
        public string GetBckForTheme(string plateforme, string theme)
        {
            var xmltheme = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, "theme.xml");
            var doc = XDocument.Load(xmltheme);
            var results = doc.Element("theme")
                .Elements("view")
                .First(e => (string)e.Attribute("name") == "system");
            var logonode = results.Elements("image")
                .First(e => (string)e.Attribute("name") == "background");
            var logopath = logonode.Element("path").Value;
            return Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "themes", theme, plateforme, logopath.Substring(2).Replace("/", "\\"));

        }
    }
}
