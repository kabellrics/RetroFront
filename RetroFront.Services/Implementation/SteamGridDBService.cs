using RestSharp;
using RestSharp.Authenticators;
using RetroFront.Models.SteamGridDB;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetroFront.Services.Implementation
{
    public class SteamGridDBService : ISteamGridDBService
    {
        private string apipath = @"https://www.steamgriddb.com/api/v2";
        private FileJSONService  jSONService = new FileJSONService();
        private RestClient client;
        public SteamGridDBService()
        {
            client = new RestClient(apipath);
            client.Authenticator = new JwtAuthenticator(jSONService.appSettings.SGDBKey);
        }

        public IEnumerable<DataSearch> SearchByName(string name)
        {
            try
            {
                var request = new RestRequest($"/search/autocomplete/{name}", Method.GET, DataFormat.Json);
                var response = client.Get(request);
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
        public DataSearch GetGameSteamId(string steamId)
        {
            try
            {
                var request = new RestRequest($"games/steam/{steamId}",Method.GET);
                var response = client.Get(request);
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
                var response = client.Get(request);
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
                var response = client.Get(request);
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
                var response = client.Get(request);
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
                var response = client.Get(request);
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
    }
}
