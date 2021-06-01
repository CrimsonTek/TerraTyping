using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class STAB : GlobalItem
    {
        //public override bool InstancePerEntity => true;
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            mult = Calc.Stab(new ItemWrapper(item, player), new PlayerWrapper(player));
        }
    }
}
