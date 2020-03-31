using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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

                await AprilFools(msg, context);

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

        private async Task AprilFools(SocketUserMessage msg, CommandContext context)
        {
            var channels = await context.Guild.GetChannelsAsync();
            var random = new Random(DateTime.Now.Millisecond);

            Discord.IGuildChannel channel = channels.ElementAt(0);
            var channelFound = false;
            while (!channelFound)
            {
                var rand = random.Next(0, channels.Count);
                channel = channels.ElementAt(rand);
                if (channel.Name.ToUpper().Equals("NIPPON") || channel.Name.ToUpper().Equals("NIPPON-LESSON") || channel.Name.ToUpper().Equals("NIPPON VOICE") 
                    || channel.Name.ToUpper().Equals("GENERAL") || channel.Name.ToUpper().Equals("GAMING 1") || channel.Name.ToUpper().Equals("GAMING 2") || channel.Name.ToUpper().Equals("NSFW-THESTORYOFBOTKUN") 
                    || channel.Name.ToUpper().Equals("TEST1") || channel.Name.ToUpper().Equals("VOICE CHANNELS"))
                {
                    continue;
                }

                if (channel == context.Channel)
                {
                    continue;
                }

                channelFound = true;
            }

            // Delete message 
            await msg.DeleteAsync();

            var newMessage = $"{context.User.Mention} : {msg.Content}";
            var ch = _client.GetChannel(channel.Id) as ISocketMessageChannel;
            await ch.SendMessageAsync(newMessage);
        }
    }
}
