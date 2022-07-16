using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class RawDataEmulatorViewModel : ViewModelBase
    {
        private IDatabaseService databaseService;
        private ObservableCollection<Emulator> _emulators;
        public ObservableCollection<Emulator> Emulators
        {
            get { return _emulators; }
            set { _emulators = value; RaisePropertyChanged(); }
        }
        private RelayCommand _unloadedCommand;
        public ICommand UnloadedCommand
        {
            get
            {
                return _unloadedCommand ?? (_unloadedCommand = new RelayCommand(UnLoaded));
            }
        }

        private void UnLoaded()
        {
            databaseService.SaveUpdate();
        }
        public RawDataEmulatorViewModel()
        {
            databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            Emulators = new ObservableCollection<Emulator>();
            foreach (var sys in databaseService.GetEmulators())
            {
                Emulators.Add(sys);
            }
        }
    }
}
