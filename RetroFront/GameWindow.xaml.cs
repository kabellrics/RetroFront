using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RetroFront
{
    /// <summary>
    /// Logique d'interaction pour GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Page
    {
        public GameWindow()
        {
            InitializeComponent();
            this.Focus();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            contentpres.Focus();
        }
    }
}
