using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class EmulateursDetailViewModel : ObservableObject
    {
        private EmulatorDetailService emulatorDetailService;
        private DialogService dialogService;
        private DisplayEmulator _source;
        public DisplayEmulator Source
        {
            get => _source;
            set
            {
                SetProperty(ref _source, value);
            }
        }
        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SaveChange));
        private ICommand _AddGameCommand;
        public ICommand AddGameCommand => _AddGameCommand ?? (_AddGameCommand = new RelayCommand(AddGame));

        private async void SaveChange()
        {
            if (Source.HasChanged == true)
            {
                var result = await dialogService.ConfirmationDialogAsync("Voulez vous sauvegarder les changements effectués ?", "Oui", "Non");
                if (result == true)
                {
                        await emulatorDetailService.UpdateEmulator(Source);
                }
            }
        }
        public EmulateursDetailViewModel()
        {
            emulatorDetailService = new EmulatorDetailService();
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
        }

        public async Task LoadDataAsync()
        {
            //Source.Clear();

            //Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");

            //foreach (var item in data)
            //{
            //    Source.Add(item);
            //}
        }

        private async void AddGame()
        {
            var files = await dialogService.MultipleFilePicker(Source.Extension.Split(" ").ToList());
            foreach(var file in files)
            {
                GameRom rom = new GameRom();
                rom.Path = file;
                rom.Name = Path.GetFileNameWithoutExtension(file);
                rom.EmulatorID = Source.ID;
                var resolveSSCP = await dialogService.ConfirmationDialogAsync("Voules-vous résoudre le jeu pour Screenscraper ?");
                if (resolveSSCP.HasValue && resolveSSCP == true)
                {
                    var result = await dialogService.SearchSteamGridDBByName(rom.Name, ScraperSource.Screenscraper);
                    if(result != null)
                    {
                        rom.ScreenScraperID = result.Id;
                        rom.Name = result.Name;
                    }
                }
                var resolveSGDB = await dialogService.ConfirmationDialogAsync("Voules-vous résoudre le jeu pour SteamGridDB ?");
                if (resolveSGDB.HasValue && resolveSGDB == true)
                {
                    var result = await dialogService.SearchSteamGridDBByName(rom.Name, ScraperSource.SGDB);
                    if (result != null)
                    {
                        rom.Sgdbid = result.Id;
                        rom.Name = result.Name;
                    }
                }
                var resolveIGDB = await dialogService.ConfirmationDialogAsync("Voules-vous résoudre le jeu pour IGDB ?");
                if (resolveIGDB.HasValue && resolveIGDB == true)
                {
                    var result = await dialogService.SearchSteamGridDBByName(rom.Name, ScraperSource.IGDB);
                    if (result != null)
                    {
                        rom.Igdbid = result.Id;
                        rom.Name = result.Name;
                    }
                }
                var t = Task.Run(async () => await emulatorDetailService.CreateGame(rom));
                await dialogService.ConfirmationDialogAsync($"Création en base de la rom : {rom.Name}");
            }
        }
        public void Initialize(string selectedsysID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedsysID))
            {
                Source = emulatorDetailService.GetEmulator(int.Parse(selectedsysID));
            }
        }
    }
}
