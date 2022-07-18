using RetroFront.DataAccess;
using RetroFront.Models;
using RetroFrontAPIService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Implémentation
{
    public class DatabaseService : IDatabaseService
    {
        private AppSQLIteContext _SQLIteContext;

        public AppSQLIteContext SQLIteContext { get => _SQLIteContext; set => _SQLIteContext = value; }

        public DatabaseService()
        {
            _SQLIteContext = new AppSQLIteContext();
            SQLIteContext.Database.EnsureCreated();
        }

        public IEnumerable<Systeme> GetSystemes()
        {
            return _SQLIteContext.Systemes;
        }
        public IEnumerable<Emulator> GetEmulators()
        {
            return _SQLIteContext.Emulators;
        }
        public IEnumerable<GameRom> GetGames()
        {
            return _SQLIteContext.Games;
        }

        public Systeme GetSysteme(int id)
        {
            return _SQLIteContext.Systemes.Find(id);
        }
        public Emulator GetEmulator(int id)
        {
            return _SQLIteContext.Emulators.Find(id);
        }
        public GameRom GetGame(int id)
        {
            return _SQLIteContext.Games.Find(id);
        }
        public IEnumerable<Emulator> GetEmulatorsForSysteme(int sysID)
        {
            return _SQLIteContext.Emulators.Where(x => x.SystemeID == sysID);
        }
        public int GetNbEmulatorForSysteme(int sysID)
        {
            return _SQLIteContext.Emulators.Where(x => x.SystemeID == sysID).Count();
        }
        public int GetNbGamesForPlateforme(int sysID)
        {
            var emus = GetEmulatorsForSysteme(sysID);
            var plateformeGames = _SQLIteContext.Games.Where(x => emus.Select(x => x.EmulatorID).Contains(x.EmulatorID));
            return plateformeGames.Count();
        }
        public IEnumerable<GameRom> GetGamesForPlateforme(int sysID)
        {
            var emus = GetEmulatorsForSysteme(sysID);
            return _SQLIteContext.Games.Where(x => emus.Select(x => x.EmulatorID).Contains(x.EmulatorID));
        }
        public IEnumerable<GameRom> GetGamesForemulator(int emuID)
        {
            return _SQLIteContext.Games.Where(x => x.EmulatorID == emuID);
        }
        public int GetNbGamesForemulator(int emuID)
        {
            return _SQLIteContext.Games.Where(x => x.EmulatorID == emuID).Count();
        }
        public Systeme GetSystemeByName(string shortname)
        {
            return _SQLIteContext.Systemes.FirstOrDefault(x => x.Shortname == shortname);
        }
        public Emulator GetEmulatorByName(string name)
        {
            return _SQLIteContext.Emulators.FirstOrDefault(x => x.Name == name);
        }
        public GameRom GetGameByName(string path)
        {
            return _SQLIteContext.Games.FirstOrDefault(x => x.Path == path);
        }

        public Systeme AddSystem(Systeme sys)
        {
            _SQLIteContext.Systemes.Add(sys);
            _SQLIteContext.SaveChanges();
            return sys;
        }
        public Emulator AddEmulator(Emulator sys)
        {
            _SQLIteContext.Emulators.Add(sys);
            _SQLIteContext.SaveChanges();
            return sys;
        }
        public GameRom AddGame(GameRom sys)
        {
            _SQLIteContext.Games.Add(sys);
            _SQLIteContext.SaveChanges();
            return sys;
        }
        public Systeme UpdateSystem(int id, Systeme sys)
        {
            var oldsys = this.GetSysteme(id);
            if (oldsys !=null)
            {
                oldsys.Logo = sys.Logo;
                oldsys.Name = sys.Name;
                oldsys.Screenshoot = sys.Screenshoot;
                oldsys.Shortname = sys.Shortname;
                oldsys.SystemeSCSPID = oldsys.SystemeSCSPID;
                oldsys.Type = oldsys.Type;
                oldsys.Video = oldsys.Video;
                _SQLIteContext.SaveChanges();
                return oldsys; 
            }
            throw new Exception("Systeme Not Found");
        }
        public Emulator UpdateEmulator(int id, Emulator emu)
        {
            var oldemu = this.GetEmulator(id);
            if (oldemu != null)
            {
                oldemu.Chemin = emu.Chemin;
                oldemu.Command = emu.Command;
                oldemu.Extension = emu.Extension;
                oldemu.IsDuplicate = emu.IsDuplicate;
                oldemu.Name = emu.Name;
                oldemu.SystemeID = emu.SystemeID;
                _SQLIteContext.SaveChanges();
                return oldemu;
            }
            throw new Exception("Emulator Not Found");
        }
        public GameRom UpdateGame(int id, GameRom game)
        {
            var oldgame = this.GetGame(id);
            if (oldgame != null)
            {
                oldgame.Boxart = game.Boxart;
                oldgame.Desc = game.Desc;
                oldgame.Dev = game.Dev;
                oldgame.Editeur = game.Editeur;
                oldgame.EmulatorID = game.EmulatorID;
                oldgame.EpicID = game.EpicID;
                oldgame.Fanart = game.Fanart;
                oldgame.Genre = game.Genre;
                oldgame.IGDBID = game.IGDBID;
                oldgame.IsDuplicate = game.IsDuplicate;
                oldgame.IsFavorite = game.IsFavorite;
                oldgame.LastStart = game.LastStart;
                oldgame.Logo = game.Logo;
                oldgame.Name = game.Name;
                oldgame.NbTimeStarted = game.NbTimeStarted;
                oldgame.OriginID = game.OriginID;
                oldgame.Path = game.Path;
                oldgame.Plateforme = game.Plateforme;
                oldgame.RAWGID = game.RAWGID;
                oldgame.RecalView = game.RecalView;
                oldgame.ScreenScraperID = game.ScreenScraperID;
                oldgame.Screenshoot = game.Screenshoot;
                oldgame.SGDBID = game.SGDBID;
                oldgame.SteamID = game.SteamID;
                oldgame.TitleScreen = game.TitleScreen;
                oldgame.Video = game.Video;
                oldgame.Year = game.Year;
                _SQLIteContext.SaveChanges();
                return oldgame;
            }
            throw new Exception("Game Not Found");
        }
        public void RemoveSystem(Systeme sys)
        {
            _SQLIteContext.Systemes.Remove(sys);
            _SQLIteContext.SaveChanges();
        }
        public void RemoveEmulator(Emulator sys)
        {
            _SQLIteContext.Emulators.Remove(sys);
            _SQLIteContext.SaveChanges();
        }
        public void RemoveGame(GameRom sys)
        {
            _SQLIteContext.Games.Remove(sys);
            _SQLIteContext.SaveChanges();
        }
        public void SaveUpdate()
        {
            _SQLIteContext.SaveChanges();
        }
    }
}
