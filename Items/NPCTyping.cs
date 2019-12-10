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
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            damage = (int)(damage * Calc.Damage(item, npc));

            float dmg = Calc.Damage(item, npc);
            string text = ((float)(int)(dmg * 100) / 100).ToString() + "x!";

            Color color = new Color(new Vector3(1, 1, 1));
            if (dmg != 1)
            {
                if (dmg == 0)
                    color = new Color(new Vector3(0.2f, 0.2f, 0.2f));

                else if (dmg > 1)
                    color = new Color(new Vector3(1 - (dmg / 3), 1, 1 - (dmg / 3)));

                else if (dmg < 1)
                    color = new Color(new Vector3((dmg * 4) / 5 + 0.2f, (dmg) / 5 + 0.2f, 0));
            }

            CombatText.NewText(npc.getRect(), color, text, false, true);
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
            damage = (int)(damage * Calc.Damage(projectile, npc));

            float dmg = Calc.Damage(projectile, npc);
            string text = ((float)(int)(dmg * 100) / 100).ToString() + "x!";

            Color color = new Color(new Vector3(1, 1, 1));
            if (dmg != 1)
            {
                if (dmg == 0)
                    color = new Color(new Vector3(0.2f, 0.2f, 0.2f));

                else if (dmg > 1)
                    color = new Color(new Vector3(1 - (dmg / 3), 1, 1 - (dmg / 3)));

                else if (dmg < 1)
                    color = new Color(new Vector3((dmg * 4) / 5 + 0.2f, (dmg) / 5 + 0.2f, 0));
            }

            CombatText.NewText(npc.getRect(), color, text, false, true);
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
