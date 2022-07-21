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
            Systeme = systeme;
        }
        public Systeme Systeme { get; }
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
            }
        }
        public string ScreenshootPath
        {
            get => Systeme.Screenshoot;
            set
            {
                SetProperty(Systeme.Screenshoot, value, Systeme, (syteme, item) => Systeme.Screenshoot = item);
                ChangeStatus();
            }
        }
        public string Logo
        {
            get => $"http://localhost:34322/api/Img/GetLogoForSystem/{ID}";
        }
        public string Screenshoot
        {
            get => $"http://localhost:34322/api/Img/GetScreenshootForSystem/{ID}";
        }

    }
}
