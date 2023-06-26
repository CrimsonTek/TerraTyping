﻿using Terraria;
using Terraria.ModLoader;
using TerraTyping.Content.Accessories.AbilityAccessories;
using TerraTyping.Core;

namespace TerraTyping.Content.Accessories.Testing
{
    public class MotorDrive : ModItem, IAbilityAccessory
    {
        public Ability GivenAbility => Ability.MotorDrive;

        public override void SetDefaults()
        {
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            AccessoriesUtil.UpdateEquipsUtil(this, player);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return AccessoriesUtil.CanEquip2(incomingItem);
        }
    }
}
