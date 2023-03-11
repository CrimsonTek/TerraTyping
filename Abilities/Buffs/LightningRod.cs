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
    public class LightningRod : ModBuff, IPowerupType
    {
        public BuffPowerupType PowerupType 
        {
            get => (parameters) => 
            {
                return new BuffPowerupTypeReturn(AbilityData.lightningRodDamageBoostPlayer, "Lightning Rod");
            }; 
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff *= AbilityData.lightningRodDamageBoostNPC;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // todo: implement
        }
    }
}
