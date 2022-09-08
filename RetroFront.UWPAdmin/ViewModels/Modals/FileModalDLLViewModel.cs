using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using Windows.ApplicationModel.Core;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class FileModalDLLViewModel : ObservableObject
    {
        private BaseService service = new BaseService();
        private String _title;
        public String Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private String _source;
        public String Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }
        private byte[] _sourcebyte;
        public byte[] SourceByte
        {
            get { return _sourcebyte; }
            set { SetProperty(ref _sourcebyte, value); }
        }
        private String _dest;
        public String Dest
        {
            get { return _dest; }
            set { SetProperty(ref _dest, value); }
        }
        private bool _EnableNextButton;
        public bool EnableNextButton
        {
            get { return _EnableNextButton; }
            set { SetProperty(ref _EnableNextButton, value); }
        }
        private bool _ProgressRolling;
        public bool ProgressRolling
        {
            get { return _ProgressRolling; }
            set { SetProperty(ref _ProgressRolling, value); }
        }

        public FileModalDLLViewModel(String source, String dest, string typeElement, string nameElement)
        {
            EnableNextButton = false;
            ProgressRolling = false;
            Source = source;
            SourceByte = null;
            Dest = dest;
            Title = $"Récupération pour {nameElement} de {typeElement}";
        }
        public FileModalDLLViewModel(byte[] source, String dest, string typeElement, string nameElement)
        {
            EnableNextButton = false;
            ProgressRolling = false;
            Source = String.Empty;
            SourceByte = source;
            Dest = dest;
            Title = $"Récupération pour {nameElement} de {typeElement}";
        }

        public FileModalDLLViewModel()
        {
        }
        public async void FinishedTask()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                EnableNextButton = true;
                ProgressRolling = false;
            });
        }
        public async void Start()
        {
            EnableNextButton = false;
            ProgressRolling = true;
            await Task.Run( ()=> StartLoading());
        }
        public async Task StartLoading()
        {
            if (SourceByte != null)
            {
                await service.WriteByte(SourceByte, Dest).ContinueWith(task => FinishedTask());
            }
            else if (Source.Contains("youtube"))
            {
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(Source.Replace("embed/", "watch?v="));
                SourceByte = video.GetBytes();
                await service.WriteByte(SourceByte, Dest).ContinueWith(task => FinishedTask());
            }
            else if (Source.StartsWith("http"))
            {
                await service.DLLFile(Source, Dest).ContinueWith(task => FinishedTask());
            }
            else
            {
                await service.CopyFile(Source, Dest).ContinueWith(task => FinishedTask());
            }
        }
    }
}
