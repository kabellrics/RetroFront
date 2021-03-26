
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using Gameloop.Vdf;
using System.Text;
using Gameloop.Vdf.Linq;
using RetroFront.Models;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace RetroFront.Services.Implementation
{
    public class SteamService : ISteamService
    {
        private FileJSONService FileJSONService = new FileJSONService();
        private DatabaseService dbService = new DatabaseService();
        private string LogoPath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/logo.png";
        private string BoxPath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/library_600x900.jpg";
        public List<GameRom> GetSteamGame(Emulator emu)
        {
            string steamfolder;
            var key64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam";
            var key32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam";
            if(Environment.Is64BitOperatingSystem)
            {
                steamfolder = (string)Microsoft.Win32.Registry.GetValue(key64, "InstallPath",string.Empty);
            }
            else
            {
                steamfolder = (string)Microsoft.Win32.Registry.GetValue(key32, "InstallPath", string.Empty);
            }

            if (steamfolder != null)
            {
                List<string> foldersTosearch = new List<string>();
                foldersTosearch.Add(Path.Combine(steamfolder, "steamapps"));
                VProperty volvo = VdfConvert.Deserialize(File.ReadAllText(Path.Combine(steamfolder, "steamapps", "libraryfolders.vdf")));
                var childs = volvo.Value.Children();
                foreach (var child in childs)
                {
                    if (Directory.Exists(((VProperty)child).Value.ToString()))
                    {
                        foldersTosearch.Add(Path.Combine(((VProperty)child).Value.ToString(), "steamapps"));
                    }
                }
                List<GameRom> gamesfind = new List<GameRom>();
                List<String> appmanifestfiles = new List<string>();
                foreach (string foldertoSeek in foldersTosearch)
                {
                    appmanifestfiles.AddRange(Directory.GetFiles(foldertoSeek, "appmanifest_*.acf").ToList());
                }

                foreach (var file in appmanifestfiles)
                {
                    dynamic appfile = VdfConvert.Deserialize(File.ReadAllText(file));
                    GameRom game = new GameRom();
                    game.EmulatorID = emu.EmulatorID;
                    game.SteamID = int.Parse(appfile.Value.appid.Value);
                    game.Name = appfile.Value.name.Value;

                    gamesfind.Add(game);
                }
                return gamesfind;
            }
            else
                return null;
        }

        public GameRom GetSteamInfos(GameRom game, Emulator emu)
        {
            var urlinfos = @"https://store.steampowered.com/api/appdetails?appids="+game.SteamID+ "&l=french";
            var plateforme = dbService.GetSysteme(emu.SystemeID);
            string imgfolder = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", plateforme.Shortname);
            var newgame = game;
            try
            {
                string jsoninfos;
                using (WebClient wc = new WebClient())
                {
                    jsoninfos = wc.DownloadString(urlinfos);
                }
                JObject json = JObject.Parse(jsoninfos);
                var datajson = json[newgame.SteamID.ToString()]["data"];
                if (datajson != null)
                {
                    var data = JsonConvert.DeserializeObject<Data>(datajson.ToString());

                    newgame.Path = $"steam://rungameid/{newgame.SteamID.ToString()}";
                    newgame.Genre = string.Join(", ", data.genres.Select(x=>x.description));
                    newgame.Editeur = string.Join(", ", data.publishers);
                    newgame.Dev = string.Join(", ", data.developers);
                    newgame.Desc = data.short_description;
                    newgame.Year = DateTime.Parse(data.release_date.date).Year.ToString();
                    Directory.CreateDirectory(Path.Combine(imgfolder, "box2dfront"));
                    Directory.CreateDirectory(Path.Combine(imgfolder, "fanart"));
                    Directory.CreateDirectory(Path.Combine(imgfolder, "images"));
                    Directory.CreateDirectory(Path.Combine(imgfolder, "wheel"));
                    Directory.CreateDirectory(Path.Combine(imgfolder, "videos"));
                    newgame.Boxart = Path.Combine(imgfolder, "box2dfront", $"{newgame.SteamID.ToString()}.jpg");
                    newgame.Fanart = Path.Combine(imgfolder, "steamgrid", $"{newgame.SteamID.ToString()}.jpg");
                    newgame.Screenshoot = Path.Combine(imgfolder, "fanart", $"{newgame.SteamID.ToString()}.jpg");
                    newgame.Logo = Path.Combine(imgfolder, "wheel", $"{newgame.SteamID.ToString()}.png");
                    newgame.Video = Path.Combine(imgfolder, "videos", $"{newgame.SteamID.ToString()}.mp4");

                    DownloadSteamData(data.header_image, newgame.Fanart);
                    DownloadSteamData(data.screenshots.First().path_full, newgame.Screenshoot);
                    DownloadSteamData(LogoPath.Replace("%STEAMID%", newgame.SteamID.ToString()), newgame.Logo);
                    DownloadSteamData(BoxPath.Replace("%STEAMID%", newgame.SteamID.ToString()), newgame.Boxart);
                    DownloadSteamData(data.movies.First().mp4.max, newgame.Video);

                    game = newgame;
                }
            }
            catch (Exception ex)
            {
                //throw;
            }

            return game;
        }

        public void DownloadSteamData(string dllpath,string target)
        {
            using (var file = File.Create(target))
            {
                using (WebClient client = new WebClient())
                {
                    file.Write(client.DownloadData(dllpath));
                    //client.DownloadFile(dllpath, file);
                }
            }
        }
    }
}
