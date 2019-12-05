using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class Guide : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Guide)
            {
                Player player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
                chat = ($"player.lifeRegen: " + player.lifeRegen);
            }
        }
    }
}
