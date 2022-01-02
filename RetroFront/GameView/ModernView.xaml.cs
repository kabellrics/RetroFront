using System;
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
    /// Logique d'interaction pour ModernView.xaml
    /// </summary>
    public partial class ModernView : UserControl
    {
        public ModernView()
        {
            InitializeComponent();
        }
        private double itemwidht = 400;
        private double CentralPix = -1;
        private void listicon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CentralPix = 200;
                var previousmargin = listicon.Margin;
                var nextmargin = previousmargin;
                //nextmargin.Left = CentralPix - (listicon.SelectedIndex * itemwidht);

                double divbyFive = listicon.SelectedIndex / 3;
                int ligneindex = Convert.ToInt32(Math.Floor(divbyFive));
                nextmargin.Left = CentralPix - (ligneindex * (itemwidht + 50));

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
            listicon.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listicon.Focus();
        }

        private void WrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var wrap = sender as WrapPanel;
                wrap.ItemHeight = wrap.ActualHeight / 3.3;
                wrap.ItemWidth = wrap.ItemHeight * 2;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
