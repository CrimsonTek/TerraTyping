using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Attributes;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public static class Enemies
    {
        public static void Load()
        {
            Type = new Dictionary<int, NPCTypeInfo>()
            {
                {NPCID.BigHornetStingy, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleHornetStingy, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.BigHornetSpikey, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleHornetSpikey, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.BigHornetLeafy, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleHornetLeafy, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.BigHornetHoney, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleHornetHoney, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.BigHornetFatty, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleHornetFatty, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },

                {NPCID.BigRainZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallRainZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.BigPantlessSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.SmallPantlessSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BigMisassembledSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.SmallMisassembledSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BigHeadacheSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.SmallHeadacheSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BigSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.SmallSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BigFemaleZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallFemaleZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.DemonEye2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.PurpleEye2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.GreenEye2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.DialatedEye2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.SleepyEye2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.CataractEye2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },

                {NPCID.BigTwiggyZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallTwiggyZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.BigSwampZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallSwampZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.BigSlimedZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallSlimedZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.BigPincushionZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallPincushionZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.BigBaldZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallBaldZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.BigZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SmallZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },

                {NPCID.BigCrimslime, new NPCTypeInfo(Element.water, Element.blood, Element.blood) },
                {NPCID.LittleCrimslime, new NPCTypeInfo(Element.water, Element.blood, Element.blood) },
                {NPCID.BigCrimera, new NPCTypeInfo(Element.blood, Element.flying, Element.blood) },
                {NPCID.LittleCrimera, new NPCTypeInfo(Element.blood, Element.flying, Element.blood) },

                {NPCID.GiantMossHornet, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.BigMossHornet, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleMossHornet, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.TinyMossHornet, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },

                {NPCID.BigStinger, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.LittleStinger, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },

                {NPCID.HeavySkeleton, new NPCTypeInfo(Element.bone, Element.steel, Element.bone) },
                {NPCID.BigBoned, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.ShortBones, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },

                {NPCID.BigEater, new NPCTypeInfo(Element.dark, Element.flying, Element.dark) },
                {NPCID.LittleEater, new NPCTypeInfo(Element.dark, Element.flying, Element.dark) },

                {NPCID.JungleSlime, new NPCTypeInfo(Element.water, Element.grass, Element.grass, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.YellowSlime, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.RedSlime, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.PurpleSlime, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.BlackSlime, new NPCTypeInfo(Element.water, Element.dark, Element.dark, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.BabySlime, new NPCTypeInfo(Element.water, Element.ground, Element.ground, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Pinky, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.GreenSlime, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Slimer2, new NPCTypeInfo(Element.water, Element.dark, Element.dark, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Slimeling, new NPCTypeInfo(Element.water, Element.dark, Element.dark, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.BlueSlime, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },

                {NPCID.DemonEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Zombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.EyeofCthulhu, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.ServantofCthulhu, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },

                {NPCID.EaterofSouls, new NPCTypeInfo(Element.dark, Element.flying, Element.dark) },
                {NPCID.DevourerHead, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },
                {NPCID.DevourerBody, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },
                {NPCID.DevourerTail, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },

                {NPCID.GiantWormHead, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },
                {NPCID.GiantWormBody, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },
                {NPCID.GiantWormTail, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },

                {NPCID.EaterofWorldsHead, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },
                {NPCID.EaterofWorldsBody, new NPCTypeInfo(Element.dark, Element.ground, Element.ground) },
                {NPCID.EaterofWorldsTail, new NPCTypeInfo(Element.dark, Element.ground, Element.ground) },

                {NPCID.MotherSlime, new NPCTypeInfo(Element.water, Element.dark, Element.dark, new AbilityContainer(AbilityID.Flammable)) },

                {NPCID.Merchant, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Nurse, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.ArmsDealer, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Dryad, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Guide, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },

                {NPCID.MeteorHead, new NPCTypeInfo(Element.rock, Element.fire, Element.rock, new AbilityContainer(AbilityID.Levitate, hiddenAbility: AbilityID.FlashFire)) }, // rock or fire attack?
                {NPCID.FireImp, new NPCTypeInfo(Element.fire, Element.none, Element.fire, new AbilityContainer(AbilityID.None, hiddenAbility: AbilityID.FlashFire)) },
                {NPCID.BurningSphere, new NPCTypeInfo(Element.fire, Element.none, Element.fire, AbilityID.FlashFire) },

                {NPCID.GoblinPeon, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GoblinThief, new NPCTypeInfo(Element.normal, Element.dark, Element.dark) },
                {NPCID.GoblinWarrior, new NPCTypeInfo(Element.normal, Element.fighting, Element.normal, AbilityID.Scrappy) },
                {NPCID.GoblinSorcerer, new NPCTypeInfo(Element.normal, Element.psychic, Element.psychic) },
                {NPCID.ChaosBall, new NPCTypeInfo(Element.psychic, Element.none, Element.psychic) },

                {NPCID.AngryBones, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.DarkCaster, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.WaterSphere, new NPCTypeInfo(Element.water, Element.none, Element.water, AbilityID.WaterAbsorb) },
                {NPCID.CursedSkull, new NPCTypeInfo(Element.bone, Element.ghost, AbilityID.Levitate, Element.bone) },
                {NPCID.SkeletronHead, new NPCTypeInfo(Element.bone, Element.ghost, Element.bone) },
                {NPCID.SkeletronHand, new NPCTypeInfo(Element.bone, Element.ghost, Element.bone) },

                {NPCID.OldMan, new NPCTypeInfo(Element.none, Element.none, Element.normal) }, //town NPCS
                {NPCID.Demolitionist, new NPCTypeInfo(Element.none, Element.none, Element.normal) }, //town NPCS

                {NPCID.BoneSerpentHead, new NPCTypeInfo(Element.bone, Element.none, Element.bone) }, //fire? ground?
                {NPCID.BoneSerpentBody, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BoneSerpentTail, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },

                {NPCID.Hornet, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.ManEater, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },

                {NPCID.UndeadMiner, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.Tim, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },

                {NPCID.Bunny, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.CorruptBunny, new NPCTypeInfo(Element.normal, Element.dark, Element.dark) },

                {NPCID.Harpy, new NPCTypeInfo(Element.normal, Element.flying, Element.flying) },
                {NPCID.CaveBat, new NPCTypeInfo(Element.normal, Element.flying, Element.flying) },

                {NPCID.KingSlime, new NPCTypeInfo(Element.water, Element.dark, Element.water, new AbilityContainer(AbilityID.Flammable)) },

                {NPCID.JungleBat, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },

                {NPCID.DoctorBones, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.TheGroom, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.Clothier, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Goldfish, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.Snatcher, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.CorruptGoldfish, new NPCTypeInfo(Element.water, Element.dark, Element.dark) },
                {NPCID.Piranha, new NPCTypeInfo(Element.water, Element.none, Element.normal) },
                {NPCID.LavaSlime, new NPCTypeInfo(Element.water, Element.fire, AbilityID.FlashFire, Element.fire) },
                {NPCID.Hellbat, new NPCTypeInfo(Element.fire, Element.flying, Element.fire, new AbilityContainer(AbilityID.None, hiddenAbility: AbilityID.FlashFire)) },
                {NPCID.Vulture, new NPCTypeInfo(Element.dark, Element.flying, Element.dark) },
                {NPCID.Demon, new NPCTypeInfo(Element.dark, Element.fire, AbilityID.Levitate, Element.fire) },

                {NPCID.BlueJellyfish, new NPCTypeInfo(Element.water, Element.electric, Element.electric, new AbilityContainer(primaryAbility: AbilityID.WaterAbsorb, hiddenAbility: AbilityID.VoltAbsorb)) },
                {NPCID.PinkJellyfish, new NPCTypeInfo(Element.water, Element.electric, Element.electric, new AbilityContainer(primaryAbility: AbilityID.WaterAbsorb, hiddenAbility: AbilityID.VoltAbsorb)) },
                {NPCID.Shark, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.VoodooDemon, new NPCTypeInfo(Element.dark, Element.fire, AbilityID.Levitate, Element.fire) },
                {NPCID.Crab, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.DungeonGuardian, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.Antlion, new NPCTypeInfo(Element.ground, Element.bug, Element.bug, new AbilityContainer(primaryAbility: AbilityID.MoldBreaker, secondaryAbility: AbilityID.SandForce)) },
                {NPCID.SpikeBall, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.DungeonSlime, new NPCTypeInfo(Element.water, Element.bone, Element.bone, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.BlazingWheel, new NPCTypeInfo(Element.fire, Element.none, Element.fire) },
                {NPCID.GoblinScout, new NPCTypeInfo(Element.normal, Element.none, Element.normal) }, // goblins?

                {NPCID.Bird, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Pixie, new NPCTypeInfo(Element.fairy, Element.none, AbilityID.Levitate, Element.fairy) },
                {NPCID.ArmoredSkeleton, new NPCTypeInfo(Element.bone, Element.steel, Element.bone) },
                {NPCID.Mummy, new NPCTypeInfo(Element.ghost, Element.none, Element.ghost, AbilityID.Mummy) },
                {NPCID.DarkMummy, new NPCTypeInfo(Element.ghost, Element.dark, Element.dark, AbilityID.Mummy) },
                {NPCID.LightMummy, new NPCTypeInfo(Element.ghost, Element.fairy, Element.fairy, AbilityID.Mummy) },
                {NPCID.CorruptSlime, new NPCTypeInfo(Element.water, Element.dark, Element.dark, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Wraith, new NPCTypeInfo(Element.ghost, Element.none, AbilityID.Levitate, Element.ghost) },

                {NPCID.CursedHammer, new NPCTypeInfo(Element.steel, Element.dark, AbilityID.Levitate, Element.steel) },
                {NPCID.EnchantedSword, new NPCTypeInfo(Element.steel, Element.fairy, AbilityID.Levitate, Element.steel) },
                {NPCID.Mimic, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },

                {NPCID.Unicorn, new NPCTypeInfo(Element.fairy, Element.normal, Element.fairy) },
                {NPCID.WyvernHead, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.WyvernLegs, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.WyvernBody, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.WyvernBody2, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.WyvernBody3, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.WyvernTail, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },

                {NPCID.GiantBat, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Corruptor, new NPCTypeInfo(Element.dark, Element.none, AbilityID.Levitate, Element.flying) },
                {NPCID.DiggerHead, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },
                {NPCID.DiggerBody, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },
                {NPCID.DiggerTail, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },
                {NPCID.SeekerHead, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },
                {NPCID.SeekerBody, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },
                {NPCID.SeekerTail, new NPCTypeInfo(Element.dark, Element.ground, Element.dark) },

                {NPCID.Clinger, new NPCTypeInfo(Element.dark, Element.normal, Element.dark) },
                {NPCID.AnglerFish, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.GreenJellyfish, new NPCTypeInfo(Element.water, Element.electric, Element.electric, new AbilityContainer(primaryAbility: AbilityID.WaterAbsorb, hiddenAbility: AbilityID.VoltAbsorb)) },
                {NPCID.Werewolf, new NPCTypeInfo(Element.dark, Element.normal, Element.dark) },

                {NPCID.BoundGoblin, new NPCTypeInfo(Element.normal, Element.none, Element.normal) }, // npc
                {NPCID.BoundWizard, new NPCTypeInfo(Element.normal, Element.none, Element.normal) }, // npc
                {NPCID.GoblinTinkerer, new NPCTypeInfo(Element.normal, Element.none, Element.normal) }, // npc
                {NPCID.Wizard, new NPCTypeInfo(Element.normal, Element.none, Element.normal) }, // npc

                {NPCID.Clown, new NPCTypeInfo(Element.normal, Element.none, Element.normal) }, // clown
                {NPCID.SkeletonArcher, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.GoblinArcher, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.VileSpit, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.WallofFlesh, new NPCTypeInfo(Element.blood, Element.fire, Element.dark) }, // mouth
                {NPCID.WallofFleshEye, new NPCTypeInfo(Element.blood, Element.fire, Element.blood) },
                {NPCID.TheHungry, new NPCTypeInfo(Element.blood, Element.none, AbilityID.Levitate, Element.blood) },
                {NPCID.TheHungryII, new NPCTypeInfo(Element.blood, Element.none, AbilityID.Levitate, Element.blood) },
                {NPCID.LeechHead, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.LeechBody, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.LeechTail, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ChaosElemental, new NPCTypeInfo(Element.fairy, Element.none, Element.fairy) },
                {NPCID.Slimer, new NPCTypeInfo(Element.water, Element.dark, AbilityID.Levitate, Element.dark) }, // with wings
                {NPCID.Gastropod, new NPCTypeInfo(Element.water, Element.electric, Element.normal, new AbilityContainer(AbilityID.Levitate, hiddenAbility: AbilityID.Flammable)) },

                {NPCID.BoundMechanic, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Mechanic, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Retinazer, new NPCTypeInfo(Element.normal, Element.flying, Element.normal, new AbilityContainer(),
                    (parameters) =>
                    {
                        if (parameters.npc.ai[0] < 2)
                        {
                            return parameters.defaultTypes;
                        }
                        else
                        {
                            return new ThreeType(Element.steel, Element.flying, Element.steel);
                        }
                    } )
                },
                {NPCID.Spazmatism, new NPCTypeInfo(Element.normal, Element.flying, Element.normal, new AbilityContainer(),
                    (parameters) =>
                    {
                        if (parameters.npc.ai[0] < 2)
                        {
                            return parameters.defaultTypes;
                        }
                        else
                        {
                            return new ThreeType(Element.steel, Element.flying, Element.steel);
                        }
                    } )
                },
                {NPCID.SkeletronPrime, new NPCTypeInfo(Element.steel, Element.ghost, Element.steel) },
                {NPCID.PrimeCannon, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.PrimeSaw, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.PrimeVice, new NPCTypeInfo(Element.steel, Element.none, Element.fighting) },
                {NPCID.PrimeLaser, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },

                {NPCID.BaldZombie, new NPCTypeInfo(Element.blood, Element.none, Element.normal) },
                {NPCID.WanderingEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.TheDestroyer, new NPCTypeInfo(Element.steel, Element.ground, Element.fighting) },
                {NPCID.TheDestroyerBody, new NPCTypeInfo(Element.steel, Element.ground, Element.steel) },
                {NPCID.TheDestroyerTail, new NPCTypeInfo(Element.steel, Element.ground, Element.steel) },
                {NPCID.IlluminantBat, new NPCTypeInfo(Element.fairy, Element.flying, Element.fairy) },
                {NPCID.IlluminantSlime, new NPCTypeInfo(Element.water, Element.fairy, Element.fairy, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Probe, new NPCTypeInfo(Element.steel, Element.none, AbilityID.Levitate, Element.steel) },
                {NPCID.ToxicSludge, new NPCTypeInfo(Element.water, Element.poison, Element.poison, new AbilityContainer(AbilityID.None, hiddenAbility: AbilityID.Corrosion)) },
                {NPCID.SantaClaus, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.SnowmanGangsta, new NPCTypeInfo(Element.ice, Element.none, Element.ice) },
                {NPCID.MisterStabby, new NPCTypeInfo(Element.ice, Element.none, Element.ice) },
                {NPCID.SnowBalla, new NPCTypeInfo(Element.ice, Element.none, Element.ice) },
                {NPCID.IceSlime, new NPCTypeInfo(Element.water, Element.ice, Element.ice, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Penguin, new NPCTypeInfo(Element.normal, Element.ice, Element.normal, new AbilityContainer(AbilityID.ThickFat)) },
                {NPCID.PenguinBlack, new NPCTypeInfo(Element.normal, Element.ice, Element.normal, new AbilityContainer(AbilityID.ThickFat)) },
                {NPCID.IceBat, new NPCTypeInfo(Element.ice, Element.flying, Element.ice) },
                {NPCID.Lavabat, new NPCTypeInfo(Element.fire, Element.flying, Element.fire, AbilityID.FlashFire) },
                {NPCID.GiantFlyingFox, new NPCTypeInfo(Element.grass, Element.flying, Element.grass) },
                {NPCID.GiantTortoise, new NPCTypeInfo(Element.grass, Element.rock, Element.grass) },
                {NPCID.IceTortoise, new NPCTypeInfo(Element.ice, Element.rock, Element.ice) },
                {NPCID.Wolf, new NPCTypeInfo(Element.normal, Element.dark, Element.normal, new AbilityContainer(AbilityID.ThickFat)) },
                {NPCID.RedDevil, new NPCTypeInfo(Element.dark, Element.fire, AbilityID.Levitate, Element.dark) },
                {NPCID.Arapaima, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.VampireBat, new NPCTypeInfo(Element.normal, Element.flying, Element.blood) },
                {NPCID.Vampire, new NPCTypeInfo(Element.normal, Element.none, Element.blood) },
                {NPCID.Truffle, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.ZombieEskimo, new NPCTypeInfo(Element.blood, Element.none, Element.normal) },
                {NPCID.Frankenstein, new NPCTypeInfo(Element.normal, Element.dark, Element.normal) },
                {NPCID.BlackRecluse, new NPCTypeInfo(Element.bug, Element.poison, Element.poison) },
                {NPCID.WallCreeper, new NPCTypeInfo(Element.bug, Element.poison, Element.poison) },
                {NPCID.WallCreeperWall, new NPCTypeInfo(Element.bug, Element.poison, Element.poison) },
                {NPCID.SwampThing, new NPCTypeInfo(Element.water, Element.grass, Element.grass) },
                {NPCID.UndeadViking, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },

                {NPCID.CorruptPenguin, new NPCTypeInfo(Element.normal, Element.blood, Element.blood, new AbilityContainer(AbilityID.ThickFat)) },
                {NPCID.IceElemental, new NPCTypeInfo(Element.ice, Element.none, AbilityID.Levitate, Element.ice) },
                {NPCID.PigronCorruption, new NPCTypeInfo(Element.dragon, Element.dark, AbilityID.Levitate, Element.dark) },
                {NPCID.PigronHallow, new NPCTypeInfo(Element.dragon, Element.fairy, AbilityID.Levitate, Element.fairy) },
                {NPCID.RuneWizard, new NPCTypeInfo(Element.bone, Element.fire, Element.bone) },
                {NPCID.Crimera, new NPCTypeInfo(Element.blood, Element.flying, Element.blood) },
                {NPCID.Herpling, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.AngryTrapper, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.MossHornet, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.Derpling, new NPCTypeInfo(Element.bug, Element.none, Element.bug) },
                {NPCID.Steampunker, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.CrimsonAxe, new NPCTypeInfo(Element.steel, Element.blood, AbilityID.Levitate, Element.steel) },
                {NPCID.PigronCrimson, new NPCTypeInfo(Element.dragon, Element.blood, Element.blood) },
                {NPCID.FaceMonster, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.FloatyGross, new NPCTypeInfo(Element.ghost, Element.blood, AbilityID.Levitate, Element.ghost) },
                {NPCID.Crimslime, new NPCTypeInfo(Element.water, Element.blood, Element.blood, new AbilityContainer(AbilityID.Flammable)) },

                {NPCID.SpikedIceSlime, new NPCTypeInfo(Element.water, Element.ice, Element.ice, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.SnowFlinx, new NPCTypeInfo(Element.normal, Element.ice, Element.ice, new AbilityContainer(primaryAbility: AbilityID.Fluffy, hiddenAbility: AbilityID.ThickFat)) },
                {NPCID.PincushionZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SlimedZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SwampZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.TwiggyZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.CataractEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.SleepyEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.DialatedEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.GreenEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.PurpleEye, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.LostGirl, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Nymph, new NPCTypeInfo(Element.normal, Element.blood, Element.blood) },
                {NPCID.ArmoredViking, new NPCTypeInfo(Element.bone, Element.steel, Element.bone) },
                {NPCID.Lihzahrd, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.LihzahrdCrawler, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.FemaleZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.HeadacheSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.MisassembledSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.PantlessSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },

                {NPCID.SpikedJungleSlime, new NPCTypeInfo(Element.water, Element.grass, Element.grass, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Moth, new NPCTypeInfo(Element.bug, Element.flying, Element.bug) },
                {NPCID.IcyMerman, new NPCTypeInfo(Element.ice, Element.water, Element.ice) },
                {NPCID.DyeTrader, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.PartyGirl, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Cyborg, new NPCTypeInfo(Element.normal, Element.steel, Element.normal) },
                {NPCID.Bee, new NPCTypeInfo(Element.bug, Element.none, AbilityID.Levitate, Element.bug) },
                {NPCID.BeeSmall, new NPCTypeInfo(Element.bug, Element.none, AbilityID.Levitate, Element.bug) },
                {NPCID.PirateDeckhand, new NPCTypeInfo(Element.normal, Element.none, Element.fighting, new AbilityContainer(AbilityID.Scrappy)) },
                {NPCID.PirateCorsair, new NPCTypeInfo(Element.normal, Element.none, Element.steel) },
                {NPCID.PirateDeadeye, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.PirateCrossbower, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.PirateCaptain, new NPCTypeInfo(Element.normal, Element.water, Element.fighting) },

                {NPCID.CochinealBeetle, new NPCTypeInfo(Element.bug, Element.none, Element.bug) },
                {NPCID.CyanBeetle, new NPCTypeInfo(Element.bug, Element.none, Element.bug) },
                {NPCID.LacBeetle, new NPCTypeInfo(Element.bug, Element.none, Element.bug) },
                {NPCID.SeaSnail, new NPCTypeInfo(Element.water, Element.rock, Element.water) },
                {NPCID.Squid, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.QueenBee, new NPCTypeInfo(Element.bug, Element.none, AbilityID.Levitate, Element.bug) },
                {NPCID.ZombieRaincoat, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.FlyingFish, new NPCTypeInfo(Element.water, Element.flying, Element.water) },
                {NPCID.UmbrellaSlime, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.FlyingSnake, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Painter, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.WitchDoctor, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Pirate, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GoldfishWalker, new NPCTypeInfo(Element.water, Element.none, Element.water) },

                {NPCID.HornetFatty, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.HornetHoney, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.HornetLeafy, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.HornetSpikey, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.HornetStingy, new NPCTypeInfo(Element.bug, Element.poison, AbilityID.Levitate, Element.poison) },
                {NPCID.JungleCreeper, new NPCTypeInfo(Element.grass, Element.bug, Element.bug) },
                {NPCID.JungleCreeperWall, new NPCTypeInfo(Element.grass, Element.bug, Element.bug) },
                {NPCID.BlackRecluseWall, new NPCTypeInfo(Element.bug, Element.poison, Element.poison) },
                {NPCID.BloodCrawler, new NPCTypeInfo(Element.blood, Element.bug, Element.blood) },
                {NPCID.BloodCrawlerWall, new NPCTypeInfo(Element.blood, Element.bug, Element.blood) },

                {NPCID.BloodFeeder, new NPCTypeInfo(Element.water, Element.blood, Element.blood) },
                {NPCID.BloodJelly, new NPCTypeInfo(Element.water, Element.blood, Element.electric, new AbilityContainer(primaryAbility: AbilityID.WaterAbsorb, hiddenAbility: AbilityID.VoltAbsorb)) },
                {NPCID.IceGolem, new NPCTypeInfo(Element.ice, Element.rock, Element.ice) },
                {NPCID.RainbowSlime, new NPCTypeInfo(Element.water, Element.fairy, Element.fairy, new AbilityContainer(AbilityID.Flammable, hiddenAbility: AbilityID.ColorChange)) },

                {NPCID.Golem, new NPCTypeInfo(Element.rock, Element.none, Element.rock, AbilityID.Heatproof) },
                {NPCID.GolemHead, new NPCTypeInfo(Element.rock, Element.none, Element.rock, AbilityID.Heatproof) },
                {NPCID.GolemFistLeft, new NPCTypeInfo(Element.rock, Element.none, Element.fighting, AbilityID.Heatproof) },
                {NPCID.GolemFistRight, new NPCTypeInfo(Element.rock, Element.none, Element.fighting, AbilityID.Heatproof) },
                {NPCID.AngryNimbus, new NPCTypeInfo(Element.water, Element.flying, Element.water, AbilityID.LightningRod) },
                {NPCID.Eyezor, new NPCTypeInfo(Element.blood, Element.none, Element.normal) },
                {NPCID.Parrot, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Reaper, new NPCTypeInfo(Element.ghost, Element.dark, AbilityID.Levitate, Element.dark) },
                {NPCID.ZombieMushroom, new NPCTypeInfo(Element.blood, Element.grass, Element.grass) },
                {NPCID.ZombieMushroomHat, new NPCTypeInfo(Element.blood, Element.grass, Element.grass) },
                {NPCID.FungoFish, new NPCTypeInfo(Element.water, Element.grass, Element.electric, new AbilityContainer(AbilityID.WaterAbsorb, AbilityID.SapSipper, AbilityID.VoltAbsorb)) },
                {NPCID.AnomuraFungus, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.MushiLadybug, new NPCTypeInfo(Element.bug, Element.grass, Element.bug) },
                {NPCID.FungiBulb, new NPCTypeInfo(Element.grass, Element.none, Element.grass, AbilityID.SapSipper) },
                {NPCID.GiantFungiBulb, new NPCTypeInfo(Element.grass, Element.none, Element.grass, AbilityID.SapSipper) },
                {NPCID.FungiSpore, new NPCTypeInfo(Element.grass, Element.poison, Element.grass) },

                {NPCID.Plantera, new NPCTypeInfo(Element.grass, Element.none, Element.grass, AbilityID.SapSipper) },
                {NPCID.PlanterasHook, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.PlanterasTentacle, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Spore, new NPCTypeInfo(Element.grass, Element.poison, Element.grass) },
                {NPCID.BrainofCthulhu, new NPCTypeInfo(Element.psychic, Element.blood, Element.blood) },
                {NPCID.Creeper, new NPCTypeInfo(Element.blood, Element.none, AbilityID.Levitate, Element.blood) },

                {NPCID.IchorSticker, new NPCTypeInfo(Element.blood, Element.none, AbilityID.Levitate, Element.blood) },
                {NPCID.RustyArmoredBonesAxe, new NPCTypeInfo(Element.bone, Element.steel, Element.steel) },
                {NPCID.RustyArmoredBonesFlail, new NPCTypeInfo(Element.bone, Element.steel, Element.steel) },
                {NPCID.RustyArmoredBonesSword, new NPCTypeInfo(Element.bone, Element.steel, Element.steel) },
                {NPCID.RustyArmoredBonesSwordNoArmor, new NPCTypeInfo(Element.bone, Element.none, Element.steel) }, // no armor
                {NPCID.HellArmoredBones, new NPCTypeInfo(Element.bone, Element.fire, Element.fire, AbilityID.FlashFire) },
                {NPCID.HellArmoredBonesSpikeShield, new NPCTypeInfo(Element.bone, Element.fire, Element.fire, AbilityID.FlashFire) },
                {NPCID.HellArmoredBonesMace, new NPCTypeInfo(Element.bone, Element.fire, Element.fire, AbilityID.FlashFire) },
                {NPCID.HellArmoredBonesSword, new NPCTypeInfo(Element.bone, Element.fire, Element.fire, AbilityID.FlashFire) },
                {NPCID.RaggedCaster, new NPCTypeInfo(Element.bone, Element.ghost, Element.bone) },
                {NPCID.RaggedCasterOpenCoat, new NPCTypeInfo(Element.bone, Element.ghost, Element.bone) },
                {NPCID.Necromancer, new NPCTypeInfo(Element.bone, Element.psychic, Element.psychic) },
                {NPCID.NecromancerArmored, new NPCTypeInfo(Element.bone, Element.psychic, Element.psychic) },
                {NPCID.DiabolistRed, new NPCTypeInfo(Element.bone, Element.fire, Element.fire) },
                {NPCID.DiabolistWhite, new NPCTypeInfo(Element.bone, Element.fire, Element.fire) },
                {NPCID.BoneLee, new NPCTypeInfo(Element.bone, Element.fighting, Element.fighting, AbilityID.Scrappy) },
                {NPCID.DungeonSpirit, new NPCTypeInfo(Element.ghost, Element.none, AbilityID.Levitate, Element.ghost) },
                {NPCID.GiantCursedSkull, new NPCTypeInfo(Element.bone, Element.ghost, AbilityID.Levitate, Element.bone) },
                {NPCID.Paladin, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.SkeletonSniper, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.TacticalSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.SkeletonCommando, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.AngryBonesBig, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.AngryBonesBigMuscle, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.AngryBonesBigHelmet, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BirdBlue, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.BirdRed, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Squirrel, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Mouse, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Raven, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },

                {NPCID.SlimeMasked, new NPCTypeInfo(Element.water, Element.normal, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.BunnySlimed, new NPCTypeInfo(Element.normal, Element.water, Element.normal) },
                {NPCID.HoppinJack, new NPCTypeInfo(Element.grass, Element.ghost, Element.ghost, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.Scarecrow1, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow2, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow3, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow4, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow5, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow6, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow7, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow8, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow9, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.Scarecrow10, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },
                {NPCID.HeadlessHorseman, new NPCTypeInfo(Element.normal, Element.ghost, Element.ghost) },
                {NPCID.Ghost, new NPCTypeInfo(Element.ghost, Element.none, AbilityID.Levitate, Element.ghost) },

                {NPCID.DemonEyeOwl, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.DemonEyeSpaceship, new NPCTypeInfo(Element.normal, Element.steel, Element.normal) },
                {NPCID.ZombieDoctor, new NPCTypeInfo(Element.blood, Element.normal, Element.blood) },
                {NPCID.ZombieSuperman, new NPCTypeInfo(Element.blood, Element.fighting, Element.blood) },
                {NPCID.ZombiePixie, new NPCTypeInfo(Element.blood, Element.fairy, Element.blood) },
                {NPCID.SkeletonTopHat, new NPCTypeInfo(Element.bone, Element.normal, Element.normal) },
                {NPCID.SkeletonAstonaut, new NPCTypeInfo(Element.bone, Element.steel, Element.normal) },
                {NPCID.SkeletonAlien, new NPCTypeInfo(Element.bone, Element.none, Element.normal) },

                {NPCID.MourningWood, new NPCTypeInfo(Element.grass, Element.dark, Element.dark, new AbilityContainer(AbilityID.None, hiddenAbility: AbilityID.SapSipper)) },
                {NPCID.Splinterling, new NPCTypeInfo(Element.grass, Element.dark, Element.grass) },
                {NPCID.Pumpking, new NPCTypeInfo(Element.dark, Element.fire, Element.dark) },
                {NPCID.PumpkingBlade, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.Hellhound, new NPCTypeInfo(Element.normal, Element.dark, Element.normal) },
                {NPCID.Poltergeist, new NPCTypeInfo(Element.ghost, Element.none, AbilityID.Levitate, Element.ghost) },
                {NPCID.ZombieXmas, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ZombieSweater, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.SlimeRibbonWhite, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.SlimeRibbonYellow, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.SlimeRibbonGreen, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.SlimeRibbonRed, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.BunnyXmas, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.ZombieElf, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ZombieElfBeard, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ZombieElfGirl, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },

                {NPCID.PresentMimic, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.GingerbreadMan, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Yeti, new NPCTypeInfo(Element.normal, Element.ice, Element.ice) },
                {NPCID.Everscream, new NPCTypeInfo(Element.grass, Element.none, Element.grass, new AbilityContainer(AbilityID.None, hiddenAbility: AbilityID.SapSipper)) },
                {NPCID.IceQueen, new NPCTypeInfo(Element.ice, Element.none, AbilityID.Levitate, Element.ice) },
                {NPCID.SantaNK1, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.ElfCopter, new NPCTypeInfo(Element.steel, Element.flying, Element.steel) },
                {NPCID.Nutcracker, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.NutcrackerSpinning, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.ElfArcher, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Krampus, new NPCTypeInfo(Element.normal, Element.dark, Element.normal) },
                {NPCID.Flocko, new NPCTypeInfo(Element.ice, Element.none, AbilityID.Levitate, Element.ice) },
                {NPCID.Stylist, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.WebbedStylist, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },

                {NPCID.Firefly, new NPCTypeInfo(Element.electric, Element.flying, Element.normal) },
                {NPCID.Butterfly, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.Worm, new NPCTypeInfo(Element.normal, Element.grass, Element.normal) },
                {NPCID.LightningBug, new NPCTypeInfo(Element.electric, Element.none, AbilityID.Levitate, Element.normal) },
                {NPCID.Snail, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GlowingSnail, new NPCTypeInfo(Element.normal, Element.electric, Element.normal) },
                {NPCID.Frog, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Duck, new NPCTypeInfo(Element.normal, Element.water, Element.normal) },
                {NPCID.Duck2, new NPCTypeInfo(Element.normal, Element.water, Element.normal) },
                {NPCID.DuckWhite, new NPCTypeInfo(Element.normal, Element.water, Element.normal) },
                {NPCID.DuckWhite2, new NPCTypeInfo(Element.normal, Element.water, Element.normal) },
                {NPCID.ScorpionBlack, new NPCTypeInfo(Element.normal, Element.poison, Element.normal) },
                {NPCID.Scorpion, new NPCTypeInfo(Element.normal, Element.poison, Element.normal) },

                {NPCID.TravellingMerchant, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Angler, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DukeFishron, new NPCTypeInfo(Element.water, Element.dragon, Element.dragon) },
                {NPCID.DetonatingBubble, new NPCTypeInfo(Element.water, Element.none, Element.water) },
                {NPCID.Sharkron, new NPCTypeInfo(Element.water, Element.dragon, Element.water) },
                {NPCID.Sharkron2, new NPCTypeInfo(Element.water, Element.dragon, Element.water) },

                {NPCID.TruffleWorm, new NPCTypeInfo(Element.bug, Element.none, Element.bug) },
                {NPCID.TruffleWormDigger, new NPCTypeInfo(Element.bug, Element.none, Element.bug) },
                {NPCID.SleepingAngler, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Grasshopper, new NPCTypeInfo(Element.normal, Element.grass, Element.normal) },
                {NPCID.ChatteringTeethBomb, new NPCTypeInfo(Element.normal, Element.steel, Element.normal) },

                {NPCID.CultistArcherBlue, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.CultistArcherWhite, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },

                #region Martians
                {NPCID.BrainScrambler, new NPCTypeInfo(Element.normal, Element.psychic, Element.psychic) }, //martian
                {NPCID.RayGunner, new NPCTypeInfo(Element.normal, Element.dark, Element.dark) },
                {NPCID.MartianOfficer, new NPCTypeInfo(Element.normal, Element.dark, Element.dark) },
                {NPCID.ForceBubble, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GrayGrunt, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.MartianEngineer, new NPCTypeInfo(Element.normal, Element.psychic, Element.psychic) },
                {NPCID.MartianTurret, new NPCTypeInfo(Element.steel, Element.none, Element.steel, new AbilityContainer(AbilityID.LightningRod, hiddenAbility: AbilityID.VoltAbsorb)) },
                {NPCID.MartianDrone, new NPCTypeInfo(Element.steel, Element.flying, Element.steel) },
                {NPCID.GigaZapper, new NPCTypeInfo(Element.normal, Element.electric, Element.electric) },
                {NPCID.ScutlixRider, new NPCTypeInfo(Element.normal, Element.dark, Element.normal) },
                {NPCID.Scutlix, new NPCTypeInfo(Element.normal, Element.dark, Element.normal) },
                {NPCID.MartianSaucer, new NPCTypeInfo(Element.steel, Element.flying, Element.steel) },
                {NPCID.MartianSaucerTurret, new NPCTypeInfo(Element.steel, Element.none, AbilityID.Levitate, Element.steel) },
                {NPCID.MartianSaucerCannon, new NPCTypeInfo(Element.steel, Element.none, AbilityID.Levitate, Element.steel) },
                {NPCID.MartianSaucerCore, new NPCTypeInfo(Element.steel, Element.electric, AbilityID.Levitate, Element.steel) }, // martian
                #endregion

                {NPCID.MoonLordHead, new NPCTypeInfo(Element.dark, Element.psychic, Element.psychic) },
                {NPCID.MoonLordHand, new NPCTypeInfo(Element.dark, Element.fighting, Element.fighting) },
                {NPCID.MoonLordCore, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.MartianProbe, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.MoonLordFreeEye, new NPCTypeInfo(Element.dark, Element.none, Element.electric) },
                {NPCID.MoonLordLeechBlob, new NPCTypeInfo(Element.dark, Element.none, Element.electric) },

                {NPCID.StardustWormHead, new NPCTypeInfo(Element.dragon, Element.ground, AbilityID.Levitate, Element.dragon) },
                {NPCID.StardustWormBody, new NPCTypeInfo(Element.dragon, Element.ground, AbilityID.Levitate, Element.dragon) },
                {NPCID.StardustWormTail, new NPCTypeInfo(Element.dragon, Element.ground, AbilityID.Levitate, Element.dragon) },
                {NPCID.StardustCellBig, new NPCTypeInfo(Element.dragon, Element.none, AbilityID.Levitate, Element.dragon) },
                {NPCID.StardustCellSmall, new NPCTypeInfo(Element.dragon, Element.none, AbilityID.Levitate, Element.dragon) },
                {NPCID.StardustJellyfishBig, new NPCTypeInfo(Element.dragon, Element.electric, AbilityID.Levitate, Element.dragon) },
                {NPCID.StardustSpiderBig, new NPCTypeInfo(Element.dragon, Element.none, Element.dragon) },
                {NPCID.StardustSpiderSmall, new NPCTypeInfo(Element.dragon, Element.none, Element.dragon) },
                {NPCID.StardustSoldier, new NPCTypeInfo(Element.dragon, Element.normal, Element.dragon) },

                {NPCID.SolarCrawltipedeHead, new NPCTypeInfo(Element.fire, Element.ground, AbilityID.Levitate, Element.fire) },
                {NPCID.SolarCrawltipedeBody, new NPCTypeInfo(Element.fire, Element.ground, AbilityID.Levitate, Element.fire) },
                {NPCID.SolarCrawltipedeTail, new NPCTypeInfo(Element.fire, Element.ground, AbilityID.Levitate, Element.fire) },
                {NPCID.SolarDrakomire, new NPCTypeInfo(Element.fire, Element.none, Element.fire) },
                {NPCID.SolarDrakomireRider, new NPCTypeInfo(Element.fire, Element.none, Element.fire) },
                {NPCID.SolarSroller, new NPCTypeInfo(Element.fire, Element.rock, Element.fire) },
                {NPCID.SolarCorite, new NPCTypeInfo(Element.fire, Element.rock, AbilityID.Levitate, Element.fire) },
                {NPCID.SolarSolenian, new NPCTypeInfo(Element.fire, Element.dark, Element.fire, AbilityID.FlashFire) },

                {NPCID.NebulaBrain, new NPCTypeInfo(Element.psychic, Element.flying, Element.psychic) },
                {NPCID.NebulaHeadcrab, new NPCTypeInfo(Element.psychic, Element.dark, AbilityID.Levitate, Element.psychic) },
                {NPCID.LunarTowerVortex, new NPCTypeInfo(Element.electric, Element.ground, Element.electric) },
                {NPCID.NebulaBeast, new NPCTypeInfo(Element.psychic, Element.none, Element.psychic) },
                {NPCID.NebulaSoldier, new NPCTypeInfo(Element.psychic, Element.none, Element.psychic) },

                {NPCID.VortexRifleman, new NPCTypeInfo(Element.electric, Element.normal, Element.electric) },
                {NPCID.VortexHornetQueen, new NPCTypeInfo(Element.electric, Element.bug, AbilityID.Levitate, Element.electric) },
                {NPCID.VortexHornet, new NPCTypeInfo(Element.electric, Element.bug, AbilityID.Levitate, Element.electric) },
                {NPCID.VortexLarva, new NPCTypeInfo(Element.electric, Element.bug, Element.electric) },
                {NPCID.VortexSoldier, new NPCTypeInfo(Element.electric, Element.normal, Element.electric) },

                {NPCID.ArmedZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ArmedZombieEskimo, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ArmedZombiePincussion, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ArmedZombieSlimed, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ArmedZombieSwamp, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ArmedZombieTwiggy, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.ArmedZombieCenx, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.CultistTablet, new NPCTypeInfo(Element.dark, Element.fire, Element.fire) },
                {NPCID.CultistDevote, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.CultistBoss, new NPCTypeInfo(Element.dark, Element.none, AbilityID.Levitate, Element.dark) },
                {NPCID.CultistBossClone, new NPCTypeInfo(Element.dark, Element.none, AbilityID.Levitate, Element.dark) },
                {NPCID.TaxCollector, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },

                {NPCID.GoldBird, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.GoldBunny, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GoldButterfly, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.GoldFrog, new NPCTypeInfo(Element.normal, Element.water, Element.normal) },
                {NPCID.GoldGrasshopper, new NPCTypeInfo(Element.normal, Element.grass, Element.normal) },
                {NPCID.GoldMouse, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GoldWorm, new NPCTypeInfo(Element.normal, Element.ground, Element.normal) },
                {NPCID.BoneThrowingSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BoneThrowingSkeleton2, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BoneThrowingSkeleton3, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.BoneThrowingSkeleton4, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.SkeletonMerchant, new NPCTypeInfo(Element.normal, Element.bone, Element.normal) },

                {NPCID.CultistDragonHead, new NPCTypeInfo(Element.dragon, Element.ghost, Element.dragon) },
                {NPCID.CultistDragonBody1, new NPCTypeInfo(Element.dragon, Element.ghost, Element.dragon) },
                {NPCID.CultistDragonBody2, new NPCTypeInfo(Element.dragon, Element.ghost, Element.dragon) },
                {NPCID.CultistDragonBody3, new NPCTypeInfo(Element.dragon, Element.ghost, Element.dragon) },
                {NPCID.CultistDragonBody4, new NPCTypeInfo(Element.dragon, Element.ghost, Element.dragon) },
                {NPCID.CultistDragonTail, new NPCTypeInfo(Element.dragon, Element.ghost, Element.dragon) },

                {NPCID.Butcher, new NPCTypeInfo(Element.dark, Element.none, Element.normal) },
                {NPCID.CreatureFromTheDeep, new NPCTypeInfo(Element.grass, Element.water, Element.grass) },
                {NPCID.Fritz, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.Nailhead, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.CrimsonBunny, new NPCTypeInfo(Element.normal, Element.blood, Element.blood) },
                {NPCID.CrimsonGoldfish, new NPCTypeInfo(Element.water, Element.blood, Element.blood) },
                {NPCID.Psycho, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.DeadlySphere, new NPCTypeInfo(Element.steel, Element.none, Element.steel) },
                {NPCID.DrManFly, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.ThePossessed, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },

                {NPCID.CrimsonPenguin, new NPCTypeInfo(Element.normal, Element.blood, Element.blood, new AbilityContainer(AbilityID.ThickFat)) },
                {NPCID.GoblinSummoner, new NPCTypeInfo(Element.normal, Element.dark, Element.dark) },
                {NPCID.ShadowFlameApparition, new NPCTypeInfo(Element.dark, Element.ghost, AbilityID.Levitate, Element.fire) },
                {NPCID.BigMimicCorruption, new NPCTypeInfo(Element.steel, Element.dark, Element.steel) },
                {NPCID.BigMimicCrimson, new NPCTypeInfo(Element.steel, Element.blood, Element.steel) },
                {NPCID.BigMimicHallow, new NPCTypeInfo(Element.steel, Element.fairy, Element.steel) },
                {NPCID.BigMimicJungle, new NPCTypeInfo(Element.steel, Element.grass, Element.steel) },

                {NPCID.Mothron, new NPCTypeInfo(Element.bug, Element.flying, Element.bug) },
                {NPCID.MothronEgg, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.MothronSpawn, new NPCTypeInfo(Element.bug, Element.flying, Element.bug) },
                {NPCID.Medusa, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.GreekSkeleton, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.GraniteGolem, new NPCTypeInfo(Element.rock, Element.none, Element.rock) },
                {NPCID.GraniteFlyer, new NPCTypeInfo(Element.rock, Element.none, AbilityID.Levitate, Element.rock) },

                {NPCID.EnchantedNightcrawler, new NPCTypeInfo(Element.normal, Element.electric, Element.normal) },
                {NPCID.Grubby, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Sluggy, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.Buggy, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.TargetDummy, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.BloodZombie, new NPCTypeInfo(Element.blood, Element.none, Element.blood) },
                {NPCID.Drippler, new NPCTypeInfo(Element.blood, Element.none, AbilityID.Levitate, Element.blood) },

                {NPCID.PirateShip, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.PirateShipCannon, new NPCTypeInfo(Element.steel, Element.flying, Element.steel) },
                {NPCID.LunarTowerStardust, new NPCTypeInfo(Element.ground, Element.dragon, Element.dragon) },
                {NPCID.Crawdad, new NPCTypeInfo(Element.water, Element.ground, Element.normal) },
                {NPCID.Crawdad2, new NPCTypeInfo(Element.water, Element.ground, Element.normal) },
                {NPCID.GiantShelly, new NPCTypeInfo(Element.rock, Element.ground, Element.normal) },
                {NPCID.GiantShelly2, new NPCTypeInfo(Element.rock, Element.ground, Element.normal) },
                {NPCID.Salamander , new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander2, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander3, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander4, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander5, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander6, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander7, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander8, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.Salamander9, new NPCTypeInfo(Element.normal, Element.poison, Element.poison, new AbilityContainer(primaryAbility: AbilityID.Corrosion, hiddenAbility: AbilityID.ColorChange)) },
                {NPCID.LunarTowerNebula, new NPCTypeInfo(Element.ground, Element.psychic, Element.psychic) },
                {NPCID.WalkingAntlion, new NPCTypeInfo(Element.ground, Element.bug, Element.bug) }, // GiantAntlionCharger
                {NPCID.FlyingAntlion, new NPCTypeInfo(Element.bug, Element.flying, Element.bug) },
                {NPCID.DuneSplicerHead, new NPCTypeInfo(Element.ground, Element.rock, Element.ground, new AbilityContainer(AbilityID.SandForce, AbilityID.MoldBreaker)) },
                {NPCID.DuneSplicerBody, new NPCTypeInfo(Element.ground, Element.rock, Element.ground, new AbilityContainer(AbilityID.SandForce, AbilityID.MoldBreaker)) },
                {NPCID.DuneSplicerTail, new NPCTypeInfo(Element.ground, Element.rock, Element.ground, new AbilityContainer(AbilityID.SandForce, AbilityID.MoldBreaker)) },
                {NPCID.TombCrawlerHead, new NPCTypeInfo(Element.ground, Element.rock, Element.ground, new AbilityContainer(AbilityID.SandForce)) },
                {NPCID.TombCrawlerBody, new NPCTypeInfo(Element.ground, Element.rock, Element.ground, new AbilityContainer(AbilityID.SandForce)) },
                {NPCID.TombCrawlerTail, new NPCTypeInfo(Element.ground, Element.rock, Element.ground, new AbilityContainer(AbilityID.SandForce)) },

                {NPCID.SolarFlare, new NPCTypeInfo(Element.fire, Element.none, Element.fire, AbilityID.FlashFire) },
                {NPCID.LunarTowerSolar, new NPCTypeInfo(Element.ground, Element.fire, Element.fire) },
                {NPCID.SolarSpearman, new NPCTypeInfo(Element.fire, Element.none, Element.fire) },
                {NPCID.SolarGoop, new NPCTypeInfo(Element.fire, Element.none, AbilityID.Levitate, Element.fire) },

                {NPCID.MartianWalker, new NPCTypeInfo(Element.steel, Element.none, Element.steel, new AbilityContainer(AbilityID.None, hiddenAbility: AbilityID.LightningRod) ) }, // martian
                {NPCID.AncientCultistSquidhead, new NPCTypeInfo(Element.ghost, Element.dark, Element.dark) },
                {NPCID.AncientLight, new NPCTypeInfo(Element.psychic, Element.none, Element.psychic) },
                {NPCID.AncientDoom, new NPCTypeInfo(Element.ghost, Element.dark, Element.dark) },

                {NPCID.DesertGhoul, new NPCTypeInfo(Element.ghost, Element.ground, Element.ghost) },
                {NPCID.DesertGhoulCorruption, new NPCTypeInfo(Element.ghost, Element.dark, Element.dark) },
                {NPCID.DesertGhoulCrimson, new NPCTypeInfo(Element.ghost, Element.blood, Element.blood) },
                {NPCID.DesertGhoulHallow, new NPCTypeInfo(Element.ghost, Element.fairy, Element.fairy) },
                {NPCID.DesertLamiaLight, new NPCTypeInfo(Element.ground, Element.none, Element.ground) },
                {NPCID.DesertLamiaDark, new NPCTypeInfo(Element.ground, Element.dark, Element.ground) },
                {NPCID.DesertScorpionWalk, new NPCTypeInfo(Element.ground, Element.poison, Element.poison) },
                {NPCID.DesertScorpionWall, new NPCTypeInfo(Element.ground, Element.poison, Element.poison) },
                {NPCID.DesertDjinn, new NPCTypeInfo(Element.ghost, Element.ground, AbilityID.Levitate, Element.ghost) },

                {NPCID.DemonTaxCollector, new NPCTypeInfo(Element.normal, Element.ghost, Element.normal) },
                {NPCID.SlimeSpiked, new NPCTypeInfo(Element.water, Element.none, Element.water, new AbilityContainer(AbilityID.Flammable)) },
                {NPCID.TheBride, new NPCTypeInfo(Element.blood, Element.normal, Element.normal) },
                {NPCID.SandSlime, new NPCTypeInfo(Element.water, Element.ground, Element.ground, new AbilityContainer(AbilityID.WaterCompaction)) },
                {NPCID.SquirrelRed, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.SquirrelGold, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.PartyBunny, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },

                {NPCID.SandElemental, new NPCTypeInfo(Element.ground, Element.none, AbilityID.Levitate, Element.ground) },
                {NPCID.SandShark, new NPCTypeInfo(Element.dragon, Element.ground, Element.ground) },
                {NPCID.SandsharkCorrupt, new NPCTypeInfo(Element.dragon, Element.dark, Element.dark) },
                {NPCID.SandsharkCrimson, new NPCTypeInfo(Element.dragon, Element.blood, Element.blood) },
                {NPCID.SandsharkHallow, new NPCTypeInfo(Element.dragon, Element.fairy, Element.fairy) },
                {NPCID.Tumbleweed, new NPCTypeInfo(Element.grass, Element.none, Element.grass) },

                {NPCID.DD2EterniaCrystal, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2LanePortal, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.DD2Bartender, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2Betsy, new NPCTypeInfo(Element.dragon, Element.fire, AbilityID.Levitate, Element.dragon) },
                {NPCID.DD2GoblinT1, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2GoblinT2, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2GoblinT3, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2GoblinBomberT1, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2GoblinBomberT2, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2GoblinBomberT3, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2WyvernT1, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.DD2WyvernT2, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.DD2WyvernT3, new NPCTypeInfo(Element.dragon, Element.flying, Element.dragon) },
                {NPCID.DD2JavelinstT1, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2JavelinstT2, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2JavelinstT3, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2DarkMageT1, new NPCTypeInfo(Element.psychic, Element.dark, Element.dark, AbilityID.Levitate) },
                {NPCID.DD2DarkMageT3, new NPCTypeInfo(Element.psychic, Element.dark, Element.dark, AbilityID.Levitate) },
                {NPCID.DD2SkeletonT1, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.DD2SkeletonT3, new NPCTypeInfo(Element.bone, Element.none, Element.bone) },
                {NPCID.DD2WitherBeastT2, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.DD2WitherBeastT3, new NPCTypeInfo(Element.dark, Element.none, Element.dark) },
                {NPCID.DD2DrakinT2, new NPCTypeInfo(Element.dragon, Element.none, Element.dragon) },
                {NPCID.DD2DrakinT3, new NPCTypeInfo(Element.dragon, Element.none, Element.dragon) },
                {NPCID.DD2KoboldWalkerT2, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2KoboldWalkerT3, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2KoboldFlyerT2, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.DD2KoboldFlyerT3, new NPCTypeInfo(Element.normal, Element.flying, Element.normal) },
                {NPCID.DD2OgreT2, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2OgreT3, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                {NPCID.DD2LightningBugT3, new NPCTypeInfo(Element.bug, Element.electric, Element.electric, new AbilityContainer(AbilityID.Levitate, AbilityID.VoltAbsorb)) },
                {NPCID.BartenderUnconscious, new NPCTypeInfo(Element.normal, Element.none, Element.normal) },
                //{1022, new EnemyInfo(Element.none, Element.none, Element.none, Element.none) },
                //{1023, new EnemyInfo(Element.none, Element.none, Element.none, Element.none) },
                //{1025, new EnemyInfo(Element.none, Element.none, Element.none, Element.none) },
                //{1026, new EnemyInfo(Element.none, Element.none, Element.none, Element.none) },
            };
        }

        public static void Unload()
        {
            Type = null;
        }

        public static Dictionary<int, NPCTypeInfo> Type;

        
        //public static void Adding()
        //{
        //    Mod calamity = ModLoader.GetMod("CalamityMod");
        //    if (calamity != null)
        //    {
        //        Enemies.Type.Add()
        //    }
        //}
    }
}

