using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Accessories.AbilityAccessories;
using TerraTyping.DataTypes;

namespace TerraTyping.Accessories.Testing
{
    public class WaterCompaction : ModItem, IAbilityAccessory
    {
        public AbilityID GivenAbility => AbilityID.WaterCompaction;

        public override void SetDefaults()
        {
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            AccessoriesUtil.UpdateEquipsUtil(this, player);
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return AccessoriesUtil.CanEquip(player, slot);
        }
    }
}
