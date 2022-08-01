using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemeDetailService: BaseService
    {
        public DisplaySysteme GetSysteme(int ID)
        {
            var sys = systemeClient.SystemeGet(ID).Result;
            return new DisplaySysteme(sys);
        }
        public async Task UpdateSysteme(DisplaySysteme system)
        {
            await systemeClient.SystemePutAsync(system.ID, system.Systeme);
        }
    }
}
