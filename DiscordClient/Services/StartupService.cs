using DataAccess;
using DataAccess.Modules;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DiscordClient.Services
{
    public static class StartupService
    {
        // Add Services to the container, must be called before the ServiceProvider is built
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddSingleton<DiscordBot>();
            services.AddSingleton<DiscordSocketClient>();
            services.AddSingleton<CommandHandlingService>();
            services.AddSingleton<CommandService>();

            // Data Access 
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<IReminderData, ReminderData>();
        }

        public static async Task StartAsync(IServiceProvider service)
        {
            // Start Bot
            await service.GetRequiredService<DiscordBot>().IntiateBot("");

            // Load Services
            service.GetRequiredService<CommandHandlingService>();
        }
    }
}
