using RetroFront.DataAccess;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class DatabaseService : IDatabaseService
    {
        private AppSQLIteContext SQLIteContext;

        public DatabaseService()
        {
            SQLIteContext = new AppSQLIteContext();
        }

        public IEnumerable<Systeme> GetSystemes()
        {
            return SQLIteContext.Systemes;
        }
        public IEnumerable<Emulator> GetEmulators()
        {
            return SQLIteContext.Emulators;
        }
        public IEnumerable<GameRom> GetGames()
        {
            return SQLIteContext.Games;
        }

        public Systeme GetSysteme(int id)
        {
            return SQLIteContext.Systemes.Find(id);
        }
        public Emulator GetEmulator(int id)
        {
            return SQLIteContext.Emulators.Find(id);
        }
        public GameRom GetGame(int id)
        {
            return SQLIteContext.Games.Find(id);
        }
        public IEnumerable<Emulator> GetEmulatorsForSysteme(int sysID)
        {
            return SQLIteContext.Emulators.Where(x=> x.SystemeID == sysID);
        }
        public int GetNbEmulatorForSysteme(int sysID)
        {
            return SQLIteContext.Emulators.Where(x => x.SystemeID == sysID).Count();
        }
        public int GetNbGamesForPlateforme(int sysID)
        {
            var emus = GetEmulatorsForSysteme(sysID);
            var plateformeGames = SQLIteContext.Games.Where(x=> emus.Select(x=>x.EmulatorID).Contains(x.EmulatorID));
            return plateformeGames.Count();
        }
        public IEnumerable<GameRom> GetGamesForPlateforme(int sysID)
        {
            var emus = GetEmulatorsForSysteme(sysID);
            return SQLIteContext.Games.Where(x => emus.Select(x => x.EmulatorID).Contains(x.EmulatorID));
        }
        public IEnumerable<GameRom> GetGamesForemulator(int emuID)
        {
            return SQLIteContext.Games.Where(x => x.EmulatorID == emuID);
        }
        public int GetNbGamesForemulator(int emuID)
        {
            return SQLIteContext.Games.Where(x => x.EmulatorID == emuID).Count();
        }
        public Systeme GetSystemeByName(string shortname)
        {
            return SQLIteContext.Systemes.FirstOrDefault(x => x.Shortname == shortname);
        }
        public Emulator GetEmulatorByName(string name)
        {
            return SQLIteContext.Emulators.FirstOrDefault(x => x.Name == name);
        }
        public GameRom GetGameByName(string path)
        {
            return SQLIteContext.Games.FirstOrDefault(x => x.Path == path);
        }

        public void AddSystem(Systeme sys)
        {
            SQLIteContext.Systemes.Add(sys);
            SQLIteContext.SaveChanges();
        }
        public void AddEmulator(Emulator sys)
        {
            SQLIteContext.Emulators.Add(sys);
            SQLIteContext.SaveChanges();
        }
        public void AddGame(GameRom sys)
        {
            SQLIteContext.Games.Add(sys);
            SQLIteContext.SaveChanges();
        }
        public void RemoveSystem(Systeme sys)
        {
            SQLIteContext.Systemes.Remove(sys);
            SQLIteContext.SaveChanges();
        }
        public void RemoveEmulator(Emulator sys)
        {
            SQLIteContext.Emulators.Remove(sys);
            SQLIteContext.SaveChanges();
        }
        public void RemoveGame(GameRom sys)
        {
            SQLIteContext.Games.Remove(sys);
            SQLIteContext.SaveChanges();
        }
        public void SaveUpdate()
        {
            SQLIteContext.SaveChanges();
        }
    }
}
