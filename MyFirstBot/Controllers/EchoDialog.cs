using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace MyFirstBot
{

    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        private int count;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)
        {
            var message = await argument;
            if (message.Text == "reset")
            {
                PromptDialog.Confirm(context, AfterResetAsync, "reset well ?", "well not know !");
            }
            else
            {
                await context.PostAsync(string.Format("{0}: well {1}", this.count++, message.Text));
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("well reset");
            }
            else
            {
                await context.PostAsync("not reset well");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}