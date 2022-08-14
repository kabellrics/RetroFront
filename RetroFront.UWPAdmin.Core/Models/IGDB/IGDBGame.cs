using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.IGDB
{
    public class IGDBGame
    {
        public int id { get; set; }
        //public List<Artwork> artworks { get; set; }
        public Cover cover { get; set; }
        public int first_release_date { get; set; }
        //public List<Genre> genres { get; set; }
        public string name { get; set; }
        //public List<Screenshot> screenshots { get; set; }
        public string storyline { get; set; }
        public string summary { get; set; }
        //public List<Theme> themes { get; set; }
        //public List<Video> videos { get; set; }
    }
}
