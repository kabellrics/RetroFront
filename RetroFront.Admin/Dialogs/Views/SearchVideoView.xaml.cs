using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetroFront.Admin.Dialogs.Views
{
    /// <summary>
    /// Logique d'interaction pour SearchVideoView.xaml
    /// </summary>
    public partial class SearchVideoView : UserControl
    {
        public SearchVideoView()
        {
            InitializeComponent();
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}
