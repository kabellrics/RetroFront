using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class Settings_Service : BaseService
    {
        public DisplaySettings GetSetting()
        {
            var setting = settingsClient.SettingsGet().Result;
            return new DisplaySettings(setting);
        }
    }
}
