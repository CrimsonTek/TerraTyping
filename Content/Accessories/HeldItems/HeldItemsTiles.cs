using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Content.Accessories.HeldItems
{
    public class HeldItemsTiles : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            int chance = Main.expertMode ? 500 : 650;
            if (type == TileID.Stone && Main.rand.NextBool(chance))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16 + 8, j * 16 + 8, 0, 0, Mod.Find<ModItem>("HardStone").Type);
            }
        }
    }
}
