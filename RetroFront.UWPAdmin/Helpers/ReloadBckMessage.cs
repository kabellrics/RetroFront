using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Helpers
{
    public class ReloadBckMessage : ValueChangedMessage<object>
    {
        public ReloadBckMessage(object ID) : base(ID)
        {
        }
    }
    public enum LocalGame
    {
        Steam,
        Epic,
        Origin
    }
}
