﻿using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordClient.Modules
{
    public class ReminderCommands : ModuleBase
    {
        [Command("RemindMe")]
        public async Task RemindMe([Remainder] string message)
        {
            await ReplyAsync(message);
        }
    }
}