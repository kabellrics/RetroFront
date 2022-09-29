using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameByURLLauncher.Models
{
    public class Systeme
    {
        public int SystemeID { get; set; }

        public int SystemeSCSPID { get; set; }

        public string Name { get; set; }

        public string Shortname { get; set; }

        public SysType Type { get; set; }

        public string Logo { get; set; }

        public string Screenshoot { get; set; }

        public string Video { get; set; }

    }
    public enum SysType
    {

        Arcade = 0,

        Console = 1,

        ConsolePortable = 2,

        GameStore = 3,

        Collection = 4,

        Standalone = 5,

    }
}
