using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameByURLLauncher.Models
{
    public class Emulator
    {
        public int EmulatorID { get; set; }

        public string Name { get; set; }

        public string Chemin { get; set; }

        public string Command { get; set; }

        public string Extension { get; set; }

        public bool IsDuplicate { get; set; }

        public int SystemeID { get; set; }
    }
}
