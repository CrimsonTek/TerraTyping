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
    public class LightningRod : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Rod");
            Description.SetDefault($"Damage boosted by {ServerConfig.Instance.AbilityConfigInstance.LightningRodDamageBoostPlayer:P2}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCTyping>().damageMultiplyByBuff += AbilityData.lightningRodDamageBoostNPC - 1;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping._allDamageModifiedByAbilities += ServerConfig.Instance.AbilityConfigInstance.LightningRodDamageBoostPlayer - 1;
            }
        }
    }
}
