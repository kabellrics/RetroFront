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
        public List<KeyValuePair<int,string>> GetSysDisplayValue()
        {
            return new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>((int)SysDisplay.FlipView,SysDisplay.FlipView.ToString()),
                new KeyValuePair<int, string>((int)SysDisplay.CarrouselLogo,SysDisplay.CarrouselLogo.ToString())
            };
        }
        public List<SysDisplay> GetSysDisplays()
        {
            return new List<SysDisplay>
            {
                SysDisplay.FlipView,
                SysDisplay.CarrouselLogo
                //SysDisplay.Modern,
                //SysDisplay.BigLogo,
                //    SysDisplay.LogoBanner,
                //    SysDisplay.CarrouselLogo,
                //    SysDisplay.WheelLeftLogo,
                //    SysDisplay.WheelRightLogo,
                //    SysDisplay.WallLogo,
                //    SysDisplay.Bezel
            };
        }
        public List<KeyValuePair<int, string>> GetHomeDisplayValue()
        {
            return new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>((int)HomeDisplay.GameOS,HomeDisplay.GameOS.ToString()),
                new KeyValuePair<int, string>((int)HomeDisplay.Hub,HomeDisplay.Hub.ToString()),
                new KeyValuePair<int, string>((int)HomeDisplay.SteamView,HomeDisplay.SteamView.ToString())
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
        public List<KeyValuePair<int, string>> GetGameDetailDisplayValue()
        {
            return new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>((int)GameDetailDisplay.Artwork,GameDetailDisplay.Artwork.ToString()),
                new KeyValuePair<int, string>((int)GameDetailDisplay.Full,GameDetailDisplay.Full.ToString())
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
        public List<KeyValuePair<int, string>> GetRomDisplayValue()
        {
            return new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>((int)RomDisplay.WallBox,RomDisplay.WallBox.ToString()),
                new KeyValuePair<int, string>((int)RomDisplay.WallBanner,RomDisplay.WallBanner.ToString())
            };
        }
        public List<RomDisplay> GetRomDisplays()
        {
            return new List<RomDisplay>
            {
                RomDisplay.WallBox,
                RomDisplay.WallBanner,
                //RomDisplay.Modern,
                //    RomDisplay.WallLogo,
                //    RomDisplay.ListBox,
                //    RomDisplay.ListBanner,
                //    RomDisplay.Screenshot,
                //    RomDisplay.Fanart,
                //    RomDisplay.FanartBox,
                //    RomDisplay.BigBox,
                //    RomDisplay.FlixView,
                //    RomDisplay.Detail,
                //    RomDisplay.BigModern
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
