using Terraria.ModLoader;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Common.Commands;

// todo: ability command
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

    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
}
