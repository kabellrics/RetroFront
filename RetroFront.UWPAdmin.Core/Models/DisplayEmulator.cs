using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Models
{
    public class DisplayEmulator : DisplayBase
    {
        public DisplayEmulator(Emulator Emu) : base()
        {
            Emulator = Emu;
        }

        public Emulator Emulator{ get; }
        public int ID
        {
            get => Emulator.EmulatorID;
            set => SetProperty(Emulator.EmulatorID, value, Emulator, (emulator, item) => Emulator.EmulatorID = item);
        }
        public int SystemeID
        {
            get => Emulator.SystemeID;
            set => SetProperty(Emulator.SystemeID, value, Emulator, (emulator, item) => Emulator.SystemeID = item);
        }
        public string Name
        {
            get => Emulator.Name;
            set { SetProperty(Emulator.Name, value, Emulator, (emulator, item) => Emulator.Name = item);
                ChangeStatus();
            }
        }
        public string Chemin
        {
            get => Emulator.Chemin;
            set { SetProperty(Emulator.Chemin, value, Emulator, (emulator, item) => Emulator.Chemin = item);
                ChangeStatus();
            }
        }
        public string Command
        {
            get => Emulator.Command;
            set { SetProperty(Emulator.Command, value, Emulator, (emulator, item) => Emulator.Command = item);
                ChangeStatus();
            }
        }
        public string Extension
        {
            get => Emulator.Extension;
            set { SetProperty(Emulator.Extension, value, Emulator, (emulator, item) => Emulator.Extension = item);
                ChangeStatus();
            }
        }
        public bool IsDuplicate
        {
            get => Emulator.IsDuplicate;
            set { SetProperty(Emulator.IsDuplicate, value, Emulator, (emulator, item) => Emulator.IsDuplicate = item);
                ChangeStatus();
            }
        }
    }
}
