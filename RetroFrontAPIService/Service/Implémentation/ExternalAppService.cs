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
        private RetroFront.Models.Pegasus.Game PegasusGameFromGameRom(GameRom gamerom)
        {
            RetroFront.Models.Pegasus.Game pggame = new RetroFront.Models.Pegasus.Game();
            pggame.Name = gamerom.Name;
            pggame.SortName = gamerom.Name;
            //pggame.File = System.IO.Path.GetFileName(gamerom.Path);
            pggame.File = gamerom.Path;
            pggame.Developer = gamerom.Dev;
            pggame.Genre = gamerom.Genre;
            pggame.Description = gamerom.Desc;
            pggame.BoxFront = gamerom.Boxart;
            pggame.Logo = gamerom.Logo;
            pggame.Video = gamerom.Video;
            pggame.Fanart = gamerom.Fanart;
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
            collection.Name = sys.Name;
            collection.Extension = emulator.Extension.Replace(".", string.Empty);
            if (sys.Type == SysType.GameStore)
            {
                collection.launch = "{file.path}";
            }
            else
            {
                collection.launch = $"{emulator.Chemin} {emulator.Command.Replace("{ImagePath}", "{file.path}")}";
                collection.launch = $"{collection.launch.Replace("%ROMPATH%", "{file.path}")}";
            }
            collection.Logo = sys.Logo;
            collection.Background = sys.Screenshoot;
            collection.Video = sys.Video;
            return collection;
        }
        private string StringFromPegasusCollection(RetroFront.Models.Pegasus.Collection collection)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"collection : {collection.Name}");
            builder.AppendLine($"extension : {collection.Extension}");
            builder.AppendLine($"launch : { collection.launch}");
            builder.AppendLine($"assets.logo : { collection.Logo}");
            return builder.ToString();
        }
        private string StringFromPegasusGame(RetroFront.Models.Pegasus.Game game)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"game : {game.Name}");
            builder.AppendLine($"sort_title : {game.Name}");
            builder.AppendLine($"file : { game.File}");
            builder.AppendLine($"developer : { game.Developer}");
            builder.AppendLine($"genre : { game.Genre}");
            builder.AppendLine($"description : { game.Description}");
            builder.AppendLine($"assets.box_front : { game.BoxFront}");
            builder.AppendLine($"assets.poster : { game.BoxFront}");
            builder.AppendLine($"assets.logo : { game.Logo}");
            builder.AppendLine($"assets.video : { game.Video}");
            foreach (var img in game.Screenshoot)
                builder.AppendLine($"assets.screenshot : { img}");
            //builder.AppendLine($"assets.background : { game.}");
            builder.AppendLine($"assets.steam : { game.Fanart}");
            builder.AppendLine($"assets.banner : { game.Fanart}");
            return builder.ToString();
        }
        public string ExportToPegasus(int emuID, int sysID)
        {
            var emu = databaseService.GetEmulator(emuID);
            var sys = databaseService.GetSysteme(sysID);
            var builder = new StringBuilder();
            builder.Append(StringFromPegasusCollection(PegasusCollectionFromEmulator(emu, sys)));
            builder.AppendLine();
            foreach (var game in databaseService.GetGamesForemulator(emu.EmulatorID))
            {
                builder.AppendLine(StringFromPegasusGame(PegasusGameFromGameRom(game)));
            }
            return builder.ToString();
        }
    }
}
