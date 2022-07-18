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
    public class GameAppController : ControllerBase
    {
        private readonly IGameAppService _GameAppService;
        public GameAppController(IGameAppService gameappService)
        {
            _GameAppService = gameappService;
        }
        [HttpPost("GetEpicGame")]
        public ActionResult<List<GameRom>> GetEpicGame(Emulator emu)
        {
            var gameitem = _GameAppService.GetEpicGame(emu);
            return Ok(gameitem);
        }
        [HttpPost("GetOriginGame")]
        public ActionResult<List<GameRom>> GetOriginGame(Emulator emu)
        {
            var gameitem = _GameAppService.GetOriginGame(emu);
            return Ok(gameitem);
        }
        [HttpPost("GetSteamGame")]
        public ActionResult<List<GameRom>> GetSteamGame(Emulator emu)
        {
            var gameitem = _GameAppService.GetSteamGame(emu);
            return Ok(gameitem);
        }
        [HttpPost("GetSteamInfos")]
        public ActionResult<GameRom> GetSteamInfos(GameRom game)
        {
            var gameitem = _GameAppService.GetSteamInfos(game);
            return Ok(gameitem);
        }
    }
}
