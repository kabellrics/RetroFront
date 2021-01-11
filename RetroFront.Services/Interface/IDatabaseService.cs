using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IDatabaseService
    {
        void AddEmulator(Emulator sys);
        void AddGame(Game sys);
        void AddSystem(Systeme sys);
        Emulator GetEmulator(int id);
        Emulator GetEmulatorByName(string name);
        IEnumerable<Emulator> GetEmulators();
        IEnumerable<Emulator> GetEmulatorsForSysteme(int sysID);
        IEnumerable<Game> GetGamesForemulator(int emuID);
        int GetNbEmulatorForSysteme(int sysID);
        int GetNbGamesForemulator(int emuID);
        int GetNbGamesForPlateforme(int sysID);
        IEnumerable<Game> GetGamesForPlateforme(int sysID);
        Game GetGame(int id);
        Game GetGameByName(string path);
        IEnumerable<Game> GetGames();
        Systeme GetSysteme(int id);
        Systeme GetSystemeByName(string shortname);
        IEnumerable<Systeme> GetSystemes();
        void RemoveEmulator(Emulator sys);
        void RemoveGame(Game sys);
        void RemoveSystem(Systeme sys);
        void SaveUpdate();
    }
}