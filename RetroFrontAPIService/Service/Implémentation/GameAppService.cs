using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetroFront.Models;
using RetroFront.Models.EAOrigin;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Implémentation
{
    public class GameAppService : IGameAppService
    {
        private FileJSONService FileJSONService = new FileJSONService();
        private DatabaseService dbService = new DatabaseService();
        private string LogoPath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/logo.png";
        private string BoxPath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/library_600x900.jpg";
        public List<GameRom> GetEpicGame(Emulator emu)
        {
            var originPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic", "EpicGamesLauncher", "Data", "Manifests");
            var manifestsFiles = Directory.GetFiles(originPath, "*.item", SearchOption.TopDirectoryOnly);
            List<GameRom> gamesfind = new List<GameRom>();
            foreach (var manifestsFile in manifestsFiles)
            {
                var manifestObject = JObject.Parse(File.ReadAllText(manifestsFile));
                var name = (string)manifestObject["DisplayName"];
                var appId = (string)manifestObject["AppName"];
                GameRom game = new GameRom();
                game.Name = name;
                game.EpicID = appId;
                game.EmulatorID = emu.EmulatorID;
                game.Path = $"com.epicgames.launcher://apps/{appId}?action=launch&silent=true";
                gamesfind.Add(game);
            }
            return gamesfind;
        }
        public List<GameRom> GetOriginGame(Emulator emu)
        {
            var originPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Origin", "LocalContent");
            if (Directory.Exists(originPath))
            {
                var manifests = Directory.GetFiles(originPath, "*.mfst", SearchOption.AllDirectories);
                List<GameRom> gamesfind = new List<GameRom>();
                foreach (var files in manifests)
                {
                    //string gameName;
                    string gameId;
                    try
                    {
                        gameId = Path.GetFileNameWithoutExtension(files);
                        if (!gameId.StartsWith("Origin"))
                        {
                            var match = Regex.Match(gameId, @"^(.*?)(\d+)$");
                            if (!match.Success)
                            {
                                continue;
                            }
                            gameId = match.Groups[1].Value + ":" + match.Groups[2].Value;
                        }
                        if (gameId.Contains("@"))
                        {
                            gameId = gameId.Substring(0, gameId.IndexOf("@"));
                        }
                        var origindata = GetGameLocalData(gameId);
                        if (origindata != null)
                        {
                            GameRom game = new GameRom();
                            game.EmulatorID = emu.EmulatorID;
                            game.Name = origindata.localizableAttributes.displayName;
                            game.Desc = origindata.localizableAttributes.longDescription;
                            game.OriginID = gameId;
                            game.Path = $"origin://launchgame/{gameId}";
                            gamesfind.Add(game);
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                return gamesfind;
            }
            else
                return null;
        }
        private OriginGame GetGameLocalData(string gameId)
        {
            try
            {
                var url = $@"https://api1.origin.com/ecommerce2/public/{gameId}/fr_FR";
                var webClient = new WebClient();
                var stringData = Encoding.UTF8.GetString(webClient.DownloadData(url));
                var data = JsonConvert.DeserializeObject<OriginGame>(stringData);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<GameRom> GetSteamGame(Emulator emu)
        {
            string steamfolder;
            var key64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam";
            var key32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam";
            if (Environment.Is64BitOperatingSystem)
            {
                steamfolder = (string)Microsoft.Win32.Registry.GetValue(key64, "InstallPath", string.Empty);
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
                    var childKV = (VProperty)child;
                    var childValueKV = childKV.Value;
                    var pathchildKV = childValueKV.FirstOrDefault();
                    if (pathchildKV != null)
                    {
                        //if (Directory.Exists(((VProperty)child).Value.ToString()))
                        if (Directory.Exists(((VProperty)pathchildKV).Value.ToString()))
                        {
                            foldersTosearch.Add(Path.Combine(((VProperty)pathchildKV).Value.ToString(), "steamapps"));
                        }
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
                    try
                    {
                        dynamic appfile = VdfConvert.Deserialize(File.ReadAllText(file));
                        GameRom game = new GameRom();
                        game.EmulatorID = emu.EmulatorID;
                        game.SteamID = int.Parse(appfile.Value.appid.Value);
                        game.Name = appfile.Value.name.Value;

                        gamesfind.Add(game);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                }
                return gamesfind;
            }
            else
                return null;
        }
        public GameRom GetSteamInfos(GameRom game)
        {
            var urlinfos = @"https://store.steampowered.com/api/appdetails?appids=" + game.SteamID + "&l=french";
            string imgfolder = Path.Combine(FileJSONService.appSettings.AppSettingsFolder, "media", "steam");
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
                    newgame.Genre = string.Join(", ", data.genres.Select(x => x.description));
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
        private void DownloadSteamData(string dllpath, string target)
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
