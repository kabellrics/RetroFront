﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RetroFront.Models
{
    public class Emulator
    {
        public int EmulatorID { get; set; }
        public string Name { get; set; }
        public string Comand { get; set; }
        public string Extension { get; set; }

        public virtual Systeme Systeme { get; set; }
        public virtual ICollection<Game> Games { get; private set; } = new ObservableCollection<Game>();
    }
}
