using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Accessories.Testing;

namespace TerraTyping.Accessories.AbilityAccessories
{
    public static class AccessoriesUtil
    {
        public static void UpdateEquipsUtil(ModItem modItem, Player player)
        {
            player.GetModPlayer<PlayerTyping>().AbilityAccessory = modItem;
        }

        public static bool CanEquip(Player player, int slot)
        {
            //for (int i = 0; i < player.armor.Length; i++)
            //{
            //    if (i == slot) continue;

            //    Item item = player.armor[i];
            //    if (item != null && item.active)
            //    {
            //        if (item.modItem != null && item.modItem is IAbilityAccessory)
            //        {
            //            return false;
            //        }
            //    }
            //}
            return true;

            //return !player.armor.Any((item) =>
            //{
            //    if (item != null && item.active)
            //    {
            //        Main.NewText(item.ToString());
            //        if (item.modItem != null && item.modItem is IAbilityAccessory abilityAccessory)
            //        {
            //            return true;
            //        }
            //    }
            //    return false;
            //});
        }
    }
}
