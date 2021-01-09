using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class LocalFileService
    {
        public LocalFileService()
        {
            System.IO.Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront");
            System.IO.Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.retrofront\\themes");
        }

    }
}
