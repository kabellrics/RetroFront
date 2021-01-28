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
    public enum SGDBType
    {
        logo,
        background,
        fanart,
        boxart
    }

    public enum SysDisplay
    {
        [Description("Big Logo")] 
        BigLogo,
        [Description("Banner")]
        LogoBanner,
        [Description("Carousel")]
        CarrouselLogo,
        [Description("Left Spin")]
        WheelLeftLogo,
        [Description("Right Spin")]
        WheelRightLogo
    }
}
