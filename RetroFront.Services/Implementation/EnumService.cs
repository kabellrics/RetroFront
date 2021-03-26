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
                    SysDisplay.WheelRightLogo,
                    SysDisplay.WallLogo,
                    SysDisplay.Bezel
            };
        }
        public List<RomDisplay> GetRomDisplays() 
        {
            return new List<RomDisplay>
            {
                    RomDisplay.WallBox,
                    RomDisplay.WallBanner,
                    RomDisplay.WallLogo,
                    RomDisplay.ListBox,
                    RomDisplay.ListBanner,
                    RomDisplay.Screenshot,
                    RomDisplay.Fanart,
                    RomDisplay.FanartBox
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
