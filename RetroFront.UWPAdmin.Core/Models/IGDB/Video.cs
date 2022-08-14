using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.IGDB
{
    public class Video
    {
        public int id { get; set; }
        public int game { get; set; }
        public string name { get; set; }
        public string video_id { get; set; }
        public string checksum { get; set; }
    }
}
