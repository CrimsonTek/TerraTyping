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

namespace TerraTyping.Items
{
    public class NPCTyping : GlobalNPC
    {
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit) {
            //if (item.modItem.mod == ModLoader.GetMod("WeaponOut"))
            //{ }

            damage = (int)(damage * Calc.Damage(item.type, npc.type, npc.ai[0], Items.Type, Enemies.Type));
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item) {
            return Calc.CanBeHit(item.type, npc.type, npc.ai[0], Items.Type, Enemies.Type);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
            damage = (int)(damage * Calc.Damage(projectile.type, npc.type, npc.ai[0], Projectiles.Type, Enemies.Type));
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile) {
            return Calc.CanBeHit(projectile.type, npc.type, npc.ai[0], Projectiles.Type, Enemies.Type);
        }
    }
}
