using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.SteamGridDB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class SearchHeroesByIdResult
    {
        public bool success { get; set; }
        public List<ImgResult> data { get; set; }
    }
}
