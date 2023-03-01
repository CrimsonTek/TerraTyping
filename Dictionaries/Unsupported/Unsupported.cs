//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TerraTyping.Attributes;
//using TerraTyping.DataTypes;
//using TerraTyping.DataTypes.Structs;

//namespace TerraTyping
//{
//    [Load]
//    [Unload]
//    public class UnsupportedAmmos
//    {
//        public static void Load() => Type = new Dictionary<int, ItemTypeInfo>();
//        public static void Unload() => Type = null;
//        public static Dictionary<int, ItemTypeInfo> Type = new Dictionary<int, ItemTypeInfo>();
//    }

//    [Load]
//    [Unload]
//    public class UnsupportedArmors
//    {
//        public static void Load()
//        {
//            Type = new Dictionary<int, ArmorTypeInfo>();
//            Helmet = new Dictionary<int, ArmorTypeInfo>();
//            Chest = new Dictionary<int, ArmorTypeInfo>();
//            Leggings = new Dictionary<int, ArmorTypeInfo>();
//        }

//        public static void Unload()
//        {
//            Type = null;
//            Helmet = null;
//            Chest = null;
//            Leggings = null;
//        }

//        public static Dictionary<int, ArmorTypeInfo> Type = new Dictionary<int, ArmorTypeInfo>() { };
//        public static Dictionary<int, ArmorTypeInfo> Helmet = new Dictionary<int, ArmorTypeInfo>() { };
//        public static Dictionary<int, ArmorTypeInfo> Chest = new Dictionary<int, ArmorTypeInfo>() { };
//        public static Dictionary<int, ArmorTypeInfo> Leggings = new Dictionary<int, ArmorTypeInfo>() { };
//    }

//    [Load]
//    [Unload]
//    public class UnsupportedEnemies
//    {
//        public static void Load() => Type = new Dictionary<int, NPCTypeInfo>();
//        public static void Unload() => Type = null;
//        public static Dictionary<int, NPCTypeInfo> Type = new Dictionary<int, NPCTypeInfo>() { };
//    }

//    [Load]
//    [Unload]
//    public class UnsupportedItems
//    {
//        public static void Load() => Type = new Dictionary<int, ItemTypeInfo>();
//        public static void Unload() => Type = null;
//        public static Dictionary<int, ItemTypeInfo> Type = new Dictionary<int, ItemTypeInfo>() { };
//    }

//    [Load]
//    [Unload]
//    public class UnsupportedProjectiles
//    {
//        public static void Load() => Type = new Dictionary<int, ProjectileTypeInfo>();
//        public static void Unload() => Type = null;
//        public static Dictionary<int, ProjectileTypeInfo> Type = new Dictionary<int, ProjectileTypeInfo>() { };
//    }
//}
