using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models
{
    public class GameRom
    {
        public int ID { get; set; }
        public int SteamID { get; set; }
        public string OriginID { get; set; }
        public string EpicID { get; set; }
        public int ScreenScraperID { get; set; }
        public int SGDBID { get; set; }
        public int IGDBID { get; set; }
        public int RAWGID { get; set; }
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
        public int EmulatorID { get; set; }
        public string Plateforme { get; set; }
    }
}
