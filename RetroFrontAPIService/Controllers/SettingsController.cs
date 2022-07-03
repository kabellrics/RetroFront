using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroFront.Models;
using RetroFront.Models.StandaloneEmulator;
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
    public class SettingsController : ControllerBase
    {
        private readonly IFileJSONService _fileJSONService;
        private readonly IEnumService _EnumService;
        public SettingsController(IFileJSONService fileJSONService, IEnumService EnumService)
        {
            _fileJSONService = fileJSONService;
            _EnumService = EnumService;
        }
        [HttpGet("SysDisplayValue")]
        public ActionResult<List<KeyValuePair<int, string>>> GetSysDisplayValue()
        {
            return Ok(_EnumService.GetSysDisplayValue());
        }
        [HttpGet("HomeDisplayValue")]
        public ActionResult<List<KeyValuePair<int, string>>> GetHomeDisplayValue()
        {
            return Ok(_EnumService.GetHomeDisplayValue());
        }
        [HttpGet("RomDisplayValue")]
        public ActionResult<List<KeyValuePair<int, string>>> GetRomDisplayValue()
        {
            return Ok(_EnumService.GetRomDisplayValue());
        }
        [HttpGet("GameDetailDisplayValue")]
        public ActionResult<List<KeyValuePair<int, string>>> GetGameDetailDisplayValue()
        {
            return Ok(_EnumService.GetGameDetailDisplayValue());
        }
        [HttpGet("SysDisplay")]
        public ActionResult<SysDisplay> GetSysDisplays()
        {
            return Ok(_EnumService.GetSysDisplays());
        }
        [HttpGet("HomeDisplay")]
        public ActionResult<HomeDisplay> GetHomeDisplays()
        {
            return Ok(_EnumService.GetHomeDisplays());
        }
        [HttpGet("RomDisplay")]
        public ActionResult<RomDisplay> GetRomDisplays()
        {
            return Ok(_EnumService.GetRomDisplays());
        }
        [HttpGet("SysType")]
        public ActionResult<SysType> GetSysTypes()
        {
            return Ok(_EnumService.GetSysTypes());
        }
        [HttpGet("Settings")]
        public ActionResult<AppSettings> GetSettings()
        {
            return Ok(_fileJSONService.appSettings);
        }
        [HttpGet("GetCurrentTheme")]
        public ActionResult<ThemePath> GetCurrentTheme()
        {
            return Ok(_fileJSONService.GetCurrentTheme());
        }
        [HttpGet("GetSysByCode")]
        public ActionResult<Systeme> GetSysByCode(string shortname)
        {
            return Ok(_fileJSONService.GetSysByCode(shortname));
        }
        [HttpGet("GetStandaloneEmulators")]
        public ActionResult<IEnumerable<StandaloneEmulator>> GetStandaloneEmulators()
        {
            return Ok(_fileJSONService.GetStandaloneEmulators());
        }
        [HttpPost("Settings")]
        public ActionResult<AppSettings> PostSettings(AppSettings apps)
        {
            _fileJSONService.UpdateSettings(apps);
            return Ok(_fileJSONService.appSettings);
        }
        [HttpPost("Theme")]
        public ActionResult PostTheme(Theme th)
        {
            _fileJSONService.ChangeCurrentTheme(th);
            return Ok();
        }
    }
}
