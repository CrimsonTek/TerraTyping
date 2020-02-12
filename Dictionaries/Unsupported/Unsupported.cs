using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping
{
    public class UnsupportedAmmos
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>();
    }
    public class UnsupportedArmors
    {
        public static Dictionary<int, Tuple<Element, Element>> Type = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<int, Tuple<Element, Element>> Helmet = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<int, Tuple<Element, Element>> Chest = new Dictionary<int, Tuple<Element, Element>>() { };
        public static Dictionary<int, Tuple<Element, Element>> Leggings = new Dictionary<int, Tuple<Element, Element>>() { };
    }
    public class UnsupportedEnemies
    {
        public static Dictionary<int, Tuple<Element, Element, Element, Element>> Type = new Dictionary<int, Tuple<Element, Element, Element, Element>>() { };
    }
    public class UnsupportedItems
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>() { };
    }
    public class UnsupportedProjectiles
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>() { };
    }
}
