using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroFront.Models;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperModelController : ControllerBase
    {
        private readonly IHelperModelService _helperModelService;
        public HelperModelController(IHelperModelService helpermodelService)
        {
            _helperModelService = helpermodelService;
        }
        [HttpPost("CreateEmulateur")]
        public ActionResult<Emulator> CreateEmulateur(Systeme platform, string Name, string Command, string Extension)
        {
            var item = _helperModelService.CreateEmulateur(platform, Name, Command, Extension);
            return Ok(item);
        }
        [HttpPost("DuplicateEmulator")]
        public ActionResult<Emulator> DuplicateEmulator(Emulator emulator)
        {
            var item = _helperModelService.DuplicateEmulator(emulator);
            return Ok(item);
        }
        [HttpPost("AddExplorer")]
        public ActionResult<Emulator> AddExplorer(Systeme systeme)
        {
            var item = _helperModelService.AddExplorer(systeme);
            return Ok(item);
        }
        [HttpPost("FormatExtension")]
        public ActionResult<string> FormatExtension(Emulator ext)
        {
            var item = _helperModelService.FormatExtension(ext);
            return Ok(item);
        }
        [HttpPost("CreateGame")]
        public ActionResult<GameRom> CreateGame(Emulator emulator, string gamefile)
        {
            var item = _helperModelService.CreateGame(gamefile, emulator);
            return Ok(item);
        }
        [HttpPost("DuplicateGame")]
        public ActionResult<GameRom> DuplicateGame(GameRom game)
        {
            var item = _helperModelService.DuplicateGame(game);
            return Ok(item);
        }
        [HttpPost("LookForData")]
        public ActionResult<GameRom> LookForData(GameRom game)
        {
            var item = _helperModelService.LookForData(game);
            return Ok(item);
        }
        //[HttpPost("ScrapeGamefromGamelist")]
        //public ActionResult<GameRom> ScrapeGamefromGamelist(GameRom game, string filefolder, Game gamedata)
        //{
        //    var item = _helperModelService.ScrapeGamefromGamelist(game, filefolder, gamedata);
        //    return Ok(item);
        //}
        [HttpPost("ImportGame")]
        public ActionResult<IEnumerable<GameRom>> ImportGame(string gamelistpath, Emulator emulator)
        {
            var item = _helperModelService.ImportGame(gamelistpath, emulator);
            return Ok(item);
        }
        [HttpPost("GetImgPathForGame")]
        public ActionResult<string> GetImgPathForGame(GameRom game, int sGDBType)
        {
            var item = _helperModelService.GetImgPathForGame(game, sGDBType);
            return Ok(item);
        }
    }
}
