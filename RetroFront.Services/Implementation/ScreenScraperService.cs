using RestSharp;
using RetroFront.Models;
using RetroFront.Models.ScreenScraper;
using RetroFront.Models.ScreenScraper.GameSearch;
using RetroFront.Models.ScreenScraper.System;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.Services.Implementation
{
    public class ScreenScraperService : IScreenScraperService
    {
        RestClient sscpclient;
        private string devid;
        private string devpwd;
        private FileJSONService FileJSONService = new FileJSONService();
        public async Task<List<Models.ScreenScraper.System.Systeme>> GetSSCPSystemes()
        {
            var request = InitREquestDefaultParam("systemesListe.php");
            //request.Resource = ;
            var SystemResult = await sscpclient.ExecuteAsync<SCSPSystemListRequest>(request);
            return SystemResult.Data.response.systemes;
            //return GetSSCPSystemes;
        }

        private void SetDevId()
        {
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),".retrofront", "SSCP.gral");
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


        public ScreenScraperService()
        {
            sscpclient = new RestClient("https://www.screenscraper.fr/api2");
            sscpclient.Timeout = -1;
            //LoadingSysteme();
        }
        private RestRequest InitREquestDefaultParam(string method)
        {
            SetDevId();
            var request = new RestRequest(method,Method.GET);
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
            foreach(var game in result)
            {
                if (game.noms != null)
                {
                    string gamename = game.noms.FirstOrDefault(x => x.region == "eu" || x.region == "fr" || x.region == "wor" || x.region == "us")?.text;
                    string gameSCSPID = game.id;
                    yield return new GameRom() { ScreenScraperID = int.Parse(gameSCSPID), Name = gamename}; 
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


    }
}
