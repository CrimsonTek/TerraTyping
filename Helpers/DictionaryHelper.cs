using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TerraTyping
{
    public static class DictionaryHelper
    {
        static readonly Mod weaponOut = ModLoader.GetMod("WeaponOut");

        public static Dictionary<int, Element> Ammo(Item item)
        {
            if (item.modItem != null)
            {
                if (item.modItem.mod == weaponOut)
                    return WeaponOutAmmos.Type;
                else
                    return UnsupportedAmmos.Type;
            }
            return Ammos.Type;
        }

        public static Dictionary<int, Tuple<Element, Element>> Armor(Item item)
        {
            if (item.modItem != null)
            {
                if (item.modItem.mod == weaponOut)
                    return WeaponOutArmors.Type;
                else
                    return UnsupportedArmors.Type;
            }
            return Armors.Type;
        }

        public static Dictionary<int, Element> Buff(int buff)
        {
            return Buffs.Type;
        }

        public static Dictionary<int, Tuple<Element, Element, Element, Element>> NPC(NPC npc)
        {
            if (npc.modNPC != null)
            {
                if (npc.modNPC.mod == weaponOut)
                    return WeaponOutEnemies.Type;
                else
                    return UnsupportedEnemies.Type;
            }
            return Enemies.Type;
        }

        public static Dictionary<int, Element> Item(Item item)
        {
            if (item.modItem != null)
            {
                if (item.modItem.mod == weaponOut)
                    return WeaponOutItems.Type;
                else
                    return UnsupportedItems.Type;
            }
            return Items.Type;
        }

        public static Dictionary<int, Element> Other(PlayerDeathReason pdr)
        {
            return OtherDict.Type;
        }

        public static Dictionary<int, Element> Projectile(Projectile projectile)
        {
            if (projectile.modProjectile != null)
            {
                if (projectile.modProjectile.mod == weaponOut)
                    return WeaponOutProjectiles.Type;
                else
                    return UnsupportedProjectiles.Type;
            }
            return Projectiles.Type;
        }
    }
}
