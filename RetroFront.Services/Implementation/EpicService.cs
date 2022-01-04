using Newtonsoft.Json.Linq;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class EpicService : IEpicService
    {
        public List<GameRom> GetEpicGame(Emulator emu)
        {
            var originPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic", "EpicGamesLauncher","Data", "Manifests");
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
    }
}
