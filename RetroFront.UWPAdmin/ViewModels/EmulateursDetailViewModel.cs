using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Animations;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using RetroFront.UWPAdmin.Views;
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
        public ObservableCollection<DisplayGame> Roms { get; } = new ObservableCollection<DisplayGame>();
        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteElement));
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

        private async void DeleteElement()
        {
            var textdialog = new StringBuilder();
            textdialog.AppendLine("Etes vous sure de vouloir supprimer cette emulateur ?");
            if(Roms.Count > 0)
                textdialog.AppendLine($"Cela supprimera les {Roms.Count} jeux liées à cette emulateur");
            var dialogresult = await dialogService.ConfirmationDialogAsync(textdialog.ToString());
            if (dialogresult.HasValue && dialogresult == true)
            {
                foreach(var item in Roms)
                {
                    var tgame = Task.Run(async () => await emulatorDetailService.DeleteGame(item.Game));
                    await dialogService.ConfirmationDialogAsync($"Jeu {item.Name} Supprimé");
                }
                var temu = Task.Run(async () => await emulatorDetailService.DeleteEmulator(Source.Emulator));
                await dialogService.ConfirmationDialogAsync($"Emulateur {Source.Name} Supprimé");
                NavigationService.GoBack();
            }
        }
        public EmulateursDetailViewModel()
        {
            emulatorDetailService = new EmulatorDetailService();
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
        }

        internal void OnItemSelected(DisplayGame args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(GamesSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<GamesDetailPage>(selected.ID.ToString());
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
            if (files != null)
            {
                foreach (var file in files)
                {
                    GameRom rom = new GameRom();
                    rom.Path = file;
                    rom.Name = Path.GetFileNameWithoutExtension(file);
                    rom.EmulatorID = Source.ID;
                    var resolveSSCP = await dialogService.ConfirmationDialogAsync($"Voules-vous résoudre le jeu {rom.Name} pour Screenscraper ?");
                    if (resolveSSCP.HasValue && resolveSSCP == true)
                    {
                        var result = await dialogService.SearchSteamGridDBByName(rom.Name, ScraperSource.Screenscraper);
                        if (result != null)
                        {
                            rom.ScreenScraperID = result.Id;
                            rom.Name = result.Name;
                        }
                    }
                    var resolveSGDB = await dialogService.ConfirmationDialogAsync($"Voules-vous résoudre le jeu {rom.Name} pour SteamGridDB ?");
                    if (resolveSGDB.HasValue && resolveSGDB == true)
                    {
                        var result = await dialogService.SearchSteamGridDBByName(rom.Name, ScraperSource.SGDB);
                        if (result != null)
                        {
                            rom.Sgdbid = result.Id;
                            rom.Name = result.Name;
                        }
                    }
                    var resolveIGDB = await dialogService.ConfirmationDialogAsync($"Voules-vous résoudre le jeu {rom.Name} pour IGDB ?");
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
                await dialogService.ConfirmationDialogAsync($"Fin du traitement de l'ajout des jeux"); 
            }
        }
        public async void Initialize(string selectedsysID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedsysID))
            {
                Source = emulatorDetailService.GetEmulator(int.Parse(selectedsysID));
                Roms.Clear();
                var games = await emulatorDetailService.GetGameForEmulator(Source.Emulator);
                foreach (var rom in games)
                    Roms.Add(new DisplayGame(rom));
            }
        }
    }
}
