using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using RetroFront.Services.Interface;

namespace RetroFront.Services.Implementation
{
    public class IGDBService : IIGDBService
    {
        private IGDBClient IGDBClient;
        private FileJSONService FileJSONService;
        private string id = "fah6fktmuph3zpfelt66hoqk4zn62i";
        private string secret = "0wxvdw6ho6u2lho7mp2i9jvnx9xlo4";

        public IGDBService()
        {
            FileJSONService = new FileJSONService();
            IGDBClient = new IGDBClient(id, secret);
        }

        public async Task<IGDB.Models.Platform> GetPlatformByName(string name)
        {
            try
            {
                //, query: $"fields checksum,created_at,generation,name,platform_logo,slug,summary,url; where name = {name}; "
                var platforms = await IGDBClient.QueryAsync<Platform>(IGDBClient.Endpoints.Platforms);
                var listplats = platforms.ToList();
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }
    }
}
