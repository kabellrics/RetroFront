using RestSharp;
using RestSharp.Authenticators;
using RetroFront.Models.SteamGridDB;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RetroFront.Services.Implementation
{
    public class SteamGridDBService : ISteamGridDBService
    {
        private string key = "1ff404b93100e8b9faabad0901044380";
        private string apipath = @"https://www.steamgriddb.com/api/v2";
        private RestClient client;
        public SteamGridDBService()
        {
            client = new RestClient(apipath);
            client.Authenticator = new JwtAuthenticator(key);
        }

        public IEnumerable<DataSearch> SearchByName(string name)
        {
            try
            {
                var request = new RestRequest($"/search/autocomplete/{name}", DataFormat.Json);
                var response = client.Get(request);
                var result = JsonConvert.DeserializeObject<SearchByNameResult>(response.Content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public IEnumerable<DataSearch> GetGameSteamId(int steamId)
        {
            try
            {
                var request = new RestRequest($"games/steam/{steamId}", DataFormat.Json);
                var response = client.Get(request);
                var result = JsonConvert.DeserializeObject<SearchByNameResult>(response.Content);
                return result.data;
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
                var result = JsonConvert.DeserializeObject<SearchHeroesByIdResult>(response.Content);
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
                var result = JsonConvert.DeserializeObject<SearchLogoByIdResult>(response.Content);
                return result.data;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
        public IEnumerable<ImgResult> GetGridsForId(int gameId)
        {
            try
            {
                var request = new RestRequest($"grids/game/{gameId}", DataFormat.Json);
                var response = client.Get(request);
                var result = JsonConvert.DeserializeObject<SearchGridByIdResult>(response.Content);
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
