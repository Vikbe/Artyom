using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Artyom
{
    class CommandHandler
    {
        DiscordSocketClient m_client;
        CommandService m_service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            m_client = client;
            m_service = new CommandService();

            await m_service.AddModulesAsync(Assembly.GetEntryAssembly());
            m_client.MessageReceived += handleCommandAsync;
        }

        private async Task handleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
                return;

            var context = new SocketCommandContext(m_client, msg);
            int argPos = 0;

            if (msg.HasStringPrefix(Config.bot.CmdPrefix, ref argPos) ||  
                msg.HasMentionPrefix(m_client.CurrentUser, ref argPos))
            {
                var result = await m_service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
