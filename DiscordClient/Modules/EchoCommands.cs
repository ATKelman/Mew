using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordClient.Modules
{
    public class EchoCommands : ModuleBase
    {
        [Command("echo")]
        [Summary("Echos a message back to the user.")]
        public async Task Echo([Remainder] string message) =>      
            await ReplyAsync(message);    
    }
}
