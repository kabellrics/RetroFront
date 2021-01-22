using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.SteamGridDB
{
    public class DataSearch
    {
        public int id { get; set; }
        public string name { get; set; }
        public int release_date { get; set; }
        public List<string> types { get; set; }
        public bool verified { get; set; }
    }
}
