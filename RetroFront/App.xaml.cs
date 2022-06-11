using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RetroFront.Converter;
using RetroFront.Services.Implementation;
using RetroFront.Services.Interface;
using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SharpDX.XInput;
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using RetroFront.Models.Messenger;
using GalaSoft.MvvmLight.Messaging;

namespace RetroFront
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        public static IServiceProvider ServiceProvider { get; private set; }
        DispatcherTimer _timer = new DispatcherTimer();
        private Controller _controller;
        public App()
        {
            host = Host.CreateDefaultBuilder()
                   .ConfigureServices((context, services) =>
                   {
                       ConfigureServices(context.Configuration, services);
                   })
                   .Build();
            ServiceProvider = host.Services;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
            //_timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            DisplayControllerInformation();
        }

        private void ConfigureServices(IConfiguration configuration,
        IServiceCollection services)
        {
            services.AddScoped<INavigationService, NavigationService>(ServiceProvider =>
             {
                 var navigationservice = new NavigationService();
                 navigationservice.Configure("Systeme", new Uri("../SystemeWindow.xaml", UriKind.Relative));
                 navigationservice.Configure("Games", new Uri("../GameWindow.xaml", UriKind.Relative));
                 navigationservice.Configure("GameStart", new Uri("../GameStartWindows.xaml", UriKind.Relative));
                 return navigationservice;
             });

            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddSingleton<IFileJSONService, FileJSONService>();
            //services.AddSingleton<IRetroarchService, RetroarchService>();
            //services.AddSingleton<IDialogService, DialogService>();
            //services.AddSingleton<IEmulateurService, EmulateurService>();
            //services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IThemeService, ThemeService>();
            //services.AddSingleton<ISteamService, SteamService>();
            //services.AddSingleton<ISteamGridDBService, SteamGridDBService>();
            services.AddSingleton<IEnumService, EnumService>();

            //Add Pages ViewModel
            services.AddSingleton<FrontSysViewModel>();
            services.AddSingleton<FrontGameViewModel>();
            services.AddSingleton<LaunchFrontViewModel>();
            services.AddSingleton<StartGameViewModel>();

            services.AddTransient(typeof(SystemeWindow));
            services.AddTransient(typeof(GameWindow));
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            #region Commentaires
            await host.StartAsync();

            //var mainWindow = host.Services.GetRequiredService<MainWindow>();
            //mainWindow.Show();

            var dbcontext = host.Services.GetRequiredService<IDatabaseService>();
            dbcontext.SQLIteContext.Database.EnsureCreated();
            base.OnStartup(e);
            #endregion
            _controller = new Controller(UserIndex.One);
            //if (_controller.IsConnected) return;
            _timer.Start();
            //await host.StartAsync();
            //var dbcontext = host.Services.GetRequiredService<IDatabaseService>();
            //dbcontext.SQLIteContext.Database.EnsureCreated();
            ////var navigationService =
            ////    ServiceProvider.GetRequiredService<INavigationService>();
            ////await navigationService.ShowAsync("Systeme");
            //base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }
            _controller = null;
            base.OnExit(e);
        }
        void DisplayControllerInformation()
        {
            try
            {
                if (ApplicationIsActivated())
                {
                    var state = _controller.GetState();
                    if (state.Gamepad.Buttons != GamepadButtonFlags.None)
                    {
                        //MessageBox.Show(string.Format("{0}", state.Gamepad.Buttons));
                        var inputSimulator = new InputSimulator();
                        switch (state.Gamepad.Buttons)
                        {
                            case GamepadButtonFlags.None:
                                break;
                            case GamepadButtonFlags.DPadUp:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.UP);
                                break;
                            case GamepadButtonFlags.DPadDown:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
                                break;
                            case GamepadButtonFlags.DPadLeft:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LEFT);
                                break;
                            case GamepadButtonFlags.DPadRight:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
                                break;
                            case GamepadButtonFlags.Start:
                                break;
                            case GamepadButtonFlags.Back:
                                break;
                            case GamepadButtonFlags.LeftThumb:
                                break;
                            case GamepadButtonFlags.RightThumb:
                                break;
                            case GamepadButtonFlags.LeftShoulder:
                                break;
                            case GamepadButtonFlags.RightShoulder:
                                break;
                            case GamepadButtonFlags.A:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                                break;
                            case GamepadButtonFlags.B:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
                                break;
                            case GamepadButtonFlags.X:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.SPACE);
                                break;
                            case GamepadButtonFlags.Y:
                                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.SPACE);
                                break;
                            default:
                                break;
                        }
                    }
                    //else if (state.Gamepad.Buttons == (GamepadButtonFlags.Back | GamepadButtonFlags.Start))
                    //{
                    //    Messenger.Default.Send(new KillGameMessage());
                    //}
                }
                else
                {
                    var state = _controller.GetState();
                    if (state.Gamepad.Buttons == (GamepadButtonFlags.Back | GamepadButtonFlags.Start))
                    {
                        Messenger.Default.Send(new KillGameMessage());
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }

        }
        public static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
    }
}
