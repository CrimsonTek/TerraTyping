using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class Justified : ModBuff
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
            // DisplayName.SetDefault("Justified");
            // Description.SetDefault($"Damage boosted by {AbilityData.justifiedDamageBoostPlayer:P2}");
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
                playerTyping.boostsToAllDamageByAbilities.Add(new Boost(damageBoostPlayer, DisplayName.Value));
            }
        }
    }
}
