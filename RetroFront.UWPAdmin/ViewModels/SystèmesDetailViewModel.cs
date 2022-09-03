using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using RetroFront.UWPAdmin.Helpers;
using RetroFront.UWPAdmin.Services;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Navigation;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class SystèmesDetailViewModel : ObservableObject
    {
        private SystemeDetailService systemeDetailService;
        private DialogService dialogService;
        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteElement));
        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SaveChange));
        private ICommand _ScrapeLogoCommand;
        public ICommand ScrapeLogoCommand => _ScrapeLogoCommand ?? (_ScrapeLogoCommand = new RelayCommand(ScrapeLogo));
        private ICommand _ScrapeScreenshootCommand;
        public ICommand ScrapeScreenshootCommand => _ScrapeScreenshootCommand ?? (_ScrapeScreenshootCommand = new RelayCommand(ScrapeScreenshoot));
        private ICommand _SaveAndDLLCommand;
        public ICommand SaveAndDLLCommand => _SaveAndDLLCommand ?? (_SaveAndDLLCommand = new RelayCommand(SaveChangeAndDLLImg));
        private ICommand _ExportPegasusCommand;
        public ICommand ExportPegasusCommand => _ExportPegasusCommand ?? (_ExportPegasusCommand = new RelayCommand(ExportPegasus));

        private async void ExportPegasus()
        {
            await systemeDetailService.ExportToPegasus(Source);
        }
        private async void SaveChange()
        {
            if (Source.HasChanged == true)
            {
                        await systemeDetailService.UpdateSysteme(Source);
            }
        }
        private String _newLogo;
        public String NewLogo
        {
            get => _newLogo;
            set
            {
                SetProperty(ref _newLogo, value);
            }
        }
        private String _newArtwork;
        public String NewArtwork
        {
            get => _newArtwork;
            set
            {
                SetProperty(ref _newArtwork, value);
            }
        }
        private DisplaySysteme _source;
        public DisplaySysteme Source
        {
            get => _source;
            set
            {
                SetProperty(ref _source, value);
            }
        }

        public SystèmesDetailViewModel()
        {
            systemeDetailService = new SystemeDetailService();
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
        }

        public async Task LoadDataAsync()
        {
            //Source.Clear();

            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");

            //foreach (var item in data)
            //{
            //    Source.Add(item);
            //}
        }

        private async void DeleteElement()
        {
            var textdialog = new StringBuilder();
            var nbemus = await systemeDetailService.GetNbEmulatorsInSystemes(Source.Systeme);
            var nbgames = await systemeDetailService.GetNbGamesInSystemes(Source.Systeme);
            textdialog.AppendLine("Etes vous sure de vouloir supprimer ce système ?");
            if (nbemus > 0)
                textdialog.AppendLine($"Cela supprimera les {nbemus} emulateurs liées à ce système");
            if(nbgames > 0)
                textdialog.AppendLine($"Cela supprimera les {nbgames} jeux liées à ce système");
            var dialogresult = await dialogService.ConfirmationDialogAsync(textdialog.ToString());
            if (dialogresult.HasValue && dialogresult == true)
            {
                var emus = await systemeDetailService.GetEmulatorsInSystemes(Source.Systeme);
                foreach(var emu in emus)
                {
                    var games = await systemeDetailService.GetGameForEmulator(emu);
                    foreach(var game in games)
                    {
                        var t = Task.Run(async () => await systemeDetailService.DeleteGame(game));
                        await dialogService.ConfirmationDialogAsync($"Jeu {game.Name} Supprimé");
                    }
                    var temu = Task.Run(async () => await systemeDetailService.DeleteEmulator(emu));
                    await dialogService.ConfirmationDialogAsync($"Emulateur {emu.Name} Supprimé");
                }
                var tsys = Task.Run(async () => await systemeDetailService.DeleteSystems(Source.Systeme));
                await dialogService.ConfirmationDialogAsync($"Système {Source.Name} Supprimé");
                NavigationService.GoBack();
            }
        }
        public void Initialize(string selectedsysID)
        {
            if (!string.IsNullOrEmpty(selectedsysID))
            {
                Source = systemeDetailService.GetSysteme(int.Parse(selectedsysID));
                NewLogo = Source.LogoPath;
                NewArtwork = Source.ScreenshootPath;
            }
        }
        public void Initialize(string selectedsysID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedsysID))
            {
                Source = systemeDetailService.GetSysteme(int.Parse(selectedsysID));
                NewLogo = Source.LogoPath;
                NewArtwork = Source.ScreenshootPath;
            }
        }
        private async void ScrapeLogo()
        {
            var findgame = await dialogService.FileImgPicker();
            if (findgame != null)
            {
                NewLogo = findgame;
            }
        }
        private async void ScrapeScreenshoot()
        {
            var findgame = await dialogService.FileImgPicker();
            if (findgame != null)
            {
                NewArtwork = findgame;
            }
        }
        private async void SaveChangeAndDLLImg()
        {
            if (Source.LogoPath != NewLogo)
            {
                var path = await systemeDetailService.GetNewLogoPath(Source);
                await dialogService.DLLFile(NewLogo, path, "Logo", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        Source.LogoPath = path;
                    });
                });
            }
            if (Source.ScreenshootPath != NewArtwork)
            {
                var path = await systemeDetailService.GetNewScreenshootPath(Source);
                await dialogService.DLLFile(NewArtwork, path, "Artwork", Source.Name).ContinueWith(async task =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        Source.ScreenshootPath = path;
                    });
                });
            }
            SaveChange();
            var saveID = Source.ID;
            Source = null;
            Initialize(saveID.ToString());
        }
    }
}
