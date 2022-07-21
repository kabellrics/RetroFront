using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Models
{
    public class DisplayGame : DisplayBase
    {
        public DisplayGame(GameRom game) : base()
        {
            Game = game;
            if (!string.IsNullOrEmpty(game.Year))
            {
                if (game.Year.Length != 4)
                {
                    DateTime date = new DateTime();
                    DateTime.TryParse(game.Year, out date);
                    Game.Year = date.Year.ToString();
                }
            }
            else
            {
                Game.Year = "Inconnu";
            }
            if (string.IsNullOrEmpty(game.Genre))
            {
                Game.Genre = "Inconnu";
            }
            if (string.IsNullOrEmpty(game.Editeur))
            {
                Game.Editeur = "Inconnu";
            }
            else if (game.Editeur.Contains(","))
            {
                var edit = game.Editeur.Substring(0, game.Editeur.IndexOf(","));
                game.Editeur = edit;
            }
            if (string.IsNullOrEmpty(game.Dev))
            {
                Game.Dev = "Inconnu";
            }
            else if (game.Dev.Contains(","))
            {
                var edit = game.Dev.Substring(0, game.Dev.IndexOf(","));
                game.Dev = edit;
            }
        }

        public GameRom Game { get; }
        public int ID
        {
            get => Game.Id;
            set => SetProperty(Game.Id, value, Game, (game, item) => Game.Id = item);
        }
        public int EmulatorID
        {
            get => Game.EmulatorID;
            set => SetProperty(Game.EmulatorID, value, Game, (game, item) => Game.EmulatorID = item);
        }
        public string Description
        {
            get => Game.Desc;
            set
            {
                SetProperty(Game.Desc, value, Game, (game, item) => Game.Desc = item);
                ChangeStatus();
            }
        }
        public string Developpeur
        {
            get => Game.Dev;
            set
            {
                SetProperty(Game.Dev, value, Game, (game, item) => Game.Dev = item);
                ChangeStatus();
            }
        }
        public string Editeur
        {
            get => Game.Editeur;
            set
            {
                SetProperty(Game.Editeur, value, Game, (game, item) => Game.Editeur = item);
                ChangeStatus();
            }
        }
        public string EpicGameID
        {
            get => Game.EpicID;
            set
            {
                SetProperty(Game.EpicID, value, Game, (game, item) => Game.EpicID = item);
                ChangeStatus();
            }
        }
        public string Genre
        {
            get => Game.Genre;
            set
            {
                SetProperty(Game.Genre, value, Game, (game, item) => Game.Genre = item);
                ChangeStatus();
            }
        }
        public int IGDBID
        {
            get => Game.Igdbid;
            set
            {
                SetProperty(Game.Igdbid, value, Game, (game, item) => Game.Igdbid = item);
                ChangeStatus();
            }
        }
        public bool IsDuplicate
        {
            get => Game.IsDuplicate;
            set
            {
                SetProperty(Game.IsDuplicate, value, Game, (game, item) => Game.IsDuplicate = item);
                ChangeStatus();
            }
        }
        public bool IsFavorite
        {
            get => Game.IsFavorite;
            set
            {
                SetProperty(Game.IsFavorite, value, Game, (game, item) => Game.IsFavorite = item);
                ChangeStatus();
            }
        }
        public DateTimeOffset? LastStart
        {
            get => Game.LastStart;
            set
            {
                SetProperty(Game.LastStart, value, Game, (game, item) => Game.LastStart = item);
                ChangeStatus();
            }
        }
        public string Name
        {
            get => Game.Name;
            set
            {
                SetProperty(Game.Name, value, Game, (game, item) => Game.Name = item);
                ChangeStatus();
            }
        }
        public int NbTimeStarted
        {
            get => Game.NbTimeStarted;
            set
            {
                SetProperty(Game.NbTimeStarted, value, Game, (game, item) => Game.NbTimeStarted = item);
                ChangeStatus();
            }
        }
        public string OriginID
        {
            get => Game.OriginID;
            set
            {
                SetProperty(Game.OriginID, value, Game, (game, item) => Game.OriginID = item);
                ChangeStatus();
            }
        }
        public string Path
        {
            get => Game.Path;
            set
            {
                SetProperty(Game.Path, value, Game, (game, item) => Game.Path = item);
                ChangeStatus();
            }
        }
        public string Plateforme
        {
            get => Game.Plateforme;
            set
            {
                SetProperty(Game.Plateforme, value, Game, (game, item) => Game.Plateforme = item);
                ChangeStatus();
            }
        }
        public int ScreenScraperID
        {
            get => Game.ScreenScraperID;
            set
            {
                SetProperty(Game.ScreenScraperID, value, Game, (game, item) => Game.ScreenScraperID = item);
                ChangeStatus();
            }
        }
        public int SteamGridDBID
        {
            get => Game.Sgdbid;
            set
            {
                SetProperty(Game.Sgdbid, value, Game, (game, item) => Game.Sgdbid = item);
                ChangeStatus();
            }
        }
        public int SteamID
        {
            get => Game.SteamID;
            set
            {
                SetProperty(Game.SteamID, value, Game, (game, item) => Game.SteamID = item);
                ChangeStatus();
            }
        }
        public string Year
        {
            get => Game.Year;
            set
            {
                SetProperty(Game.Year, value, Game, (game, item) => Game.Year = item);
                ChangeStatus();
            }
        }
        public string LogoPath
        {
            get => Game.Logo;
            set
            {
                SetProperty(Game.Logo, value, Game, (game, item) => Game.Logo = item);
                ChangeStatus();
            }
        }
        public string ScreenshootPath
        {
            get => Game.Screenshoot;
            set
            {
                SetProperty(Game.Screenshoot, value, Game, (game, item) => Game.Screenshoot = item);
                ChangeStatus();
            }
        }
        public string BoxartPath
        {
            get => Game.Boxart;
            set
            {
                SetProperty(Game.Boxart, value, Game, (game, item) => Game.Boxart = item);
                ChangeStatus();
            }
        }
        public string BannerPath
        {
            get => Game.Fanart;
            set
            {
                SetProperty(Game.Fanart, value, Game, (game, item) => Game.Fanart = item);
                ChangeStatus();
            }
        }
        public string VideoPath
        {
            get => Game.Video;
            set
            {
                SetProperty(Game.Video, value, Game, (game, item) => Game.Video = item);
                ChangeStatus();
            }
        }
        public string Logo
        {
            get => $"http://localhost:34322/api/Img/GetLogoForGame/{ID}";
        }
        public string Screenshoot
        {
            get => $"http://localhost:34322/api/Img/GetScreenshootForGame/{ID}";
        }
        public string Boxart
        {
            get => $"http://localhost:34322/api/Img/GetBoxartForGame/{ID}";
        }
        public string Banner
        {
            get => $"http://localhost:34322/api/Img/GetFanartForGame/{ID}";
        }
        public string Video
        {
            get => $"http://localhost:34322/api/Img/GetVideoForGame/{ID}";
        }
    }
}
