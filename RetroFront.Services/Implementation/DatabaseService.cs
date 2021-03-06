﻿using RetroFront.DataAccess;
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
        private AppSQLIteContext _SQLIteContext;

        public AppSQLIteContext SQLIteContext { get => _SQLIteContext; set => _SQLIteContext = value; }

        public DatabaseService()
        {
            _SQLIteContext = new AppSQLIteContext();
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
            return _SQLIteContext.Emulators.Where(x=> x.SystemeID == sysID);
        }
        public int GetNbEmulatorForSysteme(int sysID)
        {
            return _SQLIteContext.Emulators.Where(x => x.SystemeID == sysID).Count();
        }
        public int GetNbGamesForPlateforme(int sysID)
        {
            var emus = GetEmulatorsForSysteme(sysID);
            var plateformeGames = _SQLIteContext.Games.Where(x=> emus.Select(x=>x.EmulatorID).Contains(x.EmulatorID));
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
