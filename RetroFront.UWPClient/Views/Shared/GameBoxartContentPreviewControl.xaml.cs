using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace RetroFront.UWPClient.Views.Shared
{
    public sealed partial class GameBoxartContentPreviewControl : UserControl
    {
        ButtonBase clickButtonPart = null;
        public const string ClickButtonTemplatePartName = "btElement";
        public event EventHandler Click;
        public GameRom Game
        {
            get { return (GameRom)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }
        protected override void OnApplyTemplate()
        {
            // In case the template changes, you want to stop listening to the
            // old button's Click event.
            if (clickButtonPart != null)
            {
                clickButtonPart.Click -= ClickForwarder;
                clickButtonPart = null;
            }

            // Find the template child with the special name. It can be any kind
            // of ButtonBase in this example.
            clickButtonPart = GetTemplateChild(ClickButtonTemplatePartName) as ButtonBase;

            // Add a handler to its Click event that simply forwards it on to our
            // Click event.
            if (clickButtonPart != null)
            {
                clickButtonPart.Click += ClickForwarder;
            }
        }

        private void ClickForwarder(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Click?.Invoke(this, null);
        }
        // Using a DependencyProperty as the backing store for Game.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameProperty =
            DependencyProperty.Register("Game", typeof(GameRom), typeof(GameBoxartContentPreviewControl), new PropertyMetadata(0));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(GameBoxartContentPreviewControl),
            new PropertyMetadata(null, OnCommandPropertyChanged));
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object)
                                                                                   , typeof(GameRom), new PropertyMetadata(null));
        private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GameBoxartContentPreviewControl control = d as GameBoxartContentPreviewControl;
            if (control == null) return;

            control.KeyUp += Control_KeyUp;

            //control.MouseLeftButtonDown -= OnControlLeftClick;
            //control.MouseLeftButtonDown += OnControlLeftClick;
        }

        private static void Control_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            GameBoxartContentPreviewControl control = sender as GameBoxartContentPreviewControl;
            if (control == null || control.Command == null) return;

            ICommand command = control.Command;
            if (command.CanExecute(control.CommandParameter))
            {
                if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Space || e.Key == Windows.System.VirtualKey.GamepadA)
                    command.Execute(control.CommandParameter);
            }
        }

        //private static void OnControlLeftClick(object sender, MouseButtonEventArgs e)
        //{
        //    GameBoxartContentPreviewControl control = sender as GameBoxartContentPreviewControl;
        //    if (control == null || control.Command == null) return;

        //    ICommand command = control.Command;

        //    if (command.CanExecute(null))
        //        command.Execute(null);
        //}

        public GameBoxartContentPreviewControl()
        {
            this.InitializeComponent();
            player.MediaPlayer.IsMuted = true;
            player.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private async void MediaPlayer_MediaEnded(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                boxartImg.Visibility = Visibility.Visible;
                player.MediaPlayer.Pause();
            });
        }

        private async void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                boxartImg.Visibility = Visibility.Collapsed;
                player.MediaPlayer.Play();
            });
        }

        private async void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                boxartImg.Visibility = Visibility.Visible;
                player.MediaPlayer.Pause();
            });
        }
    }
}
