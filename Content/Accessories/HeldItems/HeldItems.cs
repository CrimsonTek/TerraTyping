using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerraTyping.Core;

namespace TerraTyping.Content.Accessories.HeldItems
{
    public abstract class HeldItems : ModItem
    {
        public virtual string DispName { get; set; }
        public virtual Element Element { get; set; }
        public virtual int Rarity { get; set; }
        public virtual int Value { get; set; }
        public virtual float Boost { get; set; }
        public virtual Point Size { get; set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Element.ToString(), $"{(Boost - 1) * 100:0.##}");

        public override void UpdateEquip(Player player)
        {
            HeldItemsPlayer hiPlayer = player.GetModPlayer<HeldItemsPlayer>();
            hiPlayer.heldItemBoosts[(int)Element] = new Boost(Boost, Item.Name);
        }
    }

    public class SilkScarf : HeldItems
    {
        public override string DispName => "Silk Scarf";
        public override Element Element => Element.normal;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 36;
            Item.height = 32;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
    public class Charcoal : HeldItems
    {
        public override string DispName => "Charcoal";
        public override Element Element => Element.fire;
        public override float Boost => 1.15f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = 400000;
            Item.width = 32;
            Item.height = 32;
        }
    }
    public class MysticWater : HeldItems
    {
        public override string DispName => "Mystic Water";
        public override Element Element => Element.water;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 30;
            Item.height = 36;
        }
    }
    public class Magnet : HeldItems
    {
        public override string DispName => "Magnet";
        public override Element Element => Element.electric;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 36;
            Item.height = 38;
        }
    }
    public class MiracleSeed : HeldItems
    {
        public override string DispName => "Miracle Seed";
        public override Element Element => Element.grass;
        public override float Boost => 1.12f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 250000;
            Item.width = 32;
            Item.height = 32;
        }
    }
    public class NeverMeltIce : HeldItems
    {
        public override string DispName => "Never-Melt Ice";
        public override Element Element => Element.ice;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 32;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 50);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
    public class FightersBelt : HeldItems
    {
        public override string DispName => "Fighter's Belt";
        public override Element Element => Element.fighting;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 36;
        }
    }
    public class PoisonBarb : HeldItems
    {
        public override string DispName => "Poison Barb";
        public override Element Element => Element.poison;
        public override float Boost => 1.12f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 250000;
            Item.width = 36;
            Item.height = 36;
        }
    }
    public class SoftSand : HeldItems
    {
        public override string DispName => "Soft Sand";
        public override Element Element => Element.ground;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 30;
            Item.height = 28;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SandBlock, 25);
            recipe.AddIngredient(ItemID.FossilOre, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class SharpBeak : HeldItems
    {
        public override string DispName => "Sharp Beak";
        public override Element Element => Element.flying;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 32;
        }
    }
    public class TwistedSpoon : HeldItems
    {
        public override string DispName => "Twisted Spoon";
        public override Element Element => Element.psychic;
        public override float Boost => 1.12f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 250000;
            Item.width = 34;
            Item.height = 32;
        }
    }
    public class SilverPowder : HeldItems
    {
        public override string DispName => "Silver Powder";
        public override Element Element => Element.bug;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 38;
            Item.height = 24;
        }
    }
    public class HardStone : HeldItems
    {
        public override string DispName => "Hard Stone";
        public override Element Element => Element.rock;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 32;
        }
    }
    public class SpellTag : HeldItems
    {
        public override string DispName => "Spell Tag";
        public override Element Element => Element.ghost;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 36;
        }
    }
    public class DragonFang : HeldItems
    {
        public override string DispName => "Dragon Fang";
        public override Element Element => Element.dragon;
        public override float Boost => 1.18f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 550000;
            Item.width = 28;
            Item.height = 32;
        }
    }
    public class BlackGlasses : HeldItems
    {
        public override string DispName => "Black Glasses";
        public override Element Element => Element.dark;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 38;
            Item.height = 26;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sunglasses, 1);
            recipe.AddIngredient(ItemID.BlackDye, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
    public class MetalCoat : HeldItems
    {
        public override string DispName => "Metal Coat";
        public override Element Element => Element.steel;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 38;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 20);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class ArcaneBall : HeldItems
    {
        public override string DispName => "Arcane Ball";
        public override Element Element => Element.fairy;
        public override float Boost => 1.18f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 550000;
            Item.width = 32;
            Item.height = 32;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PixieDust, 35);
            recipe.AddIngredient(ItemID.UnicornHorn, 4);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
    public class BloodyHeart : HeldItems
    {
        public override string DispName => "Bloody Heart";
        public override Element Element => Element.blood;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 24;
            Item.height = 38;
        }
    }
    public class DustySkull : HeldItems
    {
        public override string DispName => "Dusty Skull";
        public override Element Element => Element.bone;
        public override float Boost => 1.1f;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000;
            Item.width = 32;
            Item.height = 32;
        }
    }
}
