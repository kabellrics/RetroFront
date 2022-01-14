using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models
{
    public class AppSettings
    {
        public string CurrentTheme { get; set; }
        public string ScreenScraperID { get; set; }
        public string ScreenScraperPWD { get; set; }
        public string SGDBKey { get; set; }
        public string AppSettingsLocation { get; set; }
        public string AppSettingsFolder { get; set; }
        //public string RetroarchPath { get; set; }
        //public string RetroarchCMD { get; set; }
        public SysDisplay CurrentSysDisplay { get; set; }
        public RomDisplay CurrentGameDisplay { get; set; }
        public string DefaultBCK { get; set; }

    }
}
