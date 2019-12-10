using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class WeaponOutEnemies
    {
        public static Dictionary<int, Tuple<Element, Element, Element, Element>> Type = new Dictionary<int, Tuple<Element, Element, Element, Element>>() { };
        public static Dictionary<string, Tuple<Element, Element, Element, Element>> _Type = new Dictionary<string, Tuple<Element, Element, Element, Element>>()
        {
            {"ComboBubble", new Tuple<Element, Element, Element, Element>(Element.water, Element.none, Element.levitate, Element.water) }
        };
    }
}
