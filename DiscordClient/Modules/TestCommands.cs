using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordClient.Modules
{
    public class TestCommands : ModuleBase
    {
        private DiscordSocketClient _client;

        public TestCommands(DiscordSocketClient client)
        {
            _client = client;
        }

        [Command("Disconnect")]
        [Summary("Logout the DiscordSocketClient")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Disconnect() =>
            await _client.LogoutAsync();
    }
}
