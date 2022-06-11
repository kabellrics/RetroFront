using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroFront.Models;
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
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;
        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }
        [HttpGet("GetInstalledTheme")]
        public ActionResult<IEnumerable<Theme>> GetInstalledTheme()
        {
            return Ok(_themeService.GetInstalledTheme());
        }
        [HttpGet("LoadThemesForDefaultCollection")]
        public ActionResult LoadThemesForDefaultCollection()
        {
            _themeService.LoadThemesForDefaultCollection();
            return Ok();
        }
        [HttpGet("LoadDefaultBckForSysteme/{systeme}")]
        public ActionResult LoadDefaultBckForSysteme(Systeme systeme)
        {
            _themeService.LoadDefaultBckForSysteme(systeme);
            return Ok();
        }
        [HttpGet("LoadLogoForSysteme/{systeme}/{themename}/{imgpath}")]
        public ActionResult LoadLogoForSysteme(Systeme systeme, string themename, string imgpath)
        {
            _themeService.LoadLogoForSysteme(systeme,themename,imgpath);
            return Ok();
        }
        [HttpGet("LoadBckForSysteme/{systeme}/{themename}/{imgpath}")]
        public ActionResult LoadBckForSysteme(Systeme systeme, string themename, string imgpath)
        {
            _themeService.LoadBckForSysteme(systeme,themename,imgpath);
            return Ok();
        }
        [HttpGet("GetLogoForTheme/{plateforme}/{theme}")]
        public ActionResult<ThemePath> GetLogoForTheme(string plateforme, string theme)
        {
            return Ok(_themeService.GetLogoForTheme(plateforme, theme));
        }
        [HttpGet("GetBckForTheme/{plateforme}/{theme}")]
        public ActionResult<ThemePath> GetBckForTheme(string plateforme, string theme)
        {
            return Ok(_themeService.GetBckForTheme(plateforme, theme));
        }
        [HttpGet("GetLogoForTheme/{plateforme}")]
        public ActionResult<ThemePath> GetLogoForTheme(string plateforme)
        {
            return Ok(_themeService.GetLogoForTheme(plateforme));
        }
        [HttpGet("GetLogoPathForTheme/{plateforme}")]
        public ActionResult<ThemePath> GetLogoPathForTheme(string plateforme)
        {
            return Ok(_themeService.GetLogoPathForTheme(plateforme));
        }
        [HttpGet("GetVidéoForTheme/{plateforme}")]
        public ActionResult<ThemePath> GetVidéoForTheme(string plateforme)
        {
            return Ok(_themeService.GetVidéoForTheme(plateforme));
        }
        [HttpGet("GetVidéoPathForTheme/{plateforme}")]
        public ActionResult<ThemePath> GetVidéoPathForTheme(string plateforme)
        {
            return Ok(_themeService.GetVidéoPathForTheme(plateforme));
        }
        [HttpGet("GetImagePathForTheme/{plateforme}")]
        public ActionResult<ThemePath> GetImagePathForTheme(string plateforme)
        {
            return Ok(_themeService.GetImagePathForTheme(plateforme));
        }
        [HttpGet("GetImageForTheme/{plateforme}")]
        public ActionResult<ThemePath> GetImageForTheme(string plateforme)
        {
            return Ok(_themeService.GetImageForTheme(plateforme));
        }
        [HttpGet("DownloadSteamData/{dllpath}/{target}")]
        public ActionResult DownloadSteamData(string dllpath, string target)
        {
            _themeService.DownloadSteamData(dllpath, target);
            return Ok();
        }
        [HttpGet("ChangeBCK/{bcppath}")]
        public ActionResult ChangeBCK(string bcppath)
        {
            _themeService.ChangeBCK(bcppath);
            return Ok();
        }
    }
}
