using RetroFront.Models;
using RetroFront.Models.Pegasus;

namespace RetroFront.Services.Interface
{
    public interface IPegasusService
    {
        Collection PegasusCollectionFromEmulator(Emulator emulator, Systeme sys);
        Models.Pegasus.Game PegasusGameFromGameRom(GameRom gamerom);
        string StringFromPegasusCollection(Collection collection);
        string StringFromPegasusGame(Models.Pegasus.Game game);
    }
}