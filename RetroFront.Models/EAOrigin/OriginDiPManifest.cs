﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RetroFront.Models.EAOrigin
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Only used for deserialization")]
    [XmlRoot("DiPManifest")]
    public class OriginDiPManifest
    {
        public class GameTitle
        {
            [XmlAttribute()]
            public string? locale { get; set; }

            [XmlText()]
            public string? Value { get; set; }
        }

        public class Launcher
        {
            [XmlElement("name")]
            public LauncherName[]? name { get; set; }

            public string? filePath { get; set; }

            public string? parameters { get; set; }

            public bool executeElevated { get; set; }

            public bool requires64BitOS { get; set; }

            public bool trial { get; set; }

            [XmlAttribute()]
            public string? uid { get; set; }
        }

        public class LauncherName
        {
            [XmlAttribute()]
            public string? locale { get; set; }

            [XmlText()]
            public string? Value { get; set; }
        }

        [XmlArrayItem("contentID")]
        public string[]? contentIDs { get; set; }

        [XmlArrayItem("gameTitle")]
        public GameTitle[]? gameTitles { get; set; }

        [XmlArrayItem("launcher")]
        public Launcher[]? runtime { get; set; }
    }
}
