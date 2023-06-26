using Terraria.ModLoader;

namespace TerraTyping.Common.Commands;

public class WikiCommand : ModCommand
{
    public override string Command => "wiki";

    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        ModContent.GetInstance<MySystem>().ActivateWikiUI();
    }

    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
}
