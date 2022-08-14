using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.ScreenScraper.GameSearch
{
    public class Response
    {
        public Serveurs serveurs { get; set; }
        public Ssuser ssuser { get; set; }
        public List<Jeux> jeux { get; set; }
    }
}
