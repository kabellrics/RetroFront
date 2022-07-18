using System;

using RetroFront.UWPAdmin.ViewModels;

using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPAdmin.Views
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel { get; } = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
        }
    }
}
