using Terraria;
using Terraria.ModLoader;
using TerraTyping.Content.Accessories.AbilityAccessories;
using TerraTyping.Core;

namespace TerraTyping.Content.Accessories.Testing
{
    public class SandForceAccessory : ModItem, IAbilityAccessory
    {
        public Ability GivenAbility => Ability.SandForce;

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
