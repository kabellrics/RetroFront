using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using RetroFront.Models.IGDB;
using RetroFront.Services.Interface;

namespace RetroFront.Services.Implementation
{
    public class IGDBService : IIGDBService
    {
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

        public IEnumerable<Models.Search> GetPlatformByName(string name)
        {
            try
            {
                string urlrequest = "https://api.igdb.com/v4/platforms/?search=" + name + "&fields=id,name";
                var client = new RestClient(urlrequest);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                request.AddHeader("Authorization", $"Bearer {Bearer}");
                request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

                var requesturi = client.BuildUri(request);

                var response = client.Execute<IEnumerable<SearchResult>>(request, Method.POST);
                return (IEnumerable<Models.Search>)response.Data;
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }

        public IEnumerable<Models.Search> GetGameByName(string name)
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
                return (IEnumerable<Models.Search>)response.Data;
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }

        public Models.IGDB.IGDBGame GetDetailsGame(int id)
        {
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,cover.*,first_release_date,storyline,summary,version_title";
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<IEnumerable<Models.IGDB.IGDBGame>>(request);
            return response.Data.FirstOrDefault();// .Content;
        }
        public IEnumerable<Models.IGDB.Artwork> GetArtworksByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,artworks.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1,response.Content.Length-2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["artworks"] != null)
            {
                IList<JToken> results = jsondata["artworks"].Children().ToList();
                IList<Models.IGDB.Artwork> searchResults = new List<Models.IGDB.Artwork>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Models.IGDB.Artwork searchResult = result.ToObject<Models.IGDB.Artwork>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<Models.IGDB.Genre> GetGenresByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,genres.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["genres"] != null)
            {
                IList<JToken> results = jsondata["genres"].Children().ToList();
                IList<Models.IGDB.Genre> searchResults = new List<Models.IGDB.Genre>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Models.IGDB.Genre searchResult = result.ToObject<Models.IGDB.Genre>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<Models.IGDB.Screenshot> GetScreenshotsByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,screenshots.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["screenshots"] != null)
            {
                IList<JToken> results = jsondata["screenshots"].Children().ToList();
                IList<Models.IGDB.Screenshot> searchResults = new List<Models.IGDB.Screenshot>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Models.IGDB.Screenshot searchResult = result.ToObject<Models.IGDB.Screenshot>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<Models.IGDB.Video> GetVideosByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,videos.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["videos"] != null)
            {
                IList<JToken> results = jsondata["videos"].Children().ToList();
                IList<Models.IGDB.Video> searchResults = new List<Models.IGDB.Video>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Models.IGDB.Video searchResult = result.ToObject<Models.IGDB.Video>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<Models.IGDB.Theme> GetThemesByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,themes.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["themes"] != null)
            {
                IList<JToken> results = jsondata["themes"].Children().ToList();
                IList<Models.IGDB.Theme> searchResults = new List<Models.IGDB.Theme>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Models.IGDB.Theme searchResult = result.ToObject<Models.IGDB.Theme>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }

        public string GetCoverLink(string hash)
        {
            return $"https://images.igdb.com/igdb/image/upload/t_cover_big/" + hash + ".jpg";
        }
        public string GetArtWorkLink(string hash)
        {
            return $"https://images.igdb.com/igdb/image/upload/t_1080p/" + hash + ".jpg";
        }

    }
}
