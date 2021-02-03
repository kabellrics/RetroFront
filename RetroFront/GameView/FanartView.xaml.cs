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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetroFront.GameView
{
    /// <summary>
    /// Logique d'interaction pour FanartView.xaml
    /// </summary>
    public partial class FanartView : UserControl
    {
        public FanartView()
        {
            InitializeComponent();
            flip.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            flip.Focus();
        }
    }
}
