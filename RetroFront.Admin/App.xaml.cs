using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RetroFront.Admin.Dialogs;
using RetroFront.Models;
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

namespace RetroFront.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IHost host;
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            host = Host.CreateDefaultBuilder()
                   .ConfigureServices((context, services) =>
                   {
                       ConfigureServices(context.Configuration, services);
                   })
                   .Build();
            ServiceProvider = host.Services;
        }
        private void ConfigureServices(IConfiguration configuration,
        IServiceCollection services)
        {
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddSingleton<IFileJSONService, FileJSONService>();
            services.AddSingleton<IMainService, MainService>();
            services.AddSingleton<IRetroarchService, RetroarchService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IEmulateurService, EmulateurService>();
            services.AddSingleton<IGameService, GameService>();

            services.AddSingleton<MainPageViewModel>();

            services.AddTransient(typeof(MainWindow));
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
