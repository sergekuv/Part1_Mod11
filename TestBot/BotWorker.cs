using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace TestBot
{
    internal class BotWorker
    {
        private const string botToken = "2072591492:AAHQZeXbiPR7WT576F0UgHn3qb17Kc6hyL4";   //Обсудить на вебинаре, зачем создавать класс ради одной строковой константы
                                                                                            //На мой взгляд, если уж так делать, то правильнее читать токен из конфига..
        private ITelegramBotClient botClient;    // Обсудить на вебинаре: почему в приведенном в материалах образце использовался интерфейс а не класс? 
        //private BotMessageLogic logic;

        internal BotWorker()
        {
            botClient = new TelegramBotClient (botToken);

        }

        internal async Task InitAsync()
        {
            User me = await botClient.GetMeAsync();
            Console.WriteLine($"I'm {me.Id} and my name is {me.FirstName} {me.LastName}");

            using CancellationTokenSource cts = new();

            botClient.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync), cts.Token);
            Console.WriteLine($"Start listening for @{me.Username}");
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;

            Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}");

            await botClient.SendTextMessageAsync(chatId: chatId, text: BotMessageLogic.GetAnswer(update));
        }


    }
}
