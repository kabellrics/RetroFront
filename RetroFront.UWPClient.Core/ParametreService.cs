using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPClient.Core
{
    public class ParametreService : BaseService
    {
        public ParametreService()
        {

        }
        public async Task<AppSettings> GetSettingsAsync()
        {
            return await SettingsServiceAPI.SettingsGetAsync();
        }
        public async Task SaveSettingsAsync(AppSettings appSettings)
        {
            await SettingsServiceAPI.SettingsPostAsync(appSettings);
        }
        public async Task<List<KeyValuePair<int, string>>> GetHomeDisplay()
        {
            var listvalue = await SettingsServiceAPI.HomeDisplayValueAsync();
            List<KeyValuePair<int, string>> reponse = new List<KeyValuePair<int, string>>();
            foreach(var keyValue in listvalue)
            {
                reponse.Add(new KeyValuePair<int, string>(keyValue.Key,keyValue.Value));
            }
            return reponse;
        }
        public async Task<List<KeyValuePair<int, string>>> GetPlateformeDisplay()
        {
            var listvalue = await SettingsServiceAPI.SysDisplayValueAsync();
            List<KeyValuePair<int, string>> reponse = new List<KeyValuePair<int, string>>();
            foreach(var keyValue in listvalue)
            {
                reponse.Add(new KeyValuePair<int, string>(keyValue.Key,keyValue.Value));
            }
            return reponse;
        }
        public async Task<List<KeyValuePair<int, string>>> GetGameDisplay()
        {
            var listvalue = await SettingsServiceAPI.RomDisplayValueAsync();
            List<KeyValuePair<int, string>> reponse = new List<KeyValuePair<int, string>>();
            foreach(var keyValue in listvalue)
            {
                reponse.Add(new KeyValuePair<int, string>(keyValue.Key,keyValue.Value));
            }
            return reponse;
        }
        public async Task<List<KeyValuePair<int, string>>> GetGameDetailDisplay()
        {
            var listvalue = await SettingsServiceAPI.GameDetailDisplayValueAsync();
            List<KeyValuePair<int, string>> reponse = new List<KeyValuePair<int, string>>();
            foreach(var keyValue in listvalue)
            {
                reponse.Add(new KeyValuePair<int, string>(keyValue.Key,keyValue.Value));
            }
            return reponse;
        }
    }
}
