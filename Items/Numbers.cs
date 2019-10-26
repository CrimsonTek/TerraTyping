using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Items
{
    public class EffectivenessIcon : GlobalNPC
    {
        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            for (int i = 0; i < 120; i++)
            {
                //DrawLayer();
            }
        }
    }
}
