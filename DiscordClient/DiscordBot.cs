﻿using Discord;
using Discord.WebSocket;
using DiscordClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordClient
{
    public class DiscordBot
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public DiscordBot(IServiceProvider services)
        {
            _client = new DiscordSocketClient();

            _services = services;
        }

        public async Task IntiateBot(string token)
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();

                // Start command handler 
                await _services.GetRequiredService<CommandHandler>().InitalizeAsync();

                // Keep the program running until closed.
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initiate bot: {ex.Message}", ex.InnerException);
            }
        }
    }
}
