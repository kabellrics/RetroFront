using RetroFront.Models;

namespace RetroFront.Services.Interface
{
    public interface IGameService
    {
        Game CreateGame(string gamefile, Emulator emulator);
        Game ScrapeGame(Game game);
    }
}