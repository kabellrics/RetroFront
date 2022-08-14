using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.Models;
using RetroFront.UWPAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class AddPlateformeViewModal : ObservableObject
    {
        private EmulatorsModalService modalService = new EmulatorsModalService();
        private ObservableCollection<RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator> _availableEmulators;
        public ObservableCollection<RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator> AvailableEmulators
        {
            get { return _availableEmulators; }
            set { SetProperty(ref _availableEmulators, value); }
        }
        private ObservableCollection<RetroFront.UWPAdmin.Core.APIHelper.StandalonePlateforme> _availablePlateformes;
        public ObservableCollection<RetroFront.UWPAdmin.Core.APIHelper.StandalonePlateforme> AvailablePlateformes
        {
            get { return _availablePlateformes; }
            set { SetProperty(ref _availablePlateformes, value); }
        }
        private ObservableCollection<ScreenScraper.System.Systeme> _FoundedSystems;
        public ObservableCollection<ScreenScraper.System.Systeme> FoundedSystems
        {
            get { return _FoundedSystems; }
            set { SetProperty(ref _FoundedSystems, value); }
        }

        private ScreenScraper.System.Systeme _FoundedSystem;
        public ScreenScraper.System.Systeme FoundedSystem
        {
            get { return _FoundedSystem; }
            set { SetProperty(ref _FoundedSystem, value); CheckIfValidationAuthorize(); }
        }
        private RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator _selectedEmulators;
        public RetroFront.UWPAdmin.Core.APIHelper.StandaloneEmulator SelectedEmulators
        {
            get { return _selectedEmulators; }
            set { SetProperty(ref _selectedEmulators, value);ChangePlateformeList(); }
        }
        private RetroFront.UWPAdmin.Core.APIHelper.StandalonePlateforme _selectedPlateformes;
        public RetroFront.UWPAdmin.Core.APIHelper.StandalonePlateforme SelectedPlateformes
        {
            get { return _selectedPlateformes; }
            set { SetProperty(ref _selectedPlateformes, value);SearchSystem(); }
        }
        private CreatedSysAndPlateformeObject _resultObject;
        public CreatedSysAndPlateformeObject ResultObject
        {
            get { return _resultObject; }
            set { SetProperty(ref _resultObject, value);SearchSystem();}
        }

        private void CheckIfValidationAuthorize()
        {
            if (FoundedSystem != null && SelectedEmulators != null && SelectedPlateformes != null)
                EnableValider = true;
        }

        private bool _enableValider;
        public bool EnableValider
        {
            get => _enableValider;
            set
            {
                SetProperty(ref _enableValider, value);
            }
        }
        public AddPlateformeViewModal()
        {
            AvailableEmulators = new ObservableCollection<Core.APIHelper.StandaloneEmulator>();
            AvailablePlateformes = new ObservableCollection<Core.APIHelper.StandalonePlateforme>();
            FoundedSystems = new ObservableCollection<ScreenScraper.System.Systeme>();
            EnableValider = false;
        }
        public void InitializeData()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            AvailableEmulators.Clear();
            var emulators = await modalService.GetEmulators();
            foreach(var emu in emulators)
            {
                AvailableEmulators.Add(emu);
            }
        }
        private void ChangePlateformeList()
        {
            AvailablePlateformes.Clear();
            foreach (var emu in SelectedEmulators.Plateformes)
            {
                AvailablePlateformes.Add(emu);
            }
        }
        private void SearchSystem()
        {
            FoundedSystems.Clear();
            foreach (var emu in modalService.SearchSystemByName(SelectedPlateformes.Name))
            {
                FoundedSystems.Add(emu);
            }
        }

        public void SetReturnObject()
        {
            ResultObject = modalService.CreateResultObject(SelectedEmulators, SelectedPlateformes, FoundedSystem);
        }
    }
}
