using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Models
{
    public class DisplaySettings : DisplayBase
    {
        public DisplaySettings(AppSettings setting) : base()
        {
            Settings = setting;
        }
        public AppSettings Settings { get; }
        public string CurrentTheme
        {
            get => Settings.CurrentTheme;
            set
            {
                SetProperty(Settings.CurrentTheme, value, Settings, (emulator, item) => Settings.CurrentTheme = item);
                ChangeStatus();
            }
        }
        public string ScreenScraperID
        {
            get => Settings.ScreenScraperID;
            set
            {
                SetProperty(Settings.ScreenScraperID, value, Settings, (emulator, item) => Settings.ScreenScraperID = item);
                ChangeStatus();
            }
        }
        public string ScreenScraperPWD
        {
            get => Settings.ScreenScraperPWD;
            set
            {
                SetProperty(Settings.ScreenScraperPWD, value, Settings, (emulator, item) => Settings.ScreenScraperPWD = item);
                ChangeStatus();
            }
        }
        public string SteamGridDB_Key
        {
            get => Settings.SgdbKey;
            set
            {
                SetProperty(Settings.SgdbKey, value, Settings, (emulator, item) => Settings.SgdbKey = item);
                ChangeStatus();
            }
        }
        public string AppSettingsLocation
        {
            get => Settings.AppSettingsLocation;
            set
            {
                SetProperty(Settings.AppSettingsLocation, value, Settings, (emulator, item) => Settings.AppSettingsLocation = item);
                ChangeStatus();
            }
        }
        public string AppSettingsFolder
        {
            get => Settings.AppSettingsFolder;
            set
            {
                SetProperty(Settings.AppSettingsFolder, value, Settings, (emulator, item) => Settings.AppSettingsFolder = item);
                ChangeStatus();
            }
        }
        public string DefaultBCK
        {
            get => Settings.DefaultBCK;
            set
            {
                SetProperty(Settings.DefaultBCK, value, Settings, (emulator, item) => Settings.DefaultBCK = item);
                ChangeStatus();
            }
        }
        public string RetroarchPath
        {
            get => Settings.RetroarchPath;
            set
            {
                SetProperty(Settings.RetroarchPath, value, Settings, (emulator, item) => Settings.RetroarchPath = item);
                ChangeStatus();
            }
        }
        public string PegasusPCGroupName
        {
            get => Settings.PegasusPCGroupName;
            set
            {
                SetProperty(Settings.PegasusPCGroupName, value, Settings, (emulator, item) => Settings.PegasusPCGroupName = item);
                ChangeStatus();
            }
        }
        public string PegasusIconFolderPath
        {
            get => Settings.PegasusIconFolderPath;
            set
            {
                SetProperty(Settings.PegasusIconFolderPath, value, Settings, (emulator, item) => Settings.PegasusIconFolderPath = item);
                ChangeStatus();
            }
        }
        public string URLGameLauncherPath
        {
            get => Settings.URLGameLauncherPath;
            set
            {
                SetProperty(Settings.URLGameLauncherPath, value, Settings, (emulator, item) => Settings.URLGameLauncherPath = item);
                ChangeStatus();
            }
        }
        public bool RegroupPCGamesForPegasus
        {
            get => Settings.RegroupPCGamesForPegasus;
            set
            {
                SetProperty(Settings.RegroupPCGamesForPegasus, value, Settings, (emulator, item) => Settings.RegroupPCGamesForPegasus = item);
                ChangeStatus();
            }
        }
    }
}
