using Microsoft.Win32;
using RetroFront.Admin.Dialogs.ViewModel;
using RetroFront.Admin.Dialogs.Views;
using RetroFront.Models;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RetroFront.Admin.Dialogs
{
    public class DialogService : IDialogService
    {
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
            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = true;
            var dialogresult = openFileDialog.ShowDialog();
            if (dialogresult.Value)
                return openFileDialog.FileNames;
            else
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
        public string OpenDetailEmu(Emulator emu)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new CreateEmulateurModalViewModel(emu);
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public string CreateRetroarchCore()
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new AddRetroArchCoreViewModel();
            modalWindow.DataContext = vm;
            if (modalWindow.ShowDialog().Value)
            {
                return vm.ResultJson;
            }
            return null;
        }
        public bool ShowSystemeDetail(Systeme sys)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new SystemeDetailViewModel(sys);
            modalWindow.DataContext = vm;
            return modalWindow.ShowDialog().Value;
        }

        public bool ShowGameDetail(Game game)
        {
            ModalWindow modalWindow = new ModalWindow();
            var vm = new GameDetailViewModel(game);
            modalWindow.DataContext = vm;
            return modalWindow.ShowDialog().Value;
        }
    }
}
