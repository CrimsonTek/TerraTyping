using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Accessories.HeldItems
{
    public class HeldItemsPlayer : ModPlayer
    {
        public Boost[] heldItemBoosts = new Boost[22];

        public override void ResetEffects()
        {
            for (int i = 0; i < heldItemBoosts.Length; i++)
            {
                heldItemBoosts[i] = new Boost(1, string.Empty);
            }
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (Main.rand.Next(45) == 0 && liquidType == 0)
            {
                caughtType = mod.ItemType("MysticWater");
                junk = false;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            int chance = 300;
            if (crit)
            {
                if (Main.expertMode)
                    chance = 200;
                if (Main.rand.Next(chance) == 0)
                {
                    Item.NewItem(target.getRect(), mod.ItemType("FightersBelt"));
                }
            }
        }
    }
    public class HeldItemsNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void NPCLoot(NPC npc)
        {
            Player player = Main.player[npc.FindClosestPlayer()];

            // charcoal
            if (player.ZoneUnderworldHeight)
            {
                int chance = 150;
                if (Main.expertMode)
                    chance = 100;
                if (Main.rand.Next(chance) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Charcoal"));
                }
            }

            // magnet
            if (player.ZoneRockLayerHeight && !(player.ZoneCorrupt || player.ZoneCrimson || player.ZoneDesert || player.ZoneDungeon || player.ZoneGlowshroom || player.ZoneHoly || player.ZoneJungle || player.ZoneSnow || player.ZoneUndergroundDesert || player.ZoneUnderworldHeight))
            {
                int chance = 175;
                if (Main.expertMode)
                    chance = 125;
                if (Main.rand.Next(chance) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Magnet"));
                }
            }

            // twisted spoon
            if (player.ZoneDungeon)
            {
                int chance = 150;
                if (Main.expertMode)
                    chance = 100;
                if (Main.rand.Next(chance) == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType("TwistedSpoon"));
            }

            // silver powder
            if (npc.catchItem != 0)
            {
                int chance = 50;
                if (Main.expertMode)
                    chance = 30;
                if (Main.rand.Next(chance) == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType("SilverPowder"));
            }

            // ghost tag
            if (Main.moonPhase == 0)
            {
                int chance = 75;
                if (Main.expertMode)
                    chance = 50;
                if (Main.rand.Next(chance) == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType("GhostTag"));
            }

            // bloody heart
            if (Main.bloodMoon) // && elementHelper.Any(npc, Element.blood)
            {
                int chance = 133;
                if (Main.expertMode)
                    chance = 100;
                if (Main.rand.Next(chance) == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType("BloodyHeart"));
            }

            int sharpBreakChance = 35;
            int dustySkullChance = 150;
            if (Main.expertMode)
            {
                sharpBreakChance = 25;
                dustySkullChance = 125;
            }
            switch (npc.type)
            {
                case NPCID.QueenBee:
                    if (!Main.expertMode)
                        Item.NewItem(npc.getRect(), mod.ItemType("PoisonBarb"));
                    break;
                case NPCID.Vulture:
                    if (Main.rand.Next(sharpBreakChance) == 0)
                        Item.NewItem(npc.getRect(), mod.ItemType("SharpBeak"));
                    break;
                case NPCID.WyvernHead:
                case NPCID.PigronCorruption:
                case NPCID.PigronCrimson:
                case NPCID.PigronHallow:
                    if (Main.rand.Next(10) == 0)
                        Item.NewItem(npc.getRect(), mod.ItemType("DragonFang"));
                    break;
                case NPCID.Skeleton:
                case NPCID.SmallSkeleton:
                case NPCID.BigSkeleton:
                case NPCID.HeadacheSkeleton:
                case NPCID.SmallHeadacheSkeleton:
                case NPCID.BigHeadacheSkeleton:
                case NPCID.MisassembledSkeleton:
                case NPCID.SmallMisassembledSkeleton:
                case NPCID.BigMisassembledSkeleton:
                case NPCID.PantlessSkeleton:
                case NPCID.SmallPantlessSkeleton:
                case NPCID.BigPantlessSkeleton:
                case NPCID.SkeletonTopHat:
                case NPCID.SkeletonAstonaut:
                case NPCID.SkeletonAlien:
                    if (Main.rand.Next(dustySkullChance) == 0)
                        Item.NewItem(npc.getRect(), mod.ItemType("DustySkull"));
                    break;
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Dryad)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("MiracleSeed"));
                nextSlot++;
            }
        }
    }
    public class HeldItemsItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (arg == ItemID.QueenBeeBossBag)
            {
                player.QuickSpawnItem(mod.ItemType("PoisonBarb"));
            }
        }
    }
    public class HeldItemsTiles : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            int chance = 650;
            if (Main.expertMode)
                chance = 500;
            if (type == TileID.Stone && Main.rand.Next(chance) == 0)
                Item.NewItem(i * 16 + 8, j * 16 + 8, 0, 0, mod.ItemType("HardStone"));
            return base.Drop(i, j, type);
        }
    }

    public abstract class HeldItems : ModItem
    {
        public virtual string DispName { get; set; }
        public virtual Element Type { get; set; }
        public virtual int Rarity { get; set; }
        public virtual int Value { get; set; }
        public virtual float Boost { get; set; }
        public virtual Point Size { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(DispName);
            Tooltip.SetDefault($"Boosts {Type} type moves by {Math.Round((Boost - 1) * 100)}%");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.rare = Rarity;
            item.value = Value;
            item.width = Size.X;
            item.height = Size.Y;
        }
        public override void UpdateEquip(Player player)
        {
            HeldItemsPlayer hiPlayer = player.GetModPlayer<HeldItemsPlayer>();
            hiPlayer.heldItemBoosts[(int)Type] = new Boost(Boost, DispName);
        }
    }

    public class SilkScarf : HeldItems
    {
        public override string DispName => "Silk Scarf";
        public override Element Type => Element.normal;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(36, 32);
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class Charcoal : HeldItems
    {
        public override string DispName => "Charcoal";
        public override Element Type => Element.fire;
        public override int Rarity => 3;
        public override int Value => 400000;
        public override float Boost => 1.15f;
        public override Point Size => new Point(32, 32);
    }
    public class MysticWater : HeldItems
    {
        public override string DispName => "Mystic Water";
        public override Element Type => Element.water;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(30, 36);
    }
    public class Magnet : HeldItems
    {
        public override string DispName => "Magnet";
        public override Element Type => Element.electric;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(36, 38);
    }
    public class MiracleSeed : HeldItems
    {
        public override string DispName => "Miracle Seed";
        public override Element Type => Element.grass;
        public override int Rarity => 2;
        public override int Value => 250000;
        public override float Boost => 1.12f;
        public override Point Size => new Point(32, 32);
    }
    public class NeverMeltIce : HeldItems
    {
        public override string DispName => "Never-Melt Ice";
        public override Element Type => Element.ice;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 50);
            recipe.AddTile(TileID.IceMachine);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class FightersBelt : HeldItems
    {
        public override string DispName => "Fighter's Belt";
        public override Element Type => Element.fighting;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 36);
    }
    public class PoisonBarb : HeldItems
    {
        public override string DispName => "Poison Barb";
        public override Element Type => Element.poison;
        public override int Rarity => 2;
        public override int Value => 250000;
        public override float Boost => 1.12f;
        public override Point Size => new Point(36, 36);
    }
    public class SoftSand : HeldItems
    {
        public override string DispName => "Soft Sand";
        public override Element Type => Element.ground;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(30, 28);
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SandBlock, 25);
            recipe.AddIngredient(ItemID.FossilOre, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class SharpBeak : HeldItems
    {
        public override string DispName => "Sharp Beak";
        public override Element Type => Element.flying;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
    }
    public class TwistedSpoon : HeldItems
    {
        public override string DispName => "Twisted Spoon";
        public override Element Type => Element.psychic;
        public override int Rarity => 2;
        public override int Value => 250000;
        public override float Boost => 1.12f;
        public override Point Size => new Point(34, 32);
    }
    public class SilverPowder : HeldItems
    {
        public override string DispName => "Silver Powder";
        public override Element Type => Element.bug;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(38, 24);
    }
    public class HardStone : HeldItems
    {
        public override string DispName => "Hard Stone";
        public override Element Type => Element.rock;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
    }
    public class SpellTag : HeldItems
    {
        public override string DispName => "Spell Tag";
        public override Element Type => Element.ghost;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 36);
    }
    public class DragonFang : HeldItems
    {
        public override string DispName => "Dragon Fang";
        public override Element Type => Element.dragon;
        public override int Rarity => 4;
        public override int Value => 550000;
        public override float Boost => 1.18f;
        public override Point Size => new Point(28, 32);
    }
    public class BlackGlasses : HeldItems
    {
        public override string DispName => "Black Glasses";
        public override Element Type => Element.dark;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(38, 26);
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sunglasses, 1);
            recipe.AddIngredient(ItemID.BlackDye, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class MetalCoat : HeldItems
    {
        public override string DispName => "Metal Coat";
        public override Element Type => Element.steel;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 38);
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBar, 20);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class ArcaneBall : HeldItems
    {
        public override string DispName => "Arcane Ball";
        public override Element Type => Element.fairy;
        public override int Rarity => 4;
        public override int Value => 550000;
        public override float Boost => 1.18f;
        public override Point Size => new Point(32, 32);
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PixieDust, 35);
            recipe.AddIngredient(ItemID.UnicornHorn, 4);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class BloodyHeart : HeldItems
    {
        public override string DispName => "Bloody Heart";
        public override Element Type => Element.blood;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(24, 38);
    }
    public class DustySkull : HeldItems
    {
        public override string DispName => "DispName";
        public override Element Type => Element.bone;
        public override int Rarity => 1;
        public override int Value => 100000;
        public override float Boost => 1.1f;
        public override Point Size => new Point(32, 32);
    }
}
