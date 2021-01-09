using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IFileJSONService
    {
        IEnumerable<Systeme> GetAllSysFromJSON();
    }
}