using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemesService
    {
        private SystemeClient systemeClient = new SystemeClient();
        public async Task<IEnumerable<DisplaySysteme>> GetSystemes()
        {
            List<DisplaySysteme> displaySystemes = new List<DisplaySysteme>();
            var result = await systemeClient.SystemeGetAsync();
            foreach (var sys in result.Result.OrderBy(x=>x.Type).ThenBy(x=>x.Name))
            {
                displaySystemes.Add(new DisplaySysteme(sys));
            }
            return displaySystemes;
        }

    }
}
