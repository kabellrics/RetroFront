using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemeDetailService
    {
        private SystemeClient systemeClient = new SystemeClient();
        public DisplaySysteme GetSysteme(int ID)
        {
            var sys = systemeClient.SystemeGet(ID).Result;
            return new DisplaySysteme(sys);
        }
    }
}
