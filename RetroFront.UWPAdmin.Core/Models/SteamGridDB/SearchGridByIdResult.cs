﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.SteamGridDB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class SearchGridByIdResult
    {
        public bool success { get; set; }
        public List<ImgResult> data { get; set; }
    }
}
