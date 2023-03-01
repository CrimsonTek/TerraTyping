using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.DataTypes.Structs;

namespace TerraTyping.Dictionaries;

//public class Items : ILoadable
//{
//    private ItemTypeInfo[] types;
//    private SpecialTooltips[] specialTooltips;

//    public static Items Instance { get; private set; }

//    void ILoadable.Load(Mod mod)
//    {
//        Instance = this;
//    }

//    void ILoadable.Unload()
//    {
//        Instance = null;
//    }

//    public static ItemTypeInfo GetInfo(int type)
//    {
//        if (type < Instance.types.Length && type >= 0)
//        {
//            return Instance.types[type];
//        }
//        else
//        {
//            return default;
//        }
//    }

//    public static SpecialTooltips GetSpecialTooltips(int type)
//    {
//        if (type < Instance.types.Length && type >= 0)
//        {
//            return Instance.specialTooltips[type];
//        }
//        else
//        {
//            return default;
//        }
//    }

//    public static void SetupTypes()
//    {
//        Instance.types = new ItemTypeInfo[ItemLoader.ItemCount];
//        Instance.specialTooltips = new SpecialTooltips[ItemLoader.ItemCount];
//        Instance.LoadWeapons();
//        Instance.LoadAmmos();
//        Instance.LoadMiscItems();

//        int loaded = 0;
//        for (int i = 0; i < Instance.types.Length; i++)
//        {
//            ItemTypeInfo typeInfo = Instance.types[i];
//            if (typeInfo is null)
//            {
//                Instance.types[i] = ItemTypeInfo.Default;
//            }
//            else
//            {
//                loaded++;
//            }
//        }
//    }

//    private void LoadWeapons()
//    {
//        LoadWeapon(ItemID.HolyWater, Element.water); // Holy Water
//        LoadWeapon(ItemID.UnholyWater, Element.water); // Unholy Water
//        LoadWeapon(ItemID.BloodWater, Element.water); // Blood Water
//        LoadWeapon(ItemID.Cannonball, Element.normal); // Cannonball
//        LoadWeapon(ItemID.FlareGun, Element.fire); // Flare Gun
//        LoadWeapon(ItemID.ExplosiveBunny, Element.normal); // Explosive Bunny
//        LoadWeapon(ItemID.Vilethorn, Element.dark); // Vilethorn
//        LoadWeapon(ItemID.FlowerofFire, Element.fire); // Flower of Fire
//        LoadWeapon(ItemID.MagicMissile, Element.psychic); // Magic Missile
//        LoadWeapon(ItemID.SpaceGun, Element.electric); // Space Gun
//        LoadWeapon(ItemID.AquaScepter, Element.water); // Aqua Scepter
//        LoadWeapon(ItemID.WaterBolt, Element.water); // Water Bolt
//        LoadWeapon(ItemID.Flamelash, Element.fire); // Flamelash
//        LoadWeapon(ItemID.DemonScythe, Element.dark); // Demon Scythe
//        LoadWeapon(ItemID.MagicalHarp, Element.normal); // Magical Harp
//        LoadWeapon(ItemID.RainbowRod, Element.fairy); // Rainbow Rod
//        LoadWeapon(ItemID.IceRod, Element.ice); // Ice Rod
//        LoadWeapon(ItemID.LaserRifle, Element.electric); // Laser Rifle
//        LoadWeapon(ItemID.MagicDagger, Element.fighting); // Magic Dagger
//        LoadWeapon(ItemID.CrystalStorm, Element.rock); // Crystal Storm
//        LoadWeapon(ItemID.CursedFlames, Element.dark); // Cursed Flames
//        LoadWeapon(ItemID.UnholyTrident, Element.dark); // Unholy Trident
//        LoadWeapon(ItemID.FrostStaff, Element.ice); // Frost Staff
//        LoadWeapon(ItemID.AmethystStaff, Element.psychic); // Amethyst Staff
//        LoadWeapon(ItemID.TopazStaff, Element.psychic); // Topaz Staff
//        LoadWeapon(ItemID.SapphireStaff, Element.psychic); // Sapphire Staff
//        LoadWeapon(ItemID.EmeraldStaff, Element.psychic); // Emerald Staff
//        LoadWeapon(ItemID.RubyStaff, Element.psychic); // Ruby Staff
//        LoadWeapon(ItemID.DiamondStaff, Element.rock); // Diamond Staff
//        LoadWeapon(ItemID.AmberStaff, Element.psychic); // Amber Staff
//        LoadWeapon(ItemID.NettleBurst, Element.grass); // Nettle Burst
//        LoadWeapon(ItemID.BeeGun, Element.bug); // Bee Gun
//        LoadWeapon(ItemID.WaspGun, Element.bug); // Wasp Gun
//        LoadWeapon(ItemID.LeafBlower, Element.grass); // Leaf Blower
//        LoadWeapon(ItemID.NimbusRod, Element.water); // Nimbus Rod
//        LoadWeapon(ItemID.CrimsonRod, Element.blood); // Crimson Rod
//        LoadWeapon(ItemID.RainbowGun, Element.fairy); // Rainbow Gun
//        LoadWeapon(ItemID.FlowerofFrost, Element.ice); // Flower of Frost
//        LoadWeapon(ItemID.MagnetSphere, Element.electric); // Magnet Sphere
//        LoadWeapon(ItemID.HeatRay, Element.fire); // Heat Ray
//        LoadWeapon(ItemID.StaffofEarth, Element.ground); // Staff of Earth
//        LoadWeapon(ItemID.PoisonStaff, Element.poison); // Poison Staff
//        LoadWeapon(ItemID.BookofSkulls, Element.bone); // Book of Skulls
//        LoadWeapon(ItemID.GoldenShower, Element.blood); // Golden Shower
//        LoadWeapon(ItemID.ShadowbeamStaff, Element.dark); // Shadowbeam Staff
//        LoadWeapon(ItemID.InfernoFork, Element.fire); // Inferno Fork
//        LoadWeapon(ItemID.SpectreStaff, Element.ghost); // Spectre Staff
//        LoadWeapon(ItemID.BatScepter, Element.flying); // Bat Scepter
//        LoadWeapon(ItemID.Razorpine, Element.grass); // Razorpine
//        LoadWeapon(ItemID.BlizzardStaff, Element.ice); // Blizzard Staff
//        LoadWeapon(ItemID.VenomStaff, Element.poison); // Venom Staff
//        LoadWeapon(ItemID.RazorbladeTyphoon, Element.water); // Razorblade Typhoon
//        LoadWeapon(ItemID.BubbleGun, Element.water); // Bubble Gun
//        LoadWeapon(ItemID.MeteorStaff, Element.rock); // Meteor Staff
//        LoadWeapon(ItemID.LaserMachinegun, Element.electric); // Laser Machinegun
//        LoadWeapon(ItemID.ChargedBlasterCannon, Element.electric); // Charged Blaster Cannon
//        LoadWeapon(ItemID.SoulDrain, Element.blood); // Life Drain
//        LoadWeapon(ItemID.ClingerStaff, Element.dark); // Clinger Staff
//        LoadWeapon(ItemID.CrystalVileShard, Element.rock); // Crystal Vile Shard
//        LoadWeapon(ItemID.ShadowFlameHexDoll, Element.ghost); // Shadowflame Hex Doll
//        LoadWeapon(ItemID.WandofSparking, Element.fire); // Wand of Sparking
//        LoadWeapon(ItemID.ToxicFlask, Element.poison); // Toxic Flask
//        LoadWeapon(ItemID.CrystalSerpent, Element.fairy); // Crystal Serpent
//        LoadWeapon(ItemID.MedusaHead, Element.rock); // Medusa Head
//        LoadWeapon(ItemID.NebulaArcanum, Element.psychic); // Nebula Arcanum
//        LoadWeapon(ItemID.LastPrism, Element.electric); // Last Prism
//        LoadWeapon(ItemID.NebulaBlaze, Element.psychic); // Nebula Blaze
//        LoadWeapon(ItemID.LunarFlareBook, Element.dark); // Lunar Flare
//        LoadWeapon(ItemID.SpiritFlame, Element.ghost); // Spirit Flame
//        LoadWeapon(ItemID.SkyFracture, Element.flying); // Sky Fracture
//        LoadWeapon(ItemID.BookStaff, Element.psychic); // Tome of Infinite Wisdom
//        LoadWeapon(ItemID.ApprenticeStaffT3, Element.fire); // Betsy's Wrath
//        LoadWeapon(ItemID.ThunderStaff, Element.electric); // Thunder Zapper
//        LoadWeapon(ItemID.SharpTears, Element.blood); // Blood Thorn
//        LoadWeapon(ItemID.ZapinatorGray, Element.electric); // Gray Zapinator
//        LoadWeapon(ItemID.ZapinatorOrange, Element.electric); // Orange Zapinator
//        LoadWeapon(ItemID.SparkleGuitar, Element.normal); // Stellar Tune
//        LoadWeapon(ItemID.FairyQueenMagicItem, Element.fairy); // Nightglow
//        LoadWeapon(ItemID.PrincessWeapon, Element.fairy); // Resonance Scepter
//        LoadWeapon(ItemID.WeatherPain, Element.flying); // Weather Pain
//        LoadWeapon(ItemID.IronPickaxe, Element.steel); // Iron Pickaxe
//        LoadWeapon(ItemID.IronBroadsword, Element.steel); // Iron Broadsword
//        LoadWeapon(ItemID.IronHammer, Element.steel); // Iron Hammer
//        LoadWeapon(ItemID.IronAxe, Element.steel); // Iron Axe
//        LoadWeapon(ItemID.WoodenSword, Element.normal); // Wooden Sword
//        LoadWeapon(ItemID.WarAxeoftheNight, Element.dark); // War Axe of the Night
//        LoadWeapon(ItemID.LightsBane, Element.dark); // Light's Bane
//        LoadWeapon(ItemID.Starfury, Element.flying); // Starfury
//        LoadWeapon(ItemID.NightmarePickaxe, Element.dark); // Nightmare Pickaxe
//        LoadWeapon(ItemID.TheBreaker, Element.dark); // The Breaker
//        LoadWeapon(ItemID.FieryGreatsword, Element.fire); // Fiery Greatsword
//        LoadWeapon(ItemID.MoltenPickaxe, Element.fire); // Molten Pickaxe
//        LoadWeapon(ItemID.Muramasa, Element.fighting); // Muramasa
//        LoadWeapon(ItemID.BreathingReed, Element.water); // Breathing Reed
//        LoadWeapon(ItemID.BladeofGrass, Element.grass); // Blade of Grass
//        LoadWeapon(ItemID.WoodenHammer, Element.normal); // Wooden Hammer
//        LoadWeapon(ItemID.BluePhaseblade, Element.electric); // Blue Phaseblade
//        LoadWeapon(ItemID.RedPhaseblade, Element.electric); // Red Phaseblade
//        LoadWeapon(ItemID.GreenPhaseblade, Element.electric); // Green Phaseblade
//        LoadWeapon(ItemID.PurplePhaseblade, Element.electric); // Purple Phaseblade
//        LoadWeapon(ItemID.WhitePhaseblade, Element.electric); // White Phaseblade
//        LoadWeapon(ItemID.YellowPhaseblade, Element.electric); // Yellow Phaseblade
//        LoadWeapon(ItemID.MeteorHamaxe, Element.rock); // Meteor Hamaxe
//        LoadWeapon(ItemID.StaffofRegrowth, Element.grass); // Staff of Regrowth
//        LoadWeapon(ItemID.MoltenHamaxe, Element.fire); // Molten Hamaxe
//        LoadWeapon(ItemID.NightsEdge, Element.dark); // Night's Edge
//        LoadWeapon(ItemID.DarkLance, Element.dark); // Dark Lance
//        LoadWeapon(ItemID.Trident, Element.water); // Trident
//        LoadWeapon(ItemID.Spear, Element.normal); // Spear
//        LoadWeapon(ItemID.BreakerBlade, Element.steel); // Breaker Blade
//        LoadWeapon(ItemID.PickaxeAxe, Element.fighting); // Pickaxe Axe
//        LoadWeapon(ItemID.Pwnhammer, Element.fighting); // Pwnhammer
//        LoadWeapon(ItemID.Excalibur, Element.fighting); // Excalibur
//        LoadWeapon(ItemID.TrueExcalibur, Element.fighting); // True Excalibur
//        LoadWeapon(ItemID.Gungnir, Element.fighting); // Gungnir
//        LoadWeapon(ItemID.CobaltPickaxe, Element.dark); // Cobalt Pickaxe
//        LoadWeapon(ItemID.CobaltWaraxe, Element.dark); // Cobalt Waraxe
//        LoadWeapon(ItemID.CobaltSword, Element.dark); // Cobalt Sword
//        LoadWeapon(ItemID.CobaltNaginata, Element.dark); // Cobalt Naginata
//        LoadWeapon(ItemID.MythrilPickaxe, Element.dragon); // Mythril Pickaxe
//        LoadWeapon(ItemID.MythrilWaraxe, Element.dragon); // Mythril Waraxe
//        LoadWeapon(ItemID.MythrilSword, Element.dragon); // Mythril Sword
//        LoadWeapon(ItemID.MythrilHalberd, Element.dragon); // Mythril Halberd
//        LoadWeapon(ItemID.AdamantitePickaxe, Element.dragon); // Adamantite Pickaxe
//        LoadWeapon(ItemID.AdamantiteWaraxe, Element.dragon); // Adamantite Waraxe
//        LoadWeapon(ItemID.AdamantiteSword, Element.dragon); // Adamantite Sword
//        LoadWeapon(ItemID.AdamantiteGlaive, Element.dragon); // Adamantite Glaive
//        LoadWeapon(ItemID.EbonwoodSword, Element.dark); // Ebonwood Sword
//        LoadWeapon(ItemID.EbonwoodHammer, Element.dark); // Ebonwood Hammer
//        LoadWeapon(ItemID.RichMahoganySword, Element.grass); // Rich Mahogany Sword
//        LoadWeapon(ItemID.RichMahoganyHammer, Element.grass); // Rich Mahogany Hammer
//        LoadWeapon(ItemID.PearlwoodSword, Element.fairy); // Pearlwood Sword
//        LoadWeapon(ItemID.PearlwoodHammer, Element.fairy); // Pearlwood Hammer
//        LoadWeapon(ItemID.Keybrand, Element.steel); // Keybrand
//        LoadWeapon(ItemID.Cutlass, Element.water); // Cutlass
//        LoadWeapon(ItemID.TrueNightsEdge, Element.dark); // True Night's Edge
//        LoadWeapon(ItemID.Frostbrand, Element.ice); // Frostbrand
//        LoadWeapon(ItemID.EnchantedSword, Element.normal); // Enchanted Sword
//        LoadWeapon(ItemID.BeamSword, Element.normal); // Beam Sword
//        LoadWeapon(ItemID.IceBlade, Element.ice); // Ice Blade
//        LoadWeapon(ItemID.MushroomSpear, Element.grass); // Mushroom Spear
//        LoadWeapon(ItemID.TerraBlade, Element.ground); // Terra Blade
//        LoadWeapon(ItemID.Hammush, Element.grass); // Hammush
//        LoadWeapon(ItemID.BloodButcherer, Element.blood); // Blood Butcherer
//        LoadWeapon(ItemID.FleshGrinder, Element.blood); // Flesh Grinder
//        LoadWeapon(ItemID.DeathbringerPickaxe, Element.blood); // Deathbringer Pickaxe
//        LoadWeapon(ItemID.BloodLustCluster, Element.blood); // Blood Lust Cluster
//        LoadWeapon(ItemID.TheRottedFork, Element.blood); // The Rotted Fork
//        LoadWeapon(ItemID.CactusSword, Element.grass); // Cactus Sword
//        LoadWeapon(ItemID.CactusPickaxe, Element.grass); // Cactus Pickaxe
//        LoadWeapon(ItemID.ShadewoodSword, Element.blood); // Shadewood Sword
//        LoadWeapon(ItemID.ShadewoodHammer, Element.blood); // Shadewood Hammer
//        LoadWeapon(ItemID.Umbrella, Element.water); // Umbrella
//        LoadWeapon(ItemID.BeeKeeper, Element.bug); // Bee Keeper
//        LoadWeapon(ItemID.BoneSword, Element.bone); // Bone Sword
//        LoadWeapon(ItemID.PalladiumSword, Element.fighting); // Palladium Sword
//        LoadWeapon(ItemID.PalladiumPike, Element.fighting); // Palladium Pike
//        LoadWeapon(ItemID.PalladiumPickaxe, Element.fighting); // Palladium Pickaxe
//        LoadWeapon(ItemID.OrichalcumSword, Element.fairy); // Orichalcum Sword
//        LoadWeapon(ItemID.OrichalcumHalberd, Element.fairy); // Orichalcum Halberd
//        LoadWeapon(ItemID.OrichalcumPickaxe, Element.fairy); // Orichalcum Pickaxe
//        LoadWeapon(ItemID.TitaniumSword, Element.steel); // Titanium Sword
//        LoadWeapon(ItemID.TitaniumTrident, Element.steel); // Titanium Trident
//        LoadWeapon(ItemID.TitaniumPickaxe, Element.steel); // Titanium Pickaxe
//        LoadWeapon(ItemID.PalladiumWaraxe, Element.fighting); // Palladium Waraxe
//        LoadWeapon(ItemID.OrichalcumWaraxe, Element.fairy); // Orichalcum Waraxe
//        LoadWeapon(ItemID.TitaniumWaraxe, Element.steel); // Titanium Waraxe
//        LoadWeapon(ItemID.ChlorophyteClaymore, Element.grass); // Chlorophyte Claymore
//        LoadWeapon(ItemID.ChlorophyteSaber, Element.grass); // Chlorophyte Saber
//        LoadWeapon(ItemID.ChlorophytePartisan, Element.grass); // Chlorophyte Partisan
//        LoadWeapon(ItemID.ChlorophytePickaxe, Element.grass); // Chlorophyte Pickaxe
//        LoadWeapon(ItemID.ChlorophyteGreataxe, Element.grass); // Chlorophyte Greataxe
//        LoadWeapon(ItemID.ChlorophyteWarhammer, Element.grass); // Chlorophyte Warhammer
//        LoadWeapon(ItemID.Picksaw, Element.rock); // Picksaw
//        LoadWeapon(ItemID.ZombieArm, Element.blood); // Zombie Arm
//        LoadWeapon(ItemID.TheAxe, Element.normal); // The Axe
//        LoadWeapon(ItemID.IceSickle, Element.ice); // Ice Sickle
//        LoadWeapon(ItemID.BonePickaxe, Element.bone); // Bone Pickaxe
//        LoadWeapon(ItemID.ChainKnife, Element.steel); // Chain Knife
//        LoadWeapon(ItemID.DeathSickle, Element.dark); // Death Sickle
//        LoadWeapon(ItemID.SpectrePickaxe, Element.ghost); // Spectre Pickaxe
//        LoadWeapon(ItemID.SpectreHamaxe, Element.ghost); // Spectre Hamaxe
//        LoadWeapon(ItemID.Sickle, Element.normal); // Sickle
//        LoadWeapon(ItemID.TheHorsemansBlade, Element.ghost); // The Horseman's Blade
//        LoadWeapon(ItemID.BladedGlove, Element.fighting); // Bladed Glove
//        LoadWeapon(ItemID.CandyCaneSword, Element.normal); // Candy Cane Sword
//        LoadWeapon(ItemID.CnadyCanePickaxe, Element.normal); // Candy Cane Pickaxe
//        LoadWeapon(ItemID.ChristmasTreeSword, Element.grass); // Christmas Tree Sword
//        LoadWeapon(ItemID.NorthPole, Element.ice); // North Pole
//        LoadWeapon(ItemID.ShroomiteDiggingClaw, Element.grass); // Shroomite Digging Claw
//        LoadWeapon(ItemID.Katana, Element.steel); // Katana
//        LoadWeapon(ItemID.Rockfish, Element.rock); // Rockfish
//        LoadWeapon(ItemID.PurpleClubberfish, Element.dark); // Purple Clubberfish
//        LoadWeapon(ItemID.ObsidianSwordfish, Element.rock); // Obsidian Swordfish
//        LoadWeapon(ItemID.Swordfish, Element.water); // Swordfish
//        LoadWeapon(ItemID.ReaverShark, Element.water); // Reaver Shark
//        LoadWeapon(ItemID.PalmWoodHammer, Element.fairy); // Palm Wood Hammer
//        LoadWeapon(ItemID.PalmWoodSword, Element.fairy); // Palm Wood Sword
//        LoadWeapon(ItemID.FalconBlade, Element.fighting); // Falcon Blade
//        LoadWeapon(ItemID.Flairon, Element.dragon); // Flairon
//        LoadWeapon(ItemID.BorealWoodSword, Element.ice); // Boreal Wood Sword
//        LoadWeapon(ItemID.BorealWoodHammer, Element.ice); // Boreal Wood Hammer
//        LoadWeapon(ItemID.VortexPickaxe, Element.electric); // Vortex Pickaxe
//        LoadWeapon(ItemID.NebulaPickaxe, Element.psychic); // Nebula Pickaxe
//        LoadWeapon(ItemID.SolarFlarePickaxe, Element.fire); // Solar Flare Pickaxe
//        LoadWeapon(ItemID.InfluxWaver, Element.electric); // Influx Waver
//        LoadWeapon(ItemID.FetidBaghnakhs, Element.fighting); // Fetid Baghnakhs
//        LoadWeapon(ItemID.Seedler, Element.grass); // Seedler
//        LoadWeapon(ItemID.Meowmere, Element.fairy); // Meowmere
//        LoadWeapon(ItemID.StarWrath, Element.flying); // Star Wrath
//        LoadWeapon(ItemID.PsychoKnife, Element.dark); // Psycho Knife
//        LoadWeapon(ItemID.Bladetongue, Element.blood); // Bladetongue
//        LoadWeapon(ItemID.SlapHand, Element.normal); // Slap Hand
//        LoadWeapon(ItemID.DyeTradersScimitar, Element.normal); // Exotic Scimitar
//        LoadWeapon(ItemID.TaxCollectorsStickOfDoom, Element.normal); // Classy Cane
//        LoadWeapon(ItemID.StylistKilLaKillScissorsIWish, Element.normal); // Stylish Scissors
//        LoadWeapon(ItemID.StardustPickaxe, Element.dragon); // Stardust Pickaxe
//        LoadWeapon(ItemID.PlatinumHammer, Element.steel); // Platinum Hammer
//        LoadWeapon(ItemID.PlatinumAxe, Element.steel); // Platinum Axe
//        LoadWeapon(ItemID.PlatinumBroadsword, Element.steel); // Platinum Broadsword
//        LoadWeapon(ItemID.PlatinumPickaxe, Element.steel); // Platinum Pickaxe
//        LoadWeapon(ItemID.TungstenHammer, Element.steel); // Tungsten Hammer
//        LoadWeapon(ItemID.TungstenAxe, Element.steel); // Tungsten Axe
//        LoadWeapon(ItemID.TungstenBroadsword, Element.steel); // Tungsten Broadsword
//        LoadWeapon(ItemID.TungstenPickaxe, Element.steel); // Tungsten Pickaxe
//        LoadWeapon(ItemID.LeadHammer, Element.steel); // Lead Hammer
//        LoadWeapon(ItemID.LeadAxe, Element.steel); // Lead Axe
//        LoadWeapon(ItemID.LeadBroadsword, Element.steel); // Lead Broadsword
//        LoadWeapon(ItemID.LeadPickaxe, Element.steel); // Lead Pickaxe
//        LoadWeapon(ItemID.TinHammer, Element.steel); // Tin Hammer
//        LoadWeapon(ItemID.TinAxe, Element.steel); // Tin Axe
//        LoadWeapon(ItemID.TinBroadsword, Element.steel); // Tin Broadsword
//        LoadWeapon(ItemID.TinPickaxe, Element.steel); // Tin Pickaxe
//        LoadWeapon(ItemID.CopperHammer, Element.steel); // Copper Hammer
//        LoadWeapon(ItemID.CopperAxe, Element.steel); // Copper Axe
//        LoadWeapon(ItemID.CopperBroadsword, Element.steel); // Copper Broadsword
//        LoadWeapon(ItemID.CopperPickaxe, Element.steel); // Copper Pickaxe
//        LoadWeapon(ItemID.SilverHammer, Element.steel); // Silver Hammer
//        LoadWeapon(ItemID.SilverAxe, Element.steel); // Silver Axe
//        LoadWeapon(ItemID.SilverBroadsword, Element.steel); // Silver Broadsword
//        LoadWeapon(ItemID.SilverPickaxe, Element.steel); // Silver Pickaxe
//        LoadWeapon(ItemID.GoldHammer, Element.steel); // Gold Hammer
//        LoadWeapon(ItemID.GoldAxe, Element.steel); // Gold Axe
//        LoadWeapon(ItemID.GoldBroadsword, Element.steel); // Gold Broadsword
//        LoadWeapon(ItemID.GoldPickaxe, Element.steel); // Gold Pickaxe
//        LoadWeapon(ItemID.LunarHamaxeSolar, Element.fire); // Solar Flare Hamaxe
//        LoadWeapon(ItemID.LunarHamaxeVortex, Element.electric); // Vortex Hamaxe
//        LoadWeapon(ItemID.LunarHamaxeNebula, Element.psychic); // Nebula Hamaxe
//        LoadWeapon(ItemID.LunarHamaxeStardust, Element.dragon); // Stardust Hamaxe
//        LoadWeapon(ItemID.BluePhasesaber, Element.electric); // Blue Phasesaber
//        LoadWeapon(ItemID.RedPhasesaber, Element.electric); // Red Phasesaber
//        LoadWeapon(ItemID.GreenPhasesaber, Element.electric); // Green Phasesaber
//        LoadWeapon(ItemID.PurplePhasesaber, Element.electric); // Purple Phasesaber
//        LoadWeapon(ItemID.WhitePhasesaber, Element.electric); // White Phasesaber
//        LoadWeapon(ItemID.YellowPhasesaber, Element.electric); // Yellow Phasesaber
//        LoadWeapon(ItemID.AntlionClaw, Element.bug); // Mandible Blade
//        LoadWeapon(ItemID.DD2SquireDemonSword, Element.fire); // Brand of the Inferno
//        LoadWeapon(ItemID.DD2SquireBetsySword, Element.dragon); // Flying Dragon
//        LoadWeapon(ItemID.MonkStaffT2, Element.ghost); // Ghastly Glaive
//        LoadWeapon(ItemID.FossilPickaxe, Element.bone); // Fossil Pickaxe
//        LoadWeapon(ItemID.ThunderSpear, Element.electric); // Storm Spear
//        LoadWeapon(ItemID.OrangePhaseblade, Element.electric); // Orange Phaseblade
//        LoadWeapon(ItemID.OrangePhasesaber, Element.electric); // Orange Phasesaber
//        LoadWeapon(ItemID.BloodHamaxe, Element.blood); // Haemorrhaxe
//        LoadWeapon(ItemID.TragicUmbrella, Element.dark); // Tragic Umbrella
//        LoadWeapon(ItemID.GravediggerShovel, Element.ground); // Gravedigger's Shovel
//        LoadWeapon(ItemID.TentacleSpike, Element.dark); // Tentacle Spike
//        LoadWeapon(ItemID.LucyTheAxe, Element.normal); // Lucy the Axe
//        LoadWeapon(ItemID.HamBat, Element.normal); // Ham Bat
//        LoadWeapon(ItemID.BatBat, Element.flying); // Bat Bat
//        LoadWeapon(ItemID.IronShortsword, Element.steel); // Iron Shortsword
//        LoadWeapon(ItemID.EnchantedBoomerang, Element.fairy); // Enchanted Boomerang
//        LoadWeapon(ItemID.Flamarang, Element.fire); // Flamarang
//        LoadWeapon(ItemID.BallOHurt, Element.dark); // Ball O' Hurt
//        LoadWeapon(ItemID.BlueMoon, Element.water); // Blue Moon
//        LoadWeapon(ItemID.ThornChakram, Element.grass); // Thorn Chakram
//        LoadWeapon(ItemID.Sunfury, Element.fire); // Sunfury
//        LoadWeapon(ItemID.WoodenBoomerang, Element.normal); // Wooden Boomerang
//        LoadWeapon(ItemID.CobaltChainsaw, Element.dark); // Cobalt Chainsaw
//        LoadWeapon(ItemID.CobaltDrill, Element.dark); // Cobalt Drill
//        LoadWeapon(ItemID.MythrilChainsaw, Element.dragon); // Mythril Chainsaw
//        LoadWeapon(ItemID.MythrilDrill, Element.dragon); // Mythril Drill
//        LoadWeapon(ItemID.AdamantiteChainsaw, Element.dragon); // Adamantite Chainsaw
//        LoadWeapon(ItemID.AdamantiteDrill, Element.dragon); // Adamantite Drill
//        LoadWeapon(ItemID.DaoofPow, Element.dragon); // Dao of Pow
//        LoadWeapon(ItemID.Ruler, Element.normal); // Ruler
//        LoadWeapon(ItemID.LightDisc, Element.electric); // Light Disc
//        LoadWeapon(ItemID.Drax, Element.fighting); // Drax
//        LoadWeapon(ItemID.IceBoomerang, Element.ice); // Ice Boomerang
//        LoadWeapon(ItemID.TheMeatball, Element.blood); // The Meatball
//        LoadWeapon(ItemID.PossessedHatchet, Element.ghost); // Possessed Hatchet
//        LoadWeapon(ItemID.PalladiumDrill, Element.fighting); // Palladium Drill
//        LoadWeapon(ItemID.PalladiumChainsaw, Element.fighting); // Palladium Chainsaw
//        LoadWeapon(ItemID.OrichalcumDrill, Element.fairy); // Orichalcum Drill
//        LoadWeapon(ItemID.OrichalcumChainsaw, Element.fairy); // Orichalcum Chainsaw
//        LoadWeapon(ItemID.TitaniumDrill, Element.steel); // Titanium Drill
//        LoadWeapon(ItemID.TitaniumChainsaw, Element.steel); // Titanium Chainsaw
//        LoadWeapon(ItemID.ChlorophyteDrill, Element.grass); // Chlorophyte Drill
//        LoadWeapon(ItemID.ChlorophyteChainsaw, Element.grass); // Chlorophyte Chainsaw
//        LoadWeapon(ItemID.FlowerPow, Element.grass); // Flower Pow
//        LoadWeapon(ItemID.ChlorophyteJackhammer, Element.grass); // Chlorophyte Jackhammer
//        LoadWeapon(ItemID.GolemFist, Element.rock); // Golem Fist
//        LoadWeapon(ItemID.KOCannon, Element.fighting); // KO Cannon
//        LoadWeapon(ItemID.Bananarang, Element.normal); // Bananarang
//        LoadWeapon(ItemID.PaladinsHammer, Element.steel); // Paladin's Hammer
//        LoadWeapon(ItemID.VampireKnives, Element.blood); // Vampire Knives
//        LoadWeapon(ItemID.ScourgeoftheCorruptor, Element.dark); // Scourge of the Corruptor
//        LoadWeapon(ItemID.BloodyMachete, Element.blood); // Bloody Machete
//        LoadWeapon(ItemID.FruitcakeChakram, Element.normal); // Fruitcake Chakram
//        LoadWeapon(ItemID.SawtoothShark, Element.water); // Sawtooth Shark
//        LoadWeapon(ItemID.Anchor, Element.water); // Anchor
//        LoadWeapon(ItemID.VortexDrill, Element.electric); // Vortex Drill
//        LoadWeapon(ItemID.NebulaDrill, Element.psychic); // Nebula Drill
//        LoadWeapon(ItemID.SolarFlareDrill, Element.fire); // Solar Flare Drill
//        LoadWeapon(ItemID.LaserDrill, Element.electric); // Laser Drill
//        LoadWeapon(ItemID.ChainGuillotines, Element.steel); // Chain Guillotines
//        LoadWeapon(ItemID.FlyingKnife, Element.flying); // Flying Knife
//        LoadWeapon(ItemID.ShadowFlameKnife, Element.ghost); // Shadowflame Knife
//        LoadWeapon(ItemID.ButchersChainsaw, Element.blood); // Butcher's Chainsaw
//        LoadWeapon(ItemID.Code1, Element.normal); // Code 1
//        LoadWeapon(ItemID.WoodYoyo, Element.normal); // Wooden Yoyo
//        LoadWeapon(ItemID.CorruptYoyo, Element.dark); // Malaise
//        LoadWeapon(ItemID.CrimsonYoyo, Element.blood); // Artery
//        LoadWeapon(ItemID.JungleYoyo, Element.grass); // Amazon
//        LoadWeapon(ItemID.Cascade, Element.fire); // Cascade
//        LoadWeapon(ItemID.Chik, Element.rock); // Chik
//        LoadWeapon(ItemID.Code2, Element.normal); // Code 2
//        LoadWeapon(ItemID.Rally, Element.ground); // Rally
//        LoadWeapon(ItemID.Yelets, Element.grass); // Yelets
//        LoadWeapon(ItemID.RedsYoyo, Element.psychic); // Red's Throw
//        LoadWeapon(ItemID.ValkyrieYoyo, Element.fighting); // Valkyrie Yoyo
//        LoadWeapon(ItemID.Amarok, Element.ice); // Amarok
//        LoadWeapon(ItemID.HelFire, Element.fire); // Hel-Fire
//        LoadWeapon(ItemID.Kraken, Element.water); // Kraken
//        LoadWeapon(ItemID.TheEyeOfCthulhu, Element.flying); // The Eye of Cthulhu
//        LoadWeapon(ItemID.FormatC, Element.normal); // Format:C
//        LoadWeapon(ItemID.Gradient, Element.normal); // Gradient
//        LoadWeapon(ItemID.Valor, Element.fighting); // Valor
//        LoadWeapon(ItemID.Arkhalis, Element.flying); // Arkhalis
//        LoadWeapon(ItemID.Terrarian, Element.ground); // Terrarian
//        LoadWeapon(ItemID.StardustDrill, Element.dragon); // Stardust Drill
//        LoadWeapon(ItemID.SolarEruption, Element.fire); // Solar Eruption
//        LoadWeapon(ItemID.PlatinumShortsword, Element.steel); // Platinum Shortsword
//        LoadWeapon(ItemID.TungstenShortsword, Element.steel); // Tungsten Shortsword
//        LoadWeapon(ItemID.LeadShortsword, Element.steel); // Lead Shortsword
//        LoadWeapon(ItemID.TinShortsword, Element.steel); // Tin Shortsword
//        LoadWeapon(ItemID.CopperShortsword, Element.steel); // Copper Shortsword
//        LoadWeapon(ItemID.SilverShortsword, Element.steel); // Silver Shortsword
//        LoadWeapon(ItemID.GoldShortsword, Element.steel); // Gold Shortsword
//        LoadWeapon(ItemID.DayBreak, Element.fire); // Daybreak
//        LoadWeapon(ItemID.MonkStaffT1, Element.fighting); // Sleepy Octopod
//        LoadWeapon(ItemID.MonkStaffT3, Element.dragon); // Sky Dragon's Fury
//        LoadWeapon(ItemID.Terragrim, Element.ground); // Terragrim
//        LoadWeapon(ItemID.DripplerFlail, Element.blood); // Drippler Crippler
//        LoadWeapon(ItemID.Gladius, Element.fighting); // Gladius
//        LoadWeapon(ItemID.BouncingShield, Element.fighting); // Sergeant United Shield
//        LoadWeapon(ItemID.Shroomerang, Element.grass); // Shroomerang
//        LoadWeapon(ItemID.JoustingLance, Element.fighting); // Jousting Lance
//        LoadWeapon(ItemID.ShadowJoustingLance, Element.dark); // Shadow Jousting Lance
//        LoadWeapon(ItemID.HallowJoustingLance, Element.fairy); // Hallowed Jousting Lance
//        LoadWeapon(ItemID.CombatWrench, Element.fighting); // Combat Wrench
//        LoadWeapon(ItemID.PiercingStarlight, Element.fairy); // Starlight
//        LoadWeapon(ItemID.Mace, Element.steel); // Mace
//        LoadWeapon(ItemID.FlamingMace, Element.fire); // Flaming Mace
//        LoadWeapon(ItemID.WoodenBow, Element.normal); // Wooden Bow
//        LoadWeapon(ItemID.Shuriken, Element.dark); // Shuriken
//        LoadWeapon(ItemID.DemonBow, Element.dark); // Demon Bow
//        LoadWeapon(ItemID.FlintlockPistol, Element.normal); // Flintlock Pistol
//        LoadWeapon(ItemID.Musket, Element.normal); // Musket
//        LoadWeapon(ItemID.Minishark, Element.water); // Minishark
//        LoadWeapon(ItemID.IronBow, Element.steel); // Iron Bow
//        LoadWeapon(ItemID.MoltenFury, Element.fire); // Molten Fury
//        LoadWeapon(ItemID.Bone, Element.bone); // Bone
//        LoadWeapon(ItemID.Harpoon, Element.water); // Harpoon
//        LoadWeapon(ItemID.SpikyBall, Element.normal); // Spiky Ball
//        LoadWeapon(ItemID.Handgun, Element.normal); // Handgun
//        LoadWeapon(ItemID.Grenade, Element.normal); // Grenade
//        LoadWeapon(ItemID.StarCannon, Element.flying); // Star Cannon
//        LoadWeapon(ItemID.PhoenixBlaster, Element.fire); // Phoenix Blaster
//        LoadWeapon(ItemID.Sandgun, Element.ground); // Sandgun
//        LoadWeapon(ItemID.ThrowingKnife, Element.fighting); // Throwing Knife
//        LoadWeapon(ItemID.Blowpipe, Element.normal); // Blowpipe
//        LoadWeapon(ItemID.PoisonedKnife, Element.poison); // Poisoned Knife
//        LoadWeapon(ItemID.ClockworkAssaultRifle, Element.steel); // Clockwork Assault Rifle
//        LoadWeapon(ItemID.CobaltRepeater, Element.dark); // Cobalt Repeater
//        LoadWeapon(ItemID.MythrilRepeater, Element.dragon); // Mythril Repeater
//        LoadWeapon(ItemID.AdamantiteRepeater, Element.dragon); // Adamantite Repeater
//        LoadWeapon(ItemID.Flamethrower, Element.fire); // Flamethrower
//        LoadWeapon(ItemID.Megashark, Element.water); // Megashark
//        LoadWeapon(ItemID.HallowedRepeater, Element.fairy); // Hallowed Repeater
//        LoadWeapon(ItemID.EbonwoodBow, Element.dark); // Ebonwood Bow
//        LoadWeapon(ItemID.RichMahoganyBow, Element.grass); // Rich Mahogany Bow
//        LoadWeapon(ItemID.PearlwoodBow, Element.fairy); // Pearlwood Bow
//        LoadWeapon(ItemID.Shotgun, Element.normal); // Shotgun
//        LoadWeapon(ItemID.TacticalShotgun, Element.normal); // Tactical Shotgun
//        LoadWeapon(ItemID.Marrow, Element.bone); // Marrow
//        LoadWeapon(ItemID.IceBow, Element.ice); // Ice Bow
//        LoadWeapon(ItemID.GrenadeLauncher, Element.normal); // Grenade Launcher
//        LoadWeapon(ItemID.RocketLauncher, Element.normal); // Rocket Launcher
//        LoadWeapon(ItemID.ProximityMineLauncher, Element.normal); // Proximity Mine Launcher
//        LoadWeapon(ItemID.TendonBow, Element.blood); // Tendon Bow
//        LoadWeapon(ItemID.TheUndertaker, Element.blood); // The Undertaker
//        LoadWeapon(ItemID.ShadewoodBow, Element.blood); // Shadewood Bow
//        LoadWeapon(ItemID.Boomstick, Element.normal); // Boomstick
//        LoadWeapon(ItemID.Blowgun, Element.normal); // Blowgun
//        LoadWeapon(ItemID.Beenade, Element.bug); // Beenade
//        LoadWeapon(ItemID.PiranhaGun, Element.water); // Piranha Gun
//        LoadWeapon(ItemID.PalladiumRepeater, Element.fighting); // Palladium Repeater
//        LoadWeapon(ItemID.OrichalcumRepeater, Element.fairy); // Orichalcum Repeater
//        LoadWeapon(ItemID.TitaniumRepeater, Element.steel); // Titanium Repeater
//        LoadWeapon(ItemID.ChlorophyteShotbow, Element.grass); // Chlorophyte Shotbow
//        LoadWeapon(ItemID.SniperRifle, Element.fighting); // Sniper Rifle
//        LoadWeapon(ItemID.VenusMagnum, Element.grass); // Venus Magnum
//        LoadWeapon(ItemID.Stynger, Element.fighting); // Stynger
//        LoadWeapon(ItemID.Uzi, Element.normal); // Uzi
//        LoadWeapon(ItemID.SnowballCannon, Element.ice); // Snowball Cannon
//        LoadWeapon(ItemID.SDMG, Element.water); // S.D.M.G.
//        LoadWeapon(ItemID.CandyCornRifle, Element.normal); // Candy Corn Rifle
//        LoadWeapon(ItemID.JackOLanternLauncher, Element.ghost); // Jack 'O Lantern Launcher
//        LoadWeapon(ItemID.RottenEgg, Element.normal); // Rotten Egg
//        LoadWeapon(ItemID.StakeLauncher, Element.grass); // Stake Launcher
//        LoadWeapon(ItemID.RedRyder, Element.normal); // Red Ryder
//        LoadWeapon(ItemID.EldMelter, Element.fire); // Elf Melter
//        LoadWeapon(ItemID.StarAnise, Element.normal); // Star Anise
//        LoadWeapon(ItemID.ChainGun, Element.steel); // Chain Gun
//        LoadWeapon(ItemID.SnowmanCannon, Element.ice); // Snowman Cannon
//        LoadWeapon(ItemID.PulseBow, Element.normal); // Pulse Bow
//        LoadWeapon(ItemID.Revolver, Element.normal); // Revolver
//        LoadWeapon(ItemID.Gatligator, Element.water); // Gatligator
//        LoadWeapon(ItemID.PalmWoodBow, Element.water); // Palm Wood Bow
//        LoadWeapon(ItemID.StickyGrenade, Element.normal); // Sticky Grenade
//        LoadWeapon(ItemID.MolotovCocktail, Element.fire); // Molotov Cocktail
//        LoadWeapon(ItemID.Tsunami, Element.water); // Tsunami
//        LoadWeapon(ItemID.BorealWoodBow, Element.ice); // Boreal Wood Bow
//        LoadWeapon(ItemID.ElectrosphereLauncher, Element.electric); // Electrosphere Launcher
//        LoadWeapon(ItemID.Xenopopper, Element.psychic); // Xenopopper
//        LoadWeapon(ItemID.BeesKnees, Element.bug); // The Bee's Knees
//        LoadWeapon(ItemID.DartPistol, Element.blood); // Dart Pistol
//        LoadWeapon(ItemID.DartRifle, Element.dark); // Dart Rifle
//        LoadWeapon(ItemID.HellwingBow, Element.fire); // Hellwing Bow
//        LoadWeapon(ItemID.DaedalusStormbow, Element.fairy); // Daedalus Stormbow
//        LoadWeapon(ItemID.ShadowFlameBow, Element.ghost); // Shadowflame Bow
//        LoadWeapon(ItemID.Javelin, Element.fighting); // Javelin
//        LoadWeapon(ItemID.NailGun, Element.normal); // Nail Gun
//        LoadWeapon(ItemID.BouncyGrenade, Element.normal); // Bouncy Grenade
//        LoadWeapon(ItemID.FrostDaggerfish, Element.ice); // Frost Daggerfish
//        LoadWeapon(ItemID.Toxikarp, Element.poison); // Toxikarp
//        LoadWeapon(ItemID.PainterPaintballGun, Element.normal); // Paintball Gun
//        LoadWeapon(ItemID.BoneJavelin, Element.bone); // Bone Javelin
//        LoadWeapon(ItemID.BoneDagger, Element.bone); // Bone Throwing Knife
//        LoadWeapon(ItemID.VortexBeater, Element.electric); // Vortex Beater
//        LoadWeapon(ItemID.PlatinumBow, Element.steel); // Platinum Bow
//        LoadWeapon(ItemID.TungstenBow, Element.steel); // Tungsten Bow
//        LoadWeapon(ItemID.LeadBow, Element.steel); // Lead Bow
//        LoadWeapon(ItemID.TinBow, Element.steel); // Tin Bow
//        LoadWeapon(ItemID.CopperBow, Element.steel); // Copper Bow
//        LoadWeapon(ItemID.SilverBow, Element.steel); // Silver Bow
//        LoadWeapon(ItemID.GoldBow, Element.steel); // Gold Bow
//        LoadWeapon(ItemID.Phantasm, Element.ghost); // Phantasm
//        LoadWeapon(ItemID.FireworksLauncher, Element.normal); // Celebration
//        LoadWeapon(ItemID.PartyGirlGrenade, Element.normal); // Happy Grenade
//        LoadWeapon(ItemID.OnyxBlaster, Element.dark); // Onyx Blaster
//        LoadWeapon(ItemID.AleThrowingGlove, Element.fighting); // Ale Tosser
//        LoadWeapon(ItemID.DD2PhoenixBow, Element.fire); // Phantom Phoenix
//        LoadWeapon(ItemID.DD2BetsyBow, Element.fire); // Aerial Bane
//        LoadWeapon(ItemID.Celeb2, Element.normal); // Celebration Mk2
//        LoadWeapon(ItemID.SuperStarCannon, Element.flying); // Super Star Shooter
//        LoadWeapon(ItemID.PaperAirplaneA, Element.flying); // Paper Airplane
//        LoadWeapon(ItemID.PaperAirplaneB, Element.flying); // White Paper Airplane
//        LoadWeapon(ItemID.BloodRainBow, Element.blood); // Blood Rain Bow
//        LoadWeapon(ItemID.QuadBarrelShotgun, Element.normal); // Quad-Barrel Shotgun
//        LoadWeapon(ItemID.FairyQueenRangedItem, Element.fairy); // Eventide
//        LoadWeapon(ItemID.PewMaticHorn, Element.normal); // Pew-matic Horn
//        LoadWeapon(ItemID.PygmyStaff, Element.poison); // Pygmy Staff
//        LoadWeapon(ItemID.SlimeStaff, Element.water); // Slime Staff
//        LoadWeapon(ItemID.StaffoftheFrostHydra, Element.ice); // Staff of the Frost Hydra
//        LoadWeapon(ItemID.RavenStaff, Element.flying); // Raven Staff
//        LoadWeapon(ItemID.HornetStaff, Element.bug); // Hornet Staff
//        LoadWeapon(ItemID.ImpStaff, Element.fire); // Imp Staff
//        LoadWeapon(ItemID.QueenSpiderStaff, Element.poison); // Queen Spider Staff
//        LoadWeapon(ItemID.OpticStaff, Element.flying); // Optic Staff
//        LoadWeapon(ItemID.SpiderStaff, Element.poison); // Spider Staff
//        LoadWeapon(ItemID.PirateStaff, Element.water); // Pirate Staff
//        LoadWeapon(ItemID.TempestStaff, Element.water); // Tempest Staff
//        LoadWeapon(ItemID.XenoStaff, Element.psychic); // Xeno Staff
//        LoadWeapon(ItemID.DeadlySphereStaff, Element.steel); // Deadly Sphere Staff
//        LoadWeapon(ItemID.StardustCellStaff, Element.dragon); // Stardust Cell Staff
//        LoadWeapon(ItemID.StardustDragonStaff, Element.dragon); // Stardust Dragon Staff
//        LoadWeapon(ItemID.MoonlordTurretStaff, Element.dark); // Lunar Portal Staff
//        LoadWeapon(ItemID.RainbowCrystalStaff, Element.fairy); // Rainbow Crystal Staff
//        LoadWeapon(ItemID.DD2FlameburstTowerT1Popper, Element.fire); // Flameburst Rod
//        LoadWeapon(ItemID.DD2FlameburstTowerT2Popper, Element.fire); // Flameburst Cane
//        LoadWeapon(ItemID.DD2FlameburstTowerT3Popper, Element.fire); // Flameburst Staff
//        LoadWeapon(ItemID.DD2BallistraTowerT1Popper, Element.steel); // Ballista Rod
//        LoadWeapon(ItemID.DD2BallistraTowerT2Popper, Element.steel); // Ballista Cane
//        LoadWeapon(ItemID.DD2BallistraTowerT3Popper, Element.steel); // Ballista Staff
//        LoadWeapon(ItemID.DD2LightningAuraT1Popper, Element.electric); // Lightning Aura Rod
//        LoadWeapon(ItemID.DD2LightningAuraT2Popper, Element.electric); // Lightning Aura Cane
//        LoadWeapon(ItemID.DD2LightningAuraT3Popper, Element.electric); // Lightning Aura Staff
//        LoadWeapon(ItemID.DD2ExplosiveTrapT1Popper, Element.normal); // Explosive Trap Rod
//        LoadWeapon(ItemID.DD2ExplosiveTrapT2Popper, Element.normal); // Explosive Trap Cane
//        LoadWeapon(ItemID.DD2ExplosiveTrapT3Popper, Element.normal); // Explosive Trap Staff
//        LoadWeapon(ItemID.SanguineStaff, Element.blood); // Sanguine Staff
//        LoadWeapon(ItemID.VampireFrogStaff, Element.blood); // Vampire Frog Staff
//        LoadWeapon(ItemID.BabyBirdStaff, Element.flying); // Finch Staff
//        LoadWeapon(ItemID.StormTigerStaff, Element.ground); // Desert Tiger Staff
//        LoadWeapon(ItemID.Smolstar, Element.fairy); // Blade Staff
//        LoadWeapon(ItemID.EmpressBlade, Element.fairy); // Terraprisma
//        LoadWeapon(ItemID.FlinxStaff, Element.ice); // Flinx Staff
//        LoadWeapon(ItemID.AbigailsFlower, Element.ghost); // Abigail's Flower
//        LoadWeapon(ItemID.HoundiusShootius, Element.psychic); // Houndius Shootius
//        LoadWeapon(ItemID.BlandWhip, Element.normal); // Leather Whip
//        LoadWeapon(ItemID.SwordWhip, Element.fighting); // Durendal
//        LoadWeapon(ItemID.MaceWhip, Element.steel); // Morning Star
//        LoadWeapon(ItemID.ScytheWhip, Element.ghost); // Dark Harvest
//        LoadWeapon(ItemID.CoolWhip, Element.ice); // Cool Whip
//        LoadWeapon(ItemID.FireWhip, Element.fire); // Firecracker
//        LoadWeapon(ItemID.ThornWhip, Element.grass); // Snapthorn
//        LoadWeapon(ItemID.RainbowWhip, Element.fairy); // Kaleidoscope
//        LoadWeapon(ItemID.BoneWhip, Element.bone); // Spinal Tap
//        LoadWeapon(ItemID.Zenith, Element.dark);

//        //LoadWeapon(ItemID.EoCShield, ); // Shield of Cthulhu ??
//        //types[ItemID.Zenith] = WeaponTypeInfo.Get(ElementArray.Get(Element.special)); // Zenith // todo: something cool
//    }

//    private void LoadAmmos()
//    {
//        LoadAmmo(ItemID.WoodenArrow, Element.normal); // Wooden Arrow
//        LoadAmmo(ItemID.FlamingArrow, Element.fire); // Flaming Arrow
//        LoadAmmo(ItemID.UnholyArrow, Element.dark); // Unholy Arrow
//        LoadAmmo(ItemID.JestersArrow, Element.flying); // Jester's Arrow
//        LoadAmmo(ItemID.CopperCoin, Element.steel); // Copper Coin
//        LoadAmmo(ItemID.SilverCoin, Element.steel); // Silver Coin
//        LoadAmmo(ItemID.GoldCoin, Element.steel); // Gold Coin
//        LoadAmmo(ItemID.PlatinumCoin, Element.steel); // Platinum Coin
//        LoadAmmo(ItemID.MusketBall, Element.normal); // Musket Ball
//        LoadAmmo(ItemID.MeteorShot, Element.rock, Element.fire); // Meteor Shot
//        LoadAmmo(ItemID.HellfireArrow, Element.fire); // Hellfire Arrow
//        LoadAmmo(ItemID.SilverBullet, Element.steel); // Silver Bullet
//        LoadAmmo(ItemID.Seed, Element.grass); // Seed
//        LoadAmmo(ItemID.CrystalBullet, Element.rock, Element.fairy); // Crystal Bullet
//        LoadAmmo(ItemID.HolyArrow, Element.fairy); // Holy Arrow
//        LoadAmmo(ItemID.CursedArrow, Element.dark, Element.fire); // Cursed Arrow
//        LoadAmmo(ItemID.CursedBullet, Element.dark, Element.fire); // Cursed Bullet
//        LoadAmmo(ItemID.RocketI, Element.normal); // Rocket I
//        LoadAmmo(ItemID.RocketII, Element.normal); // Rocket II
//        LoadAmmo(ItemID.RocketIII, Element.normal); // Rocket III
//        LoadAmmo(ItemID.RocketIV, Element.normal); // Rocket IV
//        LoadAmmo(ItemID.Flare, Element.fire); // Flare
//        LoadAmmo(ItemID.Snowball, Element.ice); // Snowball
//        LoadAmmo(ItemID.FrostburnArrow, Element.ice); // Frostburn Arrow
//        LoadAmmo(ItemID.ChlorophyteBullet, Element.grass); // Chlorophyte Bullet
//        LoadAmmo(ItemID.ChlorophyteArrow, Element.grass); // Chlorophyte Arrow
//        LoadAmmo(ItemID.StyngerBolt, Element.fighting); // Stynger Bolt
//        LoadAmmo(ItemID.HighVelocityBullet, Element.fighting); // High Velocity Bullet
//        LoadAmmo(ItemID.PoisonDart, Element.poison); // Poison Dart
//        LoadAmmo(ItemID.IchorArrow, Element.blood); // Ichor Arrow
//        LoadAmmo(ItemID.IchorBullet, Element.blood); // Ichor Bullet
//        LoadAmmo(ItemID.VenomArrow, Element.poison); // Venom Arrow
//        LoadAmmo(ItemID.VenomBullet, Element.poison); // Venom Bullet
//        LoadAmmo(ItemID.PartyBullet, Element.fairy); // Party Bullet
//        LoadAmmo(ItemID.NanoBullet, Element.electric); // Nano Bullet
//        LoadAmmo(ItemID.ExplodingBullet, Element.normal); // Exploding Bullet
//        LoadAmmo(ItemID.GoldenBullet, Element.steel); // Golden Bullet
//        LoadAmmo(ItemID.BlueFlare, Element.fire); // Blue Flare
//        LoadAmmo(ItemID.CandyCorn, Element.normal); // Candy Corn
//        LoadAmmo(ItemID.ExplosiveJackOLantern, Element.ghost); // Explosive Jack 'O Lantern
//        LoadAmmo(ItemID.Stake, Element.grass); // Stake
//        LoadAmmo(ItemID.BoneArrow, Element.bone); // Bone Arrow
//        LoadAmmo(ItemID.CrystalDart, Element.rock, Element.fairy); // Crystal Dart
//        LoadAmmo(ItemID.CursedDart, Element.dark, Element.fire); // Cursed Dart
//        LoadAmmo(ItemID.IchorDart, Element.blood); // Ichor Dart
//        LoadAmmo(ItemID.EndlessQuiver, Element.normal); // Endless Quiver
//        LoadAmmo(ItemID.EndlessMusketPouch, Element.normal); // Endless Musket Pouch
//        LoadAmmo(ItemID.Nail, Element.steel); // Nail
//        LoadAmmo(ItemID.MoonlordBullet, Element.dark); // Luminite Bullet
//        LoadAmmo(ItemID.MoonlordArrow, Element.dark); // Luminite Arrow
//        LoadAmmo(ItemID.ClusterRocketI, Element.normal); // Cluster Rocket I
//        LoadAmmo(ItemID.ClusterRocketII, Element.normal); // Cluster Rocket II
//        LoadAmmo(ItemID.WetRocket, Element.water); // Wet Rocket
//        LoadAmmo(ItemID.LavaRocket, Element.fire); // Lava Rocket
//        LoadAmmo(ItemID.HoneyRocket, Element.bug); // Honey Rocket
//        LoadAmmo(ItemID.MiniNukeI, Element.normal); // Mini Nuke I
//        LoadAmmo(ItemID.MiniNukeII, Element.normal); // Mini Nuke II
//        LoadAmmo(ItemID.DryRocket, Element.normal); // Dry Rocket
//        LoadAmmo(ItemID.TungstenBullet, Element.steel); // Tungsten Bullet
//    }

//    private void LoadMiscItems()
//    {
//        types[ItemID.LandMine] = ItemTypeInfo.Get(ElementArray.Get(Element.normal), false);
//        types[ItemID.BoneHelm] = ItemTypeInfo.Get(ElementArray.Get(Element.bone), false);
//    }

//    /// <summary>
//    /// Has to happen after loading all other arrays.
//    /// </summary>
//    public static void LoadSpecialTooltips()
//    {
//        Mod mod = ModContent.GetInstance<TerraTyping>();

//        SpecialTooltips.Builder christmasTreeSword = new SpecialTooltips.Builder();
//        AddArrayToTooltip(christmasTreeSword, "Sword", GetInfo(ItemID.ChristmasTreeSword).Elements);
//        AddArrayToTooltip(christmasTreeSword, "Ornament", Projectiles.GetElements(ProjectileID.OrnamentFriendly));
//        Instance.specialTooltips[ItemID.ChristmasTreeSword] = christmasTreeSword.Build();

//        SpecialTooltips.Builder flairon = new SpecialTooltips.Builder();
//        AddArrayToTooltip(flairon, "Flail", GetInfo(ItemID.Flairon).Elements);
//        AddArrayToTooltip(flairon, "Bubbles", Projectiles.GetElements(ProjectileID.FlaironBubble));
//        Instance.specialTooltips[ItemID.Flairon] = flairon.Build();

//        SpecialTooltips.Builder skyDragonsFury = new SpecialTooltips.Builder();
//        AddArrayToTooltip(skyDragonsFury, "Staff", GetInfo(ItemID.MonkStaffT3).Elements);
//        AddArrayToTooltip(skyDragonsFury, "Electrosphere", Projectiles.GetElements(ProjectileID.MonkStaffT3_AltShot));
//        Instance.specialTooltips[ItemID.MonkStaffT3] = skyDragonsFury.Build();

//        SpecialTooltips.Builder tomeOfInfiniteWisdom = new SpecialTooltips.Builder();
//        AddArrayToTooltipNoParentheses(tomeOfInfiniteWisdom, Projectiles.GetElements(ProjectileID.BookStaffShot));
//        AddArrayToTooltip(tomeOfInfiniteWisdom, "Whirlwind", Projectiles.GetElements(ProjectileID.DD2ApprenticeStorm));
//        Instance.specialTooltips[ItemID.ApprenticeStaffT3] = tomeOfInfiniteWisdom.Build();

//        SpecialTooltips.Builder zenith = new SpecialTooltips.Builder();
//        zenith.Add("Uses a different type for each sword.", Color.White);
//        Instance.specialTooltips[ItemID.Zenith] = zenith.Build();

//        SpecialTooltips.Builder pewmaticHorn = new SpecialTooltips.Builder();
//        pewmaticHorn.Add("Type changes based on what is shot.", Color.White);
//        Instance.specialTooltips[ItemID.PewMaticHorn] = pewmaticHorn.Build();

//        static void AddArrayToTooltip(SpecialTooltips.Builder specialTooltipsBuilder, string parentheticalString, ElementArray itemElements)
//        {
//            foreach (Element element in itemElements)
//            {
//                specialTooltipsBuilder.Add($"{LangHelper.ElementName(element)} ({parentheticalString})", ElementColors.GetColor(element));
//            }
//        }
//        static void AddArrayToTooltipNoParentheses(SpecialTooltips.Builder specialTooltipsBuilder, ElementArray itemElements)
//        {
//            foreach (Element element in itemElements)
//            {
//                specialTooltipsBuilder.Add($"{LangHelper.ElementName(element)}", ElementColors.GetColor(element));
//            }
//        }
//    }

//    private void LoadWeapon(short type, params Element[] elements)
//    {
//        types[type] = ItemTypeInfo.Get(ElementArray.Get(elements), true);
//    }

//    private void LoadAmmo(short type, params Element[] elements)
//    {
//        types[type] = ItemTypeInfo.Get(ElementArray.Get(elements), false);
//    }
//}

//public class ItemTypeInfo
//{
//    public ElementArray Elements { get; }

//    /// <summary>
//    /// If this is allowed to get stab bonus. True for weapons, false for ammo and armor.
//    /// </summary>
//    public bool GetsStab { get; }

//    public static ItemTypeInfo Default => new ItemTypeInfo(ElementArray.Default, false);

//    ItemTypeInfo(ElementArray elements, bool getsStab)
//    {
//        Elements = elements;
//        GetsStab = getsStab;
//    }

//    /// <param name="allowedToHaveStab">If this is allowed to get stab bonus. True for weapons, false for ammo and armor.</param>
//    public static ItemTypeInfo Get(ElementArray elements, bool allowedToHaveStab)
//    {
//        return new ItemTypeInfo(elements, allowedToHaveStab);
//    }
//}

//public class SpecialTooltips
//{
//    public string Tooltip { get; }
//    public Color Color { get; }
//    public (string, Color)[] Tooltips { get; }

//    public SpecialTooltips(string tooltip, Color color)
//    {
//        Tooltip = tooltip;
//        Color = color;
//    }
//    public SpecialTooltips(params (string, Color)[] tooltips)
//    {
//        Tooltips = tooltips;
//    }

//    : remove IList if not important
//    public class Builder : IList<(string, Color)>
//    {
//        List<(string, Color)> tooltips = new List<(string, Color)>();

//        public Builder()
//        {

//        }

//        public SpecialTooltips Build()
//        {
//            return new SpecialTooltips(tooltips.ToArray());
//        }

//        (string, Color) IList<(string, Color)>.this[int index] { get => ((IList<(string, Color)>)tooltips)[index]; set => ((IList<(string, Color)>)tooltips)[index] = value; }

//        int ICollection<(string, Color)>.Count => ((ICollection<(string, Color)>)tooltips).Count;

//        bool ICollection<(string, Color)>.IsReadOnly => ((ICollection<(string, Color)>)tooltips).IsReadOnly;

//        public void Add(string @string, Color color)
//        {
//            ((ICollection<(string, Color)>)tooltips).Add((@string, color));
//        }

//        public void Add((string, Color) item)
//        {
//            ((ICollection<(string, Color)>)tooltips).Add(item);
//        }

//        void ICollection<(string, Color)>.Clear()
//        {
//            ((ICollection<(string, Color)>)tooltips).Clear();
//        }

//        bool ICollection<(string, Color)>.Contains((string, Color) item)
//        {
//            return ((ICollection<(string, Color)>)tooltips).Contains(item);
//        }

//        void ICollection<(string, Color)>.CopyTo((string, Color)[] array, int arrayIndex)
//        {
//            ((ICollection<(string, Color)>)tooltips).CopyTo(array, arrayIndex);
//        }

//        IEnumerator<(string, Color)> IEnumerable<(string, Color)>.GetEnumerator()
//        {
//            return ((IEnumerable<(string, Color)>)tooltips).GetEnumerator();
//        }

//        int IList<(string, Color)>.IndexOf((string, Color) item)
//        {
//            return ((IList<(string, Color)>)tooltips).IndexOf(item);
//        }

//        void IList<(string, Color)>.Insert(int index, (string, Color) item)
//        {
//            ((IList<(string, Color)>)tooltips).Insert(index, item);
//        }

//        bool ICollection<(string, Color)>.Remove((string, Color) item)
//        {
//            return ((ICollection<(string, Color)>)tooltips).Remove(item);
//        }

//        void IList<(string, Color)>.RemoveAt(int index)
//        {
//            ((IList<(string, Color)>)tooltips).RemoveAt(index);
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable)tooltips).GetEnumerator();
//        }
//    }

//    public void AddAllTo(Mod mod, List<TooltipLine> tooltips)
//    {
//        for (int i = 0; i < Tooltips.Length; i++)
//        {
//            tooltips.Add(new TooltipLine(mod, $"SpecialTooltip{i + 1}", Tooltips[i].Item1)
//            {
//                OverrideColor = Tooltips[i].Item2
//            });
//        }
//    }
//}