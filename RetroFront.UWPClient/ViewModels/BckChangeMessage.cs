using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.ViewModels
{
    public class BckChangeMessage : ValueChangedMessage<int>
    {
        public BckChangeMessage(int ID) : base(ID)
        {
        }
    }
}
