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
                    SysDisplay.BigLogo,
                    SysDisplay.LogoBanner,
                    SysDisplay.CarrouselLogo,
                    SysDisplay.WheelLeftLogo,
                    SysDisplay.WheelRightLogo
            };
        }
        public List<RomDisplay> GetRomDisplays() 
        {
            return new List<RomDisplay>
            {
                    RomDisplay.WallBox,
                    RomDisplay.WallBanner,
                    RomDisplay.ListLogo,
                    RomDisplay.ListBanner,
                    RomDisplay.Screenshot,
                    RomDisplay.Fanart
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
