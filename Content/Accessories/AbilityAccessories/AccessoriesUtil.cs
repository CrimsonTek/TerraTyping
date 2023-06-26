using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Content.Accessories.AbilityAccessories
{
    public static class AccessoriesUtil
    {
        public static void UpdateEquipsUtil(ModItem modItem, Player player)
        {
            player.GetModPlayer<PlayerTyping>().AbilityAccessory = modItem;
        }

        public static bool CanEquip(Player player, int slot)
        {
            for (int i = 3; i < player.armor.Length; i++)
            {
                if (i == slot) continue;

                Item item = player.armor[i];
                if (item != null && item.active)
                {
                    if (item.ModItem != null && item.ModItem is IAbilityAccessory)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public static bool CanEquip2(Item incomingItem)
        {
            if (incomingItem?.ModItem is IAbilityAccessory)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
