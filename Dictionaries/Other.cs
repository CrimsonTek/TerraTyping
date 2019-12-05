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

namespace TerraTyping
{
    public class OtherDict
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>
        {
            {0, Element.ground }, // falling
            {2, Element.fire }, // lava
            {3, Element.grass }, // vines
        };
    }
}