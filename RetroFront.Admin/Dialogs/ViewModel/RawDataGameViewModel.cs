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
    public class RawDataGameViewModel : ViewModelBase
    {
        private IDatabaseService databaseService;
        private ObservableCollection<GameRom> _games;
        public ObservableCollection<GameRom> Games
        {
            get { return _games; }
            set { _games = value; RaisePropertyChanged(); }
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
        public RawDataGameViewModel()
        {
            databaseService = App.ServiceProvider.GetRequiredService<IDatabaseService>();
            Games = new ObservableCollection<GameRom>();
            foreach (var sys in databaseService.GetGames())
            {
                Games.Add(sys);
            }
        }
    }
}
