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
        public IEnumerable<Game> GetGames()
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
        public Game GetGame(int id)
        {
            return SQLIteContext.Games.Find(id);
        }

        public Systeme GetSystemeByName(string shortname)
        {
            return SQLIteContext.Systemes.FirstOrDefault(x => x.Shortname == shortname);
        }
        public Emulator GetEmulatorByName(string name)
        {
            return SQLIteContext.Emulators.FirstOrDefault(x => x.Name == name);
        }
        public Game GetGameByName(string path)
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
        public void AddGame(Game sys)
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
        public void RemoveGame(Game sys)
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
