using System.Collections.Generic;

namespace RetroFront.UWPAdmin.ScreenScraper.System
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

        public string NomRecalbox { get { return noms.nom_recalbox; } }
        public string NomRetropie { get { return noms.nom_retropie; } }
        public string NomLaunchbox { get { return noms.nom_launchbox; } }
        public string NomCommun { get { return noms.noms_commun; } }
        public string NomUS { get { return noms.nom_us; } }
        public string NomEU { get { return noms.nom_eu; } }
        public string NomJP { get { return noms.nom_jp; } }
        public string NomCourant { 
            get 
            { 
                if (!string.IsNullOrEmpty(noms.nom_recalbox))
                { 
                    return noms.nom_recalbox;
                }
                if (!string.IsNullOrEmpty(noms.nom_retropie))
                {
                    return noms.nom_retropie;
                }
                if (!string.IsNullOrEmpty(noms.nom_launchbox))
                {
                    return noms.nom_launchbox;
                }
                if (!string.IsNullOrEmpty(noms.noms_commun))
                {
                    return noms.noms_commun;
                }
                return string.Empty;

            } 
        }
    }
}
