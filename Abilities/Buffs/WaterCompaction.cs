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
    public class WaterCompaction : ModBuff
    {
        private int playerDefense;

        public override void Load()
        {
            playerDefense = ServerConfig.Instance.AbilityConfigInstance.WaterCompactionDefenseBoostPlayer;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Compaction");
            Description.SetDefault($"Defense increased by {AbilityData.waterCompactionDefenseBoostPlayer}");
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
            player.statDefense += AbilityData.waterCompactionDefenseBoostPlayer;
        }
    }
}
