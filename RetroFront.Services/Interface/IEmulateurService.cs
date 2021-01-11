using RetroFront.Models;

namespace RetroFront.Services.Interface
{
    public interface IEmulateurService
    {
        Emulator CreateEmulateur(Systeme platform, string Name, string Command, string Extension);
    }
}