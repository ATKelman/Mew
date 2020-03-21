using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebInterface
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Initiate Logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/Mew.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                // Start dlls
                await DiscordClient.Services.StartupService.StartAsync(host.Services);

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, $"Failed to Initiate - [{ex.Message}]");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
