using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.APIHelper
{
    public class EnumHelper
    {
    }
    public enum ScraperType
    {
        Full,
        Metadata,
        Logo,
        Boxart,
        ArtWork,
        Banner,
        Video,
        Bezel
    }
    public enum ScraperSource
    {
        IGDB,
        SGDB,
        Screenscraper,
        Local
    }
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0))")]
    public enum SysType
    {

        Arcade = 0,

        Console = 1,

        ConsolePortable = 2,

        GameStore = 3,

        Collection = 4,

        Standalone = 5,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0))")]
    public enum SysDisplay
    {

        _0 = 0,

        _1 = 1,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0))")]
    public enum HomeDisplay
    {

        _0 = 0,

        _1 = 1,

        _2 = 2,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0))")]
    public enum RomDisplay
    {

        _0 = 0,

        _1 = 1,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0))")]
    public enum GameDetailDisplay
    {

        _0 = 0,

        _1 = 1,

    }
}
