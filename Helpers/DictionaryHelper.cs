//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.DataStructures;
//using TerraTyping.DataTypes;
//using TerraTyping.DataTypes.Structs;

//namespace TerraTyping
//{
//    [Obsolete]
//    public static class DictionaryHelper
//    {
//        static readonly Mod weaponOut = GetModOrNull("WeaponOut");

//        static Mod GetModOrNull(string modName)
//        {
//            if (ModLoader.TryGetMod(modName, out Mod result))
//            {
//                return result;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        public static Dictionary<int, ItemTypeInfo> Ammo(Item item)
//        {
//            if (item.ModItem != null)
//            {
//                if (item.ModItem.Mod == weaponOut)
//                    return WeaponOutAmmos.Type;
//                else
//                    return UnsupportedAmmos.Type;
//            }
//            return Ammos.Type;
//        }

//        public static Dictionary<int, ArmorTypeInfo> Armor(Item item)
//        {
//            if (item.ModItem != null)
//            {
//                if (item.ModItem.Mod == weaponOut)
//                    return WeaponOutArmors.Type;
//                else
//                    return UnsupportedArmors.Type;
//            }
//            return oldArmors.Type;
//        }

//        //public static Dictionary<int, Tuple<Element, Element>> Armor(Item item)
//        //{
//        //    if (item.modItem != null)
//        //    {
//        //        if (item.modItem.mod == weaponOut)
//        //            return WeaponOutArmors.Type;
//        //        else
//        //            return UnsupportedArmors.Type;
//        //    }
//        //    return Armors.Type;
//        //}

//        public static Dictionary<int, Element> Buff(int buff)
//        {
//            return Buffs.Type;
//        }

//        public static Dictionary<int, NPCTypeInfo> NPC(NPC npc)
//        {
//            if (npc.ModNPC != null)
//            {
//                if (npc.ModNPC.Mod == weaponOut)
//                    return WeaponOutEnemies.Type;
//                else
//                    return UnsupportedEnemies.Type;
//            }
//            return EnemiesOld.Type;
//        }

//        public static Dictionary<int, ItemTypeInfo> Item(Item item)
//        {
//            if (item.ModItem != null)
//            {
//                if (item.ModItem.Mod == weaponOut)
//                    return WeaponOutItems.Type;
//                else
//                    return UnsupportedItems.Type;
//            }
//            return ItemsOld.Type;
//        }

//        : Other damage
//        public static Dictionary<int, Element> Other(PlayerDeathReason pdr)
//        {
//            return OtherDictOld.Type;
//        }

//        public static Dictionary<int, ProjectileTypeInfo> Projectile(Projectile projectile)
//        {
//            if (projectile.ModProjectile != null)
//            {
//                if (projectile.ModProjectile.Mod == weaponOut)
//                    return WeaponOutProjectiles.Type;
//                else
//                    return UnsupportedProjectiles.Type;
//            }
//            return ProjectilesOld.Type;
//        }
//    }
//}
