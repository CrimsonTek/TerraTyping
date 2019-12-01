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
            if (item.headSlot != -1 && Armors.Helmet.ContainsKey(item.type)) { ArmorPlayer.typeHead = Armors.Type[item.type]; }
            else if (item.bodySlot != -1 && Armors.Chest.ContainsKey(item.type)) { ArmorPlayer.typeBody = Armors.Type[item.type]; }
            else if (item.legSlot != -1 && Armors.Leggings.ContainsKey(item.type)) { ArmorPlayer.typeLegs = Armors.Type[item.type]; }
            else if (item.wingSlot != -1) { ArmorPlayer.wings = true; }
            if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
            {
                if (ArmorPlayer.wings == true)
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2, Element.Type.levitate);
                else
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2, Element.Type.none);
            }
            else
            {
                if (ArmorPlayer.wings == true)
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(Element.Type.normal, Element.Type.none, Element.Type.levitate);
                else
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(Element.Type.normal, Element.Type.none, Element.Type.none);
            };
        }
    }
    public class ArmorPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            typeHead = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
            typeBody = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
            typeLegs = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
            if (typeHead.Item1 == typeBody.Item1 && typeBody.Item1 == typeLegs.Item1 && typeHead.Item2 == typeBody.Item2 && typeBody.Item2 == typeLegs.Item2)
            {
                if (wings == true)
                    typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(typeBody.Item1, typeBody.Item2, Element.Type.levitate);
                else
                    typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(typeBody.Item1, typeBody.Item2, Element.Type.none);
            }
            else
            {
                if (wings == true)
                    typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(Element.Type.normal, Element.Type.none, Element.Type.levitate);
                else
                    typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(Element.Type.normal, Element.Type.none, Element.Type.none);
            };
        }

        public static Tuple<Element.Type, Element.Type, Element.Type> typeSet = new Tuple<Element.Type, Element.Type, Element.Type>(Element.Type.normal, Element.Type.none, Element.Type.none);
        public static Tuple<Element.Type, Element.Type> typeHead = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        public static Tuple<Element.Type, Element.Type> typeBody = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        public static Tuple<Element.Type, Element.Type> typeLegs = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
        public static bool wings = false;

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Other.Type.ContainsKey(damageSource.SourceOtherIndex)) // && damageSource == PlayerDeathReason.ByOther(damageSource.SourceOtherIndex)
            {
                float multiplier = Calc.Damage(damageSource.SourceOtherIndex, typeSet, 0, Other.Type);
                if (multiplier == 0)
                    return false;
                else
                {
                    damage = (int)(damage * multiplier);
                    return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
                }
            }
            //else if (Enemies.Type.ContainsKey(damageSource.SourceNPCIndex))
            //{
            //    float multiplier = Calc.Damage(damageSource.SourceNPCIndex, typeSet, 0, Enemies.Type);
            //    if (multiplier == 0)
            //        return false;
            //    else
            //    {
            //        damage = (int)(damage * multiplier);
            //        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
            //    }
            //}
            //else if (Projectiles.Type.ContainsKey(damageSource.SourceProjectileIndex))
            //{
            //    float multiplier = Calc.Damage(damageSource.SourceProjectileIndex, typeSet, 0, Projectiles.Type);
            //    if (multiplier == 0)
            //        return false;
            //    else
            //    {
            //        damage = (int)(damage * multiplier);
            //        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
            //    }
            //}
            else return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);

            //else
            //{
            //    customDamage = true;
            //    damage = damageSource.SourceOtherIndex;
            //    return true;
            //} // finding SourceOtherIndex
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (Calc.Damage(npc.type, typeSet, npc.ai[0], Enemies.Type) == 0)
                return false;
            else
                return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            damage = (int)((float)damage * Calc.Damage(npc.type, typeSet, npc.ai[0], Enemies.Type));
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (Calc.Damage(proj.type, typeSet, proj.ai[0], Enemies.Type) == 0)
                return false;
            else
                return base.CanBeHitByProjectile(proj);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            damage = (int)((float)damage * Calc.Damage(proj.type, typeSet, proj.ai[0], Enemies.Type));
        }
    }
}
