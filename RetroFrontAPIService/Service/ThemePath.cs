using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service
{
    public class ThemePath
    {
        public string Path { get; set; }
        public ThemePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
                Path = path;
            else
                Path = string.Empty;
        }
    }
}
