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
    public class Justified : ModBuff, IPowerupType
    {
        public PowerupType PowerupType =>
            (parameters) => new PowerupTypeReturn(AbilityData.justifiedDamageBoostPlayer, "Justified");

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff *= AbilityData.justifiedDamageBoostNPC;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.allDamageModifiedByAbilities *= AbilityData.justifiedDamageBoostPlayer;
            }
        }
    }
}
