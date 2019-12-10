using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class WeaponOutArmors
    {
        public static Dictionary<int, Tuple<Element, Element>> Type = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<int, Tuple<Element, Element>> Helmet = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<int, Tuple<Element, Element>> Chest = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<int, Tuple<Element, Element>> Leggings = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<string, Tuple<Element, Element>> _Helmet = new Dictionary<string, Tuple<Element, Element>>
        {
            {"ChampionLaurels", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"ColosseumHelmet", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistBoxingHelmet", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistBoxingHelmetPlus", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistMartialHead", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistMasterHead", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistVeteranHead", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"LunarFistHead", new Tuple<Element, Element>(Element.fighting, Element.dragon) },
        };
        public static Dictionary<string, Tuple<Element, Element>> _Chest = new Dictionary<string, Tuple<Element, Element>>
        {
            {"FistDefBody", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistPowerBody", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistSpeedBody", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"HighDefBody", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"HighPowerBody", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"HighSpeedBody", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"LunarFistBody", new Tuple<Element, Element>(Element.fighting, Element.dragon) },
        };
        public static Dictionary<string, Tuple<Element, Element>> _Leggings = new Dictionary<string, Tuple<Element, Element>>
        {
            {"FistDefLegs", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistPowerLegs", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"FistSpeedLegs", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"HighDefLegs", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"HighPowerLegs", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"HighSpeedLegs", new Tuple<Element, Element>(Element.fighting, Element.none) },
            {"LunarFistLegs", new Tuple<Element, Element>(Element.fighting, Element.dragon) },
        };
    }
}
