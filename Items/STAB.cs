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

namespace TerraTyping.Items
{
    public class STAB : GlobalItem
    {
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            if (Items.Type.ContainsKey(item.type))
            {
                if (ArmorPlayer.typeHead.Item1 == ArmorPlayer.typeBody.Item1 && ArmorPlayer.typeBody.Item1 == ArmorPlayer.typeLegs.Item1 && ArmorPlayer.typeHead.Item2 == ArmorPlayer.typeBody.Item2 && ArmorPlayer.typeBody.Item2 == ArmorPlayer.typeLegs.Item2)
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(ArmorPlayer.typeBody.Item1, ArmorPlayer.typeBody.Item2);
                }
                else
                {
                    ArmorPlayer.typeSet = new Tuple<Element.Type, Element.Type>(Element.Type.normal, Element.Type.none);
                };
                float STAB = 1.0f; if (ArmorPlayer.typeSet.Item1 == Items.Type[item.type] || ArmorPlayer.typeSet.Item2 == Items.Type[item.type])
                { STAB = Config.STAB; } // effects the stab damage multiplier
                mult = STAB;
            }
        }
    }
}
