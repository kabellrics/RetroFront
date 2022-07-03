using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class EnumService : IEnumService
    {
        public List<SysDisplay> GetSysDisplays()
        {
            return new List<SysDisplay>
            {
                SysDisplay.FlipView,
                SysDisplay.CarrouselLogo
            };
        }
        public List<HomeDisplay> GetHomeDisplays()
        {
            return new List<HomeDisplay>
            {
                HomeDisplay.SteamView,
                HomeDisplay.GameOS,
                HomeDisplay.Hub
            };
        }
        public List<RomDisplay> GetRomDisplays()
        {
            return new List<RomDisplay>
            {
                RomDisplay.WallBox,
                RomDisplay.WallBanner,
            };
        }
        public List<SysType> GetSysTypes()
        {
            return new List<SysType>
            {
                SysType.Arcade,
                SysType.Console,
                SysType.ConsolePortable,
                SysType.GameStore,
                SysType.Collection,
                SysType.Standalone
            };
        }

    }
}
