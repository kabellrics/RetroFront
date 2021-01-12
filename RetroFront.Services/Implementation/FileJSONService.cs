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
                appSettings.AppSettingsFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront";
                appSettings.AppSettingsLocation = $"{appSettings.AppSettingsFolder}\\AppSettings.json";
                appSettings.RetroarchPath = @"";
                appSettings.RetroarchCMD = "%RetroarchPath% -L %RetroArchCore% %ROM_RAW%";
                var jsonApp =  JsonConvert.SerializeObject(appSettings);
                File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
            }
            System.IO.Directory.CreateDirectory(appSettings.AppSettingsFolder);
            System.IO.Directory.CreateDirectory($"{appSettings.AppSettingsFolder}\\themes");

        }
        public IEnumerable<Systeme> GetAllSysFromJSON()
        {
            var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\SystemList.json");
            JObject rss = JObject.Parse(jsonString);
            JArray categories = (JArray)rss["systemeList"]["systeme"];
            var listsys = categories.ToObject<List<XMLSystem>>();
            foreach (var jsonsys in listsys)
            {
                Systeme sys = new Systeme();
                sys.Name = jsonsys.fullname;
                sys.Shortname = jsonsys.name;
                sys.Platform = jsonsys.platform;
                sys.Theme = jsonsys.theme;
                yield return sys;
            }
        }
    }
}
