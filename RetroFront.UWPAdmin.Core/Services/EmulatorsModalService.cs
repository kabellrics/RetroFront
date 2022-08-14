using Newtonsoft.Json.Linq;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class EmulatorsModalService : BaseService
    {
        public async Task<IEnumerable<RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator>> GetEmulators()
        {
            List<RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator> displayEmus = new List<RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator>();
            var result = await settingsClient.GetStandaloneEmulatorsAsync();
            foreach (var sys in result.Result.Where(x => x.Name != "RetroArch").OrderBy(x => x.Name))
            {
                displayEmus.Add(sys);
            }
            var settings = settingsClient.SettingsGet().Result;
            if (!string.IsNullOrEmpty(settings.RetroarchPath))
            {
                var cores = externalAppClient.GetInstalledCore(settings.RetroarchPath);
                var RetroarchCores = new List<RetroarchCore>(cores.Result);
                var InstalledCoreNames = RetroarchCores.Select(x => x.Path.Substring(x.Path.LastIndexOf('\\') + 1));
                var SysTemesDispo = result.Result.First(x => x.Name == "RetroArch").Plateformes.Where(x => InstalledCoreNames.Any(s => x.StartupArguments.Contains(s)));
                var retroarchelement = new RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator() { Name = "RetroArch", Plateformes = new List<StandalonePlateforme>() };
                foreach (var sys in SysTemesDispo.OrderBy(x => x.Name))
                {
                    retroarchelement.Plateformes.Add(sys);
                }
                displayEmus.Add(retroarchelement);
            }
            return displayEmus;
        }
        public List<ScreenScraper.System.Systeme> SearchSystemByName(string Name)
        {
            var jsonString = File.ReadAllText($"SystemListSCSP.json");
            //var jsonString = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\SystemListSCSP.json");
            JObject rss = JObject.Parse(jsonString);
            JArray categories = (JArray)rss["response"]["systemes"];
            var AllSystem = categories.ToObject<List<ScreenScraper.System.Systeme>>();
            var FoundedSystem = new List<ScreenScraper.System.Systeme>();
            var searchtext = Name.Split(' ').ToList();
            searchtext.AddRange(Name.Split('_'));
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomEU)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomUS)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomJP)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomRecalbox)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomRetropie)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomLaunchbox)))
                FoundedSystem.Add(sys);
            foreach (var sys in AllSystem.Where(x => searchtext.Contains(x.NomCommun)))
                FoundedSystem.Add(sys);

            return FoundedSystem.Distinct().ToList();
        }

        public CreatedSysAndPlateformeObject CreateResultObject(APIHelper.StandaloneEmulator selectedEmulators, StandalonePlateforme selectedPlateformes, ScreenScraper.System.Systeme foundedSystem)
        {
            var sys = new Systeme();
            sys.Name = selectedPlateformes.Name;
            sys.Shortname = selectedPlateformes.Shortname;
            sys.SystemeSCSPID = foundedSystem.id;
            var emulator = new Emulator();
            emulator.Name = selectedPlateformes.Name;
            emulator.IsDuplicate = false;
            emulator.Extension = string.Join(" ", selectedPlateformes.ImageExtensions.Select(x => $".{x}"));
            emulator.Command = selectedPlateformes.StartupArguments;
            CreatedSysAndPlateformeObject result = new CreatedSysAndPlateformeObject() { emulator = emulator, systeme = sys };
            return result;
        }
    }
}
