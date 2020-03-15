using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordClient
{
    public class DiscordBot
    {
        private readonly DiscordSocketClient _client;

        public DiscordBot()
        {
            _client = new DiscordSocketClient();
        }

        public async Task IntiateBot(string token)
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initiate bot: {ex.Message}", ex.InnerException);
            }
        }
    }
}
