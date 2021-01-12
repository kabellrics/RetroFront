using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class GameService : IGameService
    {
        public Game CreateGame(string gamefile, Emulator emulator)
        {
            Game game = new Game();
            game.Path = gamefile;
            game.Name = System.IO.Path.GetFileNameWithoutExtension(gamefile);
            game.EmulatorID = emulator.EmulatorID;
            return game;
        }
    }
}
