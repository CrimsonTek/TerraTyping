using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerraTyping
{
    /// <summary>
    /// Code from https://github.com/JavidPack/ModdersToolkit/blob/02f76e799c74686dcaf60ef355faf3396cc4f97d/Tools/Hitboxes/HitboxesTool.cs#L68
    /// </summary>
    public class ItemTyping : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public Rectangle?[] meleeHitbox = new Rectangle?[Main.maxPlayers];

        public override GlobalItem NewInstance(Item item)
        {
            return base.NewInstance(item);
        }

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            meleeHitbox[player.whoAmI] = hitbox;
        }
    }
}
