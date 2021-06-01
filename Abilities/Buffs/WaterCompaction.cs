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
    public class WaterCompaction : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense += AbilityData.waterCompactionDefenseBoostNPC;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += AbilityData.waterCompactionDefenseBoostPlayer;
        }
    }
}
