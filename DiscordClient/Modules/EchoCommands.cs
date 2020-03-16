using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordClient.Modules
{
    public class EchoCommands : Discord.Commands.ModuleBase
    {
        private readonly IServiceProvider _services;

        public EchoCommands(IServiceProvider services)
        {
            _services = services;
        }

        [Command]
        public async Task Echo([Remainder] string message)
        {
            await ReplyAsync(message);
        }
    }
}
