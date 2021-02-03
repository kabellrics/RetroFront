using RetroFront.Models.IGDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.Services.Interface
{
    public interface IIGDBService
    {
        Task<IEnumerable<SearchResult>> GetPlatformByName(string name);
        Task<IEnumerable<SearchResult>> GetGameByName(string name);
        Task<IGDBGame> GetDetailsGame(int id);
        string GetCoverLink(string hash);
        string GetArtWorkLink(string hash);
    }
}
