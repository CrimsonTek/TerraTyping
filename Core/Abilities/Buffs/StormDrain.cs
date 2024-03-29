﻿using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class StormDrain : ModBuff
    {
        private float damageBoostNPC;
        private float damageBoostPlayer;

        public override void Load()
        {
            damageBoostNPC = ServerConfig.Instance.AbilityConfigInstance.StormDrainDamageBoostNPC;
            damageBoostPlayer = ServerConfig.Instance.AbilityConfigInstance.StormDrainDamageBoostPlayer;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Drain");
            // Description.SetDefault($"Damage boosted by {AbilityData.stormDrainDamageBoostNPC:P2}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.TryGetGlobalNPC(out NPCTyping npcTyping))
            {
                npcTyping.damageMultiplyByBuff += damageBoostNPC - 1;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.boostsToEachTypeByAbilities[(int)Element.water].Add(new Boost(damageBoostPlayer, DisplayName.Value));
            }
        }
    }
}
