using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.SteamGridDB
{
    public class DataSearch: Search
    {
        public int release_date { get; set; }
        public List<string> types { get; set; }
        public bool verified { get; set; }
    }
}
