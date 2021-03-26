using System.Collections.Generic;

namespace RetroFront.Models.ScreenScraper.System
{
    public class Systeme
    {
        public int id { get; set; }
        public Noms noms { get; set; }
        public string extensions { get; set; }
        public string compagnie { get; set; }
        public string type { get; set; }
        public string datedebut { get; set; }
        public string datefin { get; set; }
        public string romtype { get; set; }
        public string supporttype { get; set; }
        public List<Media> medias { get; set; }
        public int? parentid { get; set; }
    }
}
