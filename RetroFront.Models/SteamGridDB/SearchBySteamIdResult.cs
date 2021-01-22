using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.SteamGridDB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class SearchBySteamIdResult
    {
        public bool success { get; set; }
        public List<DataSearch> data { get; set; }
    }
}
