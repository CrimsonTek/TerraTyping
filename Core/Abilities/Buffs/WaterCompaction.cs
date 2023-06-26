using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class WaterCompaction : ModBuff
    {
        private int playerDefense;

        public override void Load()
        {
            playerDefense = ServerConfig.Instance.AbilityConfigInstance.WaterCompactionDefenseBoostPlayer;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Water Compaction");
            // Description.SetDefault($"Defense increased by {AbilityData.waterCompactionDefenseBoostPlayer}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.TryGetGlobalNPC(out NPCTyping result))
            {
                result.waterCompactionBuff = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += playerDefense;
        }
    }
}
