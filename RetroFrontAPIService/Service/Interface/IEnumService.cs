using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IEnumService
    {
        List<KeyValuePair<int, string>> GetSysDisplayValue();
        List<KeyValuePair<int, string>> GetHomeDisplayValue();
        List<KeyValuePair<int, string>> GetGameDetailDisplayValue();
        List<KeyValuePair<int, string>> GetRomDisplayValue();
        List<GameDetailDisplay> GetGameDetailDisplays();
        List<HomeDisplay> GetHomeDisplays();
        List<RomDisplay> GetRomDisplays();
        List<SysDisplay> GetSysDisplays();
        List<SysType> GetSysTypes();
    }
}