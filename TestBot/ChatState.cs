using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot
{
    
    internal class ChatState
    {
        public string CommandName { get; set; }
        public int CommandStep { get; set; }

        internal DictionaryItem dictItem;
        internal ChatState(string commandName, int commandStep)
        {
            this.CommandName = commandName;
            this.CommandStep = commandStep;
            this.dictItem = new();
        }

        internal void AssignNewCommand(string commandName)
        {
            this.CommandName = commandName;
            this.CommandStep = 0;
        }

        internal string AssignedCommand() => CommandName;

        internal void ClearCommand()
        {
            this.CommandName = string.Empty;
            this.CommandStep = 0;
        }

    }
}
