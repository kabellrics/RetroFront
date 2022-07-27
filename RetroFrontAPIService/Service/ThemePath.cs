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
    public class CopyAndDLLObject
    {
        public string Source { get; set; }
        public string Dest { get; set; }
        public CopyAndDLLObject(string source, string dest)
        {
            {
                if (!string.IsNullOrEmpty(source))
                    Source = source;
                else
                    Source = string.Empty;

                if (!string.IsNullOrEmpty(dest))
                    Dest = dest;
                else
                    Dest = string.Empty;

            }
        }
    }
    public class DLLByteObject
    {
        public byte[] Source { get; set; }
        public string Dest { get; set; }
        public DLLByteObject(string dest, byte[] source = null)
        {
            {
                    Source = source;
                if (!string.IsNullOrEmpty(dest))
                    Dest = dest;
                else
                    Dest = string.Empty;

            }
        }
    }
}
