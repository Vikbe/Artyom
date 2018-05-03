using System.Threading.Tasks;
using Artyom.Services;
using DarkSky.Models;
using Discord;
using Discord.Commands;

namespace Artyom.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {

        // Echo message back to user
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);

            await Context.Channel.SendMessageAsync("", false, embed);
        }


        // Google search and return the first result
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


        // Get the weather forecast for certain cities
        [Command("weather")] 
        public async Task GetWeather([Remainder]string location)
        {
            var embed = new EmbedBuilder();
            DarkSkyResponse result;

            switch (location.ToLower())
            {
                case "stockholm":
                   result  = await Weather.StockholmForecast();
                    embed.WithTitle("Stockholm Forecast");
                    break;
                case "kansas city":
                    result = await Weather.KansasCityForeCast();
                    embed.WithTitle("Kansas City Forecast");
                    break;
                default:
                    result = null;
                    break;
            }
           
            if(result != null && result.IsSuccessStatus)
            {
                embed.WithDescription(result.Response.Hourly.Summary);
                embed.WithUrl(result.DataSource);
                await Context.Channel.SendMessageAsync("Here you go", false, embed);
            }

            
        }


        //[Command("Hello")] 
        //public async Task SayHello(IGuild user)
        //{
        //    await Context.Channel.SendMessageAsync("Hello there " + user.Name + "!");
        //}

        //[Command("Kick")]
        //[RequireUserPermission(GuildPermission.KickMembers)]
        //[RequireBotPermission(GuildPermission.KickMembers)]
        //public async Task KickUser(IGuildUser user, string reason = "No reason provided.")
        //{
        //    await user.KickAsync(reason);
        //}

        //[Command("Ban")]
        //[RequireUserPermission(GuildPermission.BanMembers)]
        //[RequireBotPermission(GuildPermission.BanMembers)]
        //public async Task BanUser(IGuildUser user, string reason = "No reason provided.")
        //{
        //    await user.Guild.AddBanAsync(user, 5, reason);
        //}
    }
}
