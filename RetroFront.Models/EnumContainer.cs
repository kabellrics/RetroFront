using System;
using System.Collections.Generic;
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
        BigLogo,
        LogoBanner,
        CarrouselLogo,
        WheelLogo
    }
}
