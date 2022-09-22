using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetroFront.Models;
using RetroFront.Models.StandaloneEmulator;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Implémentation
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
                appSettings.DefaultBCK = string.Empty;
                appSettings.CurrentGameDisplay = RomDisplay.WallBox;
                appSettings.CurrentSysDisplay = SysDisplay.FlipView;
                appSettings.CurrentHomeDisplay = HomeDisplay.GameOS;
                appSettings.CurrentGameDetailDisplay = GameDetailDisplay.Artwork;
                appSettings.AppSettingsFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront";
                appSettings.AppSettingsLocation = $"{appSettings.AppSettingsFolder}\\AppSettings.json";
                appSettings.RetroarchPath = string.Empty;
                appSettings.ShowAll = true;
                appSettings.ShowFav = true;
                appSettings.ShowLastPlayed = true;
                appSettings.ShowMostPlayed = true;
                appSettings.RegroupPCGamesForPegasus = false;
                appSettings.PegasusPCGroupName = string.Empty;
                appSettings.PegasusIconFolderPath = string.Empty;
                appSettings.URLGameLauncherPath = string.Empty;
                var jsonApp = JsonConvert.SerializeObject(appSettings,Formatting.Indented);
                File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
            }
            System.IO.Directory.CreateDirectory(appSettings.AppSettingsFolder);
            System.IO.Directory.CreateDirectory($"{appSettings.AppSettingsFolder}\\themes");
            System.IO.Directory.CreateDirectory($"{appSettings.AppSettingsFolder}\\media");

        }

        public void UpdateSettings(AppSettings apps)
        {
            appSettings = apps;
            var jsonApp = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
        }
        public void UpdateSettings()
        {
            var jsonApp = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
        }
        public void ChangeCurrentTheme(Theme th)
        {
            appSettings.CurrentTheme = th.FolderName;
            var jsonApp = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            File.WriteAllText(appSettings.AppSettingsLocation, jsonApp);
        }
        public ThemePath GetCurrentTheme()
        {
            return FormatPath(appSettings.CurrentTheme);
        }
        private ThemePath FormatPath(string oldpath)
        { return new ThemePath(oldpath); }
        public IEnumerable<StandaloneEmulator> GetStandaloneEmulators()
        {

            string workingDirectory = Environment.CurrentDirectory;
            var shortWDirectory = workingDirectory.Substring(0, workingDirectory.IndexOf("RetroFront") + 10);
            var truefolder = Path.Combine(shortWDirectory, "RetroFront.Admin", "Emulator");
            var files = Directory.GetFiles(truefolder).ToList();
            foreach (var file in files.Where(x => x.EndsWith("json")))
            {
                var jsonString = File.ReadAllText(file);
                var StandEmu = JsonConvert.DeserializeObject<StandaloneEmulator>(jsonString);
                yield return StandEmu;
            }
            //return null;
        }

        public RetroFront.Models.ScreenScraper.System.Systeme GetSysByCode(string shortname)
        {
            var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\SystemListSCSP.json");
            JObject rss = JObject.Parse(jsonString);
            JArray categories = (JArray)rss["response"]["systemes"];
            var listSysSS = categories.ToObject<List<RetroFront.Models.ScreenScraper.System.Systeme>>();
            var filterrecalbox = listSysSS.Where(x => x.noms.nom_recalbox != null).FirstOrDefault(x => x.noms.nom_recalbox.ToUpper() == shortname.ToUpper());
            if (filterrecalbox != null)
            {
                return (RetroFront.Models.ScreenScraper.System.Systeme)filterrecalbox;
            }
            else
            {
                var filterretropie = listSysSS.Where(x => x.noms.nom_retropie != null).FirstOrDefault(x => x.noms.nom_retropie.ToUpper() == shortname.ToUpper());
                if (filterretropie != null)
                {
                    return (RetroFront.Models.ScreenScraper.System.Systeme)filterretropie;
                }
                else
                {
                    var filterlaunchbox = listSysSS.Where(x => x.noms.nom_launchbox != null).FirstOrDefault(x => x.noms.nom_launchbox.ToUpper() == shortname.ToUpper());
                    if (filterlaunchbox != null)
                    {
                        return (RetroFront.Models.ScreenScraper.System.Systeme)filterlaunchbox;
                    }
                    else
                    {
                        var filtercommun = listSysSS.Where(x => x.noms.noms_commun != null).FirstOrDefault(x => x.noms.noms_commun.ToUpper() == shortname.ToUpper());
                        if (filtercommun != null)
                        {
                            return (RetroFront.Models.ScreenScraper.System.Systeme)filtercommun;
                        }
                    }
                }
                return null;
            }

            //return (Models.ScreenScraper.System.Systeme)listSysSS.FirstOrDefault(x => x.noms.nom_recalbox.ToUpper() == shortname.ToUpper() || x.noms.nom_retropie.ToUpper() == shortname.ToUpper() || x.noms.nom_launchbox.ToUpper() == shortname.ToUpper() || x.noms.noms_commun.ToUpper() == shortname.ToUpper());
        }
        public IEnumerable<RetroFront.Models.ScreenScraper.System.Systeme> GetAllSysFromJSON()
        {
            var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\SystemListSCSP.json");
            JObject rss = JObject.Parse(jsonString);
            JArray categories = (JArray)rss["response"]["systemes"];
            //var listsys = categories.ToObject<List<Models.ScreenScraper.System.Systeme>>();
            return categories.ToObject<List<RetroFront.Models.ScreenScraper.System.Systeme>>();
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
