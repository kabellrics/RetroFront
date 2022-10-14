using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroFront.Models;
using RetroFront.Models.ScreenScraper.GameSearch;
using RetroFront.Models.SteamGridDB;
using RetroFrontAPIService.Service;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDataProviderController : ControllerBase
    {
        private readonly IGameDataProviderService _gameDataProviderService;
        public GameDataProviderController(IGameDataProviderService gameDataProviderService)
        {
            _gameDataProviderService = gameDataProviderService;
        }

        #region NoIntro
        [HttpGet("NoIntroProposal/{romname}/{plateforme}")]
        public ActionResult<IEnumerable<NoIntroSearchResult>> GetNoIntroName(string romname, string plateforme)
        {
            return Ok(_gameDataProviderService.GetNoIntroName(romname, plateforme));
        }
        #endregion

        #region SteamGridDB
        [HttpGet("SteamGridDB/SearchByName/{name}")]
        public ActionResult<IEnumerable<Search>> SearchByName(string name)
        {
            return Ok(_gameDataProviderService.SearchByName(name));
        }
        [HttpGet("SteamGridDB/GetGameSteamId/{steamId}")]
        public ActionResult<IEnumerable<Search>> GetGameSteamId(string steamId)
        {
            return Ok(_gameDataProviderService.GetGameSteamId(steamId));
        }
        [HttpGet("SteamGridDB/GetHeroesForId/{gameId}")]
        public ActionResult<IEnumerable<ImgResult>> GetHeroesForId(int gameId)
        {
            return Ok(_gameDataProviderService.GetHeroesForId(gameId));
        }
        [HttpGet("SteamGridDB/GetLogoForId/{gameId}")]
        public ActionResult<IEnumerable<ImgResult>> GetLogoForId(int gameId)
        {
            return Ok(_gameDataProviderService.GetLogoForId(gameId));
        }
        [HttpGet("SteamGridDB/GetGridBoxartForId/{gameId}")]
        public ActionResult<IEnumerable<ImgResult>> GetGridBoxartForId(int gameId)
        {
            return Ok(_gameDataProviderService.GetGridBoxartForId(gameId));
        }
        [HttpGet("SteamGridDB/GetGridBannerForId/{gameId}")]
        public ActionResult<IEnumerable<ImgResult>> GetGridBannerForId(int gameId)
        {
            return Ok(_gameDataProviderService.GetGridBannerForId(gameId));
        }
        #endregion

        #region ScreenScraper
        //[HttpGet]
        //public ActionResult<IEnumerable<RetroFront.Models.ScreenScraper.System.Systeme>> GetSSCPSystemes()
        //{
        //    return Ok( _gameDataProviderService.GetSSCPSystemes().Result);
        //}
        [HttpGet("ScreenScraper/SearchGame/{name}")]
        public ActionResult<IEnumerable<GameRom>> SearchGame(string name)
        {
            return Ok(_gameDataProviderService.SearchGame(name));
        }
        [HttpGet("ScreenScraper/GetJeuxDetail/{id}")]
        public ActionResult<SCGamedetail> GetJeuxDetail(int id)
        {
            return Ok(new SCGamedetail(_gameDataProviderService.GetJeuxDetail(id)));
        }
        [HttpGet("ScreenScraper/GetJeuxMedias/{id}")]
        public ActionResult<List<Media>> GetJeuxMedia(int id)
        {
            return Ok(_gameDataProviderService.GetJeuxDetail(id).medias);
        }
        [HttpGet("ScreenScraper/GetSystemeImgDLL/{type}/{SCSPSysID}")]
        public ActionResult<string> GetSystemeImgDLL(string type, string SCSPSysID)
        {
            return Ok(_gameDataProviderService.GetSystemeImgDLL(type, SCSPSysID));
        }
        [HttpGet("ScreenScraper/GetSystemeVideoDLL/{SCSPSysID}")]
        public ActionResult<string> GetSystemeVideoDLL(string SCSPSysID)
        {
            return Ok(_gameDataProviderService.GetSystemeVideoDLL(SCSPSysID));
        }
        #endregion

        #region IGDB

        [HttpGet("IGDB/GetPlatformByName/{name}")]
        public ActionResult<IEnumerable<RetroFront.Models.Search>> GetPlatformByName(string name)
        {
            return Ok(_gameDataProviderService.GetPlatformByName(name));
        }
        [HttpGet("IGDB/GetGameByName/{name}")]
        public ActionResult<IEnumerable<RetroFront.Models.Search>> GetGameByName(string name)
        {
            return Ok(_gameDataProviderService.GetGameByName(name));
        }
        [HttpGet("IGDB/GetDetailsGame/{id}")]
        public ActionResult<RetroFront.Models.IGDB.IGDBGame> GetDetailsGame(int id)
        {
            return Ok(_gameDataProviderService.GetDetailsGame(id));
        }
        [HttpGet("IGDB/GetArtworksByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Artwork>> GetArtworksByGameId(int id)
        {
            List<RetroFront.Models.IGDB.Artwork> result = new List<RetroFront.Models.IGDB.Artwork>();
            var artl = _gameDataProviderService.GetArtworksByGameId(id);
            if (artl != null)
                result.AddRange(artl);
            var scrl = _gameDataProviderService.GetScreenshotsByGameId(id);
            if (scrl != null)
                result.AddRange(scrl);
            return Ok(result);
        }
        [HttpGet("IGDB/GetGenresByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Genre>> GetGenresByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetGenresByGameId(id));
        }
        [HttpGet("IGDB/GetScreenshotsByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Screenshot>> GetScreenshotsByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetScreenshotsByGameId(id));
        }
        [HttpGet("IGDB/GetVideosByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Video>> GetVideosByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetVideosByGameId(id));
        }
        [HttpGet("IGDB/GetThemesByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Video>> GetThemesByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetThemesByGameId(id));
        }
        [HttpGet("IGDB/GetInvolvedCompanyByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.InvolvedCompany>> GetInvolvedCompanyByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetInvolvedCompanyByGameId(id));
        }
        [HttpGet("IGDB/GetDevByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Company>> GetDevByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetDevByGameId(id));
        }
        [HttpGet("IGDB/GetPublishersByGameId/{id}")]
        public ActionResult<IEnumerable<RetroFront.Models.IGDB.Company>> GetPublishersByGameId(int id)
        {
            return Ok(_gameDataProviderService.GetPublishersByGameId(id));
        }
        [HttpGet("IGDB/GetCoverLink/{hash}")]
        public ActionResult<string> GetCoverLink(string hash)
        {
            return Ok(_gameDataProviderService.GetCoverLink(hash));
        }
        [HttpGet("IGDB/GetArtWorkLink/{hash}")]
        public ActionResult<string> GetArtWorkLink(string hash)
        {
            return Ok(_gameDataProviderService.GetArtWorkLink(hash));
        }
        #endregion
    }
}
