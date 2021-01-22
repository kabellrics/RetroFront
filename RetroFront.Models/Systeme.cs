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
        public SysType Type {get;set;}
    }



    public class SystemeList
    {        public List<XMLSystem> systeme { get; set; }
    }
    public class XMLSystem
    {
        public string name { get; set; }
        public string fullname { get; set; }
        public int Type { get; set; }
    }
}
