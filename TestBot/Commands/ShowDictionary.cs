using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;


namespace TestBot
{
    partial class BotCommands
    {
        internal static string ShowDictionary(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
        {
            if (glossary.Count == 0)
                return "словарь пуст";

            StringBuilder answer = new();
            foreach (DictionaryItem item in glossary)
            {
                answer.Append(item.ShowWWord() + "\n");
            }
            chats[input.Message.Chat.Id].AssignNewCommand(string.Empty);
            return answer.ToString();
        }

    }
}


