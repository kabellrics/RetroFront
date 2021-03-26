using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroFront.ViewModels
{
    public class SCSPSystemeViewModel : ViewModelBase
    {
        private Models.ScreenScraper.System.Systeme _SCSPSysteme;
        public Models.ScreenScraper.System.Systeme SCSPSysteme
        {
            get { return _SCSPSysteme; }
            set { _SCSPSysteme = value;RaisePropertyChanged(); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;RaisePropertyChanged(); }
        }
        private string _shortname;
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value;RaisePropertyChanged(); }
        }
        public SCSPSystemeViewModel(Models.ScreenScraper.System.Systeme Sys)
        {
            SCSPSysteme = Sys;
            if(!string.IsNullOrEmpty(Sys.noms.noms_commun))
            {
                Name = Sys.noms.noms_commun.Split(",").FirstOrDefault();
            }
            else if(!string.IsNullOrEmpty(Sys.noms.nom_eu))
            {
                Name = Sys.noms.nom_eu.Split(",").FirstOrDefault();
            }
            else if(!string.IsNullOrEmpty(Sys.noms.nom_us))
            {
                Name = Sys.noms.nom_us.Split(",").FirstOrDefault();
            }
            else if(!string.IsNullOrEmpty(Sys.noms.nom_jp))
            {
                Name = Sys.noms.nom_jp.Split(",").FirstOrDefault();
            }

            if(!string.IsNullOrEmpty(Sys.noms.nom_recalbox))
            {
                ShortName = Sys.noms.nom_recalbox;
            }
            else if (!string.IsNullOrEmpty(Sys.noms.nom_retropie))
            {
                ShortName = Sys.noms.nom_retropie;
            }
        }
    }
}
