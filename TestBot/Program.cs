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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-- press Enter to finish --");
            BotMessageLogic.LoadGlossary();
            BotWorker bot = new();
            bot.InitAsync().Wait();
            Console.ReadLine();
            BotMessageLogic.SaveGlossary();

        }
    }
}
