using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using RestSharp;
using RetroFront.Models.IGDB;
using RetroFront.Services.Interface;

namespace RetroFront.Services.Implementation
{
    public class IGDBService : IIGDBService
    {
        private IGDBClient IGDBClient;
        private FileJSONService FileJSONService;
        private string id = "fah6fktmuph3zpfelt66hoqk4zn62i";
        private string secret = "0wxvdw6ho6u2lho7mp2i9jvnx9xlo4";
        private string Bearer;
        public IGDBService()
        {
            FileJSONService = new FileJSONService();
            GetBearer();
            //IGDBClient = new IGDBClient(id, secret);
        }

        private void GetBearer()
        {
            try
            {
                var client = new RestClient("https://id.twitch.tv/oauth2/token");
                var request = new RestRequest(Method.POST);
                //request.Resource = "/token";
                request.AddParameter("client_id", id);
                request.AddParameter("client_secret", secret);
                request.AddParameter("grant_type", "client_credentials");

                var response = client.Execute<AccessResponse>(request);
                var responseData = response.Data;
                Bearer = responseData.access_token;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        public async Task<IEnumerable<SearchResult>> GetPlatformByName(string name)
        {
            try
            {
                string urlrequest = "https://api.igdb.com/v4/platforms/?search="+name+ "&fields=id,name";
                var client = new RestClient(urlrequest);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                request.AddHeader("Authorization", $"Bearer {Bearer}");
                request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

                var requesturi = client.BuildUri(request);

                var response = client.Execute<IEnumerable<SearchResult>>(request,Method.POST);
                return response.Data;
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }

        public async Task<IEnumerable<SearchResult>> GetGameByName(string name)
        {
            try
            {
                string urlrequest = "https://api.igdb.com/v4/games/?search=" + name + "&fields=id,name,version_title";
                var client = new RestClient(urlrequest);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                request.AddHeader("Authorization", $"Bearer {Bearer}");
                request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");
                var requesturi = client.BuildUri(request);

                var response = client.Execute<IEnumerable<SearchResult>>(request, Method.POST);
                return response.Data;
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }

        public async Task<Models.IGDB.IGDBGame> GetDetailsGame(int id)
        {
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<Models.IGDB.IGDBGame>(request);
            return response.Data;
        }

        public string GetCoverLink(string hash)
        {
            return $"https://images.igdb.com/igdb/image/upload/t_cover_big/"+hash+".jpg";
        }
        public string GetArtWorkLink(string hash)
        {
            return $"https://images.igdb.com/igdb/image/upload/t_1080p/" +hash+".jpg";
        }
    }
}
