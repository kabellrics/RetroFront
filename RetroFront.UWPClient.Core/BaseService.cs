using RetroFront.UWPClient.Core.APIService;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPClient.Core
{
    public class BaseService
    {
        protected ThemeServiceAPI ThemeServiceAPI = new ThemeServiceAPI();
        protected SystemeServiceAPI SystemeServiceAPI = new SystemeServiceAPI();
        protected ImgServiceAPI ImgServiceAPI = new ImgServiceAPI();
        protected GameServiceAPI GameServiceAPI = new GameServiceAPI();
        protected ApiServiceAPI ApiServiceAPI = new ApiServiceAPI();
        protected EmulatorServiceAPI EmulatorServiceAPI = new EmulatorServiceAPI();
        protected SettingsServiceAPI SettingsServiceAPI = new SettingsServiceAPI();
    }
}
