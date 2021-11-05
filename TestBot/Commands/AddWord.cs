using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace TestBot
{
    partial class BotCommands
    {
        internal static string AddWord(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
        {
            ChatState chatState = chats[input.Message.Chat.Id];
            switch (chatState.CommandStep)
            {
                case 0:     //Первая стадия выполнения команды: команда получена
                    chatState.dictItem.ClearWord();
                    chatState.AssignNewCommand("addword");
                    chatState.CommandStep++;
                    return "Введите английское слово";
                case 1:
                    chatState.dictItem.AddEnWord(input.Message.Text);
                    chatState.CommandStep++;
                    return "Введите русский перевод";
                case 2:
                    chatState.dictItem.AddRuWord(input.Message.Text);
                    chatState.CommandStep++;
                    return "Введите тему, к которой относитсся слово";
                case 3:
                    chatState.dictItem.AddSubject(input.Message.Text);
                    chatState.dictItem.AddChatId(input.Message.Chat.Id);
                    DictionaryItem itemToAdd = new(chatState);
                    glossary.Add(itemToAdd);
                    chatState.CommandStep++;
                    chatState.AssignNewCommand(string.Empty);
                    return $"Слово добавлено: {chatState.dictItem.ShowWWord()}";
                default:
                    return "Введите новую команду";
            }
        }
    }
}
