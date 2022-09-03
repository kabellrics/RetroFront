using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.ViewModels
{
    public class SystèmesViewModel : ObservableObject
    {
        public const string SystèmesSelectedIdKey = "SystèmesSelectedIdKey";
        private SystemesService systemesService;
        private DialogService dialogService;
        private ICommand _itemSelectedCommand;
        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SaveChange));
        private ICommand _AddSteamCommand;
        public ICommand AddSteamCommand => _AddSteamCommand ?? (_AddSteamCommand = new RelayCommand(AddSteam));
        private ICommand _AddEpicCommand;
        public ICommand AddEpicCommand => _AddEpicCommand ?? (_AddEpicCommand = new RelayCommand(AddEpic));
        private ICommand _AddOriginCommand;
        public ICommand AddOriginCommand => _AddOriginCommand ?? (_AddOriginCommand = new RelayCommand(AddOrigin));
        private ICommand _AllExportPegasusCommand;
        public ICommand AllExportPegasusCommand => _AllExportPegasusCommand ?? (_AllExportPegasusCommand = new RelayCommand(AllExportPegasus));
        private ICommand _AddAppsCommand;
        public ICommand AddAppsCommand => _AddAppsCommand ?? (_AddAppsCommand = new RelayCommand(AddApps));

        private async void AddApps()
        {
            var appexe = await dialogService.FilePicker(new System.Collections.Generic.List<string>() { ".exe", ".lnk"});
            await Task.Run(async () => await systemesService.AddWindowsApp(appexe));
            await dialogService.ConfirmationDialogAsync("Jeux Origins ajoutés");
        }

        private async void AllExportPegasus()
        {
            await systemesService.AllExportToPegasus(Source);
        }

        private async void AddOrigin()
        {
            var games = await dialogService.GetInstalledGame(LocalGame.Origin);
            await Task.Run(async () => await systemesService.CreateOriginGames(games));
            await dialogService.ConfirmationDialogAsync("Jeux Origins ajoutés");
        }
        private async void AddSteam()
        {
            var games = await dialogService.GetInstalledGame(LocalGame.Steam);
            await Task.Run(async () => await systemesService.CreateSteamGames(games));
            await dialogService.ConfirmationDialogAsync("Jeux Steams ajoutés");
        }

        private async void AddEpic()
        {
            var games = await dialogService.GetInstalledGame(LocalGame.Epic);
            await Task.Run(async () => await systemesService.CreateEpicGames(games));
            await dialogService.ConfirmationDialogAsync("Jeux Epics ajoutés");
        }

        private async void SaveChange()
        {
           if(Source.Any(x=>x.HasChanged == true))
            {
                var result = await dialogService.ConfirmationDialogAsync("Voulez vous sauvegarder les changements effectués ?","Oui","Non");
                if(result == true)
                {
                    foreach(var row in Source.Where(x=>x.HasChanged == true))
                    {
                        await systemesService.UpdateSysteme(row);
                    }
                }
            }
        }

        public ObservableCollection<DisplaySysteme> Source { get; } = new ObservableCollection<DisplaySysteme>();

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<DisplaySysteme>(OnItemSelected));
        //public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnItemSelected));
        private bool _toggleRaw;
        public bool ToggleRaw
        {
            get => _toggleRaw;
            set
            {
                SetProperty(ref _toggleRaw, value);
            }
        }
        public SystèmesViewModel()
        {
            systemesService = new SystemesService();
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
            ToggleRaw = false;
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");
            var data = await systemesService.GetSystemes();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void OnItemSelected(DisplaySysteme args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(SystèmesSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<SystèmesDetailPage>(selected.ID.ToString());
        }
    }
}
