using GameByURLLauncher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameByURLLauncher
{
    public class APIService
    {
        static HttpClient client = new HttpClient();
        public static async Task<Emulator> GetEmulatorByID(string id)
        {
            Emulator emu = null;
            HttpResponseMessage response = await client.GetAsync($"http://localhost:34322/api/Emulator/{id}");
            if (response.IsSuccessStatusCode)
            {
                emu = await response.Content.ReadAsAsync<Emulator>();
            }
            return emu;
        }
        public static async Task<GameRom> GetGameRomByID(string id)
        {
            GameRom game = null;
            HttpResponseMessage response = await client.GetAsync($"http://localhost:34322/api/Game/{id}");
            if (response.IsSuccessStatusCode)
            {
                game = await response.Content.ReadAsAsync<GameRom>();
            }
            return game;
        }
        public static async Task<Systeme> GetSystemeByID(string id)
        {
            Systeme sys = null;
            HttpResponseMessage response = await client.GetAsync($"http://localhost:34322/api/Systeme/{id}");
            if (response.IsSuccessStatusCode)
            {
                sys = await response.Content.ReadAsAsync<Systeme>();
            }
            return sys;
        }

    }
}
