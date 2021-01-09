using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IRetroarchService
    {
        IEnumerable<string> GetInstalledCore();
    }
}