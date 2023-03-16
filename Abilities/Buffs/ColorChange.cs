using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.Abilities.Buffs
{
    public class ColorChange : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Color Change");
            Description.SetDefault("Your type is different.");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.TryGetGlobalNPC(out NPCTyping npcTyping))
            {
                npcTyping.UseModifiedElements = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.UseModifiedElements = true;
            }
        }
    }
}
