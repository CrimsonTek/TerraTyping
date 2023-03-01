//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Ionic.Zlib;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using TerraTyping.DataTypes;
//using TerraTyping.DataTypes.Structs;

//namespace TerraTyping.Dictionaries;

//[Obsolete]
//public class Projectiles : ILoadable
//{
//    private ProjectileTypeInfo[] types;
//    private ElementArray[] pewmaticHornElements;
//    private ElementArray[] deerclopsDebrisElements;

//    public static Projectiles Instance { get; private set; }

//    void ILoadable.Load(Mod mod)
//    {
//        Instance = this;
//    }

//    void ILoadable.Unload()
//    {
//        Instance = null;
//    }

//    public static ProjectileTypeInfo GetInfo(int type)
//    {
//        if (type < Instance.types.Length && type >= 0)
//        {
//            return Instance.types[type];
//        }
//        else
//        {
//            throw new IndexOutOfRangeException($"Argument {nameof(type)} ({type}) is out of the bounds of projectile types.");
//        }
//    }

//    /// <summary>
//    /// Use a projectile whenever possible because some elements may rely on some projectile data to determine their type (eg Zenith)
//    /// </summary>
//    public static ElementArray GetElements(int type)
//    {
//        if (type < Instance.types.Length && type >= 0)
//        {
//            return Instance.types[type].GetElements();
//        }
//        else
//        {
//            throw new IndexOutOfRangeException($"Argument {nameof(type)} ({type}) is out of the bounds of projectile types.");
//        }
//    }

//    public static ElementArray GetElements(Projectile projectile)
//    {
//        int type = projectile.type;
//        if (type < Instance.types.Length && type >= 0)
//        {
//            return Instance.types[type].GetElements(projectile);
//        }
//        else
//        {
//            throw new IndexOutOfRangeException($"Argument {nameof(type)} ({type}) is out of the bounds of projectile types.");
//        }
//    }

//    public static void SetupTypes()
//    {
//        Instance.types = new ProjectileTypeInfo[ProjectileLoader.ProjectileCount];
//        Instance.LoadVanillaProjectiles();

//        int typed = 0;
//        for (int i = 0; i < Instance.types.Length; i++)
//        {
//            ProjectileTypeInfo typeInfo = Instance.types[i];
//            if (typeInfo is null)
//            {
//                Instance.types[i] = ProjectileTypeInfo.Default;
//            }
//            else
//            {
//                typed++;
//            }
//        }

//        ModContent.GetInstance<TerraTyping>().Logger.Info($"Loaded {Instance.types.Length} projectiles, {typed} of which having types.");
//    }

//    private void LoadVanillaProjectiles()
//    {
//        types[0] = ProjectileTypeInfo.Default;
//        LoadProjectile(ProjectileID.AbigailCounter, Element.none);
//        LoadProjectile(ProjectileID.WoodenArrowFriendly, ItemID.WoodenArrow); LoadProjectile(ProjectileID.WoodenArrowFriendly, ItemID.WoodenArrow); // Wooden Arrow
//        LoadProjectile(ProjectileID.FireArrow, ItemID.FlamingArrow); // Fire Arrow
//        LoadProjectile(ProjectileID.Shuriken, ItemID.Shuriken); // Shuriken
//        LoadProjectile(ProjectileID.UnholyArrow, ItemID.UnholyArrow); // Unholy Arrow
//        LoadProjectile(ProjectileID.JestersArrow, ItemID.JestersArrow); // Jester's Arrow
//        LoadProjectile(ProjectileID.EnchantedBoomerang, ItemID.EnchantedBoomerang); // Enchanted Boomerang
//        LoadProjectile(ProjectileID.VilethornBase, ItemID.Vilethorn); // Vilethorn
//        LoadProjectile(ProjectileID.VilethornTip, ItemID.Vilethorn); // Vilethorn
//        LoadProjectile(ProjectileID.Starfury, ItemID.Starfury); // Starfury
//        LoadProjectile(ProjectileID.FallingStar, Element.flying); // Falling Star
//        LoadProjectile(ProjectileID.Bullet, Element.normal); // Bullet
//        LoadProjectile(ProjectileID.BallofFire, ItemID.FlowerofFire); // Ball of Fire
//        LoadProjectile(ProjectileID.MagicMissile, ItemID.MagicMissile); // Magic Missile
//        LoadProjectile(ProjectileID.Flamarang, ItemID.Flamarang); // Flamarang
//        LoadProjectile(ProjectileID.GreenLaser, ItemID.SpaceGun); // Green Laser
//        LoadProjectile(ProjectileID.Bone, ItemID.Bone); // Bone
//        LoadProjectile(ProjectileID.WaterStream, ItemID.AquaScepter); // Water Stream
//        LoadProjectile(ProjectileID.SpikyBall, ItemID.SpikyBall); // Spiky Ball
//        LoadProjectile(ProjectileID.BallOHurt, ItemID.BallOHurt); // Ball 'O Hurt
//        LoadProjectile(ProjectileID.BlueMoon, ItemID.BlueMoon); // Blue Moon
//        LoadProjectile(ProjectileID.WaterBolt, ItemID.WaterBolt); // Water Bolt
//        LoadProjectile(ProjectileID.Bomb, Element.normal); // Bomb
//        LoadProjectile(ProjectileID.Dynamite, Element.normal); // Dynamite
//        LoadProjectile(ProjectileID.Grenade, ItemID.Grenade); // Grenade
//        LoadProjectile(ProjectileID.SandBallFalling, Element.ground); // Sand Ball
//        LoadProjectile(ProjectileID.IvyWhip, Element.grass); // Ivy Whip
//        LoadProjectile(ProjectileID.ThornChakram, ItemID.ThornChakram); // Thorn Chakram
//        LoadProjectile(ProjectileID.Flamelash, ItemID.Flamelash); // Flamelash
//        LoadProjectile(ProjectileID.Sunfury, ItemID.Sunfury); // Sunfury
//        LoadProjectile(ProjectileID.MeteorShot, ItemID.MeteorShot); // Meteor Shot
//        LoadProjectile(ProjectileID.StickyBomb, Element.normal); // Sticky Bomb
//        LoadProjectile(ProjectileID.HarpyFeather, Element.flying); // Harpy Feather
//        LoadProjectile(ProjectileID.MudBall, Element.ground); // Mud Ball
//        LoadProjectile(ProjectileID.AshBallFalling, Element.fire); // Ash Ball
//        LoadProjectile(ProjectileID.HellfireArrow, ItemID.HellfireArrow); // Hellfire Arrow
//        LoadProjectile(ProjectileID.SandBallGun, Element.ground); // Sand Ball
//        LoadProjectile(ProjectileID.Tombstone, Element.ghost); // Tombstone
//        LoadProjectile(ProjectileID.DemonSickle, ItemID.DemonScythe); // Demon Sickle
//        LoadProjectile(ProjectileID.DemonScythe, ItemID.DemonScythe); // Demon Scythe
//        LoadProjectile(ProjectileID.DarkLance, ItemID.DarkLance); // Dark Lance
//        LoadProjectile(ProjectileID.Trident, ItemID.Trident); // Trident
//        LoadProjectile(ProjectileID.ThrowingKnife, ItemID.ThrowingKnife); // Throwing Knife
//        LoadProjectile(ProjectileID.Spear, ItemID.Spear); // Spear
//        LoadProjectile(ProjectileID.Glowstick, Element.normal); // Glowstick
//        LoadProjectile(ProjectileID.Seed, ItemID.Seed); // Seed
//        LoadProjectile(ProjectileID.WoodenBoomerang, ItemID.WoodenBoomerang); // Wooden Boomerang
//        LoadProjectile(ProjectileID.StickyGlowstick, Element.normal); // Sticky Glowstick
//        LoadProjectile(ProjectileID.PoisonedKnife, ItemID.PoisonedKnife); // Poisoned Knife
//        LoadProjectile(ProjectileID.Stinger, Element.poison); // Stinger
//        LoadProjectile(ProjectileID.EbonsandBallFalling, Element.dark); // Ebonsand Ball
//        LoadProjectile(ProjectileID.CobaltChainsaw, ItemID.CobaltChainsaw); // Cobalt Chainsaw
//        LoadProjectile(ProjectileID.CobaltDrill, ItemID.CobaltDrill); // Cobalt Drill
//        LoadProjectile(ProjectileID.CobaltNaginata, ItemID.CobaltNaginata); // Cobalt Naginata
//        LoadProjectile(ProjectileID.MythrilDrill, ItemID.MythrilDrill); // Mythril Drill
//        LoadProjectile(ProjectileID.MythrilChainsaw, ItemID.MythrilChainsaw); // Mythril Chainsaw
//        LoadProjectile(ProjectileID.AdamantiteChainsaw, ItemID.AdamantiteChainsaw); // Adamantite Chainsaw
//        LoadProjectile(ProjectileID.AdamantiteDrill, ItemID.AdamantiteDrill); // Adamantite Drill
//        LoadProjectile(ProjectileID.AdamantiteGlaive, ItemID.AdamantiteGlaive); // Adamantite Glaive
//        LoadProjectile(ProjectileID.TheDaoofPow, ItemID.DaoofPow); // The Dao of Pow
//        LoadProjectile(ProjectileID.MythrilHalberd, ItemID.MythrilHalberd); // Mythril Halberd
//        LoadProjectile(ProjectileID.EbonsandBallGun, Element.dark); // Ebonsand Ball
//        LoadProjectile(ProjectileID.PearlSandBallFalling, Element.fairy); // Pearl Sand Ball
//        LoadProjectile(ProjectileID.PearlSandBallGun, Element.fairy); // Pearl Sand Ball
//        LoadProjectile(ProjectileID.HolyWater, ItemID.HolyWater); // Holy Water
//        LoadProjectile(ProjectileID.UnholyWater, ItemID.UnholyWater); // Unholy Water
//        LoadProjectile(ProjectileID.SiltBall, Element.ground); // Silt Ball
//        LoadProjectile(ProjectileID.DualHookBlue, Element.steel); // Hook
//        LoadProjectile(ProjectileID.DualHookRed, Element.steel); // Hook
//        LoadProjectile(ProjectileID.HappyBomb, Element.normal); // Happy Bomb
//        LoadProjectile(ProjectileID.QuarterNote, ItemID.MagicalHarp); // Note
//        LoadProjectile(ProjectileID.EighthNote, ItemID.MagicalHarp); // Note
//        LoadProjectile(ProjectileID.TiedEighthNote, ItemID.MagicalHarp); // Note
//        LoadProjectile(ProjectileID.RainbowRodBullet, ItemID.RainbowRod); // Rainbow
//        LoadProjectile(ProjectileID.IceBlock, ItemID.IceRod); // Ice Block
//        LoadProjectile(ProjectileID.WoodenArrowHostile, Element.normal); // Wooden Arrow
//        LoadProjectile(ProjectileID.FlamingArrow, Element.fire); // Flaming Arrow
//        LoadProjectile(ProjectileID.EyeLaser, Element.electric); // Eye Laser
//        LoadProjectile(ProjectileID.PinkLaser, Element.electric); // Pink Laser
//        LoadProjectile(ProjectileID.Flames, ItemID.Flamethrower); // Flames
//        LoadProjectile(ProjectileID.PurpleLaser, ItemID.LaserRifle); // Purple Laser
//        LoadProjectile(ProjectileID.CrystalBullet, ItemID.CrystalBullet); // Crystal Bullet
//        LoadProjectile(ProjectileID.CrystalShard, ItemID.CrystalBullet); // Crystal Shard
//        LoadProjectile(ProjectileID.HolyArrow, ItemID.HolyArrow); // Holy Arrow
//        LoadProjectile(ProjectileID.HallowStar, Element.flying); // Hallow Star
//        LoadProjectile(ProjectileID.MagicDagger, ItemID.MagicDagger); // Magic Dagger
//        LoadProjectile(ProjectileID.CrystalStorm, ItemID.CrystalStorm); // Crystal Storm
//        LoadProjectile(ProjectileID.CursedFlameFriendly, ItemID.CursedFlames); // Cursed Flame
//        LoadProjectile(ProjectileID.CursedFlameHostile, Element.dark, Element.fire); // Cursed Flame
//        LoadProjectile(ProjectileID.PoisonDart, Element.poison); // Poison Dart
//        LoadProjectile(ProjectileID.Boulder, Element.rock); // Boulder
//        LoadProjectile(ProjectileID.DeathLaser, Element.electric); // Death Laser
//        LoadProjectile(ProjectileID.EyeFire, Element.dark, Element.fire); // Eye Fire
//        LoadProjectile(ProjectileID.BombSkeletronPrime, Element.normal); // Bomb
//        LoadProjectile(ProjectileID.CursedArrow, ItemID.CursedArrow); // Cursed Arrow
//        LoadProjectile(ProjectileID.CursedBullet, ItemID.CursedBullet); // Cursed Bullet
//        LoadProjectile(ProjectileID.Gungnir, ItemID.Gungnir); // Gungnir
//        LoadProjectile(ProjectileID.LightDisc, ItemID.LightDisc); // Light Disc
//        LoadProjectile(ProjectileID.Hamdrax, ItemID.Drax); // Hamdrax
//        LoadProjectile(ProjectileID.Explosives, Element.normal); // Explosives
//        LoadProjectile(ProjectileID.SnowBallHostile, Element.ice); // Snow Ball
//        LoadProjectile(ProjectileID.BulletSnowman, Element.normal); // Bullet
//        LoadProjectile(ProjectileID.IceBoomerang, ItemID.IceBoomerang); // Ice Boomerang
//        LoadProjectile(ProjectileID.UnholyTridentFriendly, ItemID.UnholyTrident); // Unholy Trident
//        LoadProjectile(ProjectileID.UnholyTridentHostile, ItemID.UnholyTrident); // Unholy Trident
//        LoadProjectile(ProjectileID.SwordBeam, ItemID.BeamSword); // Sword Beam
//        LoadProjectile(ProjectileID.BoneArrow, ItemID.Marrow); // Bone Arrow
//        LoadProjectile(ProjectileID.IceBolt, ItemID.IceBlade); // Ice Bolt
//        LoadProjectile(ProjectileID.FrostBoltSword, ItemID.Frostbrand); // Frost Bolt
//        LoadProjectile(ProjectileID.FrostArrow, ItemID.IceBow); // Frost Arrow
//        LoadProjectile(ProjectileID.AmethystBolt, ItemID.AmethystStaff); // Amethyst Bolt
//        LoadProjectile(ProjectileID.TopazBolt, ItemID.TopazStaff); // Topaz Bolt
//        LoadProjectile(ProjectileID.SapphireBolt, ItemID.SapphireStaff); // Sapphire Bolt
//        LoadProjectile(ProjectileID.EmeraldBolt, ItemID.EmeraldStaff); // Emerald Bolt
//        LoadProjectile(ProjectileID.RubyBolt, ItemID.RubyStaff); // Ruby Bolt
//        LoadProjectile(ProjectileID.DiamondBolt, ItemID.DiamondStaff); // Diamond Bolt
//        LoadProjectile(ProjectileID.FrostBlastHostile, Element.ice); // Frost Blast
//        LoadProjectile(ProjectileID.RuneBlast, Element.psychic); // Rune Blast
//        LoadProjectile(ProjectileID.MushroomSpear, ItemID.MushroomSpear); // Mushroom Spear
//        LoadProjectile(ProjectileID.Mushroom, Element.grass); // Mushroom
//        LoadProjectile(ProjectileID.TerraBeam, ItemID.TerraBlade); // Terra Beam
//        LoadProjectile(ProjectileID.GrenadeI, ItemID.RocketI); // Grenade
//        LoadProjectile(ProjectileID.RocketI, ItemID.RocketI); // Rocket
//        LoadProjectile(ProjectileID.ProximityMineI, ItemID.RocketI); // Proximity Mine
//        LoadProjectile(ProjectileID.GrenadeII, ItemID.RocketII); // Grenade
//        LoadProjectile(ProjectileID.RocketII, ItemID.RocketII); // Rocket
//        LoadProjectile(ProjectileID.ProximityMineII, ItemID.RocketII); // Proximity Mine
//        LoadProjectile(ProjectileID.GrenadeIII, ItemID.RocketIII); // Grenade
//        LoadProjectile(ProjectileID.RocketIII, ItemID.RocketIII); // Rocket
//        LoadProjectile(ProjectileID.ProximityMineIII, ItemID.RocketIII); // Proximity Mine
//        LoadProjectile(ProjectileID.GrenadeIV, ItemID.RocketIV); // Grenade
//        LoadProjectile(ProjectileID.RocketIV, ItemID.RocketIV); // Rocket
//        LoadProjectile(ProjectileID.ProximityMineIV, ItemID.RocketIV); // Proximity Mine
//        LoadProjectile(ProjectileID.NettleBurstRight, ItemID.NettleBurst); // Nettle Burst
//        LoadProjectile(ProjectileID.NettleBurstLeft, ItemID.NettleBurst); // Nettle Burst
//        LoadProjectile(ProjectileID.NettleBurstEnd, ItemID.NettleBurst); // Nettle Burst
//        LoadProjectile(ProjectileID.TheRottedFork, ItemID.TheRottedFork); // The Rotted Fork
//        LoadProjectile(ProjectileID.TheMeatball, ItemID.TheMeatball); // The Meatball
//        LoadProjectile(ProjectileID.LightBeam, ItemID.TrueExcalibur); // Light Beam
//        LoadProjectile(ProjectileID.NightBeam, ItemID.TrueNightsEdge); // Night Beam
//        LoadProjectile(ProjectileID.CopperCoin, ItemID.CoinGun); // Copper Coin
//        LoadProjectile(ProjectileID.SilverCoin, ItemID.CoinGun); // Silver Coin
//        LoadProjectile(ProjectileID.GoldCoin, ItemID.CoinGun); // Gold Coin
//        LoadProjectile(ProjectileID.PlatinumCoin, ItemID.CoinGun); // Platinum Coin
//        LoadProjectile(ProjectileID.CannonballFriendly, ItemID.Cannonball); // Cannonball
//        LoadProjectile(ProjectileID.Flare, ItemID.Flare); // Flare
//        LoadProjectile(ProjectileID.Landmine, ItemID.LandMine); // Landmine
//        LoadProjectile(ProjectileID.SnowBallFriendly, ItemID.Snowball); // Snow Ball
//        LoadProjectile(ProjectileID.FrostburnArrow, ItemID.FrostburnArrow); // Frostburn Arrow
//        LoadProjectile(ProjectileID.EnchantedBeam, ItemID.EnchantedSword); // Enchanted Beam
//        LoadProjectile(ProjectileID.IceSpike, Element.ice); // Ice Spike
//        LoadProjectile(ProjectileID.JungleSpike, Element.poison); // Jungle Spike
//        LoadProjectile(ProjectileID.SlushBall, Element.ice); // Slush Ball
//        LoadProjectile(ProjectileID.BulletDeadeye, Element.normal); // Bullet
//        LoadProjectile(ProjectileID.Bee, ItemID.BeeGun); // Bee
//        LoadProjectile(ProjectileID.PossessedHatchet, ItemID.PossessedHatchet); // Possessed Hatchet
//        LoadProjectile(ProjectileID.Beenade, ItemID.Beenade); // Beenade
//        LoadProjectile(ProjectileID.PoisonDartTrap, Element.poison); // Poison Dart
//        LoadProjectile(ProjectileID.SpikyBallTrap, Element.normal); // Spiky Ball
//        LoadProjectile(ProjectileID.SpearTrap, Element.normal); // Spear
//        LoadProjectile(ProjectileID.FlamethrowerTrap, Element.fire); // Flamethrower
//        LoadProjectile(ProjectileID.FlamesTrap, Element.fire); // Flames
//        LoadProjectile(ProjectileID.Wasp, ItemID.WaspGun); // Wasp
//        LoadProjectile(ProjectileID.MechanicalPiranha, ItemID.PiranhaGun); // Mechanical Piranha
//        LoadProjectile(ProjectileID.PygmySpear, ItemID.PygmyStaff); // Pygmy
//        LoadProjectile(ProjectileID.Leaf, ItemID.LeafBlower); // Leaf
//        LoadProjectile(ProjectileID.ChlorophyteBullet, ItemID.ChlorophyteBullet); // Bullet
//        LoadProjectile(ProjectileID.PalladiumPike, ItemID.PalladiumPike); // Palladium Pike
//        LoadProjectile(ProjectileID.PalladiumDrill, ItemID.PalladiumDrill); // Palladium Drill
//        LoadProjectile(ProjectileID.PalladiumChainsaw, ItemID.PalladiumChainsaw); // Palladium Chainsaw
//        LoadProjectile(ProjectileID.OrichalcumHalberd, ItemID.OrichalcumHalberd); // Orichalcum Halberd
//        LoadProjectile(ProjectileID.OrichalcumDrill, ItemID.OrichalcumDrill); // Orichalcum Drill
//        LoadProjectile(ProjectileID.OrichalcumChainsaw, ItemID.OrichalcumChainsaw); // Orichalcum Chainsaw
//        LoadProjectile(ProjectileID.TitaniumTrident, ItemID.TitaniumTrident); // Titanium Trident
//        LoadProjectile(ProjectileID.TitaniumDrill, ItemID.TitaniumDrill); // Titanium Drill
//        LoadProjectile(ProjectileID.TitaniumChainsaw, ItemID.TitaniumChainsaw); // Titanium Chainsaw
//        LoadProjectile(ProjectileID.FlowerPetal, Element.fairy); // Flower Petal
//        LoadProjectile(ProjectileID.ChlorophytePartisan, ItemID.ChlorophytePartisan); // Chlorophyte Partisan
//        LoadProjectile(ProjectileID.ChlorophyteDrill, ItemID.ChlorophyteDrill); // Chlorophyte Drill
//        LoadProjectile(ProjectileID.ChlorophyteChainsaw, ItemID.ChlorophyteChainsaw); // Chlorophyte Chainsaw
//        LoadProjectile(ProjectileID.ChlorophyteArrow, ItemID.ChlorophyteArrow); // Chlorophyte Arrow
//        LoadProjectile(ProjectileID.CrystalLeafShot, Element.grass); // Crystal Leaf
//        LoadProjectile(ProjectileID.SporeCloud, ItemID.ChlorophyteSaber); // Spore Cloud
//        LoadProjectile(ProjectileID.ChlorophyteOrb, ItemID.ChlorophyteClaymore); // Chlorophyte Orb
//        LoadProjectile(ProjectileID.RainFriendly, ItemID.NimbusRod); // Rain
//        LoadProjectile(ProjectileID.CannonballHostile, Element.water); // Cannonball
//        LoadProjectile(ProjectileID.CrimsandBallFalling, Element.blood); // Crimsand Ball
//        LoadProjectile(ProjectileID.BulletHighVelocity, ItemID.HighVelocityBullet); // Bullet
//        LoadProjectile(ProjectileID.BloodRain, ItemID.CrimsonRod); // Blood Rain
//        LoadProjectile(ProjectileID.Stynger, ItemID.StyngerBolt); // Stynger
//        LoadProjectile(ProjectileID.StyngerShrapnel, ItemID.StyngerBolt); // Stynger
//        LoadProjectile(ProjectileID.FlowerPow, ItemID.FlowerPow); // Flower Pow
//        LoadProjectile(ProjectileID.FlowerPowPetal, ItemID.FlowerPow); // Flower Pow
//        LoadProjectile(ProjectileID.RainbowFront, ItemID.RainbowGun); // Rainbow
//        LoadProjectile(ProjectileID.RainbowBack, ItemID.RainbowGun); // Rainbow
//        LoadProjectile(ProjectileID.ChlorophyteJackhammer, ItemID.ChlorophyteJackhammer); // Chlorophyte Jackhammer
//        LoadProjectile(ProjectileID.BallofFrost, ItemID.FlowerofFrost); // Ball of Frost
//        LoadProjectile(ProjectileID.MagnetSphereBall, ItemID.MagnetSphere); // Magnet Sphere
//        LoadProjectile(ProjectileID.MagnetSphereBolt, ItemID.MagnetSphere); // Magnet Sphere
//        LoadProjectile(ProjectileID.FrostBeam, Element.ice); // Frost Beam
//        LoadProjectile(ProjectileID.Fireball, Element.fire); // Fireball
//        LoadProjectile(ProjectileID.EyeBeam, Element.electric); // Eye Beam
//        LoadProjectile(ProjectileID.HeatRay, ItemID.HeatRay); // Heat Ray
//        LoadProjectile(ProjectileID.BoulderStaffOfEarth, ItemID.StaffofEarth); // Boulder
//        LoadProjectile(ProjectileID.GolemFist, ItemID.GolemFist); // Golem Fist
//        LoadProjectile(ProjectileID.IceSickle, ItemID.IceSickle); // Ice Sickle
//        LoadProjectile(ProjectileID.RainNimbus, Element.water); // Rain
//        LoadProjectile(ProjectileID.PoisonFang, ItemID.PoisonStaff); // Poison Fang
//        LoadProjectile(ProjectileID.BabySlime, ItemID.SlimeStaff); // Baby Slime
//        LoadProjectile(ProjectileID.PoisonDartBlowgun, ItemID.PoisonDart); // Poison Dart
//        LoadProjectile(ProjectileID.Skull, Element.bone); // Skull
//        LoadProjectile(ProjectileID.BoxingGlove, ItemID.KOCannon); // Boxing Glove
//        LoadProjectile(ProjectileID.Bananarang, ItemID.Bananarang); // Bananarang
//        LoadProjectile(ProjectileID.ChainKnife, ItemID.ChainKnife); // Chain Knife
//        LoadProjectile(ProjectileID.DeathSickle, ItemID.DeathSickle); // Death Sickle
//        LoadProjectile(ProjectileID.SeedPlantera, Element.grass); // Seed
//        LoadProjectile(ProjectileID.PoisonSeedPlantera, Element.poison); // Poison Seed
//        LoadProjectile(ProjectileID.ThornBall, Element.grass); // Thorn Ball
//        LoadProjectile(ProjectileID.IchorArrow, ItemID.IchorArrow); // Ichor Arrow
//        LoadProjectile(ProjectileID.IchorBullet, ItemID.IchorBullet); // Ichor Bullet
//        LoadProjectile(ProjectileID.GoldenShowerFriendly, ItemID.GoldenShower); // Golden Shower
//        LoadProjectile(ProjectileID.ExplosiveBunny, ItemID.ExplosiveBunny); // Explosive Bunny
//        LoadProjectile(ProjectileID.VenomArrow, ItemID.VenomArrow); // Venom Arrow
//        LoadProjectile(ProjectileID.VenomBullet, ItemID.VenomBullet); // Venom Bullet
//        LoadProjectile(ProjectileID.PartyBullet, ItemID.PartyBullet); // Party Bullet
//        LoadProjectile(ProjectileID.NanoBullet, ItemID.NanoBullet); // Nano Bullet
//        LoadProjectile(ProjectileID.ExplosiveBullet, ItemID.ExplodingBullet); // Explosive Bullet
//        LoadProjectile(ProjectileID.GoldenBullet, ItemID.GoldenBullet); // Golden Bullet
//        LoadProjectile(ProjectileID.GoldenShowerHostile, ItemID.GoldenShower); // Golden Shower
//        LoadProjectile(ProjectileID.ShadowBeamHostile, ItemID.ShadowbeamStaff); // Shadow Beam
//        LoadProjectile(ProjectileID.InfernoHostileBolt, ItemID.InfernoFork); // Inferno
//        LoadProjectile(ProjectileID.InfernoHostileBlast, ItemID.InfernoFork); // Inferno
//        LoadProjectile(ProjectileID.LostSoulHostile, ItemID.SpectreStaff); // Lost Soul
//        LoadProjectile(ProjectileID.ShadowBeamFriendly, ItemID.ShadowbeamStaff); // Shadow Beam
//        LoadProjectile(ProjectileID.InfernoFriendlyBolt, ItemID.InfernoFork); // Inferno
//        LoadProjectile(ProjectileID.InfernoFriendlyBlast, ItemID.InfernoFork); // Inferno
//        LoadProjectile(ProjectileID.LostSoulFriendly, ItemID.SpectreStaff); // Lost Soul
//        LoadProjectile(ProjectileID.Shadowflames, Element.ghost, Element.fire); // Shadowflames
//        LoadProjectile(ProjectileID.PaladinsHammerHostile, ItemID.PaladinsHammer); // Paladin's Hammer
//        LoadProjectile(ProjectileID.PaladinsHammerFriendly, ItemID.PaladinsHammer); // Paladin's Hammer
//        LoadProjectile(ProjectileID.SniperBullet, ItemID.HighVelocityBullet); // Sniper Bullet
//        LoadProjectile(ProjectileID.RocketSkeleton, ItemID.RocketI); // Rocket
//        LoadProjectile(ProjectileID.VampireKnife, ItemID.VampireKnives); // Vampire Knife
//        LoadProjectile(ProjectileID.EatersBite, ItemID.ScourgeoftheCorruptor); // Eater's Bite
//        LoadProjectile(ProjectileID.TinyEater, ItemID.ScourgeoftheCorruptor); // Tiny Eater
//        LoadProjectile(ProjectileID.FrostHydra, ItemID.StaffoftheFrostHydra); // Frost Hydra
//        LoadProjectile(ProjectileID.FrostBlastFriendly, ItemID.StaffoftheFrostHydra); // Frost Blast
//        LoadProjectile(ProjectileID.BlueFlare, ItemID.BlueFlare); // Blue Flare
//        LoadProjectile(ProjectileID.CandyCorn, ItemID.CandyCorn); // Candy Corn
//        LoadProjectile(ProjectileID.JackOLantern, ItemID.ExplosiveJackOLantern); // Jack 'O Lantern
//        LoadProjectile(ProjectileID.Bat, ItemID.BatScepter); // Bat
//        LoadProjectile(ProjectileID.Raven, ItemID.RavenStaff); // Raven
//        LoadProjectile(ProjectileID.RottenEgg, ItemID.RottenEgg); // Rotten Egg
//        LoadProjectile(ProjectileID.BloodyMachete, ItemID.BloodyMachete); // Bloody Machete
//        LoadProjectile(ProjectileID.FlamingJack, ItemID.TheHorsemansBlade); // Flaming Jack
//        LoadProjectile(ProjectileID.Stake, ItemID.StakeLauncher); // Stake
//        LoadProjectile(ProjectileID.FlamingWood, Element.fire); // Flaming Wood
//        LoadProjectile(ProjectileID.GreekFire1, Element.fire); // Greek Fire
//        LoadProjectile(ProjectileID.GreekFire2, Element.fire); // Greek Fire
//        LoadProjectile(ProjectileID.GreekFire3, Element.fire); // Greek Fire
//        LoadProjectile(ProjectileID.FlamingScythe, Element.fire); // Flaming Scythe
//        LoadProjectile(ProjectileID.StarAnise, ItemID.StarAnise); // Star Anise
//        LoadProjectile(ProjectileID.FruitcakeChakram, ItemID.FruitcakeChakram); // Fruitcake Chakram
//        LoadProjectile(ProjectileID.OrnamentFriendly, Element.ice); // Ornament
//        LoadProjectile(ProjectileID.PineNeedleFriendly, ItemID.Razorpine); // Pine Needle
//        LoadProjectile(ProjectileID.Blizzard, ItemID.BlizzardStaff); // Blizzard
//        LoadProjectile(ProjectileID.RocketSnowmanI, ItemID.SnowmanCannon); // Rocket
//        LoadProjectile(ProjectileID.RocketSnowmanII, ItemID.SnowmanCannon); // Rocket
//        LoadProjectile(ProjectileID.RocketSnowmanIII, ItemID.SnowmanCannon); // Rocket
//        LoadProjectile(ProjectileID.RocketSnowmanIV, ItemID.SnowmanCannon); // Rocket
//        LoadProjectile(ProjectileID.NorthPoleWeapon, ItemID.NorthPole); // North Pole
//        LoadProjectile(ProjectileID.NorthPoleSpear, ItemID.NorthPole); // North Pole
//        LoadProjectile(ProjectileID.NorthPoleSnowflake, ItemID.NorthPole); // North Pole
//        LoadProjectile(ProjectileID.PineNeedleHostile, ItemID.Razorpine); // Pine Needle
//        LoadProjectile(ProjectileID.OrnamentHostile, Element.ice); // Ornament
//        LoadProjectile(ProjectileID.OrnamentHostileShrapnel, Element.ice); // Ornament
//        LoadProjectile(ProjectileID.FrostWave, Element.ice); // Frost Wave
//        LoadProjectile(ProjectileID.FrostShard, Element.ice); // Frost Shard
//        LoadProjectile(ProjectileID.Missile, Element.normal); // Missile
//        LoadProjectile(ProjectileID.Present, Element.steel); // Present
//        LoadProjectile(ProjectileID.Spike, Element.steel); // Spike
//        LoadProjectile(ProjectileID.CrimsandBallGun, Element.blood); // Crimsand Ball
//        LoadProjectile(ProjectileID.VenomFang, ItemID.VenomStaff); // Venom Fang
//        LoadProjectile(ProjectileID.SpectreWrath, Element.ghost); // Spectre Wrath
//        LoadProjectile(ProjectileID.PulseBolt, ItemID.PulseBow); // Pulse Bolt
//        LoadProjectile(ProjectileID.FrostBoltStaff, ItemID.FrostStaff); // Frost Bolt
//        LoadProjectile(ProjectileID.ObsidianSwordfish, ItemID.ObsidianSwordfish); // Obsidian Swordfish
//        LoadProjectile(ProjectileID.Swordfish, ItemID.Swordfish); // Swordfish
//        LoadProjectile(ProjectileID.SawtoothShark, ItemID.SawtoothShark); // Sawtooth Shark
//        LoadProjectile(ProjectileID.Hornet, ItemID.HornetStaff); // Hornet
//        LoadProjectile(ProjectileID.HornetStinger, ItemID.HornetStaff); // Hornet Stinger
//        LoadProjectile(ProjectileID.FlyingImp, ItemID.ImpStaff); // Flying Imp
//        LoadProjectile(ProjectileID.ImpFireball, ItemID.ImpStaff); // Imp Fireball
//        LoadProjectile(ProjectileID.SpiderHiver, ItemID.QueenSpiderStaff); // Spider Turret
//        LoadProjectile(ProjectileID.SpiderEgg, ItemID.QueenSpiderStaff); // Spider Egg
//        LoadProjectile(ProjectileID.BabySpider, ItemID.QueenSpiderStaff); // Baby Spider
//        LoadProjectile(ProjectileID.Anchor, ItemID.Anchor); // Anchor
//        LoadProjectile(ProjectileID.Sharknado, Element.water); // Sharknado
//        LoadProjectile(ProjectileID.SharknadoBolt, Element.water); // Sharknado Bolt
//        LoadProjectile(ProjectileID.Cthulunado, Element.water); // Cthulunado
//        LoadProjectile(ProjectileID.Retanimini, Element.steel); // Retanimini
//        LoadProjectile(ProjectileID.Spazmamini, Element.steel); // Spazmamini
//        LoadProjectile(ProjectileID.MiniRetinaLaser, Element.electric); // Mini Retina Laser
//        LoadProjectile(ProjectileID.VenomSpider, ItemID.SpiderStaff); // Venom Spider
//        LoadProjectile(ProjectileID.JumperSpider, ItemID.SpiderStaff); // Jumper Spider
//        LoadProjectile(ProjectileID.DangerousSpider, ItemID.SpiderStaff); // Dangerous Spider
//        LoadProjectile(ProjectileID.OneEyedPirate, ItemID.PirateStaff); // One Eyed Pirate
//        LoadProjectile(ProjectileID.SoulscourgePirate, ItemID.PirateStaff); // Soulscourge Pirate
//        LoadProjectile(ProjectileID.PirateCaptain, ItemID.PirateStaff); // Pirate Captain
//        LoadProjectile(ProjectileID.StickyGrenade, ItemID.StickyGrenade); // Sticky Grenade
//        LoadProjectile(ProjectileID.MolotovCocktail, ItemID.MolotovCocktail); // Molotov Cocktail
//        LoadProjectile(ProjectileID.MolotovFire, ItemID.MolotovCocktail); // Molotov Fire
//        LoadProjectile(ProjectileID.MolotovFire2, ItemID.MolotovCocktail); // Molotov Fire
//        LoadProjectile(ProjectileID.MolotovFire3, ItemID.MolotovCocktail); // Molotov Fire
//        LoadProjectile(ProjectileID.Flairon, Element.dragon); // Flairon
//        LoadProjectile(ProjectileID.FlaironBubble, Element.water); // Flairon Bubble
//        LoadProjectile(ProjectileID.Tempest, Element.water); // Tempest
//        LoadProjectile(ProjectileID.MiniSharkron, Element.dragon); // Mini Sharkron
//        LoadProjectile(ProjectileID.Typhoon, ItemID.RazorbladeTyphoon); // Typhoon
//        LoadProjectile(ProjectileID.Bubble, ItemID.BubbleGun); // Bubble
//        LoadProjectile(ProjectileID.CopperCoinsFalling, Element.normal); // Copper Coins
//        LoadProjectile(ProjectileID.SilverCoinsFalling, Element.normal); // Silver Coins
//        LoadProjectile(ProjectileID.GoldCoinsFalling, Element.normal); // Gold Coins
//        LoadProjectile(ProjectileID.PlatinumCoinsFalling, Element.normal); // Platinum Coins
//        LoadProjectile(ProjectileID.UFOMinion, ItemID.XenoStaff); // UFO
//        LoadProjectile(ProjectileID.Meteor1, ItemID.MeteorStaff); // Meteor
//        LoadProjectile(ProjectileID.Meteor2, ItemID.MeteorStaff); // Meteor
//        LoadProjectile(ProjectileID.Meteor3, ItemID.MeteorStaff); // Meteor
//        LoadProjectile(ProjectileID.VortexChainsaw, ItemID.VortexDrill); // Vortex Chainsaw
//        LoadProjectile(ProjectileID.VortexDrill, ItemID.VortexDrill); // Vortex Drill
//        LoadProjectile(ProjectileID.NebulaChainsaw, ItemID.NebulaDrill); // Nebula Chainsaw
//        LoadProjectile(ProjectileID.NebulaDrill, ItemID.NebulaDrill); // Nebula Drill
//        LoadProjectile(ProjectileID.SolarFlareChainsaw, ItemID.SolarFlareDrill); // Solar Flare Chainsaw
//        LoadProjectile(ProjectileID.SolarFlareDrill, ItemID.SolarFlareDrill); // Solar Flare Drill
//        LoadProjectile(ProjectileID.UFOLaser, Element.electric); // UFO Ray
//        LoadProjectile(ProjectileID.ScutlixLaserFriendly, Element.psychic); // Scutlix Laser
//        LoadProjectile(ProjectileID.MartianTurretBolt, Element.electric); // Electric Bolt
//        LoadProjectile(ProjectileID.BrainScramblerBolt, Element.dark); // Brain Scrambling Bolt
//        LoadProjectile(ProjectileID.GigaZapperSpear, Element.electric); // Gigazapper Spearhead
//        LoadProjectile(ProjectileID.RayGunnerLaser, Element.electric); // Laser Ray
//        LoadProjectile(ProjectileID.LaserMachinegun, ItemID.LaserMachinegun); // Laser Machinegun
//        LoadProjectile(ProjectileID.LaserMachinegunLaser, Element.electric); // Laser
//        LoadProjectile(ProjectileID.ScutlixLaserCrosshair, Element.electric); // Scutlix Crosshair
//        LoadProjectile(ProjectileID.ElectrosphereMissile, ItemID.ElectrosphereLauncher); // Electrosphere Missile
//        LoadProjectile(ProjectileID.Electrosphere, Element.electric); // Electrosphere
//        LoadProjectile(ProjectileID.Xenopopper, ItemID.Xenopopper); // Xenopopper
//        LoadProjectile(ProjectileID.LaserDrill, ItemID.LaserDrill); // Laser Drill
//        LoadProjectile(ProjectileID.SaucerDeathray, Element.psychic); // Martian Deathray
//        LoadProjectile(ProjectileID.SaucerMissile, Element.normal); // Martian Rocket
//        LoadProjectile(ProjectileID.SaucerLaser, Element.electric); // Saucer Laser
//        LoadProjectile(ProjectileID.SaucerScrap, Element.steel); // Saucer Scrap
//        LoadProjectile(ProjectileID.InfluxWaver, Element.electric); // Influx Waver
//        LoadProjectile(ProjectileID.ChargedBlasterOrb, ItemID.ChargedBlasterCannon); // Charged Blaster Orb
//        LoadProjectile(ProjectileID.ChargedBlasterLaser, ItemID.ChargedBlasterCannon); // Charged Blaster Laser
//        LoadProjectile(ProjectileID.CultistBossIceMist, Element.ice); // Ice Mist
//        LoadProjectile(ProjectileID.CultistBossLightningOrb, Element.electric); // Lightning Orb
//        LoadProjectile(ProjectileID.CultistBossLightningOrbArc, Element.electric); // Lightning Orb Arc
//        LoadProjectile(ProjectileID.CultistBossFireBall, Element.fire); // Fireball
//        LoadProjectile(ProjectileID.CultistBossFireBallClone, Element.dark); // Shadow Fireball
//        LoadProjectile(ProjectileID.BeeArrow, ItemID.BeesKnees); // Bee Arrow
//        LoadProjectile(ProjectileID.SkeletonBone, Element.bone); // Bone
//        LoadProjectile(ProjectileID.WebSpit, Element.poison); // Web spit
//        LoadProjectile(ProjectileID.BoneArrowFromMerchant, ItemID.BoneArrow); // Bone Arrow
//        LoadProjectile(ProjectileID.SoulDrain, ItemID.SoulDrain); // Soul Drain
//        LoadProjectile(ProjectileID.CrystalDart, ItemID.CrystalDart); // Crystal Dart
//        LoadProjectile(ProjectileID.CursedDart, ItemID.CursedDart); // Cursed Dart
//        LoadProjectile(ProjectileID.CursedDartFlame, ItemID.CursedDart); // Cursed Flame
//        LoadProjectile(ProjectileID.IchorDart, ItemID.IchorDart); // Ichor Dart
//        LoadProjectile(ProjectileID.ChainGuillotine, ItemID.ChainGuillotines); // Chain Guillotine
//        LoadProjectile(ProjectileID.ClingerStaff, ItemID.ClingerStaff); // Cursed Flames
//        LoadProjectile(ProjectileID.SeedlerNut, Element.grass); // Seedler
//        LoadProjectile(ProjectileID.SeedlerThorn, Element.grass); // Seedler
//        LoadProjectile(ProjectileID.Hellwing, ItemID.HellwingBow); // Hellwing
//        LoadProjectile(ProjectileID.FlyingKnife, ItemID.FlyingKnife); // Flying Knife
//        LoadProjectile(ProjectileID.CrystalVileShardHead, ItemID.CrystalVileShard); // Crystal Vile Shard
//        LoadProjectile(ProjectileID.CrystalVileShardShaft, ItemID.CrystalVileShard); // Crystal Vile Shard
//        LoadProjectile(ProjectileID.ShadowFlameArrow, Element.ghost, Element.fire); // Shadowflame Arrow
//        LoadProjectile(ProjectileID.ShadowFlame, ItemID.ShadowFlameHexDoll); // Shadowflame
//        LoadProjectile(ProjectileID.ShadowFlameKnife, ItemID.ShadowFlameKnife); // Shadowflame Knife
//        LoadProjectile(ProjectileID.Nail, Element.steel); // Nail
//        LoadProjectile(ProjectileID.DrManFlyFlask, Element.poison); // Flask
//        LoadProjectile(ProjectileID.Meowmere, ItemID.Meowmere); // Meowmere
//        LoadProjectile(ProjectileID.StarWrath, ItemID.StarWrath); // Star Wrath
//        LoadProjectile(ProjectileID.Spark, Element.fire); // Spark
//        LoadProjectile(ProjectileID.JavelinFriendly, ItemID.Javelin); // Javelin
//        LoadProjectile(ProjectileID.JavelinHostile, ItemID.Javelin); // Javelin
//        LoadProjectile(ProjectileID.ButchersChainsaw, ItemID.ButchersChainsaw); // Butcher's Chainsaw
//        LoadProjectile(ProjectileID.ToxicFlask, ItemID.ToxicFlask); // Toxic Flask
//        LoadProjectile(ProjectileID.ToxicCloud, ItemID.ToxicFlask); // Toxic Cloud
//        LoadProjectile(ProjectileID.ToxicCloud2, ItemID.ToxicFlask); // Toxic Cloud
//        LoadProjectile(ProjectileID.ToxicCloud3, ItemID.ToxicFlask); // Toxic Cloud
//        LoadProjectile(ProjectileID.NailFriendly, ItemID.NailGun); // Nail
//        LoadProjectile(ProjectileID.BouncyGrenade, ItemID.BouncyGrenade); // Bouncy Grenade
//        LoadProjectile(ProjectileID.FrostDaggerfish, ItemID.FrostDaggerfish); // Frost Daggerfish
//        LoadProjectile(ProjectileID.CrystalPulse, ItemID.CrystalSerpent); // Crystal Charge
//        LoadProjectile(ProjectileID.CrystalPulse2, ItemID.CrystalSerpent); // Crystal Charge
//        LoadProjectile(ProjectileID.ToxicBubble, ItemID.Toxikarp); // Toxic Bubble
//        LoadProjectile(ProjectileID.IchorSplash, ItemID.Bladetongue); // Ichor Splash
//        LoadProjectile(ProjectileID.CultistBossParticle, Element.electric); // Energy
//        LoadProjectile(ProjectileID.BoneGloveProj, Element.bone); // XBone
//        LoadProjectile(ProjectileID.DeadlySphere, ItemID.DeadlySphereStaff); // Deadly Sphere
//        LoadProjectile(ProjectileID.Code1, ItemID.Code1); // Yoyo
//        LoadProjectile(ProjectileID.MedusaHead, ItemID.MedusaHead); // Medusa Ray
//        LoadProjectile(ProjectileID.MedusaHeadRay, ItemID.MedusaHead); // Medusa Ray
//        LoadProjectile(ProjectileID.StardustSoldierLaser, Element.dragon); // Stardust Laser
//        LoadProjectile(ProjectileID.Twinkle, Element.dragon); // Twinkle
//        LoadProjectile(ProjectileID.StardustJellyfishSmall, Element.dragon); // Flow Invader
//        LoadProjectile(ProjectileID.StardustTowerMark, Element.dragon); // Starmark
//        LoadProjectile(ProjectileID.WoodYoyo, ItemID.WoodYoyo); // Yoyo
//        LoadProjectile(ProjectileID.CorruptYoyo, ItemID.CorruptYoyo); // Yoyo
//        LoadProjectile(ProjectileID.CrimsonYoyo, ItemID.CrimsonYoyo); // Yoyo
//        LoadProjectile(ProjectileID.JungleYoyo, ItemID.JungleYoyo); // Yoyo
//        LoadProjectile(ProjectileID.Cascade, ItemID.Cascade); // Yoyo
//        LoadProjectile(ProjectileID.Chik, ItemID.Chik); // Yoyo
//        LoadProjectile(ProjectileID.Code2, ItemID.Code2); // Yoyo
//        LoadProjectile(ProjectileID.Rally, ItemID.Rally); // Yoyo
//        LoadProjectile(ProjectileID.Yelets, ItemID.Yelets); // Yoyo
//        LoadProjectile(ProjectileID.RedsYoyo, ItemID.RedsYoyo); // Yoyo
//        LoadProjectile(ProjectileID.ValkyrieYoyo, ItemID.ValkyrieYoyo); // Yoyo
//        LoadProjectile(ProjectileID.Amarok, ItemID.Amarok); // Yoyo
//        LoadProjectile(ProjectileID.HelFire, ItemID.HelFire); // Yoyo
//        LoadProjectile(ProjectileID.Kraken, ItemID.Kraken); // Yoyo
//        LoadProjectile(ProjectileID.TheEyeOfCthulhu, ItemID.TheEyeOfCthulhu); // Yoyo
//        LoadProjectile(ProjectileID.BlackCounterweight, Element.dark); // Counterweight
//        LoadProjectile(ProjectileID.BlueCounterweight, Element.water); // Counterweight
//        LoadProjectile(ProjectileID.GreenCounterweight, Element.grass); // Counterweight
//        LoadProjectile(ProjectileID.PurpleCounterweight, Element.poison); // Counterweight
//        LoadProjectile(ProjectileID.RedCounterweight, Element.fire); // Counterweight
//        LoadProjectile(ProjectileID.YellowCounterweight, Element.electric); // Counterweight
//        LoadProjectile(ProjectileID.FormatC, ItemID.FormatC); // Yoyo
//        LoadProjectile(ProjectileID.Gradient, ItemID.Gradient); // Yoyo
//        LoadProjectile(ProjectileID.Valor, ItemID.Valor); // Yoyo
//        LoadProjectile(ProjectileID.BrainOfConfusion, Element.psychic); // Brain of Confusion
//        LoadProjectile(ProjectileID.GiantBee, Element.bug); // Bee
//        LoadProjectile(ProjectileID.SporeTrap, Element.grass); // Spore
//        LoadProjectile(ProjectileID.SporeTrap2, Element.grass); // Spore
//        LoadProjectile(ProjectileID.SporeGas, Element.grass); // Spore
//        LoadProjectile(ProjectileID.SporeGas2, Element.grass); // Spore
//        LoadProjectile(ProjectileID.SporeGas3, Element.grass); // Spore
//        LoadProjectile(ProjectileID.SalamanderSpit, Element.poison); // Poison Spit
//        LoadProjectile(ProjectileID.NebulaBolt, Element.psychic); // Nebula Piercer
//        LoadProjectile(ProjectileID.NebulaEye, Element.psychic); // Nebula Eye
//        LoadProjectile(ProjectileID.NebulaSphere, Element.psychic); // Nebula Sphere
//        LoadProjectile(ProjectileID.NebulaLaser, Element.psychic); // Nebula Laser
//        LoadProjectile(ProjectileID.VortexLaser, Element.electric); // Vortex Laser
//        LoadProjectile(ProjectileID.VortexVortexLightning, Element.electric); // Vortex
//        LoadProjectile(ProjectileID.VortexVortexPortal, Element.electric); // Vortex
//        LoadProjectile(ProjectileID.VortexLightning, Element.electric); // Vortex Lightning
//        LoadProjectile(ProjectileID.VortexAcid, Element.bug); // Alien Goop
//        LoadProjectile(ProjectileID.MechanicWrench, Element.steel); // Mechanic's Wrench
//        LoadProjectile(ProjectileID.NurseSyringeHurt, Element.psychic); // Syringe
//        LoadProjectile(ProjectileID.NurseSyringeHeal, Element.psychic); // Syringe
//        LoadProjectile(ProjectileID.ClothiersCurse, Element.bone); // Skull
//        LoadProjectile(ProjectileID.DryadsWardCircle, Element.fairy); // Dryad's ward
//        LoadProjectile(ProjectileID.PainterPaintball, ItemID.PainterPaintballGun); // Paintball
//        LoadProjectile(ProjectileID.PartyGirlGrenade, ItemID.PartyGirlGrenade); // Confetti Grenade
//        LoadProjectile(ProjectileID.SantaBombs, Element.ice); // Christmas Ornament
//        LoadProjectile(ProjectileID.TruffleSpore, Element.grass); // Truffle Spore
//        LoadProjectile(ProjectileID.MinecartMechLaser, Element.steel); // Minecart Laser
//        LoadProjectile(ProjectileID.MartianWalkerLaser, Element.electric); // Laser Ray
//        LoadProjectile(ProjectileID.AncientDoomProjectile, Element.dragon); // Prophecy's End
//        LoadProjectile(ProjectileID.BlowupSmoke, Element.normal); // Blowup Smoke
//        LoadProjectile(ProjectileID.Arkhalis, ItemID.Arkhalis); // Arkhalis
//        LoadProjectile(ProjectileID.DesertDjinnCurse, Element.ghost); // Desert Spirit's Curse
//        LoadProjectile(ProjectileID.AmberBolt, ItemID.AmberStaff); // Amber Bolt
//        LoadProjectile(ProjectileID.BoneJavelin, ItemID.BoneJavelin); // Bone Javelin
//        LoadProjectile(ProjectileID.BoneDagger, ItemID.BoneDagger); // Bone Dagger
//        LoadProjectile(ProjectileID.Terrarian, ItemID.Terrarian); // Terrarian
//        LoadProjectile(ProjectileID.TerrarianBeam, ItemID.Terrarian); // Terrarian
//        LoadProjectile(ProjectileID.SpikedSlimeSpike, Element.water); // Slime Spike
//        LoadProjectile(ProjectileID.ScutlixLaser, Element.psychic); // Laser
//        LoadProjectile(ProjectileID.SolarFlareRay, Element.fire); // Solar Flare
//        LoadProjectile(ProjectileID.SolarCounter, Element.fire); // Solar Radiance
//        LoadProjectile(ProjectileID.StardustDrill, ItemID.StardustDrill); // Stardust Drill
//        LoadProjectile(ProjectileID.StardustChainsaw, ItemID.StardustDrill); // Stardust Chainsaw
//        LoadProjectile(ProjectileID.SolarWhipSword, ItemID.SolarEruption); // Solar Eruption
//        LoadProjectile(ProjectileID.SolarWhipSwordExplosion, ItemID.SolarEruption); // Solar Explosion
//        LoadProjectile(ProjectileID.StardustCellMinion, ItemID.StardustCellStaff); // Stardust Cell
//        LoadProjectile(ProjectileID.StardustCellMinionShot, ItemID.StardustCellStaff); // Stardust Cell
//        LoadProjectile(ProjectileID.VortexBeaterRocket, ItemID.VortexBeater); // Vortex Rocket
//        LoadProjectile(ProjectileID.NebulaArcanum, ItemID.NebulaArcanum); // Nebula Arcanum
//        LoadProjectile(ProjectileID.NebulaArcanumSubshot, ItemID.NebulaArcanum); // Nebula Arcanum
//        LoadProjectile(ProjectileID.NebulaArcanumExplosionShot, ItemID.NebulaArcanum); // Nebula Arcanum
//        LoadProjectile(ProjectileID.NebulaArcanumExplosionShotShard, ItemID.NebulaArcanum); // Nebula Arcanum
//        LoadProjectile(ProjectileID.BloodWater, ItemID.BloodWater); // Blood Water
//        LoadProjectile(ProjectileID.StardustGuardian, Element.dragon); // Stardust Guardian
//        LoadProjectile(ProjectileID.StardustGuardianExplosion, Element.dragon); // Starburst
//        LoadProjectile(ProjectileID.StardustDragon1, ItemID.StardustDragonStaff); // Stardust Dragon
//        LoadProjectile(ProjectileID.StardustDragon2, ItemID.StardustDragonStaff); // Stardust Dragon
//        LoadProjectile(ProjectileID.StardustDragon3, ItemID.StardustDragonStaff); // Stardust Dragon
//        LoadProjectile(ProjectileID.StardustDragon4, ItemID.StardustDragonStaff); // Stardust Dragon
//        LoadProjectile(ProjectileID.TowerDamageBolt, Element.normal); // Released Energy
//        LoadProjectile(ProjectileID.PhantasmArrow, ItemID.Phantasm); // Phantasm
//        LoadProjectile(ProjectileID.LastPrismLaser, ItemID.LastPrism); // Last Prism
//        LoadProjectile(ProjectileID.NebulaBlaze1, ItemID.NebulaBlaze); // Nebula Blaze
//        LoadProjectile(ProjectileID.NebulaBlaze2, ItemID.NebulaBlaze); // Nebula Blaze Ex
//        LoadProjectile(ProjectileID.Daybreak, ItemID.DayBreak); // Daybreak
//        LoadProjectile(ProjectileID.MoonlordBullet, ItemID.MoonlordBullet); // Luminite Bullet
//        LoadProjectile(ProjectileID.MoonlordArrow, ItemID.MoonlordArrow); // Luminite Arrow
//        LoadProjectile(ProjectileID.MoonlordArrowTrail, ItemID.MoonlordArrow); // Luminite Arrow
//        LoadProjectile(ProjectileID.MoonlordTurretLaser, ItemID.MoonlordTurretStaff); // Lunar Portal Laser
//        LoadProjectile(ProjectileID.RainbowCrystalExplosion, ItemID.RainbowCrystalStaff); // Rainbow Explosion
//        LoadProjectile(ProjectileID.LunarFlare, ItemID.LunarFlareBook); // Lunar Flare
//        LoadProjectile(ProjectileID.GeyserTrap, Element.fire); // Geyser
//        LoadProjectile(ProjectileID.BeeHive, Element.bug); // Bee Hive
//        LoadProjectile(ProjectileID.SandnadoFriendly, Element.ground); // Ancient Storm
//        LoadProjectile(ProjectileID.SandnadoHostile, Element.ground); // Ancient Storm
//        LoadProjectile(ProjectileID.SandnadoHostileMark, Element.ground); // Ancient Storm
//        LoadProjectile(ProjectileID.SpiritFlame, ItemID.SpiritFlame); // Spirit Flame
//        LoadProjectile(ProjectileID.SkyFracture, ItemID.SkyFracture); // Sky Fracture
//        LoadProjectile(ProjectileID.BlackBolt, ItemID.OnyxBlaster); // Onyx Blaster
//        LoadProjectile(ProjectileID.DD2JavelinHostile, Element.fighting); // Javelin
//        LoadProjectile(ProjectileID.DD2FlameBurstTowerT1Shot, ItemID.DD2FlameburstTowerT1Popper); // Flameburst Tower
//        LoadProjectile(ProjectileID.DD2FlameBurstTowerT2Shot, ItemID.DD2FlameburstTowerT2Popper); // Flameburst Tower
//        LoadProjectile(ProjectileID.DD2FlameBurstTowerT3Shot, ItemID.DD2FlameburstTowerT3Popper); // Flameburst Tower
//        LoadProjectile(ProjectileID.Ale, ItemID.AleThrowingGlove); // Ale
//        LoadProjectile(ProjectileID.DD2OgreStomp, Element.ground); // Ogre's Stomp
//        LoadProjectile(ProjectileID.DD2DrakinShot, Element.dragon); // Drakin
//        LoadProjectile(ProjectileID.DD2DarkMageBolt, Element.dark); // Dark Energy
//        LoadProjectile(ProjectileID.DD2OgreSpit, Element.poison); // Ogre Spit
//        LoadProjectile(ProjectileID.DD2BallistraProj, ItemID.DD2BallistraTowerT1Popper); // Ballista
//        LoadProjectile(ProjectileID.DD2GoblinBomb, Element.normal); // Goblin Bomb
//        LoadProjectile(ProjectileID.DD2LightningBugZap, Element.electric); // Withering Bolt
//        LoadProjectile(ProjectileID.DD2OgreSmash, Element.ground); // Ogre's Stomp
//        LoadProjectile(ProjectileID.DD2SquireSonicBoom, Element.dragon); // Hearty Slash
//        LoadProjectile(ProjectileID.DD2JavelinHostileT3, Element.fighting); // Javelin
//        LoadProjectile(ProjectileID.DD2BetsyFireball, Element.fire); // Betsy's Fireball
//        LoadProjectile(ProjectileID.DD2BetsyFlameBreath, Element.fire); // Betsy's Breath
//        LoadProjectile(ProjectileID.DD2ExplosiveTrapT1Explosion, ItemID.DD2ExplosiveTrapT1Popper); // Explosive Trap
//        LoadProjectile(ProjectileID.DD2ExplosiveTrapT2Explosion, ItemID.DD2ExplosiveTrapT2Popper); // Explosive Trap
//        LoadProjectile(ProjectileID.DD2ExplosiveTrapT3Explosion, ItemID.DD2ExplosiveTrapT3Popper); // Explosive Trap
//        LoadProjectile(ProjectileID.MonkStaffT1, ItemID.MonkStaffT1); // Sleepy Octopod
//        LoadProjectile(ProjectileID.MonkStaffT1Explosion, Element.fighting); // Pole Smash
//        LoadProjectile(ProjectileID.MonkStaffT2, ItemID.MonkStaffT2); // Ghastly Glaive
//        LoadProjectile(ProjectileID.MonkStaffT2Ghast, Element.ghost); // Ghast
//        LoadProjectile(ProjectileID.DD2ApprenticeStorm, Element.flying); // Whirlwind of Infinite Wisdom
//        LoadProjectile(ProjectileID.DD2PhoenixBowShot, Element.fire); // Phantom Phoenix
//        LoadProjectile(ProjectileID.MonkStaffT3, ItemID.MonkStaffT3); // Sky Dragon's Fury
//        LoadProjectile(ProjectileID.MonkStaffT3_Alt, ItemID.MonkStaffT3); // Sky Dragon's Fury
//        LoadProjectile(ProjectileID.MonkStaffT3_AltShot, Element.electric); // Sky Dragon's Fury
//        LoadProjectile(ProjectileID.DD2BetsyArrow, ItemID.DD2BetsyBow); // Aerial Bane
//        LoadProjectile(ProjectileID.ApprenticeStaffT3Shot, ItemID.ApprenticeStaffT3); // Betsy's Wrath
//        LoadProjectile(ProjectileID.BookStaffShot, ItemID.BookStaff); // Tome of Infinite Wisdom
//        LoadProjectile(ProjectileID.Celeb2Rocket, ItemID.Celeb2); // Celebration Mk2
//        LoadProjectile(ProjectileID.Celeb2RocketExplosive, ItemID.Celeb2); // Celebration Mk2
//        LoadProjectile(ProjectileID.Celeb2RocketLarge, ItemID.Celeb2); // Celebration Mk2
//        LoadProjectile(ProjectileID.Celeb2RocketExplosiveLarge, ItemID.Celeb2); // ProjectileName.Celeb2RocketExplosiveLarge
//        LoadProjectile(ProjectileID.QueenBeeStinger, Element.poison); // Queen Bee's Stinger
//        LoadProjectile(ProjectileID.FallingStarSpawner, Element.flying); // Falling Star
//        LoadProjectile(ProjectileID.ManaCloakStar, ItemID.ManaCloak); // Mana Cloak
//        LoadProjectile(ProjectileID.BeeCloakStar, ItemID.BeeCloak); // Bee Cloak
//        LoadProjectile(ProjectileID.StarVeilStar, ItemID.StarVeil); // Star Veil
//        LoadProjectile(ProjectileID.StarCloakStar, ItemID.StarCloak); // Star Cloak
//        LoadProjectile(ProjectileID.RollingCactus, Element.grass); // Rolling Cactus
//        LoadProjectile(ProjectileID.SuperStar, ItemID.SuperStarCannon); // Super Star Cannon
//        LoadProjectile(ProjectileID.SuperStarSlash, ItemID.SuperStarCannon); // Super Star Cannon
//        LoadProjectile(ProjectileID.ThunderStaffShot, ItemID.ThunderStaff); // Thunder Zapper
//        LoadProjectile(ProjectileID.ThunderSpearShot, ItemID.ThunderSpear); // Storm Spear
//        LoadProjectile(ProjectileID.Terragrim, ItemID.Terragrim); // Terragrim
//        LoadProjectile(ProjectileID.BatOfLight, ItemID.SanguineStaff); // Sanguine Bat
//        LoadProjectile(ProjectileID.SharpTears, ItemID.SharpTears); // Blood Thorn
//        LoadProjectile(ProjectileID.DripplerFlail, ItemID.DripplerFlail); // Drippler Crippler
//        LoadProjectile(ProjectileID.VampireFrog, ItemID.VampireFrogStaff); // Vampire Frog
//        LoadProjectile(ProjectileID.BabyBird, ItemID.BabyBirdStaff); // Finch
//        LoadProjectile(ProjectileID.PaperAirplaneA, ItemID.PaperAirplaneA); // Paper Airplane
//        LoadProjectile(ProjectileID.PaperAirplaneB, ItemID.PaperAirplaneB); // Paper Airplane
//        LoadProjectile(ProjectileID.RollingCactusSpike, Element.grass); // Rolling Cactus Spike
//        LoadProjectile(ProjectileID.ClusterRocketI, ItemID.ClusterRocketI); // Rocket
//        LoadProjectile(ProjectileID.ClusterGrenadeI, ItemID.ClusterRocketI); // Grenade
//        LoadProjectile(ProjectileID.ClusterMineI, ItemID.ClusterRocketI); // Proximity Mine
//        LoadProjectile(ProjectileID.ClusterFragmentsI, ItemID.ClusterRocketI); // Cluster Fragment
//        LoadProjectile(ProjectileID.ClusterRocketII, ItemID.ClusterRocketII); // Rocket
//        LoadProjectile(ProjectileID.ClusterGrenadeII, ItemID.ClusterRocketII); // Grenade
//        LoadProjectile(ProjectileID.ClusterMineII, ItemID.ClusterRocketII); // Proximity Mine
//        LoadProjectile(ProjectileID.ClusterFragmentsII, ItemID.ClusterRocketII); // Cluster Fragment
//        LoadProjectile(ProjectileID.WetRocket, ItemID.WetRocket); // Rocket
//        LoadProjectile(ProjectileID.WetGrenade, ItemID.WetRocket); // Grenade
//        LoadProjectile(ProjectileID.WetMine, ItemID.WetRocket); // Proximity Mine
//        LoadProjectile(ProjectileID.LavaRocket, ItemID.LavaRocket); // Rocket
//        LoadProjectile(ProjectileID.LavaGrenade, ItemID.LavaRocket); // Grenade
//        LoadProjectile(ProjectileID.LavaMine, ItemID.LavaRocket); // Proximity Mine
//        LoadProjectile(ProjectileID.HoneyRocket, ItemID.HoneyRocket); // Rocket
//        LoadProjectile(ProjectileID.HoneyGrenade, ItemID.HoneyRocket); // Grenade
//        LoadProjectile(ProjectileID.HoneyMine, ItemID.HoneyRocket); // Proximity Mine
//        LoadProjectile(ProjectileID.MiniNukeRocketI, ItemID.MiniNukeI); // Rocket
//        LoadProjectile(ProjectileID.MiniNukeGrenadeI, ItemID.MiniNukeI); // Grenade
//        LoadProjectile(ProjectileID.MiniNukeMineI, ItemID.MiniNukeI); // Proximity Mine
//        LoadProjectile(ProjectileID.MiniNukeRocketII, ItemID.MiniNukeII); // Rocket
//        LoadProjectile(ProjectileID.MiniNukeGrenadeII, ItemID.MiniNukeII); // Grenade
//        LoadProjectile(ProjectileID.MiniNukeMineII, ItemID.MiniNukeII); // Proximity Mine
//        LoadProjectile(ProjectileID.DryRocket, ItemID.DryRocket); // Rocket
//        LoadProjectile(ProjectileID.DryGrenade, ItemID.DryRocket); // Grenade
//        LoadProjectile(ProjectileID.DryMine, ItemID.DryRocket); // Proximity Mine
//        LoadProjectile(ProjectileID.GladiusStab, ItemID.Gladius); // Gladius
//        LoadProjectile(ProjectileID.ClusterSnowmanRocketI, ItemID.ClusterRocketI); // Rocket
//        LoadProjectile(ProjectileID.ClusterSnowmanRocketII, ItemID.ClusterRocketII); // Rocket
//        LoadProjectile(ProjectileID.WetSnowmanRocket, ItemID.WetRocket); // Rocket
//        LoadProjectile(ProjectileID.LavaSnowmanRocket, ItemID.LavaRocket); // Rocket
//        LoadProjectile(ProjectileID.HoneySnowmanRocket, ItemID.HoneyRocket); // Rocket
//        LoadProjectile(ProjectileID.MiniNukeSnowmanRocketI, ItemID.MiniNukeI); // Rocket
//        LoadProjectile(ProjectileID.MiniNukeSnowmanRocketII, ItemID.MiniNukeII); // Rocket
//        LoadProjectile(ProjectileID.DrySnowmanRocket, ItemID.DryRocket); // Rocket
//        LoadProjectile(ProjectileID.BloodShot, Element.blood); // Blood Shot
//        LoadProjectile(ProjectileID.ShellPileFalling, Element.water); // Shell Pile
//        LoadProjectile(ProjectileID.BloodNautilusTears, Element.blood); // Blood Tears
//        LoadProjectile(ProjectileID.BloodNautilusShot, Element.blood); // Blood Shot
//        LoadProjectile(ProjectileID.WhiteTigerPounce, ItemID.StormTigerStaff); // Desert Tiger
//        LoadProjectile(ProjectileID.BloodArrow, ItemID.BloodRainBow); // Blood Rain
//        LoadProjectile(ProjectileID.StormTigerGem, ItemID.StormTigerStaff); // Desert Tiger
//        LoadProjectile(ProjectileID.StormTigerAttack, ItemID.StormTigerStaff); // Desert Tiger
//        LoadProjectile(ProjectileID.StormTigerTier1, ItemID.StormTigerStaff); // Desert Tiger
//        LoadProjectile(ProjectileID.StormTigerTier2, ItemID.StormTigerStaff); // Desert Tiger
//        LoadProjectile(ProjectileID.StormTigerTier3, ItemID.StormTigerStaff); // Desert Tiger
//        LoadProjectile(ProjectileID.DandelionSeed, Element.grass); // Dandelion Seed
//        LoadProjectile(ProjectileID.BookOfSkullsSkull, ItemID.BookofSkulls); // Skull
//        LoadProjectile(ProjectileID.BlandWhip, ItemID.BlandWhip); // Leather Whip
//        LoadProjectile(ProjectileID.RulerStab, ItemID.Ruler); // Ruler
//        LoadProjectile(ProjectileID.SwordWhip, ItemID.SwordWhip); // Durendal
//        LoadProjectile(ProjectileID.MaceWhip, ItemID.MaceWhip); // Morning Star
//        LoadProjectile(ProjectileID.ScytheWhip, ItemID.ScytheWhip); // Dark Harvest
//        LoadProjectile(ProjectileID.SparkleGuitar, ItemID.SparkleGuitar); // Stellar Tune
//        LoadProjectile(ProjectileID.ClusterSnowmanFragmentsI, ItemID.ClusterRocketI); // Cluster Fragment
//        LoadProjectile(ProjectileID.ClusterSnowmanFragmentsII, ItemID.ClusterRocketII); // Cluster Fragment
//        LoadProjectile(ProjectileID.Smolstar, ItemID.Smolstar); // Enchanted Dagger
//        LoadProjectile(ProjectileID.BouncingShield, ItemID.BouncingShield); // Sergeant United Shield
//        LoadProjectile(ProjectileID.Shroomerang, ItemID.Shroomerang); // Shroomerang
//        LoadProjectile(ProjectileID.HallowBossLastingRainbow, Element.fairy); // Everlasting Rainbow
//        LoadProjectile(ProjectileID.HallowBossRainbowStreak, Element.fairy); // Prismatic Bolt
//        LoadProjectile(ProjectileID.ZapinatorLaser, Element.electric); // Zapinator
//        LoadProjectile(ProjectileID.JoustingLance, ItemID.JoustingLance); // Jousting Lance
//        LoadProjectile(ProjectileID.ShadowJoustingLance, ItemID.ShadowJoustingLance); // Shadow Jousting Lance
//        LoadProjectile(ProjectileID.HallowJoustingLance, ItemID.HallowJoustingLance); // Hallowed Jousting Lance
//        LoadProjectile(ProjectileID.ZoologistStrikeGreen, Element.normal); // ProjectileName.ZoologistStrikeGreen
//        LoadProjectile(ProjectileID.CombatWrench, ItemID.CombatWrench); // Combat Wrench
//        LoadProjectile(ProjectileID.OrnamentStar, Element.ice); // Ornament
//        LoadProjectile(ProjectileID.TitaniumStormShard, Element.steel); // Titanium Shard
//        LoadProjectile(ProjectileID.RockGolemRock, Element.rock); // Rock
//        LoadProjectile(ProjectileID.CoolWhip, ItemID.CoolWhip); // Cool Whip
//        LoadProjectile(ProjectileID.FireWhip, ItemID.FireWhip); // Firecracker
//        LoadProjectile(ProjectileID.ThornWhip, ItemID.ThornWhip); // Snapthorn
//        LoadProjectile(ProjectileID.RainbowWhip, ItemID.RainbowWhip); // Kaleidoscope
//        LoadProjectile(ProjectileID.ScytheWhipProj, Element.ghost); // Reaping
//        LoadProjectile(ProjectileID.CoolWhipProj, Element.ice); // Cool Flake
//        LoadProjectile(ProjectileID.FireWhipProj, Element.fire); // Firecracker
//        LoadProjectile(ProjectileID.FairyQueenLance, Element.fairy); // Ethereal Lance
//        LoadProjectile(ProjectileID.QueenSlimeMinionBlueSpike, Element.rock, Element.fairy); // Crystal Spike
//        LoadProjectile(ProjectileID.QueenSlimeMinionPinkBall, Element.water); // Bouncy Gel
//        LoadProjectile(ProjectileID.QueenSlimeSmash, Element.ground); // Queenly Smash
//        LoadProjectile(ProjectileID.FairyQueenSunDance, Element.fairy); // Sun Dance
//        LoadProjectile(ProjectileID.FairyQueenHymn, Element.fairy); // ProjectileName.FairyQueenHymn
//        LoadProjectile(ProjectileID.StardustPunch, Element.dragon); // Stardust Guardian
//        LoadProjectile(ProjectileID.QueenSlimeGelAttack, Element.water); // Regal Gel
//        LoadProjectile(ProjectileID.PiercingStarlight, ItemID.PiercingStarlight); // Starlight
//        LoadProjectile(ProjectileID.DripplerFlailExtraBall, ItemID.DripplerFlail); // Drippler Crippler
//        LoadProjectile(ProjectileID.ZoologistStrikeRed, Element.normal); // ProjectileName.ZoologistStrikeRed
//        LoadProjectile(ProjectileID.SantankMountRocket, Element.normal); // Rocket
//        LoadProjectile(ProjectileID.FairyQueenMagicItemShot, ItemID.FairyQueenMagicItem); // Nightglow
//        LoadProjectile(ProjectileID.FairyQueenRangedItemShot, Element.fairy); // Twilight Lance
//        LoadProjectile(ProjectileID.VolatileGelatinBall, Element.water); // Volatile Gelatin
//        LoadProjectile(ProjectileID.CopperShortswordStab, ItemID.CopperShortsword); // Copper Shortsword
//        LoadProjectile(ProjectileID.TinShortswordStab, ItemID.TinShortsword); // Tin Shortsword
//        LoadProjectile(ProjectileID.IronShortswordStab, ItemID.IronShortsword); // Iron Shortsword
//        LoadProjectile(ProjectileID.LeadShortswordStab, ItemID.LeadShortsword); // Lead Shortsword
//        LoadProjectile(ProjectileID.SilverShortswordStab, ItemID.SilverShortsword); // Silver Shortsword
//        LoadProjectile(ProjectileID.TungstenShortswordStab, ItemID.TungstenShortsword); // Tungsten Shortsword
//        LoadProjectile(ProjectileID.GoldShortswordStab, ItemID.GoldShortsword); // Gold Shortsword
//        LoadProjectile(ProjectileID.PlatinumShortswordStab, ItemID.PlatinumShortsword); // Platinum Shortsword
//        LoadProjectile(ProjectileID.EmpressBlade, ItemID.EmpressBlade); // Terraprisma
//        LoadProjectile(ProjectileID.Mace, ItemID.Mace); // Mace
//        LoadProjectile(ProjectileID.FlamingMace, ItemID.FlamingMace); // Flaming Mace
//        LoadProjectile(ProjectileID.TorchGod, Element.fire); // The Torch God
//        LoadProjectile(ProjectileID.PrincessWeapon, ItemID.PrincessWeapon); // Royal Resonance
//        LoadProjectile(ProjectileID.FlinxMinion, ItemID.FlinxStaff); // Flinx
//        LoadProjectile(ProjectileID.BoneWhip, ItemID.BoneWhip); // Spinal Tap
//        LoadProjectile(ProjectileID.DaybreakExplosion, Element.fire); // Solar Explosion
//        LoadProjectile(ProjectileID.WandOfSparkingSpark, ItemID.WandofSparking); // Spark
//        LoadProjectile(ProjectileID.StarCannonStar, ItemID.StarCannon); // Falling Star
//        LoadProjectile(ProjectileID.DeerclopsIceSpike, Element.ice); // Ice Spike
//        LoadProjectile(ProjectileID.AbigailMinion, ItemID.AbigailsFlower); // Abigail
//        LoadProjectile(ProjectileID.InsanityShadowFriendly, ItemID.BoneHelm); // Shadow Hand
//        LoadProjectile(ProjectileID.InsanityShadowHostile, Element.ghost); // Shadow Hand
//        LoadProjectile(ProjectileID.HoundiusShootiusFireball, ItemID.HoundiusShootius); // Houndius Shootius
//        LoadProjectile(ProjectileID.WeatherPainShot, ItemID.WeatherPain); // Hurtnado
//        LoadProjectile(ProjectileID.AbigailCounter, ItemID.AbigailsFlower); // ProjectileName.AbigailCounter
//        LoadProjectile(ProjectileID.TentacleSpike, ItemID.TentacleSpike); // Tentacle Spike

//        //LoadProjectile(ProjectileID.IcewaterSpit, ElementArray.Get(Element.water), (ref float baseEffectiveness, Element offensive, Element defensive) =>
//        //{
//        //    if (defensive == Element.grass)
//        //    {
//        //        baseEffectiveness = Table.Mult;
//        //    }
//        //}); // Icewater Spit

//        //LoadProjectile(ProjectileID.Harpoon, Items.GetInfo(ItemID.Harpoon).Elements, (ref float baseEffectiveness, Element offensive, Element defensive) =>
//        //{
//        //    if (defensive == Element.water)
//        //    {
//        //        baseEffectiveness = Table.Mult;
//        //    }
//        //}); // Harpoon

//        //LoadDeerclopsDebrisElements();
//        //LoadProjectile(ProjectileID.DeerclopsRangedProjectile, ElementArray.Get(Element.ice), DeerclopsDebrisProj); // Debris

//        //LoadPewmaticHorn();
//        //LoadProjectile(ProjectileID.PewMaticHornShot, ElementArray.Get(Element.normal), PewmaticHornProj); // Pew-Matic Stuff

//        //LoadProjectile(ProjectileID.FinalFractal, ElementArray.Get(Element.normal), ZenithProj); // Zenith
//    }

//    private void LoadPewmaticHorn()
//    {
//        pewmaticHornElements = new ElementArray[]
//        {
//            ElementArray.Get(Element.rock),     //  0, stone block
//            ElementArray.Get(Element.ground),   //  1, dirt block
//            ElementArray.Get(Element.steel), //  2, copper ore
//            ElementArray.Get(Element.bug), //  3, cobweb
//            ElementArray.Get(Element.water), //  4, waterleaf
//            ElementArray.Get(Element.bone), //  5, bone
//            ElementArray.Get(Element.blood), //  6, rotten chunk
//            ElementArray.Get(Element.flying), //  7, feather
//            ElementArray.Get(Element.grass), //  8, jungle grass seeds
//            ElementArray.Get(Element.grass), //  9, mushroom
//            ElementArray.Get(Element.grass), // 10, daybloom
//            ElementArray.Get(Element.dark), // 11, moonglow
//            ElementArray.Get(Element.ground), // 12, blinkroot
//            ElementArray.Get(Element.normal), // 13, wood platform
//            ElementArray.Get(Element.ghost), // 14, deathweed
//            ElementArray.Get(Element.fire), // 15, fireblossom
//            ElementArray.Get(Element.ice), // 16, shiverthorn
//            ElementArray.Get(Element.rock), // 17, amethyst
//            ElementArray.Get(Element.rock), // 18, topaz
//            ElementArray.Get(Element.rock), // 19, emerald
//            ElementArray.Get(Element.rock), // 20, ruby
//            ElementArray.Get(Element.rock), // 21, sapphire
//            ElementArray.Get(Element.rock, Element.fairy), // 22, diamond
//            ElementArray.Get(Element.rock), // 23, amber
//        };
//    }

//    private void LoadDeerclopsDebrisElements()
//    {
//        deerclopsDebrisElements = new ElementArray[]
//        {
//            ElementArray.Get(Element.ground), // 0, dirt
//            ElementArray.Get(Element.ground), // 1, dirt
//            ElementArray.Get(Element.ground), // 2, dirt
//            ElementArray.Get(Element.rock), // 3, stone
//            ElementArray.Get(Element.rock), // 4, stone
//            ElementArray.Get(Element.rock), // 5, stone
//            ElementArray.Get(Element.ice), // 6, ice
//            ElementArray.Get(Element.ice), // 7, ice
//            ElementArray.Get(Element.ice), // 8, ice
//            ElementArray.Get(Element.ice), // 9, ice
//            ElementArray.Get(Element.ice), // 10, ice
//            ElementArray.Get(Element.ice), // 11, ice
//        };
//    }

//    private static ElementArray PewmaticHornProj(Projectile projectile)
//    {
//        if (Instance is not null && Instance.pewmaticHornElements is not null && projectile is not null)
//        {
//            int ai1 = (int)projectile.ai[1];
//            if (ai1 >= 0 && ai1 < Instance.pewmaticHornElements.Length)
//            {
//                return Instance.pewmaticHornElements[ai1];
//            }
//        }

//        return ElementArray.Default;
//    }

//    private static ElementArray DeerclopsDebrisProj(Projectile projectile)
//    {
//        if (Instance is not null && Instance.deerclopsDebrisElements is not null && projectile is not null)
//        {
//            int ai1 = (int)projectile.ai[1];
//            if (ai1 >= 0 && ai1 < Instance.deerclopsDebrisElements.Length)
//            {
//                return Instance.deerclopsDebrisElements[ai1];
//            }
//        }

//        return ElementArray.Default;
//    }

//    private static ElementArray ZenithProj(Projectile projectile)
//    {
//        if (projectile is not null)
//        {
//            //return Items.GetInfo((int)projectile.ai[1]).Elements;
//            throw new NotImplementedException();
//        }
//        else
//        {
//            return ElementArray.Default;
//        }
//    }

//    private void LoadProjectile(int projectileID, params Element[] elements)
//    {
//        //types[projectileID] = ProjectileTypeInfo.Get(ElementArray.Get(elements));
//    }

//    private void LoadProjectile(int projectileID, int loadFromItemID)
//    {
//        //types[projectileID] = ProjectileTypeInfo.Get(Items.GetInfo(loadFromItemID).Elements);
//    }

//    private void LoadProjectile(int projectileID, ElementArray fallbackElements, Func<Projectile, ElementArray> specialType)
//    {
//        //types[projectileID] = ProjectileTypeInfo.Get(fallbackElements, specialType);
//    }

//    private void LoadProjectile(int projectileID, ElementArray elements, ModifyEffectivenessDelegate modifyEffectivenessDelegate)
//    {
//        //types[projectileID] = ProjectileTypeInfo.Get(elements, modifyEffectivenessDelegate);
//    }
//}
