using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Accessories.AbilityAccessories;
using TerraTyping.DataTypes;

namespace TerraTyping.Accessories.Testing
{
    public class SandForceAccessory : ModItem, IAbilityAccessory
    {
        public AbilityID GivenAbility => AbilityID.SandForce;

        public override void SetDefaults()
        {
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            AccessoriesUtil.UpdateEquipsUtil(this, player);
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            return AccessoriesUtil.CanEquip(player, slot);
        }
    }
}
