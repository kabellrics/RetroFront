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
    public class SystemeController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public SystemeController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Systeme>> Get()
        {
            return Ok(_databaseService.GetSystemes());
        }
        [HttpGet("{id}")]
        public ActionResult<Systeme> Get(int id)
        {
            return Ok(_databaseService.GetSysteme(id));
        }
        [HttpGet("GetEmulatorsForSysteme/{sysID}")]
        public ActionResult<Emulator> GetEmulatorsForSysteme(int sysID)
        {
            return Ok(_databaseService.GetEmulatorsForSysteme(sysID));
        }
        [HttpGet("GetNbEmulatorForSysteme/{sysID}")]
        public ActionResult<int> GetNbEmulatorForSysteme(int sysID)
        {
            return Ok(_databaseService.GetNbEmulatorForSysteme(sysID));
        }
        [HttpGet("GetNbGamesForPlateforme/{sysID}")]
        public ActionResult<int> GetNbGamesForPlateforme(int sysID)
        {
            return Ok(_databaseService.GetNbGamesForPlateforme(sysID));
        }
        [HttpGet("GetGamesForPlateforme/{sysID}")]
        public ActionResult<GameRom> GetGamesForPlateforme(int sysID)
        {
            return Ok(_databaseService.GetGamesForPlateforme(sysID));
        }
        [HttpGet("GetSystemeByName/{sysname}")]
        public ActionResult<Systeme> GetSystemeByName(string sysname)
        {
            return Ok(_databaseService.GetSystemeByName(sysname));
        }
        [HttpPost]
        public ActionResult<Systeme> Post(Systeme systeme)
        {
            var fileitem = _databaseService.AddSystem(systeme);
            return Ok(fileitem);
        }
        [HttpDelete("{id}")]
        public ActionResult<Systeme> Delete(int id)
        {
            var todoItem = _databaseService.GetSysteme(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _databaseService.RemoveSystem(todoItem);

            return todoItem;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Systeme Item)
        {
            if (id != Item.SystemeID)
            {
                return BadRequest();
            }
            try
            {
                _databaseService.UpdateSystem(id, Item);
            }
            catch (Exception ex)
            {
               return NotFound();
            }
            return NoContent();
        }
    }
}
