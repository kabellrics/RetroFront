using System;

using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class JeuxPage : Page
    {
        public JeuxViewModel ViewModel { get; } = new JeuxViewModel();

        public JeuxPage()
        {
            InitializeComponent();
        }
    }
}
