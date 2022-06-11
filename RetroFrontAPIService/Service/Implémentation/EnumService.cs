using RetroFront.Models;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Implémentation
{
    public class EnumService : IEnumService
    {
        public List<SysDisplay> GetSysDisplays()
        {
            return new List<SysDisplay>
            {
                SysDisplay.FlipView,
                SysDisplay.Modern,
                SysDisplay.BigLogo,
                    SysDisplay.LogoBanner,
                    SysDisplay.CarrouselLogo,
                    SysDisplay.WheelLeftLogo,
                    SysDisplay.WheelRightLogo,
                    SysDisplay.WallLogo,
                    SysDisplay.Bezel
            };
        }
        public List<HomeDisplay> GetHomeDisplays()
        {
            return new List<HomeDisplay>
            {
                HomeDisplay.FlixView,
                HomeDisplay.GameOS,
                HomeDisplay.Hub
            };
        }
        public List<GameDetailDisplay> GetGameDetailDisplays()
        {
            return new List<GameDetailDisplay>
            {
                GameDetailDisplay.Artwork,
                GameDetailDisplay.Full
            };
        }
        public List<RomDisplay> GetRomDisplays()
        {
            return new List<RomDisplay>
            {
                RomDisplay.WallBox,
                RomDisplay.Modern,
                    RomDisplay.WallLogo,
                    RomDisplay.ListBox,
                    RomDisplay.ListBanner,
                    RomDisplay.Screenshot,
                    RomDisplay.Fanart,
                    RomDisplay.FanartBox,
                    RomDisplay.BigBox,
                    RomDisplay.FlixView,
                    RomDisplay.Detail,
                    RomDisplay.BigModern
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
