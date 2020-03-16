using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordClient.Services;
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

        public DiscordBot(IServiceProvider services)
        {
            _services = services;
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _commands = _services.GetRequiredService<CommandService>();
        }

        public async Task IntiateBot(string token)
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, token);
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
