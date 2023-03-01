using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Accessories.HeldItems
{
    public class HeldItemsGlobalItem : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PoisonBarb").Type));
        }
    }
}
