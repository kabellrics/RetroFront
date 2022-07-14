using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RetroFront.Admin.Dialogs.ViewModel
{
    public class DllModalViewModel : ViewModelBase
    {
        private ICommand _yesCommand;
        private String _URLfile;
        public String URLfile
        {
            get { return _URLfile; }
            set { _URLfile = value; RaisePropertyChanged(); }
        }
        private byte[] _bytefile;
        public byte[] Bytefile
        {
            get { return _bytefile; }
            set { _bytefile = value; RaisePropertyChanged(); }
        }
        private String _destinationfile;
        public String DestinationFile
        {
            get { return _destinationfile; }
            set { _destinationfile = value; RaisePropertyChanged(); }
        }
        private String _resultText;
        public String ResultText
        {
            get { return _resultText; }
            set { _resultText = value; RaisePropertyChanged(); }
        }
        private String _targetname;
        public String TargetName
        {
            get { return _targetname; }
            set { _targetname = value; RaisePropertyChanged(); }
        }
        private String _typetodll;
        public String TypeToDLL
        {
            get { return _typetodll; }
            set { _typetodll = value; RaisePropertyChanged(); }
        }
        private int _PercentageCompletion;
        public int PercentageCompletion
        {
            get { return _PercentageCompletion; }
            set { _PercentageCompletion = value; RaisePropertyChanged(); }
        }
        private bool _EnableOKButton;
        public bool EnableOKButton
        {
            get { return _EnableOKButton; }
            set { _EnableOKButton = value; RaisePropertyChanged(); }
        }
        private bool _IsIndeterminate;
        public bool IsIndeterminate
        {
            get { return _IsIndeterminate; }
            set { _IsIndeterminate = value; RaisePropertyChanged(); }
        }

        public DllModalViewModel(string uritodll, string destifile,string targetname, string typetodll)
        {
            URLfile = uritodll;
            DestinationFile = destifile;
            TargetName = targetname;
            TypeToDLL = typetodll;
            EnableOKButton = false;
            IsIndeterminate = false;
            downloadFile();
        }

        public DllModalViewModel(byte[] bytetoWrite, string destifile,string targetname, string typetodll)
        {
            Bytefile = bytetoWrite;
            DestinationFile = destifile;
            TargetName = targetname;
            TypeToDLL = typetodll;
            EnableOKButton = false;
            IsIndeterminate = true;
            downloadFilebyte();
        }

        private void downloadFilebyte()
        {
            File.WriteAllBytesAsync(TargetName, Bytefile).ContinueWith(task=> 
            {
                ResultText = "File succesfully downloaded";
                EnableOKButton = true;
            });
        }
        private void downloadFile()
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(URLfile), DestinationFile);
            }
        }/// <summary>
         ///  Show the progress of the download in a progressbar
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // In case you don't have a progressBar Log the value instead 
            // Console.WriteLine(e.ProgressPercentage);
            PercentageCompletion = e.ProgressPercentage;
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //PercentageCompletion = 0;
            EnableOKButton = true;
            if (e.Cancelled)
            {
                ResultText = "The download has been cancelled";
                return;
            }

            if (e.Error != null) // We have an error! Retry a few times, then abort.
            {
                ResultText = "An error ocurred while trying to download file";
                return;
            }
            ResultText = "File succesfully downloaded";
        }
        public void CloseDialogWithResult(Window dialog, bool result)
        {
            if (dialog != null)
                dialog.DialogResult = result;
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
            CloseDialogWithResult(parameter as Window, true);
        }
    }
}
