using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items
{
    //public class HardStone : GlobalTile
    //{
    //    public override bool Drop(int i, int j, int type)
    //    {
    //        Random random = new Random();
    //        Tile t = Main.tile[i, j];
    //        if (random.Next(5) == 0)
    //        {
    //            Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Diamond, 1);
    //        }
    //        return base.Drop(i, j, type);
    //    }
    //}
    //public class Charcoal : GlobalNPC
    //{
    //    public override void NPCLoot(NPC npc)
    //    {
    //        if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneUnderworldHeight == true)
    //        {
    //            Item.NewItem(npc.getRect(), ItemID.Coal, 1);
    //        }
    //    }
    //}
    //public class BlackBelt : ModPlayer
    //{
    //    public override void OnHitAnything(float x, float y, Entity victim)
    //    {
    //        Random random = new Random();
    //        if (random.Next(10) == 0)
    //        {
    //            Item.NewItem(victim.Center, victim.Size, ItemID.BlackBelt, 1, false);
    //        }
    //    }
    //}
}
