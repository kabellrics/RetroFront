using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemesService: BaseService
    {
        public async Task UpdateSysteme(DisplaySysteme system)
        {
            await systemeClient.SystemePutAsync(system.ID, system.Systeme);
        }
    }
}
