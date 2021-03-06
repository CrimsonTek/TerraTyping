﻿using System;
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
        public PowerupType PowerupType 
        {
            get => (parameters) => 
            {
                return new PowerupTypeReturn(AbilityData.lightningRodDamageBoostPlayer, "Lightning Rod");
            }; 
            set { }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);

            npc.GetGlobalNPC<NPCTyping>().DamageMultiplyByBuff *= AbilityData.lightningRodDamageBoostNPC;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
        }
    }
}
