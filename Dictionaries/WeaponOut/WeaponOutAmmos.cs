using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class WeaponOutAmmos
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>() { };
        public static Dictionary<string, Element> _Type = new Dictionary<string, Element>()
        {
            {"SplinterShot", Element.dark },
            {"MeteorBreakshot", Element.fire },
            {"ScatterShot", Element.fairy },
        };
    }
}
