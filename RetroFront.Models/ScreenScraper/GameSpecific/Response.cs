using RetroFront.Models.ScreenScraper.GameSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.ScreenScraper.GameSpecific
{
    public class Response
    {
        public Serveurs serveurs { get; set; }
        public Ssuser ssuser { get; set; }
        public Jeux jeu { get; set; }
    }
}
