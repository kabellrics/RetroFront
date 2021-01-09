using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RetroFront.Models
{
    public class Systeme
    {
        public int SystemeID { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public string Platform { get; set; }
        public string Theme { get; set; }
        public virtual ICollection<Emulator> Emulators { get; private set; } = new ObservableCollection<Emulator>();
    }
}
