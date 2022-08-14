using System.Collections.Generic;

namespace RetroFront.UWPAdmin.ScreenScraper.GameSearch
{
    public class Theme
    {
        public string id { get; set; }
        public string nomcourt { get; set; }
        public string principale { get; set; }
        public string parentid { get; set; }
        public List<Nom> noms { get; set; }
    }
}
