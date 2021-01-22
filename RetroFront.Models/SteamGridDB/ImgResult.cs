using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.SteamGridDB
{
    public class ImgResult
    {
        public int id { get; set; }
        public int score { get; set; }
        public string style { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool nsfw { get; set; }
        public bool humor { get; set; }
        public object notes { get; set; }
        public string language { get; set; }
        public string url { get; set; }
        public string thumb { get; set; }
        public bool @lock { get; set; }
        public bool epilepsy { get; set; }
        public Author author { get; set; }
    }
}
