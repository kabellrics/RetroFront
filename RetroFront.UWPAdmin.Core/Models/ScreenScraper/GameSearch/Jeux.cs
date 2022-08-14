using System.Collections.Generic;

namespace RetroFront.UWPAdmin.ScreenScraper.GameSearch
{
    public class Jeux
    {
        public string id { get; set; }
        public List<Nom> noms { get; set; }
        public Systeme systeme { get; set; }
        public object topstaff { get; set; }
        public string rotation { get; set; }
        public List<Media> medias { get; set; }
        public Editeur editeur { get; set; }
        public Developpeur developpeur { get; set; }
        public Joueurs joueurs { get; set; }
        public Note note { get; set; }
        public List<Synopsis> synopsis { get; set; }
        public List<Classification> classifications { get; set; }
        public List<Date> dates { get; set; }
        public List<Genre> genres { get; set; }
        public List<Mode> modes { get; set; }
        public List<Famille> familles { get; set; }
        public List<Numero> numeros { get; set; }
        public List<Theme> themes { get; set; }
        public List<Style> styles { get; set; }
    }
}
