using Newtonsoft.Json;
using RestSharp.Extensions;
using RetroFront.Models;
using RetroFront.Models.EAOrigin;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace RetroFront.Services.Implementation
{
    public class OriginService : IOriginService
    {
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
    }
}
