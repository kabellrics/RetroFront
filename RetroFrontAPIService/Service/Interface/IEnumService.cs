using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IEnumService
    {
        List<GameDetailDisplay> GetGameDetailDisplays();
        List<HomeDisplay> GetHomeDisplays();
        List<RomDisplay> GetRomDisplays();
        List<SysDisplay> GetSysDisplays();
        List<SysType> GetSysTypes();
    }
}