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
    public class Mummy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Mummy");
            Description.SetDefault("Your ability is now mummy");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.TryGetGlobalNPC(out NPCTyping npcTyping))
            {
                npcTyping.ModifiedAbility = AbilityID.Mummy;
                npcTyping.UseModifiedAbility = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.ModifiedAbility = AbilityID.Mummy;
                playerTyping.UseModifiedAbility = true;
            }
        }
    }
}
