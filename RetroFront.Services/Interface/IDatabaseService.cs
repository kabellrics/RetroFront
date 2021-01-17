using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFront.Services.Interface
{
    public interface IDatabaseService
    {
        void AddEmulator(Emulator sys);
        void AddGame(GameRom sys);
        void AddSystem(Systeme sys);
        Emulator GetEmulator(int id);
        Emulator GetEmulatorByName(string name);
        IEnumerable<Emulator> GetEmulators();
        IEnumerable<Emulator> GetEmulatorsForSysteme(int sysID);
        IEnumerable<GameRom> GetGamesForemulator(int emuID);
        int GetNbEmulatorForSysteme(int sysID);
        int GetNbGamesForemulator(int emuID);
        int GetNbGamesForPlateforme(int sysID);
        IEnumerable<GameRom> GetGamesForPlateforme(int sysID);
        GameRom GetGame(int id);
        GameRom GetGameByName(string path);
        IEnumerable<GameRom> GetGames();
        Systeme GetSysteme(int id);
        Systeme GetSystemeByName(string shortname);
        IEnumerable<Systeme> GetSystemes();
        void RemoveEmulator(Emulator sys);
        void RemoveGame(GameRom sys);
        void RemoveSystem(Systeme sys);
        void SaveUpdate();
    }
}