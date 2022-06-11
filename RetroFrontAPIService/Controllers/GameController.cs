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
    public class GameController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public GameController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<GameRom>> Get()
        {
            return Ok(_databaseService.GetGames());
        }
        [HttpGet("{id}")]
        public ActionResult<GameRom> Get(int id)
        {
            return Ok(_databaseService.GetGame(id));
        }
        [HttpGet("GetGameByName/{emuname}")]
        public ActionResult<GameRom> GetGameByName(string gamepath)
        {
            return Ok(_databaseService.GetGameByName(gamepath));
        }
        [HttpPost]
        public ActionResult<GameRom> Post(GameRom game)
        {
            var gameitem = _databaseService.AddGame(game);
            return Ok(gameitem);
        }
        [HttpDelete("{id}")]
        public ActionResult<GameRom> Delete(int id)
        {
            var todoItem = _databaseService.GetGame(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _databaseService.RemoveGame(todoItem);

            return todoItem;
        }
    }
}
