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
    public class WeaponOutEnemies
    {
        public static void Load()
        {
            Type = new Dictionary<int, NPCTypeInfo>();
            _Type = new Dictionary<string, NPCTypeInfo>()
            {
                {"ComboBubble", new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Levitate)) }
            };
        }

        public static void Unload()
        {
            Type = null;
            _Type = null;
        }

        public static Dictionary<int, NPCTypeInfo> Type;
        public static Dictionary<string, NPCTypeInfo> _Type;
    }
}
