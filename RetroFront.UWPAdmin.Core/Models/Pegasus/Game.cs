using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Pegasus
{
    public class Game
    {
        public string Name { get; set; }
        public string SortName { get; set; }
        public string File { get; set; }
        public string Developer { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string BoxFront { get; set; }
        public string Logo { get; set; }
        public string Video { get; set; }
        public string Fanart { get; set; }
        public List<string> Screenshoot { get; set; }
        public Game()
        {
            Screenshoot = new List<string>();
        }
    }
}
