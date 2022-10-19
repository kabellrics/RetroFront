using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"Log/{DateTime.Now.ToString("dd.MM.yyyy")}.txt")
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
              .CreateLogger();
            Log.Information("----------------------------------------------------------------------------------");
            Log.Information("------------------------Lancement d'un jeu grâce au Launcher----------------------");
            Log.Information("----------------------------------------------------------------------------------");
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                //throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureWebHost(config =>
                {
                    config.UseUrls("http://localhost:34322");

                }).UseWindowsService();
    }
}
