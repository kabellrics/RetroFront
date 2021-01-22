﻿using RetroFront.Models;
using RetroFront.Models.SteamGridDB;
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
        string showInputDialog(string title = null);
        string showImgPickerForPlateformeDialog(Systeme title, string themename);
        //string OpenFolderPicker();
        string CreateJsonEmu();
        string CreateJsonSys();
        string OpenDetailEmu(Emulator emu);
        string CreateRetroarchCore();
        bool ShowSystemeDetail(Systeme sys);
        bool ShowGameDetail(GameRom game);
        bool ShowParameters();
        List<GameRom> ShowSteamGamesFounded(List<GameRom> foundedgame);
        List<GameRom> AddGamesToCollection(string collecName, IEnumerable<GameRom> foundedgame);
        DataSearch SearchSteamGridDBByName(string name);
        string SearchImgInSteamGridDB(Game game, string type, string target = null);
    }
}
