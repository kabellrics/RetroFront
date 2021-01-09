﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Desc { get; set; }
        public string Year { get; set; }
        public string Editeur { get; set; }
        public string Dev { get; set; }
        public string Boxart { get; set; }
        public string Fanart { get; set; }
    }
}