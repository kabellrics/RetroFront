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
        }

        public GameRom Game{ get ; }
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
            set => SetProperty(Game.Desc, value, Game, (game, item) => Game.Desc = item);
        }
        public string Développeur
        {
            get => Game.Dev;
            set => SetProperty(Game.Dev, value, Game, (game, item) => Game.Dev = item);
        }
        public string Editeur
        {
            get => Game.Editeur;
            set => SetProperty(Game.Editeur, value, Game, (game, item) => Game.Editeur = item);
        }
        public string EpicGameID
        {
            get => Game.EpicID;
            set => SetProperty(Game.EpicID, value, Game, (game, item) => Game.EpicID = item);
        }
        public string Genre
        {
            get => Game.Genre;
            set => SetProperty(Game.Genre, value, Game, (game, item) => Game.Genre = item);
        }
        public int IGDBID
        {
            get => Game.Igdbid;
            set => SetProperty(Game.Igdbid, value, Game, (game, item) => Game.Igdbid = item);
        }
        public bool IsDuplicate
        {
            get => Game.IsDuplicate;
            set => SetProperty(Game.IsDuplicate, value, Game, (game, item) => Game.IsDuplicate = item);
        }
        public bool IsFavorite
        {
            get => Game.IsFavorite;
            set => SetProperty(Game.IsFavorite, value, Game, (game, item) => Game.IsFavorite = item);
        }
        public DateTimeOffset? LastStart
        {
            get => Game.LastStart;
            set => SetProperty(Game.LastStart, value, Game, (game, item) => Game.LastStart = item);
        }
        public string Name
        {
            get => Game.Name;
            set => SetProperty(Game.Name, value, Game, (game, item) => Game.Name = item);
        }
        public int NbTimeStarted
        {
            get => Game.NbTimeStarted;
            set => SetProperty(Game.NbTimeStarted, value, Game, (game, item) => Game.NbTimeStarted = item);
        }
        public string OriginID
        {
            get => Game.OriginID;
            set => SetProperty(Game.OriginID, value, Game, (game, item) => Game.OriginID = item);
        }
        public string Path
        {
            get => Game.Path;
            set => SetProperty(Game.Path, value, Game, (game, item) => Game.Path = item);
        }
        public string Plateforme
        {
            get => Game.Plateforme;
            set => SetProperty(Game.Plateforme, value, Game, (game, item) => Game.Plateforme = item);
        }
        public int RAWGid
        {
            get => Game.Rawgid;
            set => SetProperty(Game.Rawgid, value, Game, (game, item) => Game.Rawgid = item);
        }
        public int ScreenScraperID
        {
            get => Game.ScreenScraperID;
            set => SetProperty(Game.ScreenScraperID, value, Game, (game, item) => Game.ScreenScraperID = item);
        }
        public int SteamGridDBID
        {
            get => Game.Sgdbid;
            set => SetProperty(Game.Sgdbid, value, Game, (game, item) => Game.Sgdbid = item);
        }
        public int SteamID
        {
            get => Game.SteamID;
            set => SetProperty(Game.SteamID, value, Game, (game, item) => Game.SteamID = item);
        }
        public string Year
        {
            get => Game.Year;
            set => SetProperty(Game.Year, value, Game, (game, item) => Game.Year = item);
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
