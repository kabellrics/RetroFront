using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RetroFront.Models;
using RetroFront.Models.StandaloneEmulator;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class AddRetroArchCoreViewModel : ViewModelBase
    {
        public StandalonePlateforme ResultJson { get; internal set; }
        private IDatabaseService _databaseService;
        private IRetroarchService _retroarchService;
        private IFileJSONService _fileJSONService;
        private IDialogService _dialogService;
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
        private StandalonePlateforme _Selectedsysteme;
        public StandalonePlateforme Selectedsysteme
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
        private ObservableCollection<StandalonePlateforme> _systemes;
        public ObservableCollection<StandalonePlateforme> Systemes
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
        public AddRetroArchCoreViewModel(string retroarchpath)
        {
            _retroarchService = App.ServiceProvider.GetRequiredService<IRetroarchService>();
            _databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            _fileJSONService = App.ServiceProvider.GetRequiredService<IFileJSONService>();
            _dialogService = App.ServiceProvider.GetRequiredService<IDialogService>();
            var plateformes = _fileJSONService.GetStandaloneEmulators().FirstOrDefault(x => x.Name == "RetroArch");
            LookForInstalledCore(retroarchpath, plateformes);
        }

        private void LookForInstalledCore(string retroarchpath, StandaloneEmulator plateformes)
        {
            //if (plateformes != null)
            //{
            //    Systemes = new ObservableCollection<StandalonePlateforme>(plateformes.Plateformes.OrderBy(x => x.Name));
            //}
            var cores = _retroarchService.GetInstalledCore(retroarchpath);
            RetroarchCores = new ObservableCollection<RetroarchCore>(cores);
            var InstalledCoreNames = RetroarchCores.Select(x=> x.path.Substring(x.path.LastIndexOf('\\')+1));
            var SysTemesDispo = plateformes.Plateformes.Where(x=> InstalledCoreNames.Any(s=>x.StartupArguments.Contains(s)));
            Systemes = new ObservableCollection<StandalonePlateforme>(SysTemesDispo.OrderBy(x => x.Name));
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
            ResultJson = Selectedsysteme;
            CloseDialogWithResult(parameter as Window, true);
        }
        //private StandalonePlateforme GetJsonEmu()
        //{
        //    Emulator emu = new Emulator();
        //    emu.Name = $"{Selectedsysteme.Name} - Retroarch {SelectedRetroarchCore.Name}";
        //    emu.Extension = Extension;
        //    var SCSPsys = _dialogService.SearchSSysInSSCPByName(Selectedsysteme.Name);
        //    if(SCSPsys != null)
        //    {
        //        emu.SystemeID = SCSPsys.;
        //        //emu.Chemin = _retroarchService.RetroarchEXEPath();
        //        emu.Command = $"-L {SelectedRetroarchCore.path} %ROM_RAW%";
        //        return JsonConvert.SerializeObject(emu);
        //    }
        //    return string.Empty;
        //}
    }
}
