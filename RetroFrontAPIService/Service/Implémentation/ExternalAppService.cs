using Microsoft.Win32;
using RetroFront.Models;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Implémentation
{
    public class ExternalAppService : IExternalAppService
    {
        private GameDataProviderService gameDataProviderService = new GameDataProviderService();
        private DatabaseService databaseService = new DatabaseService();
        private FileJSONService fileJSONService = new FileJSONService();
        private ComputerService computerService = new ComputerService();
        public IEnumerable<RetroarchCore> GetInstalledCore(string retroarchpath)
        {
            FileInfo fi = new FileInfo(retroarchpath);
            var retroarchfolder = fi.DirectoryName;
            var cores = System.IO.Directory.EnumerateFiles(System.IO.Path.Combine(retroarchfolder, "cores"));
            TextInfo textInfo = new CultureInfo("fr-FR", false).TextInfo;
            foreach (var core in cores)
            {
                RetroarchCore rcore = new RetroarchCore();
                rcore.path = core;
                var corename = System.IO.Path.GetFileNameWithoutExtension(core);
                var coreNolibName = corename.Substring(0, corename.IndexOf("_libretro"));
                rcore.Name = textInfo.ToTitleCase(coreNolibName.Replace("_", " "));
                yield return rcore;
            }
        }

        public string CreateCFGFileForRetroarch(int gameid)
        {
            var rom = databaseService.GetGame(gameid);
            if(rom != null) 
            {
                List<string> replist = new List<string>();
                var settings = fileJSONService.appSettings;
                var retroarchpath = Path.GetDirectoryName(settings.RetroarchPath);
                Directory.CreateDirectory(Path.Combine(retroarchpath,"overlays","GameBezels"));
                var romname = Path.GetFileNameWithoutExtension(rom.Path);
                var bezelcfgname = $"{romname}.cfg";
                //var bezelpngname = $"{romname}.png";
                var bezelcfgpath = Path.Combine(retroarchpath, "overlays", "GameBezels",  bezelcfgname);
                var cfgcontent = GenerateCFGContent(rom.RecalView);
                var cfggamecontent = GenerateRomCFGContent(bezelcfgpath);
                //var cfgcontent = GenerateCFGContent(bezelpngname);
                if (File.Exists(bezelcfgpath))
                    File.Delete(bezelcfgpath);
                File.WriteAllText(bezelcfgpath, cfgcontent);

                var filecfgrom = Path.Combine(retroarchpath,"config", GetCoreFolderForCFG(rom), $"{Path.GetFileNameWithoutExtension(rom.Path)}.cfg");
                Directory.CreateDirectory(Path.Combine(retroarchpath, "config", GetCoreFolderForCFG(rom)));
                File.WriteAllText(filecfgrom, cfggamecontent);

                //computerService.FileCopy(rom.RecalView,Path.Combine(retroarchpath, "overlays", "GameBezels", sys.Shortname, bezelpngname));
                return bezelcfgpath;
            }
            else
            {
                return string.Empty;
            }
        }
        public string GetCoreFolderForCFG(GameRom rom)
        {
            var emu = databaseService.GetEmulator(rom.EmulatorID);
            if(emu != null)
            {
                var corename = emu.Command.Split("\"", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(x => x.Contains(".dll"));
                if (corename != null)
                {
                    var settings = fileJSONService.appSettings;
                    var retroarchpath = Path.GetDirectoryName(settings.RetroarchPath);
                    var infolefilename = $"{Path.GetFileNameWithoutExtension(corename)}.info";
                    var infofile = Path.Combine(retroarchpath, "info", infolefilename);
                    var lines = File.ReadAllLines(infofile);
                    foreach(var line in lines)
                    {
                        if(line.StartsWith("corename = \""))
                        {
                            var trimline = line.Trim();
                            var dataline = trimline.Substring(12);
                            return dataline.Remove(dataline.Length - 1);
                        }
                    }
                    return String.Empty;
                }
                else return String.Empty;
            }
            else return String.Empty;
        }
        private string GenerateRomCFGContent(string cfgpath)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"input_overlay_enable = true");
            builder.AppendLine($"input_overlay = \"{cfgpath}\"");
            builder.AppendLine($"input_overlay_opacity = 1.0");
            builder.AppendLine($"input_overlay_scale = 1.0");
            return builder.ToString();
        }
        private string GenerateCFGContent(string bezelPath)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"overlays = 1");
            builder.AppendLine();
            builder.AppendLine($"overlay0_overlay = \"{bezelPath}\"");
            builder.AppendLine();
            builder.AppendLine($"overlay0_full_screen = true");
            builder.AppendLine();
            builder.AppendLine($"overlay0_descs = 0");
            builder.AppendLine();
            return builder.ToString();
        }
        private RetroFront.Models.Pegasus.Game PegasusGameFromGameRom(GameRom gamerom)
        {
            RetroFront.Models.Pegasus.Game pggame = new RetroFront.Models.Pegasus.Game();
            pggame.Name = gamerom.Name;
            pggame.SortName = gamerom.Name;
            //pggame.File = System.IO.Path.GetFileName(gamerom.Path);
            if (gamerom.SteamID > 0)
            {
                pggame.File = "steam:" + gamerom.SteamID;
            }
            else
            {
                pggame.File = $"{gamerom.ID}.id";
            }
            pggame.Id = gamerom.ID;
            pggame.Developer = gamerom.Dev;
            pggame.Genre = gamerom.Genre;
            pggame.Description = gamerom.Desc;
            pggame.BoxFront = gamerom.Boxart;
            pggame.Logo = gamerom.Logo;
            pggame.Video = gamerom.Video;
            pggame.Fanart = gamerom.Fanart;
            //pggame.Screenshoot.Add(gamerom.Fanart);
            pggame.Screenshoot.Add(gamerom.Screenshoot);
            try
            {
                var arts = gameDataProviderService.GetJeuxDetail(gamerom.ScreenScraperID);
                if (arts != null)
                {
                    foreach (var img in arts.medias.Where(x => x.type == "fanart" || x.type == "ss" || x.type == "themehs"))
                    {
                        pggame.Screenshoot.Add(img.url);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
            try
            {
                var detailart = gameDataProviderService.GetArtworksByGameId(gamerom.IGDBID);
                var detailsch = gameDataProviderService.GetScreenshotsByGameId(gamerom.IGDBID);
                if (detailart != null)
                {
                    var resultartigdb = detailart.Select(x => gameDataProviderService.GetArtWorkLink(x.image_id));
                    foreach (var img in resultartigdb)
                    {
                        pggame.Screenshoot.Add(img);
                    }
                }
                if (detailsch != null)
                {
                    var resultschigdb = detailsch.Select(x => gameDataProviderService.GetArtWorkLink(x.image_id));
                    foreach (var img in resultschigdb)
                    {
                        pggame.Screenshoot.Add(img);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
            return pggame;
        }
        private RetroFront.Models.Pegasus.Collection PegasusCollectionFromEmulator(Emulator emulator, Systeme sys)
        {
            RetroFront.Models.Pegasus.Collection collection = new RetroFront.Models.Pegasus.Collection();
            var settings = fileJSONService.appSettings;
            if (settings.RegroupPCGamesForPegasus == true && sys.Type == SysType.GameStore && sys.Shortname != "steam")
            {
                collection.Name = settings.PegasusPCGroupName;
                collection.shortname = "pcgames";
            }
            else
            {
                collection.Name = sys.Name;
                collection.shortname = sys.Shortname;
            }
            //collection.Extension = emulator.Extension.Replace(".", string.Empty);
            //if(sys.Type == SysType.GameStore && sys.Shortname != "steam")
            //{
            //    collection.launch = $"\"{fileJSONService.appSettings.URLGameLauncherPath}\""+" {file.path}";
            //    collection.Extension = String.Empty;
            //}
            //else if (sys.Type != SysType.GameStore)
            //{
            //    collection.launch = $"\"{emulator.Chemin}\" {emulator.Command?.Replace("{ImagePath}", "{file.path}")}";
            //    collection.launch = $"{collection.launch.Replace("%ROMPATH%", "{file.path}")}";
            //}

            collection.launch = $"\"{fileJSONService.appSettings.URLGameLauncherPath}\"" + " {file.path}";
            collection.Extension = "id";

            collection.Logo = sys.Logo;
            collection.Background = sys.Screenshoot;
            collection.Video = sys.Video;
            return collection;
        }
        private string StringFromPegasusCollection(RetroFront.Models.Pegasus.Collection collection)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"collection : {collection.Name}");
            builder.AppendLine($"shortname : {collection.shortname}");
            if(!string.IsNullOrEmpty(collection.Extension))
            builder.AppendLine($"extension : {collection.Extension}");
            builder.AppendLine($"launch : {collection.launch}");
            builder.AppendLine($"assets.logo : {collection.Logo}");
            return builder.ToString();
        }
        private string StringFromPegasusGame(RetroFront.Models.Pegasus.Game game)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"game : {game.Name}");
            builder.AppendLine($"sort_title : {game.Name}");
            builder.AppendLine($"file : {game.File}");
            builder.AppendLine($"developer : {game.Developer}");
            builder.AppendLine($"genre : {game.Genre}");
            builder.AppendLine($"description : {game.Description}");
            builder.AppendLine($"assets.box_front : {game.BoxFront}");
            builder.AppendLine($"assets.poster : {game.BoxFront}");
            builder.AppendLine($"assets.logo : {game.Logo}");
            builder.AppendLine($"assets.video : {game.Video}");
            foreach (var img in game.Screenshoot)
                builder.AppendLine($"assets.screenshot : {img}");
            //builder.AppendLine($"assets.background : { game.}");
            builder.AppendLine($"assets.steam : {game.Fanart}");
            builder.AppendLine($"assets.banner : {game.Fanart}");
            return builder.ToString();
        }
        public string ExportToPegasus(int sysID)
        {
            var sys = databaseService.GetSysteme(sysID);
            var emus = databaseService.GetEmulatorsForSysteme(sys.SystemeID);
            foreach (var emu in emus)
            {
                var builder = new StringBuilder();
                string stringpath = CreatePegasusFile(sys, emu);
                builder.Append(StringFromPegasusCollection(PegasusCollectionFromEmulator(emu, sys)));
                builder.AppendLine();
                foreach (var game in databaseService.GetGamesForemulator(emu.EmulatorID))
                {
                    builder.AppendLine(StringFromPegasusGame(PegasusGameFromGameRom(game)));
                }
                File.WriteAllText(stringpath, builder.ToString());
                var setting = fileJSONService.appSettings;
                if (setting.RegroupPCGamesForPegasus == true && sys.Type == SysType.GameStore)
                {
                    computerService.FileCopy(@"Assets/defaut/pcgames/logo2.png", Path.Combine(setting.PegasusIconFolderPath, "pcgames.png"));
                }
                else
                {
                    computerService.FileCopy(sys.Logo, Path.Combine(setting.PegasusIconFolderPath, sys.Shortname + ".png"));
                }
            }
            return string.Empty;
        }

        private string CreatePegasusFile(Systeme sys, Emulator emu)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pegasus"));
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pegasus", sys.Name + "-" + emu.Name + ".metadata.pegasus.txt");
            if (File.Exists(filepath))
                File.Delete(filepath);
            return filepath;
        }
    }
}
