using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class BaseService
    {
        protected SystemeClient systemeClient = new SystemeClient();
        protected GameClient gameClient = new GameClient();
        protected EmulatorClient emulatorClient = new EmulatorClient();
        protected GameDataProviderClient gameDataProvider;
        protected ComputerClient computerClient = new ComputerClient();
        protected HelperModelClient helperModelClient = new HelperModelClient();
        protected ThemeClient themeClient = new ThemeClient();
        protected SettingsClient settingsClient = new SettingsClient();
        protected ExternalAppClient externalAppClient = new ExternalAppClient();

        public async Task CopyDLLFile(String source,String dest)
        {
            var obj = new CopyAndDLLObject();
            obj.Dest = dest;
            obj.Source = source;
            if (obj.Source.StartsWith("http"))
            {
                Task t = Task.Run(() => computerClient.FileCopyAsync(string.Empty, obj));
            }
            else
            {
                Task t = Task.Run(() => computerClient.FileCopyAsync(string.Empty, obj));
            }
        }
        public async Task CopyFile(String source,String dest)
        {
            var obj = new CopyAndDLLObject();
            obj.Dest = dest;
            obj.Source = source;
            await computerClient.FileCopyAsync(dest, obj);
        }
        public async Task DLLFile(String source, String dest)
        {
            var obj = new CopyAndDLLObject();
            obj.Dest = dest;
            obj.Source = source;
            await computerClient.FileDLLAsync(dest, obj);
        }
        public async Task WriteByte(byte[] source, String dest)
        {
            var obj = new DLLByteObject();
            obj.Source = source;
            obj.Dest = dest;
            await computerClient.ByteArrayDLLAsync(dest, obj);
        }

        public async Task DeleteGame(GameRom game)
        {
            await gameClient.GameDeleteAsync(game.Id);
        }
        public async Task DeleteEmulator(Emulator emulator) 
        {
            await emulatorClient.EmulatorDeleteAsync(emulator.EmulatorID);
        }
        public async Task DeleteSystems(Systeme systeme)
        {
            await systemeClient.SystemeDeleteAsync(systeme.SystemeID);
        }

        public async Task<int> GetNbGamesInEmulateur(Emulator emulator)
        {
            var result = await gameClient.GameGetAsync();
            return result.Result.Count(x => x.EmulatorID == emulator.EmulatorID);
        }
        public async Task<int> GetNbEmulatorsInSystemes(Systeme systeme)
        {
            var result = await emulatorClient.EmulatorGetAsync();
            return result.Result.Count(x => x.SystemeID == systeme.SystemeID);
        }
        public async Task<int> GetNbGamesInSystemes(Systeme systeme)
        {
            var resultemu = await emulatorClient.EmulatorGetAsync();
            var emus = resultemu.Result.Where(x => x.SystemeID == systeme.SystemeID);
            var result = await gameClient.GameGetAsync();
            return result.Result.Count(x=> emus.Any(y=> y.EmulatorID == x.EmulatorID));
        }
    }
}
