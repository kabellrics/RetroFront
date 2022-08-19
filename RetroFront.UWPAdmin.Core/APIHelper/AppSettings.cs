using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.APIHelper
{

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class AppSettings
    {
        [Newtonsoft.Json.JsonProperty("currentTheme", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CurrentTheme { get; set; }

        [Newtonsoft.Json.JsonProperty("screenScraperID", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ScreenScraperID { get; set; }

        [Newtonsoft.Json.JsonProperty("screenScraperPWD", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ScreenScraperPWD { get; set; }

        [Newtonsoft.Json.JsonProperty("sgdbKey", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SgdbKey { get; set; }

        [Newtonsoft.Json.JsonProperty("appSettingsLocation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AppSettingsLocation { get; set; }

        [Newtonsoft.Json.JsonProperty("appSettingsFolder", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AppSettingsFolder { get; set; }

        [Newtonsoft.Json.JsonProperty("currentSysDisplay", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SysDisplay CurrentSysDisplay { get; set; }

        [Newtonsoft.Json.JsonProperty("currentGameDisplay", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public RomDisplay CurrentGameDisplay { get; set; }

        [Newtonsoft.Json.JsonProperty("currentHomeDisplay", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public HomeDisplay CurrentHomeDisplay { get; set; }

        [Newtonsoft.Json.JsonProperty("currentGameDetailDisplay", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public GameDetailDisplay CurrentGameDetailDisplay { get; set; }

        [Newtonsoft.Json.JsonProperty("defaultBCK", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DefaultBCK { get; set; }

        [Newtonsoft.Json.JsonProperty("showAll", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ShowAll { get; set; }

        [Newtonsoft.Json.JsonProperty("showFav", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ShowFav { get; set; }

        [Newtonsoft.Json.JsonProperty("showLastPlayed", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ShowLastPlayed { get; set; }

        [Newtonsoft.Json.JsonProperty("showMostPlayed", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ShowMostPlayed { get; set; }
        [Newtonsoft.Json.JsonProperty("RetroarchPath", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string RetroarchPath { get; set; }

    }
}
