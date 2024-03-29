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

namespace RetroFront.Admin.Dialogs.Views
{
    /// <summary>
    /// Logique d'interaction pour GameDetailView.xaml
    /// </summary>
    public partial class GameDetailView : UserControl
    {
        public GameDetailView()
        {
            InitializeComponent();
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                videoplayer.Pause();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                videoplayer.Play();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
