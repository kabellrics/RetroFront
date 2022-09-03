using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Models
{
    public class DisplaySysteme : DisplayBase
    {
        public DisplaySysteme(Systeme systeme) : base()
        {
            Systeme = systeme; InitImg();
        }
        public Systeme Systeme { get; }
        private void InitImg()
        {
            Logo = string.Empty;
            if (LogoPath.Contains("ms-appx"))
            {
                Logo = LogoPath;
            }
            else
            {
                Logo = string.Format(LogoWebPath, ID);
            }
            Screenshoot = string.Empty;
            if (ScreenshootPath.Contains("ms-appx"))
            {
                Screenshoot = ScreenshootPath;
            }
            else
            {
                Screenshoot = string.Format(ScreenshootWebPath, ID);
            }
        }
        public int ID
        {
            get => Systeme.SystemeID;
            set => SetProperty(Systeme.SystemeID, value, Systeme, (syteme, item) => Systeme.SystemeID = item);
        }
        public string Name
        {
            get => Systeme.Name;
            set { SetProperty(Systeme.Name, value, Systeme, (syteme, item) => Systeme.Name = item); 
                ChangeStatus();
            }
        }
        public string ShortName
        {
            get => Systeme.Shortname;
            set
            {
                SetProperty(Systeme.Shortname, value, Systeme, (syteme, item) => Systeme.Shortname = item);
                ChangeStatus();
            }
        }
        public int ScreenScraperID
        {
            get => Systeme.SystemeSCSPID;
            set
            {
                SetProperty(Systeme.SystemeSCSPID, value, Systeme, (syteme, item) => Systeme.SystemeSCSPID = item);
                ChangeStatus();
            }
        }
        public SysType Type
        {
            get => Systeme.Type;
            set
            {
                SetProperty(Systeme.Type, value, Systeme, (syteme, item) => Systeme.Type = item);
                ChangeStatus();
            }
        }
        public string LogoPath
        {
            get => Systeme.Logo;
            set
            {
                SetProperty(Systeme.Logo, value, Systeme, (syteme, item) => Systeme.Logo = item);
                ChangeStatus();
                Logo = string.Empty;
                if (LogoPath.Contains("ms-appx"))
                {
                    Logo = LogoPath;
                }
                else
                {
                    Logo = string.Format(LogoWebPath, ID);
                }
            }
        }
        public string ScreenshootPath
        {
            get => Systeme.Screenshoot;
            set
            {
                SetProperty(Systeme.Screenshoot, value, Systeme, (syteme, item) => Systeme.Screenshoot = item);
                ChangeStatus();
                Screenshoot = string.Empty;
                if (ScreenshootPath.Contains("ms-appx"))
                {
                    Screenshoot = ScreenshootPath;
                }
                else
                {
                    Screenshoot = string.Format(ScreenshootWebPath, ID);
                }
            }
        }
        private string _logo;
        public string Logo
        {
            get => _logo;
            set
            {
                SetProperty(ref _logo, value);
            }
        }
        private string _screenshoot;
        public string Screenshoot
        {
            get => _screenshoot;
            set
            {
                SetProperty(ref _screenshoot, value);
            }
        }
        private string LogoWebPath = "http://localhost:34322/api/Img/GetLogoForSystem/{0}";
        private string ScreenshootWebPath =  "http://localhost:34322/api/Img/GetScreenshootForSystem/{0}";
    }
}
