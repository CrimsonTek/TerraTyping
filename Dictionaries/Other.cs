using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Attributes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public static class OtherDict
    {
        public static void Load()
        {
            Type = new Dictionary<int, Element>
            {
                {0, Element.ground }, // falling
                {2, Element.fire }, // lava
                {3, Element.grass }, // vines
            };
        }

        public static void Unload()
        {
            Type = null;
        }

        public static Dictionary<int, Element> Type;
    }
}