using RetroFront.Models;
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
            System.IO.Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront");
            System.IO.Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\themes"); 
            var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\AppSettings.json");
            appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonString);
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
