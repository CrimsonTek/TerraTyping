using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Data;

namespace TerraTyping.Abilities.Buffs
{
    /// <summary>
    /// For players only
    /// </summary>
    public class WaterCompaction : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Compaction");
            Description.SetDefault($"Increases defense by {AbilityData.waterCompactionDefenseBoostPlayer}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            TerraTyping.Instance.Logger.Warn($"NPC [{npc}] has buff Water Compaction.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += AbilityData.waterCompactionDefenseBoostPlayer;
        }
    }
}
