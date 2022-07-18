using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IExternalAppService
    {
        string ExportToPegasus(int emuID, int sysID);
        IEnumerable<RetroarchCore> GetInstalledCore(string retroarchpath);
    }
}