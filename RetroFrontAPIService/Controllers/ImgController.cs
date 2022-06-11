using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IThemeService _themeService;
        private readonly IFileJSONService _fileJSONService;
        public ImgController(IDatabaseService databaseService, IThemeService themeService, IFileJSONService fileJSONService)
        {
            _databaseService = databaseService;
            _fileJSONService = fileJSONService;
            _themeService = themeService;
        }
        [HttpGet("GetLogoForSystem/{idsys}")]
        public ActionResult GetLogoForSystem(int idsys)
        {
            var sys = _databaseService.GetSysteme(idsys);
            if(sys !=null)
            {
                if (System.IO.File.Exists(sys.Logo))
                {
                    return GetFile(sys.Logo);
                }
            }
            return NotFound();
        }
        [HttpGet("GetScreenshootForSystem/{idsys}")]
        public ActionResult GetScreenshootForSystem(int idsys)
        {
            var sys = _databaseService.GetSysteme(idsys);
            if(sys !=null)
            {
                //if (System.IO.File.Exists(sys.Screenshoot))
                //{
                //    return GetFile(sys.Screenshoot);
                //}
                //else
                //{
                    var currenttheme = _fileJSONService.GetCurrentTheme().Path;
                   var bckforsystem = _themeService.GetBckForTheme(sys.Shortname, currenttheme).Path;
                    return GetFile(bckforsystem);
                //}
            }
            return NotFound();
        }
        [HttpGet("GetLogoForGame/{idgame}")]
        public ActionResult GetLogoForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.Logo))
                {
                    return GetFile(game.Logo);
                }
            }
            return NotFound();
        }
        [HttpGet("GetBoxartForGame/{idgame}")]
        public ActionResult GetBoxartForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.Boxart))
                {
                    return GetFile(game.Boxart);
                }
            }
            return NotFound();
        }
        [HttpGet("GetFanartForGame/{idgame}")]
        public ActionResult GetFanartForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.Fanart))
                {
                    return GetFile(game.Fanart);
                }
            }
            return NotFound();
        }
        [HttpGet("GetRecalViewForGame/{idgame}")]
        public ActionResult GetRecalViewForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.RecalView))
                {
                    return GetFile(game.RecalView);
                }
            }
            return NotFound();
        }
        [HttpGet("GetScreenshootForGame/{idgame}")]
        public ActionResult GetScreenshootForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.Screenshoot))
                {
                    return GetFile(game.Screenshoot);
                }
            }
            return NotFound();
        }
        
        [HttpGet("GetTitleScreenForGame/{idgame}")]
        public ActionResult GetTitleScreenForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.TitleScreen))
                {
                    return GetFile(game.TitleScreen);
                }
            }
            return NotFound();
        }
        [HttpGet("GetVideoForGame/{idgame}")]
        public ActionResult GetVideoForGame(int idgame)
        {
            var game = _databaseService.GetGame(idgame);
            if (game != null)
            {
                if (System.IO.File.Exists(game.Video))
                {
                    return GetFile(game.Video);
                }
            }
            return NotFound();
        }
        [HttpGet("GetImg/{imgPath}")]
        public ActionResult GetImg(string imgPath)
        {
            if(System.IO.File.Exists(imgPath))
            {
                return GetFile(imgPath);
            }
            else
                return NotFound();
        }
        private ActionResult GetFile(string path)
        {
            var name = System.IO.Path.GetFileName(path);
            var imgbyte = System.IO.File.ReadAllBytes(path);
            string imgtype = string.Empty;
            if (System.IO.Path.GetExtension(name) == ".png")
            {
                imgtype = "image/png";
            }
            else if (System.IO.Path.GetExtension(name) == ".jpg")
            {
                imgtype = "image/jpg";
            }
            else if (System.IO.Path.GetExtension(name) == ".jpeg")
            {
                imgtype = "image/jpeg";
            }
            else if (System.IO.Path.GetExtension(name) == ".avi")
            {
                imgtype = "video/avi";
            }
            else if (System.IO.Path.GetExtension(name) == ".mp4")
            {
                imgtype = "video/mp4";
            }
            return File(imgbyte, imgtype);
        }
    }
}
