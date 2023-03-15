using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Common;

namespace TerraTyping.Commands;

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
