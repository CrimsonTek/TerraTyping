using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Data;

namespace TerraTyping.Abilities.Buffs
{
    public class StormDrain : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Drain");
            Description.SetDefault($"Damage boosted by {AbilityData.stormDrainDamageBoostNPC:P2}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCTyping>().damageMultiplyByBuff += AbilityData.stormDrainDamageBoostNPC - 1;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping._allDamageModifiedByAbilities += ServerConfig.Instance.AbilityConfigInstance.StormDrainDamageBoostPlayer - 1;
            }
        }
    }
}
