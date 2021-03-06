﻿using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using RetroFront.Services.Interface;
using Newtonsoft.Json;

namespace RetroFront.Services.Implementation
{
    public class FileJSONService : IFileJSONService
    {

        private DatabaseService dbService = new DatabaseService();
        public AppSettings appSettings { get; set; }
        public FileJSONService()
        {
            //System.IO.Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront");
            //System.IO.Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\themes");
            if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\AppSettings.json"))
            {
                var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\AppSettings.json");
                appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonString);
            }
            else
            {
                appSettings = new AppSettings();
                appSettings.CurrentTheme = "simple";
                appSettings.DefaultBCK = string.Empty;
                appSettings.CurrentGameDisplay = RomDisplay.WallBox;
                appSettings.CurrentSysDisplay = SysDisplay.BigLogo;
                appSettings.AppSettingsFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront";
                appSettings.AppSettingsLocation = $"{appSettings.AppSettingsFolder}\\AppSettings.json";
                appSettings.RetroarchPath = @"C:\Users\yflec\AppData\Roaming\RetroArch";
                appSettings.RetroarchCMD = "%RetroarchPath% -L %RetroArchCore% %ROM_RAW%";
                var jsonApp =  JsonConvert.SerializeObject(appSettings);
                File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
            }
            System.IO.Directory.CreateDirectory(appSettings.AppSettingsFolder);
            System.IO.Directory.CreateDirectory($"{appSettings.AppSettingsFolder}\\themes");
            System.IO.Directory.CreateDirectory($"{appSettings.AppSettingsFolder}\\media");

        }

        public void UpdateSettings(AppSettings apps)
        {
            appSettings = apps;
            var jsonApp = JsonConvert.SerializeObject(appSettings);
            File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
        }
        public void UpdateSettings()
        {
            var jsonApp = JsonConvert.SerializeObject(appSettings);
            File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
        }
        public void ChangeCurrentTheme(Theme th)
        {
            appSettings.CurrentTheme = th.FolderName;
            var jsonApp = JsonConvert.SerializeObject(appSettings);
            File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
        }
        public string GetCurrentTheme()
        {
            return appSettings.CurrentTheme;
        }
        public IEnumerable<Models.ScreenScraper.System.Systeme> GetAllSysFromJSON()
        {
            var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\SystemListSCSP.json");
            JObject rss = JObject.Parse(jsonString);
            JArray categories = (JArray)rss["response"]["systemes"];
            //var listsys = categories.ToObject<List<Models.ScreenScraper.System.Systeme>>();
            return categories.ToObject<List<Models.ScreenScraper.System.Systeme>>();
            //foreach (var jsonsys in listsys)
            //{
            //    Systeme sys = new Systeme();
            //    sys.Name = jsonsys.fullname;
            //    sys.Shortname = jsonsys.name;
            //    sys.Type = (SysType)jsonsys.Type;
            //    yield return sys;
            //}
        }
    }
}
