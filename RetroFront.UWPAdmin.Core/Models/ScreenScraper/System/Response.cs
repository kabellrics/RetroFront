using System.Collections.Generic;

namespace RetroFront.UWPAdmin.ScreenScraper.System
{
    public class Response
    {
        public Serveurs serveurs { get; set; }
        public Ssuser ssuser { get; set; }
        public List<Systeme> systemes { get; set; }
    }
}
