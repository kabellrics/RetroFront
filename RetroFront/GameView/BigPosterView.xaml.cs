﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetroFront.GameView
{
    /// <summary>
    /// Logique d'interaction pour BigPosterView.xaml
    /// </summary>
    public partial class BigPosterView : UserControl
    {
        private double CentralPix = -1; 
        public BigPosterView()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listicon.Focus();
        }
        private void listicon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var splitbyheight = this.ActualWidth / 8;
                CentralPix = 710;
                int itemwidht = 355;
                var previousmargin = listicon.Margin;
                var nextmargin = previousmargin;
                //nextmargin.Left = -itemwidht * listicon.SelectedIndex;
                nextmargin.Left = CentralPix - (listicon.SelectedIndex * itemwidht);
                var sb = new Storyboard();
                var ta = new ThicknessAnimation();
                ta.BeginTime = new TimeSpan(0);
                ta.SetValue(Storyboard.TargetNameProperty, "listicon");
                Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

                ta.From = previousmargin;
                ta.To = nextmargin;
                ta.Duration = new Duration(TimeSpan.FromMilliseconds(500));

                sb.Children.Add(ta);
                sb.Begin(this);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void listicon_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
    }
}
