using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
//using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Part1_Mod11
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient botClient = new TelegramBotClient(BotCredentials.BotToken);

            //botClient.StartReceiving(new DefaultUpdateHandler( )

            Console.WriteLine("-- end --");
        }
    }
}

// Перед рефакторинг