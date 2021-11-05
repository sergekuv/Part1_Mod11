using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System.Reflection;

namespace TestBot
{
    /* Список комманд для копирования в бота (при добавлении команды не забудьте добавить ее в список команд бота, больше ничего делать не нужно
       Но помните про сигнатуру - у всех комманд она должна быть одинаковой
    addword - добавление слова в словарь
    deleteword - удаление слова
    trainingeng - обучение в русско-английском режиме 
    trainingrus - обучение в англо-русском режиме
    stoptraining - останов режима обучения
    showdictionary - вывод списка слов словаря
    */
    /// <summary>
    /// ВСЕ МЕТОДЫ ЭТОГО КЛАССА, КОТОРЫЕ ПЛАНИРУЕТСЯ ИСПОЛЬЗОВАТЬ ДЛЯ ОБРАБОТКИ КОММАНД, ДОЛЖНЫ УДОВЛЕТВОРЯТЬ ШАБЛОНУ
    /// string  CommandNamer(Update input, Dictionary<long, ChatState> chats, List<DictionaryItem> glossary)
    /// </summary>
    partial class BotCommands
    {
        internal static Dictionary<string, MethodInfo> commands = new();

        static BotCommands()
        {
            System.Diagnostics.Debug.WriteLine("Starting BotCommand static ctor");
            foreach (var method in typeof(BotCommands).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                commands.Add(method.Name.ToLower(), method);
                System.Diagnostics.Debug.WriteLine("KEY: " + method.Name.ToLower());
            }
        }

    }
}
