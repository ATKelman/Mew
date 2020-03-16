using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordClient.Services
{
    public class CommandHandler
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services)
        {
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            // register modules - public & inherit ModuleBase
            _ = await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task MessageReceivedAsync(SocketMessage e)
        {
            try
            {
                SocketUserMessage msg = (SocketUserMessage)e;
                if (msg == null) return;

                CommandContext context = new CommandContext(_client, msg);
                if (context.User.IsBot) return;

                int argPos = 0;
                if (msg.HasCharPrefix('!', ref argPos))
                {
                    var result = await _commands.ExecuteAsync(context, argPos, null);

                    if (!result.IsSuccess)
                        await context.Channel.SendMessageAsync(result.ErrorReason.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to handle message: {ex.Message}", ex.InnerException);
            }
        }
    }
}
