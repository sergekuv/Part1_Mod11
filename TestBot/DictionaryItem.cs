using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot
{
    class DictionaryItem
    {
        public long ChatId { get; set; }
        public string RuWord { get; set; }
        public string EnWord { get; set; }
        public string Subject { get; set; }

        internal DictionaryItem(long chatId, string rusWord, string engWord, string subject)
        {
            this.ChatId = chatId;
            this.RuWord = rusWord;
            this.EnWord = engWord;
            this.Subject = subject;
        }
        internal DictionaryItem(long chatId)
        {
            this.ChatId = chatId;
            this.RuWord = this.EnWord = this.Subject = string.Empty;
        }
        internal DictionaryItem()
        {
            this.ChatId = default;
            this.RuWord = this.EnWord = this.Subject = string.Empty;
        }

        internal DictionaryItem(ChatState chatState)
        {
            this.ChatId = chatState.dictItem.ChatId;
            this.EnWord = chatState.dictItem.EnWord;
            this.RuWord = chatState.dictItem.RuWord;
            this.Subject = chatState.dictItem.Subject;
        }

        internal void ClearWord() => this.RuWord = this.EnWord = this.Subject = string.Empty;
        internal void AddEnWord(string input) => this.EnWord = input;
        internal void AddRuWord(string input) => this.RuWord = input;
        internal void AddSubject(string input) => this.Subject = input;
        internal void AddChatId(long input) => this.ChatId = input;

        internal string ShowWWord() => this.EnWord + " - " + this.RuWord + " (" + this.Subject + ")";
    }

}
