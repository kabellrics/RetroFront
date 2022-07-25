using RetroFront.UWPAdmin.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Boîte de dialogue de contenu, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace RetroFront.UWPAdmin.Views.Modals
{
    public sealed partial class ImgProposalContentDialog : ContentDialog
    {
        public ImgProposalViewModel ViewModel { get; } = new ImgProposalViewModel();
        public ImgProposalContentDialog(ImgProposalViewModel vm)
        {
            ViewModel = vm;
            this.InitializeComponent();
            ViewModel.LoadDataAsync();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ViewModel.Result = string.Empty;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
