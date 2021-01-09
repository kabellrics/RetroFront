using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class RetroarchService : IRetroarchService
    {
        private const string RetroarchPath = @"C:\Users\yflec\AppData\Roaming\RetroArch";
        //private const string RetroarchFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.emulationstation";
        public IEnumerable<string> GetInstalledCore()
        {
            return System.IO.Directory.EnumerateFiles(System.IO.Path.Combine(RetroarchPath, "cores"));
        }

    }
}
