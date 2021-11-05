using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace TestBot
{
    partial class BotCommands
    {
        internal static string TrainingEng(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
        {
            Random rand = new();
            ChatState chatState = chats[input.Message.Chat.Id];
            string answer;
            int index = rand.Next(glossary.Count);

            switch (chatState.CommandStep)
            {
                case 0:     // найти случайное слово и попросить первод
                    chatState.AssignNewCommand("trainingeng");
                    chatState.dictItem.EnWord = glossary[index].EnWord;
                    chatState.dictItem.RuWord = glossary[index].RuWord;
                    chatState.CommandStep++;
                    return $"введите перевод для: {chatState.dictItem.EnWord}";
                case 1:      // проверить перевод
                    if (chatState.dictItem.RuWord.ToLower() == input.Message.Text.Trim().ToLower())
                    {
                        answer = $"Правильно. Следующее слово:\n";
                    }
                    else
                    {
                        answer = $"Нет. Правильный ответ: {chatState.dictItem.RuWord} \nСледующее слово:\n";
                    }
                    chatState.dictItem.EnWord = glossary[index].EnWord;
                    chatState.dictItem.RuWord = glossary[index].RuWord;
                    return $"{answer} {chatState.dictItem.EnWord}";
                default:
                    return "что-то непонятное слоучилось при тестировании..";
            }
        }
    }
}