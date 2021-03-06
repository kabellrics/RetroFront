﻿using Microsoft.Extensions.Configuration;
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

namespace RetroFront
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
            services.AddScoped<INavigationService,NavigationService>(ServiceProvider =>
            {
                var navigationservice = new NavigationService();
                navigationservice.Configure("Systeme",new Uri("../SystemeWindow.xaml",UriKind.Relative));
                navigationservice.Configure("Games", new Uri("../GameWindow.xaml", UriKind.Relative));
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

            base.OnExit(e);
        }
    }
}
