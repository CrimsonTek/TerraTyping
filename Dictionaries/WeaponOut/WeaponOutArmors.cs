//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria.ModLoader;
//using TerraTyping.Attributes;
//using TerraTyping.DataTypes;

//namespace TerraTyping
//{
//    [Obsolete]
//    [Load]
//    [Unload]
//    public class WeaponOutArmors
//    {
//        public static void Load()
//        {
//            Type = new Dictionary<int, ArmorTypeInfo>();
//            Helmet = new Dictionary<int, ArmorTypeInfo>();
//            Chest = new Dictionary<int, ArmorTypeInfo>();
//            Leggings = new Dictionary<int, ArmorTypeInfo>();
//            _Helmet = new Dictionary<string, ArmorTypeInfo>
//            {
//                {"ChampionLaurels", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"ColosseumHelmet", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistBoxingHelmet", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistBoxingHelmetPlus", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistMartialHead", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistMasterHead", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistVeteranHead", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"LunarFistHead", new ArmorTypeInfo(Element.fighting, Element.dragon) },
//            };
//            _Chest = new Dictionary<string, ArmorTypeInfo>
//            {
//                {"FistDefBody", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistPowerBody", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistSpeedBody", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"HighDefBody", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"HighPowerBody", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"HighSpeedBody", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"LunarFistBody", new ArmorTypeInfo(Element.fighting, Element.dragon) },
//            };
//            _Leggings = new Dictionary<string, ArmorTypeInfo>
//            {
//                {"FistDefLegs", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistPowerLegs", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"FistSpeedLegs", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"HighDefLegs", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"HighPowerLegs", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"HighSpeedLegs", new ArmorTypeInfo(Element.fighting, Element.none) },
//                {"LunarFistLegs", new ArmorTypeInfo(Element.fighting, Element.dragon) },
//            };
//        }

//        public static void Unload()
//        {
//            Type = null;
//            Helmet = null;
//            Chest = null;
//            Leggings = null;
//            _Helmet = null;
//            _Chest = null;
//            _Leggings = null;
//        }

//        public static Dictionary<int, ArmorTypeInfo> Type;
//        public static Dictionary<int, ArmorTypeInfo> Helmet;
//        public static Dictionary<int, ArmorTypeInfo> Chest;
//        public static Dictionary<int, ArmorTypeInfo> Leggings;
//        public static Dictionary<string, ArmorTypeInfo> _Helmet;
//        public static Dictionary<string, ArmorTypeInfo> _Chest;
//        public static Dictionary<string, ArmorTypeInfo> _Leggings;
//    }
//}
