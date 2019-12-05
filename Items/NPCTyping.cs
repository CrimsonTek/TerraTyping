using System;
using System.Collections.Generic;

using System.Collections;
using System.IO;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace TerraTyping
{
    public class NPCTyping : GlobalNPC
    {
        //public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        //{
        //    damage = (int)(damage * Calc.Damage(item.type, npc.type, npc.ai[0], dict.Item(item), dict.NPC(npc)));
        //}

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            damage = Calc.Damage(item, npc, damage);
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (Calc.Damage(item, npc) == 0)
                return false;
            else
                return base.CanBeHitByItem(npc, player, item);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = Calc.Damage(projectile, npc, damage);
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (Calc.Damage(projectile, npc) == 0)
                return false;
            else
                return base.CanBeHitByProjectile(npc, projectile);
        }
    }
}
