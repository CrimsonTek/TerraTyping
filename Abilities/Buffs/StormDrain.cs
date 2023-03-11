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
    public class StormDrain : ModBuff, IPowerupType
    {
        public BuffPowerupType PowerupType 
        { 
            get => (paramters) => 
            { 
                return new BuffPowerupTypeReturn(AbilityData.stormDrainDamageBoostPlayer, "Storm Drain"); 
            };
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff *= AbilityData.stormDrainDamageBoostNPC;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // todo: implement
        }
    }
}
