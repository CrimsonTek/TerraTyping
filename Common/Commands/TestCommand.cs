using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Core;
using TerraTyping.TypeLoaders;

namespace TerraTyping.Common.Commands;

public class TestCommand : ModCommand
{
    public override string Command => "test";

    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length != 1)
        {
            caller.Reply("Args length must be 1");
            return;
        }

        if (!Enum.TryParse(args[0], true, out Element element))
        {
            caller.Reply("Unknown element");
        }

        for (int i = 0; i < ItemLoader.ItemCount; i++)
        {
            ElementArray elements = WeaponTypeLoader.GetElements(i);
            if (elements.Length > 0)
            {
                if (elements.HasElement(element))
                {
                    Item item = caller.Player.QuickSpawnItemDirect(null, i);
                    item.ResetPrefix();
                }
            }
        }
    }
}
