using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class AddRetroArchCoreViewModel : ViewModelBase
    {
        public string ResultJson { get; internal set; }
        private IDatabaseService _databaseService;
        private IRetroarchService _retroarchService;
        private ICommand _cancelCommand;
        private ICommand _yesCommand;
        private string _name; 
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set { _extension = value; RaisePropertyChanged(); }
        }
        private Systeme _Selectedsysteme;
        public Systeme Selectedsysteme
        {
            get { return _Selectedsysteme; }
            set { _Selectedsysteme = value; RaisePropertyChanged(); }
        }
        private RetroarchCore _selectedretroarchCore;
        public RetroarchCore SelectedRetroarchCore
        {
            get { return _selectedretroarchCore; }
            set { _selectedretroarchCore = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Systeme> _systemes;
        public ObservableCollection<Systeme> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<RetroarchCore> _retroarchCores;
        public ObservableCollection<RetroarchCore> RetroarchCores
        {
            get { return _retroarchCores; }
            set { _retroarchCores = value; RaisePropertyChanged(); }
        }
        public AddRetroArchCoreViewModel()
        {
            _retroarchService = App.ServiceProvider.GetRequiredService<IRetroarchService>();
            _databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            var plateformes = _databaseService.GetSystemes();
            Systemes = new ObservableCollection<Systeme>(plateformes);
            var cores = _retroarchService.GetInstalledCore();
            RetroarchCores = new ObservableCollection<RetroarchCore>(cores);
        }
        public void CloseDialogWithResult(Window dialog, bool result)
        {
            if (dialog != null)
                dialog.DialogResult = result;
        }
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(CancelClick));
            }
        }
        private void CancelClick(object parameter)
        {
            CloseDialogWithResult(parameter as Window, false);
        }
        public ICommand YesCommand
        {
            get
            {
                return _yesCommand ?? (_yesCommand = new RelayCommand<object>(ValidateClick));
            }
        }
        private void ValidateClick(object parameter)
        {
            ResultJson = GetJsonEmu();
            CloseDialogWithResult(parameter as Window, true);
        }
        private string GetJsonEmu()
        {
            Emulator emu = new Emulator();
            emu.Name = $"{Selectedsysteme.Name} - Retroarch {SelectedRetroarchCore.Name}";
            emu.Extension = Extension;
            emu.SystemeID = Selectedsysteme.SystemeID;
            emu.Chemin = _retroarchService.RetroarchEXEPath();
            emu.Command = $"-L {SelectedRetroarchCore.path} %ROM_RAW%";
            return JsonConvert.SerializeObject(emu);
        }
    }
}
