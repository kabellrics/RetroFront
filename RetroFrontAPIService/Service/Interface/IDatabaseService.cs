using RetroFront.DataAccess;
using RetroFront.Models;
using System.Collections.Generic;

namespace RetroFrontAPIService.Service.Interface
{
    public interface IDatabaseService
    {
        AppSQLIteContext SQLIteContext { get; set; }

        Emulator AddEmulator(Emulator sys);
        GameRom AddGame(GameRom sys);
        Systeme AddSystem(Systeme sys);
        Emulator GetEmulator(int id);
        Emulator GetEmulatorByName(string name);
        IEnumerable<Emulator> GetEmulators();
        IEnumerable<Emulator> GetEmulatorsForSysteme(int sysID);
        GameRom GetGame(int id);
        GameRom GetGameByName(string path);
        IEnumerable<GameRom> GetGames();
        IEnumerable<GameRom> GetGamesForemulator(int emuID);
        IEnumerable<GameRom> GetGamesForPlateforme(int sysID);
        int GetNbEmulatorForSysteme(int sysID);
        int GetNbGamesForemulator(int emuID);
        int GetNbGamesForPlateforme(int sysID);
        Systeme GetSysteme(int id);
        Systeme GetSystemeByName(string shortname);
        IEnumerable<Systeme> GetSystemes();
        void RemoveEmulator(Emulator sys);
        void RemoveGame(GameRom sys);
        void RemoveSystem(Systeme sys);
        void SaveUpdate();
    }
}