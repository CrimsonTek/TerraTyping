using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Accessories.HeldItems
{
    public abstract class HeldItems : ModItem
    {
        public virtual string DispName { get; set; }
        public virtual Element Element { get; set; }
        public virtual int Rarity { get; set; }
        public virtual int Value { get; set; }
        public virtual float Boost { get; set; }
        public virtual Point Size { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(DispName);
            Tooltip.SetDefault($"Boosts {Element} type moves by {Math.Round((Boost - 1) * 100)}%");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = Rarity;
            Item.value = Value;
            Item.width = Size.X;
            Item.height = Size.Y;
        }
        public override void UpdateEquip(Player player)
        {
            HeldItemsPlayer hiPlayer = player.GetModPlayer<HeldItemsPlayer>();
            hiPlayer.heldItemBoosts[(int)Element] = new Boost(Boost, DispName);
        }
    }

    public class SilkScarf : HeldItems
    {
        public override string DispName => "Silk Scarf";
        public override Element Element => Element.normal;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(36, 32);
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
        public override int Rarity => 3;
        public override int Value => 400000;
        public override float Boost => 1.15f;
        public override Point Size => new Point(32, 32);
    }
    public class MysticWater : HeldItems
    {
        public override string DispName => "Mystic Water";
        public override Element Element => Element.water;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(30, 36);
    }
    public class Magnet : HeldItems
    {
        public override string DispName => "Magnet";
        public override Element Element => Element.electric;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(36, 38);
    }
    public class MiracleSeed : HeldItems
    {
        public override string DispName => "Miracle Seed";
        public override Element Element => Element.grass;
        public override int Rarity => 2;
        public override int Value => 250000;
        public override float Boost => 1.12f;
        public override Point Size => new Point(32, 32);
    }
    public class NeverMeltIce : HeldItems
    {
        public override string DispName => "Never-Melt Ice";
        public override Element Element => Element.ice;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
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
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 36);
    }
    public class PoisonBarb : HeldItems
    {
        public override string DispName => "Poison Barb";
        public override Element Element => Element.poison;
        public override int Rarity => 2;
        public override int Value => 250000;
        public override float Boost => 1.12f;
        public override Point Size => new Point(36, 36);
    }
    public class SoftSand : HeldItems
    {
        public override string DispName => "Soft Sand";
        public override Element Element => Element.ground;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(30, 28);
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
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
    }
    public class TwistedSpoon : HeldItems
    {
        public override string DispName => "Twisted Spoon";
        public override Element Element => Element.psychic;
        public override int Rarity => 2;
        public override int Value => 250000;
        public override float Boost => 1.12f;
        public override Point Size => new Point(34, 32);
    }
    public class SilverPowder : HeldItems
    {
        public override string DispName => "Silver Powder";
        public override Element Element => Element.bug;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(38, 24);
    }
    public class HardStone : HeldItems
    {
        public override string DispName => "Hard Stone";
        public override Element Element => Element.rock;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
    }
    public class SpellTag : HeldItems
    {
        public override string DispName => "Spell Tag";
        public override Element Element => Element.ghost;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 36);
    }
    public class DragonFang : HeldItems
    {
        public override string DispName => "Dragon Fang";
        public override Element Element => Element.dragon;
        public override int Rarity => 4;
        public override int Value => 550000;
        public override float Boost => 1.18f;
        public override Point Size => new Point(28, 32);
    }
    public class BlackGlasses : HeldItems
    {
        public override string DispName => "Black Glasses";
        public override Element Element => Element.dark;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(38, 26);
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
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 38);
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBar, 20);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class ArcaneBall : HeldItems
    {
        public override string DispName => "Arcane Ball";
        public override Element Element => Element.fairy;
        public override int Rarity => 4;
        public override int Value => 550000;
        public override float Boost => 1.18f;
        public override Point Size => new Point(32, 32);
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
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(24, 38);
    }
    public class DustySkull : HeldItems
    {
        public override string DispName => "DispName";
        public override Element Element => Element.bone;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
    }
}
