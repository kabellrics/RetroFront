﻿using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class SystemesService
    {
        private SystemeClient systemeClient = new SystemeClient();
        public IEnumerable<DisplaySysteme> GetSystemes()
        {
            foreach(var sys in systemeClient.SystemeGet().Result.OrderBy(x=>x.Type).ThenBy(x=>x.Name))
            {
                yield return new DisplaySysteme(sys);
            }
        }

    }
}
