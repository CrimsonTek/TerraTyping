using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerraTyping.Accessories.HeldItems
{
    public class HeldItemsPlayer : ModPlayer
    {
        private ModItem mysticWater;
        private ModItem fightersBelt;

        public Boost[] heldItemBoosts = new Boost[22];

        public ModItem MysticWater
        {
            get
            {
                if (mysticWater is null)
                {
                    mysticWater = Mod.Find<ModItem>("MysticWater");
                }
                return mysticWater;
            }
        }
        public ModItem FightersBelt
        {
            get
            {
                if (fightersBelt is null)
                {
                    fightersBelt = Mod.Find<ModItem>("FightersBelt");
                }
                return fightersBelt;
            }
        }

        public override void ResetEffects()
        {
            for (int i = 0; i < heldItemBoosts.Length; i++)
            {
                heldItemBoosts[i] = new Boost(1, string.Empty);
            }
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (!attempt.inHoney && !attempt.inLava && Main.rand.NextBool(45))
            {
                itemDrop = MysticWater.Type;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            int chance = 300;
            if (crit)
            {
                if (Main.expertMode)
                    chance = 200;
                else if (Main.masterMode)
                    chance = 150;

                if (Main.rand.NextBool(chance))
                {
                    Item.NewItem(new EntitySource_Misc("OnHitNPC"), target.getRect(), FightersBelt.Type);
                }
            }
        }
    }
}
