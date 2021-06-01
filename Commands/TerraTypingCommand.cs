using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping.Commands
{
    public class TerraTypingCommand : ModCommand
    {
        public override string Command => "TerraTyping";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            caller.Reply($"Welcome to TerraTyping! Here are some commands to try:");
            caller.Reply($" /type");
            caller.Reply($"More commands coming soon.");
        }
    }
}
