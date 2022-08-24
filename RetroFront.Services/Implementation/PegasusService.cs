using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RetroFront.Services.Implementation
{
    public class PegasusService : IPegasusService
    {
        private IGDBService iGDBService = new IGDBService();
        private ScreenScraperService screenScraperService = new ScreenScraperService();
        public Models.Pegasus.Game PegasusGameFromGameRom(GameRom gamerom)
        {
            Models.Pegasus.Game pggame = new Models.Pegasus.Game();
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
                var arts = screenScraperService.GetJeuxDetail(gamerom.ScreenScraperID);
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
                var detailart = iGDBService.GetArtworksByGameId(gamerom.IGDBID);
                var detailsch = iGDBService.GetScreenshotsByGameId(gamerom.IGDBID);
                if (detailart != null)
                {
                    var resultartigdb = detailart.Select(x => iGDBService.GetArtWorkLink(x.image_id));
                    foreach (var img in resultartigdb)
                    {
                        pggame.Screenshoot.Add(img);
                    }
                }
                if (detailsch != null)
                {
                    var resultschigdb = detailsch.Select(x => iGDBService.GetArtWorkLink(x.image_id));
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
        public Models.Pegasus.Collection PegasusCollectionFromEmulator(Emulator emulator, Systeme sys)
        {
            Models.Pegasus.Collection collection = new Models.Pegasus.Collection();
            collection.Name = sys.Name;
            collection.shortname = sys.Shortname;
            collection.Extension = emulator.Extension.Replace(".",string.Empty);
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
        public string StringFromPegasusCollection(Models.Pegasus.Collection collection)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"collection : {collection.Name}");
            builder.AppendLine($"shortname : {collection.shortname}");
            builder.AppendLine($"extension : {collection.Extension}");
            builder.AppendLine($"launch : { collection.launch}");
            builder.AppendLine($"assets.logo : { collection.Logo}");
            return builder.ToString();
        }
        public string StringFromPegasusGame(Models.Pegasus.Game game)
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
            foreach(var img in game.Screenshoot)
                builder.AppendLine($"assets.screenshot : { img}");
            //builder.AppendLine($"assets.background : { game.}");
            builder.AppendLine($"assets.steam : { game.Fanart}");
            builder.AppendLine($"assets.banner : { game.Fanart}");
            return builder.ToString();
        }
    }
}
