using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordClient
{
    public class DiscordBot
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;

        public DiscordBot(IServiceProvider services)
        {
            _services = services;
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _commands = _services.GetRequiredService<CommandService>();
            _config = _services.GetRequiredService<IConfiguration>();
        }

        public async Task IntiateBot()
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, _config["Token"]);
                await _client.StartAsync();

                // Register modules - MUST BE PUBLIC AND INHERIT MODULE BASE
                await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initiate bot: {ex.Message}", ex.InnerException);
            }
        }
    }
}
