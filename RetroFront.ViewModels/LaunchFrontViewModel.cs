using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.ViewModels
{
    public class LaunchFrontViewModel : ViewModelBase
    {
        private RelayCommand _loadedCommand;
        private INavigationService _navigationService;

        public LaunchFrontViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand
                    ?? (_loadedCommand = new RelayCommand(
                    () =>
                    {
                        //_navigationService.NavigateTo("Splash", "MainFrame");
                        _navigationService.NavigateTo("Systeme", null, string.Empty);
                    }));
            }
        }
    }
}
