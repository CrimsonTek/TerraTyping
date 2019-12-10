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

namespace TerraTyping
{
    public class ArmorTyping : GlobalItem
    {
        public override void UpdateEquip(Item item, Player player)
        {
            if (item.headSlot != -1 && Armors.Helmet.ContainsKey(item.type)) { ArmorPlayer.typeHead = Armors.Type[item.type]; }
            else if (item.bodySlot != -1 && Armors.Chest.ContainsKey(item.type)) { ArmorPlayer.typeBody = Armors.Type[item.type]; }
            else if (item.legSlot != -1 && Armors.Leggings.ContainsKey(item.type)) { ArmorPlayer.typeLegs = Armors.Type[item.type]; }
            else if (item.wingSlot != -1) { ArmorPlayer.wings = true; }
            if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
            {
                if (ArmorPlayer.wings == true)
                    ArmorPlayer.typeSet = new Tuple<Element, Element, Element>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2, Element.levitate);
                else
                    ArmorPlayer.typeSet = new Tuple<Element, Element, Element>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2, Element.none);
            }
            else
            {
                if (ArmorPlayer.wings == true)
                    ArmorPlayer.typeSet = new Tuple<Element, Element, Element>(Element.normal, Element.none, Element.levitate);
                else
                    ArmorPlayer.typeSet = new Tuple<Element, Element, Element>(Element.normal, Element.none, Element.none);
            };
        }
    }
    public class ArmorPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            typeHead = new Tuple<Element, Element>(Element.normal, Element.none);
            typeBody = new Tuple<Element, Element>(Element.normal, Element.none);
            typeLegs = new Tuple<Element, Element>(Element.normal, Element.none);
            if (typeHead.Item1 == typeBody.Item1 && typeBody.Item1 == typeLegs.Item1 && typeHead.Item2 == typeBody.Item2 && typeBody.Item2 == typeLegs.Item2)
            {
                if (wings == true)
                    typeSet = new Tuple<Element, Element, Element>(typeBody.Item1, typeBody.Item2, Element.levitate);
                else
                    typeSet = new Tuple<Element, Element, Element>(typeBody.Item1, typeBody.Item2, Element.none);
            }
            else
            {
                if (wings == true)
                    typeSet = new Tuple<Element, Element, Element>(Element.normal, Element.none, Element.levitate);
                else
                    typeSet = new Tuple<Element, Element, Element>(Element.normal, Element.none, Element.none);
            };
        }

        public static Tuple<Element, Element, Element> typeSet = new Tuple<Element, Element, Element>(Element.normal, Element.none, Element.none);
        public static Tuple<Element, Element> typeHead = new Tuple<Element, Element>(Element.normal, Element.none);
        public static Tuple<Element, Element> typeBody = new Tuple<Element, Element>(Element.normal, Element.none);
        public static Tuple<Element, Element> typeLegs = new Tuple<Element, Element>(Element.normal, Element.none);
        public static bool wings = false;

        //public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        //{
        //    Tuple<Element, Element, Element> thingy = new Tuple<Element, Element, Element> (Element.grass, Element.steel, Element.levitate);
        //    float newDamage = Calc.Damage(damageSource, thingy, damage);
        //    //if (newDamage == 0)
        //    //{
        //    //    damage = (int)newDamage;
        //    //    return false;
        //    //}
        //    //else
        //    //{
        //        damage = (int)newDamage;
        //        return true;
        //    //}




        //    //if (Other.Type.ContainsKey(damageSource.SourceOtherIndex)) // && damageSource == PlayerDeathReason.ByOther(damageSource.SourceOtherIndex)
        //    //{
        //    //    float multiplier = Calc.Damage(damageSource.SourceOtherIndex, typeSet, 0, Other.Type);
        //    //    if (multiplier == 0)
        //    //        return false;
        //    //    else
        //    //    {
        //    //        damage = (int)(damage * multiplier);
        //    //        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        //    //    }
        //    //}
        //    //else if (Enemies.Type.ContainsKey(damageSource.SourceNPCIndex))
        //    //{
        //    //    float multiplier = Calc.Damage(damageSource.SourceNPCIndex, typeSet, 0, Enemies.Type);
        //    //    if (multiplier == 0)
        //    //        return false;
        //    //    else
        //    //    {
        //    //        damage = (int)(damage * multiplier);
        //    //        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        //    //    }
        //    //}
        //    //else if (Projectiles.Type.ContainsKey(damageSource.SourceProjectileIndex))
        //    //{
        //    //    float multiplier = Calc.Damage(damageSource.SourceProjectileIndex, typeSet, 0, Projectiles.Type);
        //    //    if (multiplier == 0)
        //    //        return false;
        //    //    else
        //    //    {
        //    //        damage = (int)(damage * multiplier);
        //    //        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        //    //    }
        //    //}
        //    //else return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);

        //    //else
        //    //{
        //    //    customDamage = true;
        //    //    damage = damageSource.SourceOtherIndex;
        //    //    return true;
        //    //} // finding SourceOtherIndex
        //}

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            damage = (int)(damage * Calc.Damage(npc, typeSet));

            float dmg = Calc.Damage(npc, typeSet);
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

            CombatText.NewText(player.getRect(), color, text, false, true);
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (Calc.Damage(npc, typeSet) == 0)
                return false;
            else
                return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            damage = (int)(damage * Calc.Damage(proj, typeSet));

            float dmg = Calc.Damage(proj, typeSet);
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

            CombatText.NewText(player.getRect(), color, text, false, true);
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (Calc.Damage(proj, typeSet) == 0)
                return false;
            else
                return base.CanBeHitByProjectile(proj);
        }
    }
}
