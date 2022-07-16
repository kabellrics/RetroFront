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
    public class RawDataSystemeViewModel : ViewModelBase
    {
        private IDatabaseService databaseService;
        private ObservableCollection<Systeme> _systemes;
        public ObservableCollection<Systeme> Systemes
        {
            get { return _systemes; }
            set { _systemes = value; RaisePropertyChanged(); }
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

        public RawDataSystemeViewModel()
        {
            databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            Systemes = new ObservableCollection<Systeme>();
            foreach(var sys in databaseService.GetSystemes())
            {
                Systemes.Add(sys);
            }
        }
    }
}
