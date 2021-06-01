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
        public PowerupType PowerupType 
        { 
            get => (paramters) => 
            { 
                return new PowerupTypeReturn(AbilityData.stormDrainDamageBoostPlayer, "Storm Drain"); 
            };
            set => throw new NotImplementedException();
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);

            npc.GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff *= AbilityData.stormDrainDamageBoostNPC;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
        }
    }
}
