using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TestBot
{
    static class BotMessageLogic
    {
        static readonly string glossaryPath = "glossary.csv";
        private static Dictionary<long, ChatState> chats = new();
        private static List<DictionaryItem> glossary  = new();      // здесь, вероятно, подошел бы Dictionary, но нам же нужно искать и по русскому,
                                                                    // и по английскому написанию слова? Какая структура будет тут оптимальной?

        public static void LoadGlossary()       // Это можно очень по-разному реализовывать. Например, сделать свой словарь для каждого клиента
        {
            try
            {
                glossary.Clear();
                using FileStream fileStream = System.IO.File.OpenRead(glossaryPath);
                using StreamReader streamReader = new (fileStream);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] words = line.Split("\t");
                    if (words.Length == 4)
                    {
                        DictionaryItem article = new(Int64.Parse(words[0]), words[1], words[2], words[3]);
                        glossary.Add(article);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to load glossary from {glossaryPath}: {ex.Message}");
            }
        }
        public static void SaveGlossary()
        {
            try
            {
                using TextWriter writer = new StreamWriter(glossaryPath);
                foreach (DictionaryItem article in glossary)
                {
                    writer.WriteLine($"{article.ChatId}\t{article.RuWord}\t{article.EnWord}\t{article.Subject}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save Dictionary to : {ex.Message}");
            }
        }

        internal static string GetAnswer(Update update)
        {
            if (!chats.ContainsKey(update.Message.Chat.Id))         // Если чата еще нет в списке - добавляем
            {
                chats.Add(update.Message.Chat.Id, new ChatState(commandName: string.Empty, commandStep:0));
                System.Diagnostics.Debug.WriteLine("Adding a new chat");
            }

            object[] parameters = new object[3]; // Наверное, правильнее было бы делать это при помощи делегатов? Нужно посмотреть на досуге..
            parameters[0] = update;
            parameters[1] = chats;
            parameters[2] = glossary;

            string msg = update.Message.Text.Trim();       

            if (msg[0] == '/' && BotCommands.commands.ContainsKey(msg[1..]))       // Это значит, что пришла комманда
            {
                System.Diagnostics.Debug.WriteLine("command found in dictionary: " + msg);
                chats[update.Message.Chat.Id].AssignNewCommand(commandName: msg[1..]);

                return (string)BotCommands.commands[msg[1..]].Invoke(null, parameters );
            }
            else if (chats[update.Message.Chat.Id].AssignedCommand() != string.Empty)     // в данном чате идет процесс обработки команды - 
            {
                return (string)BotCommands.commands[chats[update.Message.Chat.Id].AssignedCommand()].Invoke(null, parameters);
            }
            else
            {
                return "не удалось распознать команду: " + msg + "\nсм. список комманд";
            }
        }

    }
}
