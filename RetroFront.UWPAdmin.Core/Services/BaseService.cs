using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
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
            Task t = Task.Run(()=> computerClient.FileCopyAsync(string.Empty, obj));
        }
        public async Task DLLFile(String source, String dest)
        {
            var obj = new CopyAndDLLObject();
            obj.Dest = dest;
            obj.Source = source;
            Task t = Task.Run(() => computerClient.FileDLLAsync(string.Empty, obj));
        }
        public async Task WriteByte(byte[] source, String dest)
        {
            var obj = new DLLByteObject();
            obj.Source = source;
            obj.Dest = dest;
            Task t = Task.Run(() => computerClient.ByteArrayDLLAsync(string.Empty, obj));
        }
    }
}
