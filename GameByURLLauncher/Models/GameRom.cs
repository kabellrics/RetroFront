using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameByURLLauncher.Models
{
    public class GameRom
    {
        public int Id { get; set; }

        public int SteamID { get; set; }

        public string OriginID { get; set; }

        public string EpicID { get; set; }

        public int ScreenScraperID { get; set; }

        public int Sgdbid { get; set; }

        public int Igdbid { get; set; }

        public int Rawgid { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Desc { get; set; }

        public string Year { get; set; }

        public string Editeur { get; set; }

        public string Dev { get; set; }

        public string Genre { get; set; }

        public string Boxart { get; set; }

        public string Logo { get; set; }

        public string Screenshoot { get; set; }

        public string Fanart { get; set; }

        public string RecalView { get; set; }

        public string Video { get; set; }

        public string TitleScreen { get; set; }

        public bool IsDuplicate { get; set; }

        public bool IsFavorite { get; set; }
        public int EmulatorID { get; set; }

        public int NbTimeStarted { get; set; }

        public System.DateTimeOffset? LastStart { get; set; }

        public string Plateforme { get; set; }
    }
}
