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

namespace TerraTyping.Items
{
    public class Other
    {
        public static Dictionary<int, Element.Type> Type = new Dictionary<int, Element.Type>
        {
            {0, Element.Type.ground }, // falling
            {2, Element.Type.fire }, // lava
            {3, Element.Type.grass }, // vines
        };
    }
}