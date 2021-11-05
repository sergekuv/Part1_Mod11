using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace TestBot
{
    partial class BotCommands
    {
        internal static string StopTraining(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
        {
            chats[input.Message.Chat.Id].AssignNewCommand(string.Empty);
            return "training stopped";
        }
    }
}
