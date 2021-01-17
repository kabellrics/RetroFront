using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RetroFront.Models;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class SteamGamesFoundedViewModel:ViewModelBase
    {
        private ICommand _cancelCommand;
        private ICommand _yesCommand; 
        private List<GameViewModel> _foundedgame;
        private List<GameRom> _resultgame;
        public List<GameViewModel> FoundedGame
        {
            get { return _foundedgame; }
            set { _foundedgame = value;RaisePropertyChanged(); }
        } 
        public List<GameRom> Resultgame
        {
            get { return _resultgame; }
            set { _resultgame = value;RaisePropertyChanged(); }
        }

        public SteamGamesFoundedViewModel(List<GameRom> foundedgame)
        {
            FoundedGame = new List<GameViewModel>();
            Resultgame = new List<GameRom>();
            foreach(var game in foundedgame.OrderBy(x => x.Name))
            {
                FoundedGame.Add(new GameViewModel(game));
            }
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
            Resultgame = FoundedGame.Where(x => x.IsSelected == true).Select(s => s.Game).ToList();
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
