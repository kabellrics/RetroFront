using Microsoft.Win32;
using RetroFront.Admin.Dialogs.ViewModel;
using RetroFront.Admin.Dialogs.Views;
using RetroFront.Models;
using RetroFront.Models.StandaloneEmulator;
using RetroFront.Models.SteamGridDB;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
//using Microsoft.WindowsAPICodePack.Dialogs;

namespace RetroFront.Admin.Dialogs
{
    public class DialogService : IDialogService
    {
        public string ShowSaveFileDialog(string defaultfilename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!string.IsNullOrEmpty(defaultfilename))
                saveFileDialog.FileName = defaultfilename;
            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;
            else
                return string.Empty;

        }
        public bool ShowMessageOk(string title, string message)
        {
            MessageBoxResult dialogResult = MessageBox.Show(message, title, MessageBoxButton.OK);
            if (dialogResult == MessageBoxResult.OK || dialogResult == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
        public bool ShowMessageOkCancel(string title, string message)
        {
            MessageBoxResult dialogResult = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            if (dialogResult == MessageBoxResult.OK || dialogResult == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
        public bool showMessageYesNoCancel(string title, string message)
        {
            MessageBoxResult dialogResult = MessageBox.Show(message, title, MessageBoxButton.YesNoCancel);
            if (dialogResult == MessageBoxResult.OK || dialogResult == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
        public bool showMessageYesNo(string title, string message)
        {
            MessageBoxResult dialogResult = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.OK || dialogResult == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
        public void ShowRawDataSysteme()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new RawDataSystemeViewModel();
            modalWindow.DataContext = vm;
            modalWindow.ShowDialog();
        }
        public void ShowRawDataEmulator()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new RawDataEmulatorViewModel();
            modalWindow.DataContext = vm;
            modalWindow.ShowDialog();
        }
        public void ShowRawDataGame()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new RawDataGameViewModel();
            modalWindow.DataContext = vm;
            modalWindow.ShowDialog();
        }
        public string showInputDialog(string title = null)
        {
            var modaltitle = string.IsNullOrEmpty(title) ? "Entrez le nom de votre thème" : title;
            ModalWindow modalWindow = new ModalWindow();
            var vm = new CreateThemeViewModel(modaltitle);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public string showImgPickerForPlateformeDialog(Systeme title, string themename, ScraperType scraperType)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new GetPictureForSystemViewModel(title, themename,scraperType);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultPath;
            }
            return null;
        }
        public string OpenUniqueFileDialog(string filter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = false;
            var dialogresult = openFileDialog.ShowDialog();
            if (dialogresult.Value)
                return openFileDialog.FileName;
            else
                return null;
        }
        public IEnumerable<string> OpenMultiFileDialog(string filter)
        {
            var openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = filter;
            openFileDialog.Multiselect = true;
            var dialogresult = openFileDialog.ShowDialog();
            if (dialogresult.Value)
                return openFileDialog.FileNames;
            else
                return null;
        }
        //public string OpenFolderPicker()
        //{
        //    var dialog = new CommonOpenFileDialog();
        //    dialog.IsFolderPicker = true;
        //    CommonFileDialogResult dialogresult = dialog.ShowDialog();
        //    if (dialogresult == CommonFileDialogResult.Ok)
        //        return dialog.FileName;
        //    else
        //        return null;
        //}
        public string[] CreateStandalone()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new AddStandaloneViewModel();
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public string CreateJsonSys()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new AddSystemeViewModel();
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public string CreateJsonEmu()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new CreateEmulateurModalViewModel();
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public Emulator OpenDetailEmu(Emulator emu)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new CreateEmulateurModalViewModel(emu);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.Result;
            }
            return null;
        }
        public StandalonePlateforme CreateRetroarchCore(string retroarchpath)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new AddRetroArchCoreViewModel(retroarchpath);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public Systeme ShowSystemeDetail(Systeme sys)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new SystemeDetailViewModel(sys);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.Sys;
            }
            return null;
        }

        public GameRom ShowGameDetail(GameRom game)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new GameDetailViewModel(game);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.GameCurrent;
            }
            return null;
        }
        public bool ShowParameters()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new SettingsViewModel();
            modalWindow.DataContext = vm;
            return modalWindow.ShowDialog().Value;
        }

        public bool DllContent(string uritodll, string destifile, string targetname, string typetodll)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new DllModalViewModel(uritodll,destifile,targetname,typetodll);
            modalWindow.DataContext = vm;
            return modalWindow.ShowDialog().Value;
        }
        public bool DllContent(byte[] bytetoWrite, string destifile, string targetname, string typetodll)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new DllModalViewModel(bytetoWrite, destifile,targetname,typetodll);
            modalWindow.DataContext = vm;
            return modalWindow.ShowDialog().Value;
        }

        public List<GameRom> ShowSteamGamesFounded(List<GameRom> foundedgame)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new SteamGamesFoundedViewModel(foundedgame);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.Resultgame;
            }
            return null;
        }
        public List<GameRom> AddGamesToCollection(string collecName, IEnumerable<GameRom> foundedgame)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new AddGamesToCollectionViewModel(collecName,foundedgame);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.Resultgame;
            }
            return null;
        }
        public Search SearchSteamGridDBByName(string name, ScraperSource source)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new SteamGridDBGameFinderViewModel(name,source);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.Resultgame;
            }
            return null;
        }
        public RetroFront.Models.ScreenScraper.System.Systeme SearchSSysInSSCPByName(string name)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new FindScreenScraperSystemeViewModel(name);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultSystem;
            }
            return null;
        }
        public string SearchImgInSteamGridDB(GameRom game, ScraperType type, ScraperSource source)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new ImgFinderSearchViewModel(game,source,type);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultPath;
            }
            return null;
        }
        public string SearchVideo(GameRom game, ScraperType type, ScraperSource source)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new VideoFinderSearchViewModel(game,source,type);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultPath;
            }
            return null;
        }
    }
}
