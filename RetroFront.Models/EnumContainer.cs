using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RetroFront.Models
{
    class EnumContainer
    {
    }
    public enum SysType
    {        
        Arcade,
        Console,
        ConsolePortable,
        GameStore,
        Collection,
        Standalone
    }
    //public enum SGDBType
    //{
    //    logo,
    //    background,
    //    fanart,
    //    boxart
    //}
    public enum ScraperSource
    {
        IGDB,
        SGDB,
        Screenscraper,
        Local
    }
    public enum ScraperType
    {
        Full,
        Metadata,
        Logo,
        Boxart,
        ArtWork,
        Banner,
        Video
    }

    public enum SysDisplay
    {
        [Description("Modern")]
        Modern, 
        [Description("Big Logo")] 
        BigLogo,
        [Description("Banner")]
        LogoBanner,
        [Description("Carousel")]
        CarrouselLogo,
        [Description("Left Spin")]
        WheelLeftLogo,
        [Description("Right Spin")]
        WheelRightLogo,
        [Description("Wall Logo")]
        WallLogo,
        [Description("Bezel")]
        Bezel
    }

    public enum RomDisplay
    {
        [Description("Modern")]
        Modern,
        [Description("Wall Box")] 
        WallBox,
        [Description("Wall Logo")] 
        WallLogo,
        [Description("Box Detail")] 
        ListBox,
        [Description("Banner Detail")] 
        ListBanner,
        [Description("Screenshot")] 
        Screenshot,
        [Description("Carousel Banner")] 
        Fanart,
        [Description("Carousel Box")] 
        FanartBox,
        [Description("Big Box")] 
        BigBox,
        [Description("Flix")] 
        FlixView,
        [Description("Detail")]
        Detail,
    }
}
