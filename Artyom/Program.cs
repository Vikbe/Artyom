using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Artyom
{
    class Program
    {
        DiscordSocketClient m_client;
        CommandHandler m_handler;

        static void Main(string[] args)
        {
            new Program()
                .StartAsync()
                .GetAwaiter()
                .GetResult();
        }

        public async Task StartAsync()
        {
            if (Config.bot.Token == "" || Config.bot.Token == null)
                return;

            m_client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            m_client.Log += Log;

            await m_client.LoginAsync(TokenType.Bot, Config.bot.Token);
            await m_client.StartAsync();

            m_handler = new CommandHandler();
            await m_handler.InitializeAsync(m_client);
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
           Console.WriteLine(msg.Message);
            return Task.CompletedTask; 
        }

     
    }
}
