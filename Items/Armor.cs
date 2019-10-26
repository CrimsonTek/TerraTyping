using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Items
{
    public class ArmorTyping : GlobalItem
    {
        public override void UpdateEquip(Item item, Player player)
        {
            //if (Armors.Type.ContainsKey(item.type)) { ArmorPlayer.typeHead = Armors.Type[item.type]; }
            //if (Armors.Type.ContainsKey(item.type)) { ArmorPlayer.typeBody = Armors.Type[item.type]; }
            //if (Armors.Type.ContainsKey(item.type)) { ArmorPlayer.typeLegs = Armors.Type[item.type]; }
            //if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
            //{
            //    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2);
            //}
            //else
            //{
            //    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
            //};
            if (Armors.Helmet.ContainsKey(item.type)) { ArmorPlayer.typeHead = Armors.Type[item.type]; }
            else if (Armors.Chest.ContainsKey(item.type)) { ArmorPlayer.typeBody = Armors.Type[item.type]; }
            else if (Armors.Leggings.ContainsKey(item.type)) { ArmorPlayer.typeLegs = Armors.Type[item.type]; }
        }
    }
    public class ArmorPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            typeHead = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
            typeBody = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
            typeLegs = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        }

        public static Tuple<Element.Type, Element.Type> typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        public static Tuple<Element.Type, Element.Type> typeHead = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        public static Tuple<Element.Type, Element.Type> typeBody = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        public static Tuple<Element.Type, Element.Type> typeLegs = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (Enemies.Type.ContainsKey(npc.type))
            {
                if (typeHead.Item1 == typeBody.Item1 && typeBody.Item1 == typeLegs.Item1 && typeHead.Item2 == typeBody.Item2 && typeBody.Item2 == typeLegs.Item2)
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2);
                }
                else
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
                };
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (npc.ai[0] >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item2];
                        if (multiplier1 == 0 || multiplier2 == 0) { return false; }
                        else { return true; }
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item2];
                        if (multiplier1 == 0 || multiplier2 == 0) { return false; }
                        else { return true; }
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)Enemies.Type[npc.type].Item4, (int)typeSet.Item1];
                    float multiplier2 = Table.Effectiveness[(int)Enemies.Type[npc.type].Item4, (int)typeSet.Item2];
                    if (multiplier1 == 0 || multiplier2 == 0) { return false; }
                    else { return true; }
                };
            }
            else { return true; }
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (Enemies.Type.ContainsKey(npc.type))
            {
                if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2);
                }
                else
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
                };
                if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    if (npc.ai[0] >= 2)
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.steel, (int)typeSet.Item2];
                        damage = (int)Math.Round(damage * multiplier1 * multiplier2);
                    }
                    else
                    {
                        float multiplier1 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item1];
                        float multiplier2 = Table.Effectiveness[(int)Element.Type.normal, (int)typeSet.Item2];
                        damage = (int)Math.Round(damage * multiplier1 * multiplier2);
                    }
                }
                else
                {
                    float multiplier1 = Table.Effectiveness[(int)Enemies.Type[npc.type].Item4, (int)typeSet.Item1];
                    float multiplier2 = Table.Effectiveness[(int)Enemies.Type[npc.type].Item4, (int)typeSet.Item2];
                    damage = (int)Math.Round(damage * multiplier1 * multiplier2);
                }
            }
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (Projectiles.Type.ContainsKey(proj.type))
            {
                if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2);
                }
                else
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
                };
                float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[proj.type], (int)typeSet.Item1];
                float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[proj.type], (int)typeSet.Item2];
                if (multiplier1 == 0 || multiplier2 == 0) { return false; }
                else { return true; };
            }
            else { return true; }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (Projectiles.Type.ContainsKey(proj.type))
            {
                if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2);
                }
                else
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
                };
                float multiplier1 = Table.Effectiveness[(int)Projectiles.Type[proj.type], (int)typeSet.Item1];
                float multiplier2 = Table.Effectiveness[(int)Projectiles.Type[proj.type], (int)typeSet.Item2];
                damage = (int)Math.Round(damage * multiplier1 * multiplier2);
            }
        }
    }
}
