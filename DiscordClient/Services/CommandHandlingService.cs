using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordClient.Services
{
    public class CommandHandlingService
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandlingService(IServiceProvider services)
        {
            _services = services;
            _commands = _services.GetRequiredService<CommandService>();
            _client = _services.GetRequiredService<DiscordSocketClient>();

            _client.MessageReceived += MessageReceivedAsync;
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
                    var result = await _commands.ExecuteAsync(context, argPos, _services);

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
