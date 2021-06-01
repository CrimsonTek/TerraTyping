using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Attributes;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public static class WeaponOutAmmos
    {
        public static void Load()
        {
            Type = new Dictionary<int, ItemTypeInfo>();
            _Type = new Dictionary<string, ItemTypeInfo>()
            {
                {"SplinterShot", new ItemTypeInfo(Element.dark) },
                {"MeteorBreakshot", new ItemTypeInfo(Element.fire) },
                {"ScatterShot", new ItemTypeInfo(Element.fairy) },
            };
        }

        public static void Unload()
        {
            Type = null;
            _Type = null;
        }

        public static Dictionary<int, ItemTypeInfo> Type;
        public static Dictionary<string, ItemTypeInfo> _Type;
    }
}
