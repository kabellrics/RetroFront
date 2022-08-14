using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.SteamGridDB
{
    public class DataSearch: Search
    {
        public int release_date { get; set; }
        public List<string> types { get; set; }
        public bool verified { get; set; }
    }
}
