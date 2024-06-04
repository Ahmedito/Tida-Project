using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tida.Modules
{
    public class SayModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("say", "Make the bot say something.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Say(string text)
        {
            await RespondAsync(text);
        }

    }
}
