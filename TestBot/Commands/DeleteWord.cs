using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace TestBot
{
    partial class BotCommands
    {
        internal static string DeleteWord(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
        {
            ChatState chatState = chats[input.Message.Chat.Id];
            switch (chatState.CommandStep)
            {
                case 0:
                    chatState.dictItem.ClearWord();
                    chatState.AssignNewCommand("deleteword");
                    chatState.CommandStep++;
                    return "Введите английское слово для удаления";
                case 1:
                    //string wordToDelete = input.Message.Text;
                    string result;
                    int indexToDelete = glossary.FindIndex(item => item.EnWord == input.Message.Text);
                    System.Diagnostics.Debug.WriteLine($"--indexToDelete for '{input.Message.Text}': '{indexToDelete}'");

                    if (indexToDelete >= 0)
                    {
                        glossary.RemoveAt(indexToDelete);
                        result = "deleted";
                    }
                    else
                    {
                        result = "слово не найдено";
                    }
                    chatState.AssignNewCommand(string.Empty);
                    return result;
                default:
                    return "Введите новую команду";
            }
        }

    }
}