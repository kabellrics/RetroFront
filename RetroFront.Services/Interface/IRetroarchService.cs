using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IRetroarchService
    {
        IEnumerable<RetroarchCore> GetInstalledCore(string retroarchpath);
        //string RetroarchEXEPath();
    }
}