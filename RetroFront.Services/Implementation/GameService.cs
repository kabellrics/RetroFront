﻿using RetroFront.Models;
using RetroFront.Services.Interface;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Net;

namespace RetroFront.Services.Implementation
{
    public class GameService : IGameService
    {
        private FileJSONService FileJSONService = new FileJSONService();
        private DatabaseService dbService = new DatabaseService();
        public GameRom CreateGame(string gamefile, Emulator emulator)
        {
            GameRom game = new GameRom();
            var plateforme = dbService.GetSysteme(emulator.SystemeID);
            string imgfolder = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme.Shortname);
            game.Path = gamefile;
            game.Name = System.IO.Path.GetFileNameWithoutExtension(gamefile);
            game.EmulatorID = emulator.EmulatorID;
            game.Boxart = Path.Combine(imgfolder, "box2dfront",$"{game.Name}.png");
            game.Fanart = Path.Combine(imgfolder,"fanart", $"{game.Name}.jpg");
            game.Screenshoot = Path.Combine(imgfolder,"images", $"{game.Name}.png");
            game.Logo = Path.Combine(imgfolder,"wheel", $"{game.Name}.png");
            game.TitleScreen = Path.Combine(imgfolder, "screentitle", $"{game.Name}.png");
            game.RecalView = Path.Combine(imgfolder, "recalview", $"{game.Name}.png");
            game.Video = Path.Combine(imgfolder, "videos", $"{game.Name}.mp4");
            game.IsDuplicate = false;
            return LookForData(game);
        }
        public GameRom DuplicateGame(GameRom game)
        {
            GameRom duplicate = new GameRom();
            duplicate.Path = game.Path;
            duplicate.Name = game.Name;
            duplicate.Boxart = game.Boxart;
            duplicate.Fanart = game.Fanart;
            duplicate.Screenshoot = game.Screenshoot;
            duplicate.Logo = game.Logo;
            duplicate.TitleScreen = game.TitleScreen;
            duplicate.RecalView = game.RecalView;
            duplicate.Video = game.Video;
            duplicate.Desc = game.Desc;
            duplicate.Dev = game.Dev;
            duplicate.Editeur = game.Editeur;
            duplicate.Year = game.Year;
            duplicate.Genre = game.Genre;
            duplicate.Name = game.Name;
            duplicate.IsDuplicate = true;
            return duplicate;
        }
        public IEnumerable<GameRom> ImportGame(string gamelistpath, Emulator emulator)
        {

            var plateforme = dbService.GetSysteme(emulator.SystemeID);
            string imgfolder = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme.Shortname); 
            var xmlgamelist = File.ReadAllText(gamelistpath);
            var folderpath = Path.GetDirectoryName(gamelistpath);
            XmlSerializer serializer = new XmlSerializer(typeof(GameList));
            using (StringReader reader = new StringReader(xmlgamelist))
            {
                var gamelistfile = (GameList)serializer.Deserialize(reader);
                var gamelist = gamelistfile.Game;
                foreach (var gamedata in gamelist)
                {
                    if(File.Exists(Path.Combine(folderpath,gamedata.Path.Substring(2))))
                    {
                        var newgame = new GameRom();
                        newgame.Name = Path.GetFileNameWithoutExtension(gamedata.Path);
                        newgame.Path = Path.Combine(folderpath, gamedata.Path.Substring(2));
                        newgame.EmulatorID = emulator.EmulatorID;
                        newgame.Boxart = Path.Combine(imgfolder, "box2dfront", $"{newgame.Name}.png");
                        newgame.Fanart = Path.Combine(imgfolder, "fanart", $"{newgame.Name}.jpg");
                        newgame.Screenshoot = Path.Combine(imgfolder, "images", $"{newgame.Name}.png");
                        newgame.Logo = Path.Combine(imgfolder, "wheel", $"{newgame.Name}.png");
                        newgame.TitleScreen = Path.Combine(imgfolder, "screentitle", $"{newgame.Name}.png");
                        newgame.RecalView = Path.Combine(imgfolder, "recalview", $"{newgame.Name}.png");
                        newgame.Video = Path.Combine(imgfolder, "videos", $"{newgame.Name}.mp4");
                        newgame = ScrapeGamefromGamelist(newgame, folderpath, gamedata);
                        newgame.IsDuplicate = false;
                        yield return newgame;
                    }
                }
            }
        }
        public GameRom LookForData(GameRom game)
        {
            var filefolder = Path.GetDirectoryName(game.Path);
            if(File.Exists(Path.Combine(filefolder, "gamelist.xml")))
            {
                var xmlgamelist = File.ReadAllText(Path.Combine(filefolder, "gamelist.xml"));
                XmlSerializer serializer = new XmlSerializer(typeof(GameList));
                using (StringReader reader = new StringReader(xmlgamelist))
                {
                    var gamelistfile = (GameList)serializer.Deserialize(reader);
                    var gamelist = gamelistfile.Game;
                    foreach(var gamedata in gamelist)
                    {
                        var gamedatapath = Path.Combine(filefolder, gamedata.Path.Substring(2));
                        if(gamedatapath == game.Path)
                        {
                            game = ScrapeGamefromGamelist(game, filefolder, gamedata);
                        }
                    }
                }
            }
            return game;
        }

        public string GetImgPathForGame(GameRom game, SGDBType sGDBType)
        {
            var emulator = dbService.GetEmulator(game.EmulatorID);
            var plateforme = dbService.GetSysteme(emulator.SystemeID);
            string imgfolder = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme.Shortname);
            if(sGDBType == SGDBType.background)
            {
                return Path.Combine(imgfolder, "images", $"{game.Name}");
            }
            else if (sGDBType == SGDBType.boxart) 
            {
                return Path.Combine(imgfolder, "box2dfront", $"{game.Name}");
            }
            else if (sGDBType == SGDBType.fanart)
            {
                return Path.Combine(imgfolder, "fanart", $"{game.Name}");
            }
            else if (sGDBType == SGDBType.logo)
            {
                return Path.Combine(imgfolder, "wheel", $"{game.Name}");
            }
            else
            {
                return string.Empty;
            }
        }

        private GameRom ScrapeGamefromGamelist(GameRom game, string filefolder, Game gamedata)
        {
            game.Desc = gamedata.Desc;
            game.Dev = gamedata.Developer;
            game.Editeur = gamedata.Publisher;
            if(!string.IsNullOrEmpty(gamedata.Releasedate) && !string.IsNullOrWhiteSpace(gamedata.Releasedate))
                game.Year = gamedata.Releasedate.Substring(0, 4);
            game.Genre = gamedata.Genre;
            game.Name = gamedata.Name;
            return game;
        }
        public string DownloadImgData(string dllpath, string target)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(dllpath), target);
                    return target;
                }
            }
            catch (Exception ex)
            {
                //throw;
                return string.Empty;
            }
        }

        public GameRom ScrapeGame(GameRom game)
        {
            return game;
        }
    }
}
