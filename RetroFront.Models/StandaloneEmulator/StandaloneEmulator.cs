using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Models.StandaloneEmulator
{
    public class StandaloneEmulator
    {
        public string Name { get; set; }
        public List<StandalonePlateforme> Plateformes { get; set; }
    }
}
