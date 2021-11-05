using System.Collections.Generic;
using Telegram.Bot.Types;

using System;

namespace TestBot
{
    partial class BotCommands
    {
        internal static string TrainingRus(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
        {
            Random rand = new();
            ChatState chatState = chats[input.Message.Chat.Id];
            string answer;
            int index = rand.Next(glossary.Count);

            switch (chatState.CommandStep)
            {
                case 0:     // найти случайное слово и попросить первод
                    chatState.AssignNewCommand("trainingrus");
                    chatState.dictItem.EnWord = glossary[index].EnWord;
                    chatState.dictItem.RuWord = glossary[index].RuWord;
                    chatState.CommandStep++;
                    return $"введите перевод для: {chatState.dictItem.RuWord}";
                case 1:      // проверить перевод
                    if (chatState.dictItem.EnWord.ToLower() == input.Message.Text.Trim().ToLower())
                    {
                        answer = $"Правильно. Следующее слово:\n";
                    }
                    else
                    {
                        answer = $"Нет. Правильный ответ: {chatState.dictItem.EnWord} \nСледующее слово:\n";
                    }
                    chatState.dictItem.EnWord = glossary[index].EnWord;
                    chatState.dictItem.RuWord = glossary[index].RuWord;
                    return $"{answer} {chatState.dictItem.RuWord}";
                default:
                    return "что-то непонятное слоучилось при тестировании..";
            }
        }
    }
}
