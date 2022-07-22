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
    public class EmulatorController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public EmulatorController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Emulator>> Get()
        {
            return Ok(_databaseService.GetEmulators());
        }
        [HttpGet("{id}")]
        public ActionResult<Emulator> Get(int id)
        {
            return Ok(_databaseService.GetEmulator(id));
        }
        [HttpGet("GetEmulatorByName/{emuname}")]
        public ActionResult<Emulator> GetEmulatorByName(string emuname)
        {
            return Ok(_databaseService.GetEmulatorByName(emuname));
        }
        [HttpGet("GetGamesForemulator/{emuID}")]
        public ActionResult<IEnumerable<GameRom>> GetGamesForemulator(int emuID)
        {
            return Ok(_databaseService.GetGamesForemulator(emuID));
        }
        [HttpGet("GetNbGamesForemulator/{emuID}")]
        public ActionResult<int> GetNbGamesForemulator(int emuID)
        {
            return Ok(_databaseService.GetNbGamesForemulator(emuID));
        }
        [HttpPost]
        public ActionResult<Emulator> Post(Emulator emulator)
        {
            var emuitem = _databaseService.AddEmulator(emulator);
            return Ok(emuitem);
        }
        [HttpDelete("{id}")]
        public ActionResult<Emulator> Delete(int id)
        {
            var todoItem = _databaseService.GetEmulator(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _databaseService.RemoveEmulator(todoItem);

            return todoItem;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Emulator Item)
        {
            if (id != Item.EmulatorID)
            {
                return BadRequest();
            }
            try
            {
                _databaseService.UpdateEmulator(id, Item);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
