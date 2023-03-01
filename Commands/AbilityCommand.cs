using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TerraTyping.Commands;

public class AbilityCommand : ModCommand
{
    public override string Command => "ability";

    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (caller?.Player is null)
        {
            caller.Reply($"Command must be used by a player.");
            return;
        }

        if (!caller.Player.TryGetModPlayer(out PlayerTyping playerTyping))
        {
            caller.Reply($"Could not find mod player instance on command caller.");
            return;
        }

        //caller.Reply($"AbilityID: {playerTyping.AbilityID}");
    }
}
