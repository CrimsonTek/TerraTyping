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
        Mod weaponOut = ModLoader.GetMod("WeaponOut");

        public Dictionary<int, Element> Ammo(Item item)
        {
            return Ammos.Type;
        }

        public Dictionary<int, Tuple<Element, Element>> Armor(Item item)
        {
            return Armors.Type;
        }

        public Dictionary<int, Element> Buff(int buff)
        {
            return Buffs.Type;
        }

        public Dictionary<int, Tuple<Element, Element, Element, Element>> NPC(NPC npc)
        {
            return Enemies.Type;
        }

        public Dictionary<int, Element> Item(Item item)
        {
            //if (item.modItem.mod == weaponOut) 
            //    return Items.WeaponOut;
            //else
                return Items.Type;

        }

        public Dictionary<int, Element> Other(PlayerDeathReason pdr)
        {
            return OtherDict.Type;
        }

        public Dictionary<int, Element> Projectile(Projectile projectile)
        {
            return Projectiles.Type;
        }
    }
}
