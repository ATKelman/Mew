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
        private readonly IConfiguration _config;

        public DiscordBot()
        {
            _client = new DiscordSocketClient();
        }

        public async Task IntiateBot(string token)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initiate bot: {ex.Message}", ex.InnerException);
            }
        }
    }
}
