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
using TerraTyping.DataTypes.Structs;

namespace TerraTyping
{
    public class ArmorTyping : GlobalItem
    {
        public override void UpdateEquip(Item item, Player player)
        {
            //PlayerTyping armorPlayer = player.GetModPlayer<PlayerTyping>();

            //if (item.headSlot != -1 && Armors.Helmet.ContainsKey(item.type)) 
            //{
            //    armorPlayer.typeHead = Armors.Type[item.type];
            //}
            //else if (item.bodySlot != -1 && Armors.Chest.ContainsKey(item.type)) 
            //{
            //    armorPlayer.typeBody = Armors.Type[item.type];
            //}
            //else if (item.legSlot != -1 && Armors.Leggings.ContainsKey(item.type)) 
            //{
            //    armorPlayer.typeLegs = Armors.Type[item.type]; 
            //}
            //else if (item.wingSlot != -1) 
            //{
            //    armorPlayer.wings = true; 
            //}

            //if (armorPlayer.typeHead.Primary == armorPlayer.typeBody.Primary && armorPlayer.typeBody.Primary == armorPlayer.typeLegs.Primary && armorPlayer.typeHead.Secondary == armorPlayer.typeBody.Secondary && armorPlayer.typeBody.Secondary == armorPlayer.typeLegs.Secondary)
            //{
            //    armorPlayer.newTypeSet = new TypeSet(armorPlayer.typeBody.Primary, armorPlayer.typeBody.Secondary);
            //}
            //else
            //{
            //    armorPlayer.newTypeSet = new TypeSet(Element.normal, Element.none);
            //};
        }
    }
}
