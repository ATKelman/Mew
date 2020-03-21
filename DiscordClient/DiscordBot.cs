using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordClient
{
    public class DiscordBot
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public DiscordBot(IServiceProvider services)
        {
            _services = services;
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _commands = _services.GetRequiredService<CommandService>();
            _config = _services.GetRequiredService<IConfiguration>();
            _logger = services.GetRequiredService<ILogger<DiscordBot>>();
        }

        public async Task IntiateBot()
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, _config["Token"]);
                await _client.StartAsync();

                _client.LoggedOut += OnClientLoggedOut;
                _client.Disconnected += OnClientDisconnected;


                // Register modules - MUST BE PUBLIC AND INHERIT MODULE BASE
                await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initiate bot: {ex.Message}", ex.InnerException);
            }
        }

        private async Task OnClientLoggedOut()
        {
            await TryToReconnect();
        }

        private async Task OnClientDisconnected(Exception arg)
        {
            await TryToReconnect();
        }

        private async Task TryToReconnect()
        {
            try
            {
                _logger.LogInformation($"Attemping to Reconnect...");

                await IntiateBot();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Reconnection attempt failed - [{ex.Message}]");

                Thread.Sleep(2000);

                await TryToReconnect();
            }
        }
    }
}
