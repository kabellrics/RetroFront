using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ComputerController : ControllerBase
    {
        private readonly IComputerService _computerservice;
        public ComputerController(IComputerService computerservice)
        {
            _computerservice = computerservice;
        }
        //[FromQueryAttribute]
        [HttpPost("FileCopy/{data}")]
        public ActionResult FileCopy(CopyAndDLLObject data)
        {
            try
            {
                _computerservice.FileCopy(data.Source, data.Dest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("FileDLL/{data}")]
        public ActionResult FileDLL(CopyAndDLLObject data)
        {
            try
            {
                _computerservice.FileDownload(data.Source, data.Dest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ByteArrayDLL/{data}")]
        public ActionResult ByteArrayDLL(DLLByteObject data)
        {
            try
            {
                _computerservice.WriteByte(data.Source, data.Dest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
