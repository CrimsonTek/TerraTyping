using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Core;
using TerraTyping.TypeLoaders;

namespace TerraTyping.Common.Commands;

public class TestCommand : ModCommand
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }

    public override string Command => "test";

    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        NPC npc = FindNPCNearCursor();
        if (npc is not null)
        {
            caller.Reply($"{npc.TypeName}: {npc.type}/{npc.netID}");
        }
    }

    private static NPC FindNPCNearCursor()
    {
        Vector2 mouseWorld = Main.MouseWorld;
        NPC npc = Main.npc.Where(npc => npc.active).OrderBy(npc => Vector2.DistanceSquared(mouseWorld, npc.Center)).FirstOrDefault();
        return npc;
    }

    private static void SpawnAllItemsOfElement(CommandCaller caller, string[] args)
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
