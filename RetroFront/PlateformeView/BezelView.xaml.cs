﻿using System;
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

namespace RetroFront.PlateformeView
{
    /// <summary>
    /// Logique d'interaction pour BezelView.xaml
    /// </summary>
    public partial class BezelView : UserControl
    {
        public BezelView()
        {
            InitializeComponent();
            listicon.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listicon.Focus();
        }
    }
}
