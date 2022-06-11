using System;

using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class QuitterPage : Page
    {
        public QuitterViewModel ViewModel { get; } = new QuitterViewModel();

        public QuitterPage()
        {
            InitializeComponent();
        }
    }
}
