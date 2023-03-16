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
    public class Justified : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Justified");
            Description.SetDefault($"Damage boosted by {AbilityData.justifiedDamageBoostPlayer:P2}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCTyping>().damageMultiplyByBuff += AbilityData.justifiedDamageBoostNPC - 1;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping._allDamageModifiedByAbilities += ServerConfig.Instance.AbilityConfigInstance.JustifiedDamageBoostPlayer - 1;
            }
        }
    }
}
