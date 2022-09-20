using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RetroFront.Models.EAOrigin
{
    public class OriginGameManifest
    {
        public class LocaleInfo
        {
            public string? title { get; set; }

            [XmlAttribute()]
            public string? locale { get; set; }
        }

        public class Metadata
        {
            [XmlElement("localeInfo")]
            public LocaleInfo[]? localeInfo { get; set; }
        }

        [XmlArrayItem("contentID")]
        public string[]? contentIDs { get; set; }

        public Metadata? metadata { get; set; }
    }
}
