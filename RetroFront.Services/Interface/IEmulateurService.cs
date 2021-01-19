using RetroFront.Models;

namespace RetroFront.Services.Interface
{
    public interface IEmulateurService
    {
        Emulator CreateEmulateur(Systeme platform, string Name, string Command, string Extension);
        Emulator AddExplorer(Systeme systeme);
        string FormatExtension(Emulator ext);
        Emulator DuplicateEmulator(Emulator emulator);
    }
}