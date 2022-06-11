using System;

using RetroFront.UWPClient.ViewModels;

using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPClient.Views
{
    public sealed partial class ParametrePage : Page
    {
        public ParametreViewModel ViewModel { get; } = new ParametreViewModel();

        public ParametrePage()
        {
            InitializeComponent();
        }
    }
}
