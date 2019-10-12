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

namespace Terramon.Items
{
    public class NPCTyping : GlobalNPC
    {
        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (Items.Type.ContainsKey(item.type) && Enemies.Type.ContainsKey(npc.type))
            {
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (npc.ai[0] >= 2)
                    {
                        //float STAB = 1f; if (ArmorPlayer.typeSet.Item1 == Items.Type[item.type] || ArmorPlayer.typeSet.Item2 == Items.Type[item.type]) { STAB = 1.5f; }
                        float multiplier1 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.steel];
                        float multiplier2 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.none];
                        damage = (int)(damage * multiplier1 * multiplier2 * multiplier3);
                        knockback = (knockback * multiplier1 * multiplier2 * multiplier3);
                    }
                    else
                    {
                        //float STAB = 1f; if (ArmorPlayer.typeSet.Item1 == Items.Type[item.type] || ArmorPlayer.typeSet.Item2 == Items.Type[item.type]) { STAB = 1.5f; }
                        float multiplier1 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.normal];
                        float multiplier2 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.none];
                        damage = (int)(damage * multiplier1 * multiplier2 * multiplier3);
                        knockback = (knockback * multiplier1 * multiplier2 * multiplier3);
                    }
                }
                else
                {
                    //float STAB = 1f; if (ArmorPlayer.typeSet.Item1 == Items.Type[item.type] || ArmorPlayer.typeSet.Item2 == Items.Type[item.type]) { STAB = 1.5f; }
                    float multiplier1 = Table.Effectiveness[(int)Items.Type[item.type], (int)Enemies.Type[npc.type].Item1];
                    float multiplier2 = Table.Effectiveness[(int)Items.Type[item.type], (int)Enemies.Type[npc.type].Item2];
                    float multiplier3 = Table.Effectiveness[(int)Items.Type[item.type], (int)Enemies.Type[npc.type].Item3];
                    damage = (int)(damage * multiplier1 * multiplier2 * multiplier3);
                    knockback = (knockback * multiplier1 * multiplier2 * multiplier3);
                }
            }
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (Items.Type.ContainsKey(item.type) && Enemies.Type.ContainsKey(npc.type))
            {
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (npc.ai[0] >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.steel];
                        float multiplier2 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.none];
                        if (multiplier1 == 0 || multiplier2 == 0 || multiplier3 == 0) { return false; }
                        return null;
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.normal];
                        float multiplier2 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Items.Type[item.type], (int)Element.Type.none];
                        if (multiplier1 == 0 || multiplier2 == 0 || multiplier3 == 0) { return false; }
                        return null;
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)Items.Type[item.type], (int)Enemies.Type[npc.type].Item1];
                    float multiplier2 = Table.Effectiveness[(int)Items.Type[item.type], (int)Enemies.Type[npc.type].Item2];
                    float multiplier3 = Table.Effectiveness[(int)Items.Type[item.type], (int)Enemies.Type[npc.type].Item3];
                    if (multiplier1 == 0 || multiplier2 == 0 || multiplier3 == 0) { return false; }
                    return null;
                };
            }
            else { return null; }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectiles.Type.ContainsKey(projectile.type) && Enemies.Type.ContainsKey(npc.type))
            {
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (npc.ai[0] >= 2)
                    {
                        //float STAB = 1f; if (ArmorPlayer.typeSet.Item1 == Projectiles.Type[projectile.type] || ArmorPlayer.typeSet.Item2 == Projectiles.Type[projectile.type]) { STAB = 1.5f; }
                        float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.steel];
                        float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.none];
                        damage = (int)(damage * multiplier1 * multiplier2 * multiplier3);
                        knockback = (knockback * multiplier1 * multiplier2 * multiplier3);
                    }
                    else
                    {
                        //float STAB = 1f; if (ArmorPlayer.typeSet.Item1 == Projectiles.Type[projectile.type] || ArmorPlayer.typeSet.Item2 == Projectiles.Type[projectile.type]) { STAB = 1.5f; }
                        float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.normal];
                        float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.none];
                        damage = (int)(damage * multiplier1 * multiplier2 * multiplier3);
                        knockback = (knockback * multiplier1 * multiplier2 * multiplier3);
                    }
                }
                else
                {
                    //float STAB = 1f; if (ArmorPlayer.typeSet.Item1 == Projectiles.Type[projectile.type] || ArmorPlayer.typeSet.Item2 == Projectiles.Type[projectile.type]) { STAB = 1.5f; }
                    float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Enemies.Type[npc.type].Item1];
                    float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Enemies.Type[npc.type].Item2];
                    float multiplier3 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Enemies.Type[npc.type].Item3];
                    damage = (int)(damage * multiplier1 * multiplier2 * multiplier3);
                    knockback = (knockback * multiplier1 * multiplier2 * multiplier3);
                }
            }
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (Projectiles.Type.ContainsKey(projectile.type) && Enemies.Type.ContainsKey(npc.type))
            {
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (npc.ai[0] >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.steel];
                        float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.none];
                        if (multiplier1 == 0 || multiplier2 == 0 || multiplier3 == 0) { return false; }
                        return null;
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.normal];
                        float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.flying];
                        float multiplier3 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Element.Type.none];
                        if (multiplier1 == 0 || multiplier2 == 0 || multiplier3 == 0) { return false; }
                        return null;
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Enemies.Type[npc.type].Item1];
                    float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Enemies.Type[npc.type].Item2];
                    float multiplier3 = Table.Effectiveness[(int)Projectiles.Type[projectile.type], (int)Enemies.Type[npc.type].Item3];
                    if (multiplier1 == 0 || multiplier2 == 0 || multiplier3 == 0) { return false; }
                    return null;
                };
            }
            else { return null; }
        }
    }
}
