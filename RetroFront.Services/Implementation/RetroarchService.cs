using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class RetroarchService : IRetroarchService
    {
        //private FileJSONService FileJSONService = new FileJSONService();
        //private const string RetroarchPath = @"C:\Users\yflec\AppData\Roaming\RetroArch";
        //private const string RetroarchCMD = "%RetroarchPath% -L %RetroArchCore% %ROM_RAW%";
        //private const string RetroarchFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\.emulationstation";
        //public IEnumerable<RetroarchCore> GetInstalledCore()
        //{
        //    var cores = System.IO.Directory.EnumerateFiles(System.IO.Path.Combine(FileJSONService.appSettings.RetroarchPath, "cores"));
        //    TextInfo textInfo = new CultureInfo("fr-FR", false).TextInfo;
        //    foreach (var core in cores)
        //    {
        //        RetroarchCore rcore = new RetroarchCore();
        //        rcore.path = core;
        //        var corename = System.IO.Path.GetFileNameWithoutExtension(core);
        //        var coreNolibName = corename.Substring(0, corename.IndexOf("_libretro"));
        //        rcore.Name = textInfo.ToTitleCase(coreNolibName.Replace("_", " "));
        //        yield return rcore;
        //    }
        //}
        //public string RetroarchEXEPath()
        //{ return $"{FileJSONService.appSettings.RetroarchPath}\retroarch.exe"; }

    }
}
