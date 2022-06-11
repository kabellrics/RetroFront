using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RetroFront.UWPClient.Services;
using RetroFront.UWPClient.Core.APIService;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using RetroFront.UWPClient.ViewModels;
using RetroFront.UWPClient.Core;

namespace RetroFront.UWPClient
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;
            this.FocusVisualKind = FocusVisualKind.Reveal;
            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<ApiServiceAPI>()
                .AddSingleton<ComputerServiceAPI>()
                .AddSingleton<EmulatorServiceAPI>()
                .AddSingleton<GameServiceAPI>()
                .AddSingleton<ImgServiceAPI>()
                .AddSingleton<SettingsServiceAPI>()
                .AddSingleton<SystemeServiceAPI>()
                .AddSingleton<ThemeServiceAPI>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<PlateformeViewModel>()
                .BuildServiceProvider());
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.HomePage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
