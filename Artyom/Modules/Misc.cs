using System.Threading.Tasks;
using Disco.Services;
using Discord;
using Discord.Commands;

namespace Artyom.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0)); 
            

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("google")]
        public async Task Google([Remainder]string query)
        {
            var results = await GoogleSearch.Search(query);
            var embed = new EmbedBuilder();
            embed.WithTitle(results[0].Title);
            embed.WithDescription(results[0].Snippet);
            embed.Url = results[0].Link;

            await Context.Channel.SendMessageAsync("This was the first result: ", false, embed);
        }
    }
}
