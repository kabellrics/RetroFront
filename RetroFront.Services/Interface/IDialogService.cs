using RetroFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Interface
{
    public interface IDialogService
    {
        bool ShowMessageOk(string title, string message);
        bool ShowMessageOkCancel(string title, string message);
        bool showMessageYesNoCancel(string title, string message);
        bool showMessageYesNo(string title, string message);
        string OpenUniqueFileDialog(string filter);
        IEnumerable<string> OpenMultiFileDialog(string filter);
        string CreateJsonEmu();
        string CreateJsonSys();
        string OpenDetailEmu(Emulator emu);
        string CreateRetroarchCore();
        bool ShowSystemeDetail(Systeme sys);
        bool ShowGameDetail(GameRom game);
        bool ShowParameters();
        List<GameRom> ShowSteamGamesFounded(List<GameRom> foundedgame);
    }
}
