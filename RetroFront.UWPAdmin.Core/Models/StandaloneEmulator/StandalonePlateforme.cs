using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.StandaloneEmulator
{
    public class StandalonePlateforme
    {
        public string Name { get; set; }
        public string Shortname { get; set; }
        public string StartupArguments { get; set; }
        public List<string> Platforms { get; set; }
        public List<string> ImageExtensions { get; set; }
        public string StartupExecutable { get; set; }
    }
}
