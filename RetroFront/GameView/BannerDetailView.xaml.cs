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
    /// Logique d'interaction pour BannerDetailView.xaml
    /// </summary>
    public partial class BannerDetailView : UserControl
    {
        private double CentralPix = -1;
        public BannerDetailView()
        {
            InitializeComponent();
        }

        private void listicon_Loaded(object sender, RoutedEventArgs e)
        {
            listicon.Focus();
        }

        private void listicon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var splitbyheight = this.ActualHeight / 12;
                CentralPix = splitbyheight * 6;
                int itemheight = (int)splitbyheight;
                var previousmargin = listicon.Margin;
                var nextmargin = previousmargin;
                //nextmargin.Top = -itemheight * listicon.SelectedIndex;
                nextmargin.Top = CentralPix - (listicon.SelectedIndex * itemheight);
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
    }
}
