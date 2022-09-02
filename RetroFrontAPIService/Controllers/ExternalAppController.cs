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
    public class ExternalAppController : ControllerBase
    {
        private readonly IExternalAppService _externalAppService;
        public ExternalAppController(IExternalAppService externalAppService)
        {
            _externalAppService = externalAppService;
        }
        [HttpGet("GetInstalledCore/{retroarchpath}")]
        public ActionResult<IEnumerable<RetroarchCore>> GetInstalledCore(string retroarchpath)
        {
            return Ok(_externalAppService.GetInstalledCore(retroarchpath));
        }
        [HttpGet("ExportToPegasus/{sysID}")]
        public ActionResult<string> ExportToPegasus( int sysID)
        {
            return Ok(_externalAppService.ExportToPegasus( sysID));
        }
    }
}
