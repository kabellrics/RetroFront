using System;
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
    public class EmulateursViewModel : ObservableObject
    {
        public const string EmulateursSelectedIdKey = "EmulateursSelectedIdKey";
        private DialogService dialogService;
        private EmulatorsService emulatorsService;
        private ICommand _itemSelectedCommand;
        private ICommand _SaveChangeCommand;
        public ICommand SaveChangeCommand => _SaveChangeCommand ?? (_SaveChangeCommand = new RelayCommand(SaveChange));
        private ICommand _addPlateformeCommand;
        public ICommand AddPlateformeCommand => _addPlateformeCommand ?? (_addPlateformeCommand = new RelayCommand(AddPlateforme));

        private async void SaveChange()
        {
            if (Source.Any(x => x.HasChanged == true))
            {
                var result = await dialogService.ConfirmationDialogAsync("Voulez vous sauvegarder les changements effectués ?", "Oui", "Non");
                if (result == true)
                {
                    foreach (var row in Source.Where(x => x.HasChanged == true))
                    {
                        await emulatorsService.UpdateEmulator(row);
                    }
                }
            }
        }
        private async void AddPlateforme()
        {
            var result = await dialogService.AddPlateformeDialog();
            if (result != null)
            {
                var emupath = await dialogService.FilePicker(new System.Collections.Generic.List<string>() { ".exe" });
                if (!string.IsNullOrEmpty(emupath))
                {

                    var syss = await emulatorsService.GetSystemes();
                    var sysdis = syss.FirstOrDefault(x => x.ShortName == result.systeme.Shortname);
                    Systeme sys = new Systeme();
                    if(sysdis == null)
                    {
                        sys = result.systeme;
                        sys = await emulatorsService.CreateSysteme(sys);
                    }
                    else
                    {
                        sys = sysdis.Systeme;
                    }
                    var emu = result.emulator;
                    emu.SystemeID = sys.SystemeID;
                    emu.Chemin = emupath;
                    emu = await emulatorsService.CreateEmulator(emu);
                    await dialogService.MessageDialogAsync("Plateforme et Emulateur créer en base de données", "Veuillez rechargez la page");
                } 
            }
        }
        public ObservableCollection<DisplayEmulator> Source { get; } = new ObservableCollection<DisplayEmulator>();

        //public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnItemSelected));
        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<DisplayEmulator>(OnItemSelected));
        private bool _toggleRaw;
        public bool ToggleRaw
        {
            get => _toggleRaw;
            set
            {
                SetProperty(ref _toggleRaw, value);
            }
        }
        public EmulateursViewModel()
        {
            emulatorsService = new EmulatorsService();
            dialogService = Ioc.Default.GetRequiredService<DialogService>();
            ToggleRaw = false;
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            //var data = await SampleDataService.GetImageGalleryDataAsync("ms-appx:///Assets");
            var data = await emulatorsService.GetEmulators();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void OnItemSelected(DisplayEmulator args)
        {
            var selected = args;
            //ImagesNavigationHelper.AddImageId(EmulateursSelectedIdKey, selected.ID);
            NavigationService.Frame.SetListDataItemForNextConnectedAnimation(selected);
            NavigationService.Navigate<EmulateursDetailPage>(selected.ID.ToString());
        }
    }
}
