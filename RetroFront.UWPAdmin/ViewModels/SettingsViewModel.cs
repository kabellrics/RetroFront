using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace RetroFront.UWPAdmin.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings.md
    public class SettingsViewModel : ObservableObject
    {
        private Settings_Service service = new Settings_Service();
        private DialogService dialogService;
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { SetProperty(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { SetProperty(ref _versionDescription, value); }
        }
        
        private DisplaySettings _Settings;

        public DisplaySettings Settings
        {
            get { return _Settings; }

            set { SetProperty(ref _Settings, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SaveChange));

        private async void SaveChange()
        {
            if(Settings.HasChanged)
            {
                var result = await dialogService.ConfirmationDialogAsync("Voulezvous sauvegardez vos changements ?");
                if(result == true)
                {
                    service.SaveSetting(Settings);
                }
            }
        }

        private ICommand _folderDataCommand;
        public ICommand FolderDataCommand => _folderDataCommand ?? (_folderDataCommand = new RelayCommand(FolderData));

        private async void FolderData()
        {
            var folder = await dialogService.FolderPicker();
            if (!string.IsNullOrEmpty(folder))
            {
                Settings.AppSettingsFolder = folder;
            }
        }

        private ICommand _folderRetroarchCommand;
        public ICommand FolderRetroarchCommand => _folderRetroarchCommand ?? (_folderRetroarchCommand = new RelayCommand(FolderRetroarch));

        private async void FolderRetroarch()
        {
            var folder = await dialogService.FilePicker(new List<string>() {"retroarch.exe" });
            if (!string.IsNullOrEmpty(folder))
            {
                Settings.RetroarchPath = folder;
            }
        }

        private ICommand _folderPegasusLogoCommand;
        public ICommand FolderPegasusLogoCommand => _folderPegasusLogoCommand ?? (_folderPegasusLogoCommand = new RelayCommand(FolderPegasusLogo));

        private async void FolderPegasusLogo()
        {
            var folder = await dialogService.FolderPicker();
            if(!string.IsNullOrEmpty(folder))
            {
                Settings.PegasusIconFolderPath = folder;
            }
        }

        public SettingsViewModel()
        {
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            Settings = service.GetSetting();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
