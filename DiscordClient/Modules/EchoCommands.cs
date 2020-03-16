using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordClient.Modules
{
    public class EchoCommands : ModuleBase
    {
        private readonly IServiceProvider _services;

        public EchoCommands(IServiceProvider services)
        {
            _services = services;
        }

        [Command("echo")]
        public async Task Echo([Remainder] string message)
        {
            await ReplyAsync(message);
        }
    }
}
