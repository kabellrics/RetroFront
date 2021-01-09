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

    public class SystemeList
    {        public List<XMLSystem> systeme { get; set; }
    }
    public class XMLSystem
    {
        public string name { get; set; }
        public string fullname { get; set; }
        public string platform { get; set; }
        public string theme { get; set; }
    }
}
