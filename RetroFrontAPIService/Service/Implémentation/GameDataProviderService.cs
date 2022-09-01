using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RetroFront.Models.IGDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFront.Models.ScreenScraper;
using RetroFront.Models;
using System.Net;
using RetroFront.Models.ScreenScraper.GameSearch;
using RetroFront.Models.SteamGridDB;
using RetroFrontAPIService.Service.Interface;

namespace RetroFrontAPIService.Service.Implémentation
{
    public class GameDataProviderService : IGameDataProviderService
    {
        RestClient sgdbclient;
        RestClient sscpclient;
        private string devid;
        private string devpwd;
        private FileJSONService FileJSONService = new FileJSONService();
        private string id = "fah6fktmuph3zpfelt66hoqk4zn62i";
        private string secret = "0wxvdw6ho6u2lho7mp2i9jvnx9xlo4";
        private string Bearer;
        private string apipath = @"https://www.steamgriddb.com/api/v2";

        public GameDataProviderService()
        {
            sgdbclient = new RestClient(apipath);
            sgdbclient.Authenticator = new JwtAuthenticator(FileJSONService.appSettings.SGDBKey);
            sscpclient = new RestClient("https://www.screenscraper.fr/api2");
            sscpclient.Timeout = -1;
            GetBearer();
        }
        #region IGDB
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
        public IEnumerable<RetroFront.Models.Search> GetPlatformByName(string name)
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
                return (IEnumerable<RetroFront.Models.Search>)response.Data;
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }
        public IEnumerable<RetroFront.Models.Search> GetGameByName(string name)
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
                return (IEnumerable<RetroFront.Models.Search>)response.Data;
            }
            catch (Exception ex)
            {
                //throw;
            }
            return null;
        }
        public RetroFront.Models.IGDB.IGDBGame GetDetailsGame(int id)
        {
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,cover.*,first_release_date,storyline,summary,version_title";
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<IEnumerable<RetroFront.Models.IGDB.IGDBGame>>(request);
            return response.Data.FirstOrDefault();// .Content;
        }
        public IEnumerable<RetroFront.Models.IGDB.Artwork> GetArtworksByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,artworks.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<RetroFront.Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["artworks"] != null)
            {
                IList<JToken> results = jsondata["artworks"].Children().ToList();
                IList<RetroFront.Models.IGDB.Artwork> searchResults = new List<RetroFront.Models.IGDB.Artwork>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    RetroFront.Models.IGDB.Artwork searchResult = result.ToObject<RetroFront.Models.IGDB.Artwork>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.Genre> GetGenresByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,genres.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<RetroFront.Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["genres"] != null)
            {
                IList<JToken> results = jsondata["genres"].Children().ToList();
                IList<RetroFront.Models.IGDB.Genre> searchResults = new List<RetroFront.Models.IGDB.Genre>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    RetroFront.Models.IGDB.Genre searchResult = result.ToObject<RetroFront.Models.IGDB.Genre>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.Artwork> GetScreenshotsByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,screenshots.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<RetroFront.Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["screenshots"] != null)
            {
                IList<JToken> results = jsondata["screenshots"].Children().ToList();
                IList<RetroFront.Models.IGDB.Artwork> searchResults = new List<RetroFront.Models.IGDB.Artwork>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    RetroFront.Models.IGDB.Artwork searchResult = result.ToObject<RetroFront.Models.IGDB.Artwork>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.Video> GetVideosByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,videos.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<RetroFront.Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["videos"] != null)
            {
                IList<JToken> results = jsondata["videos"].Children().ToList();
                IList<RetroFront.Models.IGDB.Video> searchResults = new List<RetroFront.Models.IGDB.Video>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    RetroFront.Models.IGDB.Video searchResult = result.ToObject<RetroFront.Models.IGDB.Video>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.Theme> GetThemesByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,themes.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<RetroFront.Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["themes"] != null)
            {
                IList<JToken> results = jsondata["themes"].Children().ToList();
                IList<RetroFront.Models.IGDB.Theme> searchResults = new List<RetroFront.Models.IGDB.Theme>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    RetroFront.Models.IGDB.Theme searchResult = result.ToObject<RetroFront.Models.IGDB.Theme>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.InvolvedCompany> GetInvolvedCompanyByGameId(int id)
        {
            //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
            string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,involved_companies.*";
            var client = new RestClient(urlrequest);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

            var response = client.Execute<RetroFront.Models.IGDB.IGDBGame>(request);
            var rawdata = response.Content.Substring(1, response.Content.Length - 2);

            JObject jsondata = JObject.Parse(rawdata);
            if (jsondata["involved_companies"] != null)
            {
                IList<JToken> results = jsondata["involved_companies"].Children().ToList();
                IList<RetroFront.Models.IGDB.InvolvedCompany> searchResults = new List<RetroFront.Models.IGDB.InvolvedCompany>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    RetroFront.Models.IGDB.InvolvedCompany searchResult = result.ToObject<RetroFront.Models.IGDB.InvolvedCompany>();
                    searchResults.Add(searchResult);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.Company> GetDevByGameId(int id)
        {
            var involvedComps = this.GetInvolvedCompanyByGameId(id);
            if (involvedComps != null)
            {
                var devlist = involvedComps.Where(x => x.developer == true || x.supporting == true);
                IList<RetroFront.Models.IGDB.Company> searchResults = new List<RetroFront.Models.IGDB.Company>();
                foreach (var dev in devlist)
                {
                    string urlrequest = "https://api.igdb.com/v4/companies/" + dev.company.ToString() + "?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;";
                    //var client = new RestClient("https://api.igdb.com/v4/companies/1514?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;");
                    var client = new RestClient(urlrequest);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                    request.AddHeader("Authorization", $"Bearer {Bearer}");
                    request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");
                    var response = client.Execute<RetroFront.Models.IGDB.Company>(request);
                    var rawdata = response.Content.Substring(1, response.Content.Length - 2);
                    RetroFront.Models.IGDB.Company comp = JsonConvert.DeserializeObject<RetroFront.Models.IGDB.Company>(rawdata);
                    searchResults.Add(comp);
                }
                return searchResults;
            }
            else
                return null;
        }
        public IEnumerable<RetroFront.Models.IGDB.Company> GetPublishersByGameId(int id)
        {
            var involvedComps = this.GetInvolvedCompanyByGameId(id);
            var devlist = involvedComps.Where(x => x.publisher == true);
            IList<RetroFront.Models.IGDB.Company> searchResults = new List<RetroFront.Models.IGDB.Company>();
            foreach (var dev in devlist)
            {
                try
                {
                    string urlrequest = "https://api.igdb.com/v4/companies/" + dev.company.ToString() + "?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;";
                    //var client = new RestClient("https://api.igdb.com/v4/companies/1514?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;");
                    var client = new RestClient(urlrequest);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                    request.AddHeader("Authorization", $"Bearer {Bearer}");
                    request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");
                    var response = client.Execute<RetroFront.Models.IGDB.Company>(request);
                    var rawdata = response.Content.Substring(1, response.Content.Length - 2);
                    RetroFront.Models.IGDB.Company comp = JsonConvert.DeserializeObject<RetroFront.Models.IGDB.Company>(rawdata);
                    searchResults.Add(comp);
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
            return searchResults;
        }
        public string GetCoverLink(string hash)
        {
            return $"https://images.igdb.com/igdb/image/upload/t_cover_big/" + hash + ".jpg";
        }
        public string GetArtWorkLink(string hash)
        {
            return $"https://images.igdb.com/igdb/image/upload/t_1080p/" + hash + ".jpg";
        }

        #endregion

        #region ScreenScraper
        public async Task<List<RetroFront.Models.ScreenScraper.System.Systeme>> GetSSCPSystemes()
        {
            var request = InitREquestDefaultParam("systemesListe.php");
            //request.Resource = ;
            var SystemResult = await sscpclient.ExecuteAsync<SCSPSystemListRequest>(request);
            return SystemResult.Data.response.systemes;
            //return GetSSCPSystemes;
        }
        private void SetDevId()
        {
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".retrofront", "SSCP.gral");
            string[] lines = System.IO.File.ReadAllLines(filepath);
            byte[] raw = new byte[lines[0].Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(lines[0].Substring(i * 2, 2), 16);
            }
            var devid64 = Encoding.ASCII.GetString(raw);
            byte[] data = Convert.FromBase64String(devid64);
            devid = Encoding.UTF8.GetString(data);

            byte[] raw2 = new byte[lines[1].Length / 2];
            for (int i = 0; i < raw2.Length; i++)
            {
                raw2[i] = Convert.ToByte(lines[1].Substring(i * 2, 2), 16);
            }
            var devpwd64 = Encoding.ASCII.GetString(raw2);
            byte[] data2 = Convert.FromBase64String(devpwd64);
            devpwd = Encoding.UTF8.GetString(data2);
        }
        private RestRequest InitREquestDefaultParam(string method)
        {
            SetDevId();
            var request = new RestRequest(method, Method.GET);
            request.AddParameter("devid", devid);
            request.AddParameter("devpassword", devpwd);
            request.AddParameter("softname", "RetroFront");
            request.AddParameter("output", "json");
            request.AddParameter("ssid", FileJSONService.appSettings.ScreenScraperID);
            request.AddParameter("sspassword", FileJSONService.appSettings.ScreenScraperPWD);
            return request;
        }
        public IEnumerable<GameRom> SearchGame(string name)
        {
            //var client = new RestClient("https://www.screenscraper.fr/api2/jeuRecherche.php?devid=kabellrics&devpassword=ujBRtBI1njI&softname=RetroFront&output=json&ssid=kabellrics&sspassword=m9a7d2&recherche=gran turismo");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);

            var request = InitREquestDefaultParam("jeuRecherche.php");
            //request.Resource = ;
            request.AddParameter("recherche", name);
            //request.AddHeader("Cookie", "PHPSESSID=b03ec97508aac291eaea253d453b6af9; SERVERID=clone");
            var response = sscpclient.Execute<SCSPGameRequest>(request);
            var result = response.Data.response.jeux;
            foreach (var game in result)
            {
                if (game.noms != null)
                {
                    string gamename = game.noms.FirstOrDefault(x => x.region == "eu" || x.region == "fr" || x.region == "wor" || x.region == "us")?.text;
                    string gameSCSPID = game.id;
                    yield return new GameRom() { ScreenScraperID = int.Parse(gameSCSPID), Name = gamename };
                }
            }
        }
        public Jeux GetJeuxDetail(int id)
        {
            //var client = new RestClient("https://www.screenscraper.fr/api2/jeuInfos.php?devid=kabellrics&devpassword=ujBRtBI1njI&softname=RetroFront&output=json&ssid=kabellrics&sspassword=m9a7d2&gameid=19295");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Cookie", "SERVERID=main; SESSID=22d189c444d3e75931c8d11b1876b3a3");

            try
            {
                var request = InitREquestDefaultParam("jeuInfos.php");
                //request.Resource = ;
                request.AddParameter("gameid", id.ToString());

                var response = sscpclient.Execute<SCSPGameSpecificRequest>(request);
                return response.Data.response.jeu;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }
        public string GetSystemeImgDLL(string type, string SCSPSysID)
        {
            try
            {
                //var client = new RestClient("https://www.screenscraper.fr/api2/mediaSysteme.php?devid=kabellrics&devpassword=ujBRtBI1njI&softname=RetroFront&ssid=kabellrics&sspassword=m9a7d2&crc=&md5=&sha1=&systemeid=1&media=wheel(wor)");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("Cookie", "SERVERID=main; SESSID=22d189c444d3e75931c8d11b1876b3a3");
                var request = InitREquestDefaultParam("mediaSysteme.php");
                request.AddParameter("systemeid", SCSPSysID);
                request.AddParameter("media", $"{type}(wor)");
                return sscpclient.BuildUri(request).ToString();
                //var response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetSystemeVideoDLL(string SCSPSysID)
        {
            try
            {
                var request = InitREquestDefaultParam("mediaVideoSysteme.php");
                request.AddParameter("systemeid", SCSPSysID);
                request.AddParameter("media", "video");
                return sscpclient.BuildUri(request).ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public void DownloadSteamData(string dllpath, string target)
        {
            using (var file = File.Create(target))
            {
                using (WebClient client = new WebClient())
                {
                    file.Write(client.DownloadData(dllpath));
                    //client.DownloadFile(dllpath, file);
                }
            }
        }

        #endregion

        #region SteamGridDB
        public IEnumerable<Search> SearchByName(string name)
        {
            try
            {
                var request = new RestRequest($"/search/autocomplete/{name}", Method.GET, DataFormat.Json);
                var response = sgdbclient.Get(request);
                var content = response.Content;
                var result = JsonConvert.DeserializeObject<SearchByNameResult>(content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public Search GetGameSteamId(string steamId)
        {
            try
            {
                var request = new RestRequest($"games/steam/{steamId}", Method.GET);
                var response = sgdbclient.Get(request);
                var content = response.Content;
                JObject json = JObject.Parse(content);
                var datajson = json["data"];
                var result = JsonConvert.DeserializeObject<DataSearch>(datajson.ToString());
                return result;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public IEnumerable<ImgResult> GetHeroesForId(int gameId)
        {
            try
            {
                var request = new RestRequest($"heroes/game/{gameId}", DataFormat.Json);
                var response = sgdbclient.Get(request);
                var content = response.Content;
                var result = JsonConvert.DeserializeObject<SearchHeroesByIdResult>(content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public IEnumerable<ImgResult> GetLogoForId(int gameId)
        {
            try
            {
                var request = new RestRequest($"logos/game/{gameId}", DataFormat.Json);
                var response = sgdbclient.Get(request);
                var content = response.Content;
                var result = JsonConvert.DeserializeObject<SearchLogoByIdResult>(content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public IEnumerable<ImgResult> GetGridBoxartForId(int gameId)
        {
            try
            {
                var request = new RestRequest($"grids/game/{gameId}?dimensions=600x900,342x482,660x930", DataFormat.Json);
                var response = sgdbclient.Get(request);
                var content = response.Content;
                var result = JsonConvert.DeserializeObject<SearchGridByIdResult>(content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public IEnumerable<ImgResult> GetGridBannerForId(int gameId)
        {
            try
            {
                var request = new RestRequest($"grids/game/{gameId}?dimensions=460x215,920x430", DataFormat.Json);
                var response = sgdbclient.Get(request);
                var content = response.Content;
                var result = JsonConvert.DeserializeObject<SearchGridByIdResult>(content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        #endregion
    }
}
