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
    public class DictionaryHelper
    {
        static Mod weaponOut = ModLoader.GetMod("WeaponOut");

        public Dictionary<int, Element> Ammo(Item item)
        {
            if (item.modItem != null)
            {
                if (item.modItem.mod == weaponOut)
                    return WeaponOutAmmos.Type;
            }
            return Ammos.Type;
        }

        public Dictionary<int, Tuple<Element, Element>> Armor(Item item)
        {
            if (item.modItem != null)
            {
                if (item.modItem.mod == weaponOut)
                    return WeaponOutArmors.Type;
            }
            return Armors.Type;
        }

        public Dictionary<int, Element> Buff(int buff)
        {
            return Buffs.Type;
        }

        public Dictionary<int, Tuple<Element, Element, Element, Element>> NPC(NPC npc)
        {
            if (npc.modNPC != null)
            {
                if (npc.modNPC.mod == weaponOut)
                    return WeaponOutEnemies.Type;
            }
            return Enemies.Type;
        }

        public Dictionary<int, Element> Item(Item item)
        {
            if (item.modItem != null)
            {
                if (item.modItem.mod == weaponOut)
                    return WeaponOutItems.Type;
            }
            return Items.Type;
        }

        public Dictionary<int, Element> Other(PlayerDeathReason pdr)
        {
            return OtherDict.Type;
        }

        public Dictionary<int, Element> Projectile(Projectile projectile)
        {
            if (projectile.modProjectile != null)
            {
                if (projectile.modProjectile.mod == weaponOut)
                    return WeaponOutProjectiles.Type;
            }
            return Projectiles.Type;
        }
    }
}
