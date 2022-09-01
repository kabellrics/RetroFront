using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemesService: BaseService
    {
        public async Task UpdateSysteme(DisplaySysteme system)
        {
            await systemeClient.SystemePutAsync(system.ID, system.Systeme);
        }
        public async Task<DisplaySysteme> CreateSysteme(Systeme system)
        {
            return new DisplaySysteme((await systemeClient.SystemePostAsync(system)).Result);
        }
        public async Task<Emulator> CreateEmulator(Emulator emulator)
        {
            return (await emulatorClient.EmulatorPostAsync(emulator)).Result;
        }
        public async Task<DisplaySysteme> GetSystemeByShortname(string shortname)
        {
            var result = await systemeClient.SystemeGetAsync();
            var sys = result.Result;
            var dissys = sys.FirstOrDefault(x=> x.Shortname == shortname);
            if (dissys == null)
                return null;
            else
                return new DisplaySysteme(dissys);
        }
        public async Task CreateSteamGames(IEnumerable<DisplayGame> games)
        {
            if (games != null)
            {
                var steamsys = await GetSystemeByShortname("steam");
                if (steamsys == null)
                {
                    var sysSteam = new Systeme();
                    sysSteam.Name = "Steam";
                    sysSteam.Type = SysType.GameStore;
                    sysSteam.Shortname = "steam";
                    sysSteam.Logo = "ms-appx:///Assets/defaut/steam/logo.png";
                    sysSteam.Screenshoot = "ms-appx:///Assets/defaut/steam/bck.jpg";
                    steamsys = await CreateSysteme(sysSteam);
                }
                var steamemus = (await GetEmulatorsInSystemes(steamsys.Systeme)).FirstOrDefault();
                if (steamemus == null)
                {
                    var emusteam = new Emulator();
                    emusteam.SystemeID = steamsys.ID;
                    emusteam.Name = "Steam Executable";
                    emusteam.Chemin = @"C:\Windows\explorer.exe";
                    emusteam.Extension = ".exe .EXE .lnk .LNK";
                    steamemus = await CreateEmulator(emusteam);
                }
                var registergames = await gameClient.GameGetAsync();
                var registerSteamGame = registergames.Result.Where(x=>x.SteamID > 0);
                foreach(var game in games)
                {
                    if(registerSteamGame.Any(x=>x.SteamID == game.SteamID)) { }
                    else
                    {
                        game.Game.EmulatorID = steamemus.EmulatorID;
                        await gameClient.GamePostAsync(game.Game);
                    }
                }
                foreach(var game in registerSteamGame)
                {
                    if(games.Any(x=>x.SteamID == game.SteamID)) { }
                    else
                    {
                        await gameClient.GameDeleteAsync(game.Id);
                    }
                }
            }
        }
        public async Task CreateEpicGames(IEnumerable<DisplayGame> games)
        {
            if (games != null)
            {
                var steamsys = await GetSystemeByShortname("epic");
                if (steamsys == null)
                {
                    var sysSteam = new Systeme();
                    sysSteam.Name = "Epic Game Store";
                    sysSteam.Type = SysType.GameStore;
                    sysSteam.Shortname = "epic";
                    sysSteam.Logo = "ms-appx:///Assets/defaut/epic/logo.png";
                    sysSteam.Screenshoot = "ms-appx:///Assets/defaut/epic/bck.jpg";
                    steamsys = await CreateSysteme(sysSteam);
                }
                var steamemus = (await GetEmulatorsInSystemes(steamsys.Systeme)).FirstOrDefault();
                if (steamemus == null)
                {
                    var emusteam = new Emulator();
                    emusteam.SystemeID = steamsys.ID;
                    emusteam.Name = "Epic Executable";
                    emusteam.Chemin = @"C:\Windows\explorer.exe";
                    emusteam.Extension = ".exe .EXE .lnk .LNK";
                    steamemus = await CreateEmulator(emusteam);
                }
                var registergames = await gameClient.GameGetAsync();
                var registerSteamGame = registergames.Result.Where(x=>!string.IsNullOrEmpty(x.EpicID));
                foreach(var game in games)
                {
                    if(registerSteamGame.Any(x=>x.EpicID == game.EpicGameID)) { }
                    else
                    {
                        game.Game.EmulatorID = steamemus.EmulatorID;
                        await gameClient.GamePostAsync(game.Game);
                    }
                }
                foreach(var game in registerSteamGame)
                {
                    if(games.Any(x=>x.EpicGameID == game.EpicID)) { }
                    else
                    {
                        await gameClient.GameDeleteAsync(game.Id);
                    }
                }
            }
        }
        public async Task CreateOriginGames(IEnumerable<DisplayGame> games)
        {
            if (games != null)
            {
                var steamsys = await GetSystemeByShortname("origin");
                if (steamsys == null)
                {
                    var sysSteam = new Systeme();
                    sysSteam.Name = "EA Origin";
                    sysSteam.Type = SysType.GameStore;
                    sysSteam.Shortname = "origin";
                    sysSteam.Logo = "ms-appx:///Assets/defaut/origin/logo.png";
                    sysSteam.Screenshoot = "ms-appx:///Assets/defaut/origin/bck.jpg";
                    steamsys = await CreateSysteme(sysSteam);
                }
                var steamemus = (await GetEmulatorsInSystemes(steamsys.Systeme)).FirstOrDefault();
                if (steamemus == null)
                {
                    var emusteam = new Emulator();
                    emusteam.SystemeID = steamsys.ID;
                    emusteam.Name = "Origin Executable";
                    emusteam.Chemin = @"C:\Windows\explorer.exe";
                    emusteam.Extension = ".exe .EXE .lnk .LNK";
                    steamemus = await CreateEmulator(emusteam);
                }
                var registergames = await gameClient.GameGetAsync();
                var registerSteamGame = registergames.Result.Where(x=>!string.IsNullOrEmpty(x.OriginID));
                foreach(var game in games)
                {
                    if(registerSteamGame.Any(x=>x.OriginID == game.OriginID)) { }
                    else
                    {
                        game.Game.EmulatorID = steamemus.EmulatorID;
                        await gameClient.GamePostAsync(game.Game);
                    }
                }
                foreach(var game in registerSteamGame)
                {
                    if(games.Any(x=>x.OriginID == game.OriginID)) { }
                    else
                    {
                        await gameClient.GameDeleteAsync(game.Id);
                    }
                }
            }
        }
    }
}
