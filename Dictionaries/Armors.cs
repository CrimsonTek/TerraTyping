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
    public static class Armors
    {
        public static void Load()
        {
            Helmet = new Dictionary<int, ArmorTypeInfo>
            {
                {ItemID.MiningHelmet, new ArmorTypeInfo(Element.ground, Element.normal) },
                {ItemID.WoodHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.RichMahoganyHelmet, new ArmorTypeInfo(Element.grass, Element.normal) },
                {ItemID.BorealWoodHelmet, new ArmorTypeInfo(Element.ice, Element.normal) },
                {ItemID.PalmWoodHelmet, new ArmorTypeInfo(Element.water, Element.normal) },
                {ItemID.EbonwoodHelmet, new ArmorTypeInfo(Element.dark, Element.normal) },
                {ItemID.ShadewoodHelmet, new ArmorTypeInfo(Element.dark, Element.normal) },

                {ItemID.EskimoHood, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PinkEskimoHood, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.AnglerHat, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.CactusHelmet, new ArmorTypeInfo(Element.grass, Element.none) },

                {ItemID.CopperHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.TinHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PumpkinHelmet, new ArmorTypeInfo(Element.normal, Element.none) },

                {ItemID.GladiatorHelmet, new ArmorTypeInfo(Element.fighting, Element.steel) },
                {ItemID.AncientIronHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.IronHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.LeadHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.SilverHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.TungstenHelmet, new ArmorTypeInfo(Element.steel, Element.normal) },
                {ItemID.GoldHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PlatinumHelmet, new ArmorTypeInfo(Element.steel, Element.normal) },

                {ItemID.NinjaHood, new ArmorTypeInfo(Element.dark, Element.fighting) },
                {ItemID.FossilHelm, new ArmorTypeInfo(Element.bone, Element.ground) },
                {ItemID.ObsidianHelm, new ArmorTypeInfo(Element.fire, Element.water) },
                {ItemID.BeeHeadgear, new ArmorTypeInfo(Element.bug, Element.flying) },
                {ItemID.JungleHat, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.AncientCobaltHelmet, new ArmorTypeInfo(Element.steel, Element.none) },

                {ItemID.MeteorHelmet, new ArmorTypeInfo(Element.fire, Element.rock) },
                {ItemID.NecroHelmet, new ArmorTypeInfo(Element.bone, Element.none) },
                {ItemID.ShadowHelmet, new ArmorTypeInfo(Element.dark, Element.none) },
                {ItemID.AncientShadowHelmet, new ArmorTypeInfo(Element.dark, Element.none) },
                {ItemID.CrimsonHelmet, new ArmorTypeInfo(Element.blood, Element.none) },
                {ItemID.MoltenHelmet, new ArmorTypeInfo(Element.fire, Element.steel) },

                {ItemID.PearlwoodHelmet, new ArmorTypeInfo(Element.fairy, Element.normal) },
                {ItemID.SpiderMask, new ArmorTypeInfo(Element.bug, Element.poison) },

                {ItemID.CobaltHat, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.CobaltHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.CobaltMask, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.PalladiumHeadgear, new ArmorTypeInfo(Element.fighting, Element.none) },
                {ItemID.PalladiumHelmet, new ArmorTypeInfo(Element.fighting, Element.none) },
                {ItemID.PalladiumMask, new ArmorTypeInfo(Element.fighting, Element.none) },
                {ItemID.MythrilHat, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.MythrilHelmet, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.MythrilHood, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.OrichalcumHeadgear, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.OrichalcumHelmet, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.OrichalcumMask, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.AdamantiteHeadgear, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.AdamantiteHelmet, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.AdamantiteMask, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.TitaniumHeadgear, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.TitaniumHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.TitaniumMask, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.FrostHelmet, new ArmorTypeInfo(Element.ice, Element.none) },
                {ItemID.AncientBattleArmorHat, new ArmorTypeInfo(Element.rock, Element.none) },

                {ItemID.ApprenticeHat, new ArmorTypeInfo(Element.psychic, Element.none) },
                {ItemID.SquireGreatHelm, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.HuntressWig, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.MonkBrows, new ArmorTypeInfo(Element.fighting, Element.none) },

                {ItemID.HallowedHeadgear, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                {ItemID.HallowedHelmet, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                {ItemID.HallowedMask, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                {ItemID.ChlorophyteHeadgear, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.ChlorophyteHelmet, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.ChlorophyteMask, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.TurtleHelmet, new ArmorTypeInfo(Element.grass, Element.rock) },
                {ItemID.TikiMask, new ArmorTypeInfo(Element.grass, Element.normal) },
                {ItemID.SpookyHelmet, new ArmorTypeInfo(Element.dark, Element.grass) },
                {ItemID.ShroomiteHeadgear, new ArmorTypeInfo(Element.grass, Element.steel) },
                {ItemID.ShroomiteHelmet, new ArmorTypeInfo(Element.grass, Element.steel) },
                {ItemID.ShroomiteMask, new ArmorTypeInfo(Element.grass, Element.steel) },
                {ItemID.SpectreHood, new ArmorTypeInfo(Element.ghost, Element.none) },
                {ItemID.SpectreMask, new ArmorTypeInfo(Element.ghost, Element.none) },
                {ItemID.BeetleHelmet, new ArmorTypeInfo(Element.bug, Element.rock) },

                {ItemID.ApprenticeAltHead, new ArmorTypeInfo(Element.psychic, Element.dark) }, // psychic
                {ItemID.MonkAltHead, new ArmorTypeInfo(Element.steel, Element.dark) }, // steel
                {ItemID.HuntressAltHead, new ArmorTypeInfo(Element.fairy, Element.dark) }, // fairy
                {ItemID.SquireAltHead, new ArmorTypeInfo(Element.fighting, Element.dark) }, // fighting

                {ItemID.SolarFlareHelmet, new ArmorTypeInfo(Element.fire, Element.none) },
                {ItemID.VortexHelmet, new ArmorTypeInfo(Element.electric, Element.none) },
                {ItemID.NebulaHelmet, new ArmorTypeInfo(Element.psychic, Element.none) },
                {ItemID.StardustHelmet, new ArmorTypeInfo(Element.dragon, Element.none) },

                //{ItemID.None, new ArmorTypeInfo(Element.none, Element.none) },
            };

            Chest = new Dictionary<int, ArmorTypeInfo>
            {
                {ItemID.MiningShirt, new ArmorTypeInfo(Element.ground, Element.normal) },
                {ItemID.WoodBreastplate, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.RichMahoganyBreastplate, new ArmorTypeInfo(Element.grass, Element.normal) },
                {ItemID.BorealWoodBreastplate, new ArmorTypeInfo(Element.ice, Element.normal) },
                {ItemID.PalmWoodBreastplate, new ArmorTypeInfo(Element.water, Element.normal) },
                {ItemID.EbonwoodBreastplate, new ArmorTypeInfo(Element.dark, Element.normal) },
                {ItemID.ShadewoodBreastplate, new ArmorTypeInfo(Element.dark, Element.normal) },

                {ItemID.EskimoCoat, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PinkEskimoCoat, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.AnglerVest, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.CactusBreastplate, new ArmorTypeInfo(Element.grass, Element.none) },

                {ItemID.CopperChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.TinChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PumpkinBreastplate, new ArmorTypeInfo(Element.normal, Element.none) },

                {ItemID.GladiatorBreastplate, new ArmorTypeInfo(Element.fighting, Element.steel) },
                {ItemID.IronChainmail, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.LeadChainmail, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.SilverChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.TungstenChainmail, new ArmorTypeInfo(Element.steel, Element.normal) },
                {ItemID.GoldChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PlatinumChainmail, new ArmorTypeInfo(Element.steel, Element.normal) },

                {ItemID.NinjaShirt, new ArmorTypeInfo(Element.dark, Element.fighting) },
                {ItemID.FossilShirt, new ArmorTypeInfo(Element.bone, Element.ground) },
                {ItemID.ObsidianShirt, new ArmorTypeInfo(Element.fire, Element.water) },
                {ItemID.BeeBreastplate, new ArmorTypeInfo(Element.bug, Element.flying) },
                {ItemID.JungleShirt, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.AncientCobaltBreastplate, new ArmorTypeInfo(Element.steel, Element.none) },

                {ItemID.MeteorSuit, new ArmorTypeInfo(Element.fire, Element.rock) },
                {ItemID.NecroBreastplate, new ArmorTypeInfo(Element.bone, Element.none) },
                {ItemID.ShadowScalemail, new ArmorTypeInfo(Element.dark, Element.none) },
                {ItemID.AncientShadowScalemail, new ArmorTypeInfo(Element.dark, Element.none) },
                {ItemID.CrimsonScalemail, new ArmorTypeInfo(Element.blood, Element.none) },
                {ItemID.MoltenBreastplate, new ArmorTypeInfo(Element.fire, Element.steel) },

                {ItemID.PearlwoodBreastplate, new ArmorTypeInfo(Element.fairy, Element.normal) },
                {ItemID.SpiderBreastplate, new ArmorTypeInfo(Element.bug, Element.poison) },

                {ItemID.CobaltBreastplate, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.PalladiumBreastplate, new ArmorTypeInfo(Element.fighting, Element.none) },
                {ItemID.MythrilChainmail, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.OrichalcumBreastplate, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.AdamantiteBreastplate, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.TitaniumBreastplate, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.FrostBreastplate, new ArmorTypeInfo(Element.ice, Element.none) },
                {ItemID.AncientBattleArmorShirt, new ArmorTypeInfo(Element.rock, Element.none) },

                {ItemID.ApprenticeRobe, new ArmorTypeInfo(Element.psychic, Element.none) },
                {ItemID.SquirePlating, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.HuntressJerkin, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.MonkShirt, new ArmorTypeInfo(Element.fighting, Element.none) },

                {ItemID.HallowedPlateMail, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                {ItemID.ChlorophytePlateMail, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.TurtleScaleMail, new ArmorTypeInfo(Element.grass, Element.rock) },
                {ItemID.TikiShirt, new ArmorTypeInfo(Element.grass, Element.normal) },
                {ItemID.SpookyBreastplate, new ArmorTypeInfo(Element.dark, Element.grass) },
                {ItemID.ShroomiteBreastplate, new ArmorTypeInfo(Element.grass, Element.steel) },
                {ItemID.SpectreRobe, new ArmorTypeInfo(Element.ghost, Element.none) },
                {ItemID.BeetleScaleMail, new ArmorTypeInfo(Element.bug, Element.rock) },
                {ItemID.BeetleShell, new ArmorTypeInfo(Element.bug, Element.rock) },

                {ItemID.ApprenticeAltShirt, new ArmorTypeInfo(Element.psychic, Element.dark) },
                {ItemID.MonkAltShirt, new ArmorTypeInfo(Element.steel, Element.dark) },
                {ItemID.HuntressAltShirt, new ArmorTypeInfo(Element.fairy, Element.dark) },
                {ItemID.SquireAltShirt, new ArmorTypeInfo(Element.fighting, Element.dark) },

                {ItemID.SolarFlareBreastplate, new ArmorTypeInfo(Element.fire, Element.none) },
                {ItemID.VortexBreastplate, new ArmorTypeInfo(Element.electric, Element.none) },
                {ItemID.NebulaBreastplate, new ArmorTypeInfo(Element.psychic, Element.none) },
                {ItemID.StardustBreastplate, new ArmorTypeInfo(Element.dragon, Element.none) },

                //{ItemID.None, new ArmorTypeInfo(Element.none, Element.none) },
            };
            
            Leggings = new Dictionary<int, ArmorTypeInfo>
            {
                {ItemID.MiningPants, new ArmorTypeInfo(Element.ground, Element.normal) },
                {ItemID.WoodGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.RichMahoganyGreaves, new ArmorTypeInfo(Element.grass, Element.normal) },
                {ItemID.BorealWoodGreaves, new ArmorTypeInfo(Element.ice, Element.normal) },
                {ItemID.PalmWoodGreaves, new ArmorTypeInfo(Element.water, Element.normal) },
                {ItemID.EbonwoodGreaves, new ArmorTypeInfo(Element.dark, Element.normal) },
                {ItemID.ShadewoodGreaves, new ArmorTypeInfo(Element.dark, Element.normal) },

                {ItemID.EskimoPants, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PinkEskimoPants, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.AnglerPants, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.CactusLeggings, new ArmorTypeInfo(Element.grass, Element.none) },

                {ItemID.CopperGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.TinGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PumpkinLeggings, new ArmorTypeInfo(Element.normal, Element.none) },

                {ItemID.GladiatorLeggings, new ArmorTypeInfo(Element.fighting, Element.steel) },
                {ItemID.IronGreaves, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.LeadGreaves, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.SilverGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.TungstenGreaves, new ArmorTypeInfo(Element.steel, Element.normal) },
                {ItemID.GoldGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                {ItemID.PlatinumGreaves, new ArmorTypeInfo(Element.steel, Element.normal) },

                {ItemID.NinjaPants, new ArmorTypeInfo(Element.dark, Element.fighting) },
                {ItemID.FossilPants, new ArmorTypeInfo(Element.bone, Element.ground) },
                {ItemID.ObsidianPants, new ArmorTypeInfo(Element.fire, Element.water) },
                {ItemID.BeeGreaves, new ArmorTypeInfo(Element.bug, Element.flying) },
                {ItemID.JunglePants, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.AncientCobaltLeggings, new ArmorTypeInfo(Element.steel, Element.none) },

                {ItemID.MeteorLeggings, new ArmorTypeInfo(Element.fire, Element.rock) },
                {ItemID.NecroGreaves, new ArmorTypeInfo(Element.bone, Element.none) },
                {ItemID.ShadowGreaves, new ArmorTypeInfo(Element.dark, Element.none) },
                {ItemID.AncientShadowGreaves, new ArmorTypeInfo(Element.dark, Element.none) },
                {ItemID.CrimsonGreaves, new ArmorTypeInfo(Element.blood, Element.none) },
                {ItemID.MoltenGreaves, new ArmorTypeInfo(Element.fire, Element.steel) },

                {ItemID.PearlwoodGreaves, new ArmorTypeInfo(Element.fairy, Element.normal) },
                {ItemID.SpiderGreaves, new ArmorTypeInfo(Element.bug, Element.poison) },

                {ItemID.CobaltLeggings, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.PalladiumLeggings, new ArmorTypeInfo(Element.fighting, Element.none) },
                {ItemID.MythrilGreaves, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.OrichalcumLeggings, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.AdamantiteLeggings, new ArmorTypeInfo(Element.dragon, Element.none) },
                {ItemID.TitaniumLeggings, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.FrostLeggings, new ArmorTypeInfo(Element.ice, Element.none) },
                {ItemID.AncientBattleArmorPants, new ArmorTypeInfo(Element.rock, Element.none) },

                {ItemID.ApprenticeTrousers, new ArmorTypeInfo(Element.psychic, Element.none) },
                {ItemID.SquireGreaves, new ArmorTypeInfo(Element.steel, Element.none) },
                {ItemID.HuntressPants, new ArmorTypeInfo(Element.fairy, Element.none) },
                {ItemID.MonkPants, new ArmorTypeInfo(Element.fighting, Element.none) },

                {ItemID.HallowedGreaves, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                {ItemID.ChlorophyteGreaves, new ArmorTypeInfo(Element.grass, Element.none) },
                {ItemID.TurtleLeggings, new ArmorTypeInfo(Element.grass, Element.rock) },
                {ItemID.TikiPants, new ArmorTypeInfo(Element.grass, Element.normal) },
                {ItemID.SpookyLeggings, new ArmorTypeInfo(Element.dark, Element.grass) },
                {ItemID.ShroomiteLeggings, new ArmorTypeInfo(Element.grass, Element.steel) },
                {ItemID.SpectrePants, new ArmorTypeInfo(Element.ghost, Element.none) },
                {ItemID.BeetleLeggings, new ArmorTypeInfo(Element.bug, Element.rock) },

                {ItemID.ApprenticeAltPants, new ArmorTypeInfo(Element.psychic, Element.dark) },
                {ItemID.MonkAltPants, new ArmorTypeInfo(Element.steel, Element.dark) },
                {ItemID.HuntressAltPants, new ArmorTypeInfo(Element.fairy, Element.dark) },
                {ItemID.SquireAltPants, new ArmorTypeInfo(Element.fighting, Element.dark) },

                {ItemID.SolarFlareLeggings, new ArmorTypeInfo(Element.fire, Element.none) },
                {ItemID.VortexLeggings, new ArmorTypeInfo(Element.electric, Element.none) },
                {ItemID.NebulaLeggings, new ArmorTypeInfo(Element.psychic, Element.none) },
                {ItemID.StardustLeggings, new ArmorTypeInfo(Element.dragon, Element.none) },

                //{ItemID.None, new ArmorTypeInfo(Element.none, Element.none) },
            };

            Type = new Dictionary<int, ArmorTypeInfo>
            {
                //{ItemID.MiningHelmet, new ArmorTypeInfo(Element.ground, Element.normal) },
                //{ItemID.MiningShirt, new ArmorTypeInfo(Element.ground, Element.normal) },
                //{ItemID.MiningPants, new ArmorTypeInfo(Element.ground, Element.normal) },
                //{ItemID.WoodHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.WoodBreastplate, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.WoodGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.RichMahoganyHelmet, new ArmorTypeInfo(Element.grass, Element.normal) },
                //{ItemID.RichMahoganyBreastplate, new ArmorTypeInfo(Element.grass, Element.normal) },
                //{ItemID.RichMahoganyGreaves, new ArmorTypeInfo(Element.grass, Element.normal) },
                //{ItemID.BorealWoodHelmet, new ArmorTypeInfo(Element.ice, Element.normal) },
                //{ItemID.BorealWoodBreastplate, new ArmorTypeInfo(Element.ice, Element.normal) },
                //{ItemID.BorealWoodGreaves, new ArmorTypeInfo(Element.ice, Element.normal) },
                //{ItemID.PalmWoodHelmet, new ArmorTypeInfo(Element.water, Element.normal) },
                //{ItemID.PalmWoodBreastplate, new ArmorTypeInfo(Element.water, Element.normal) },
                //{ItemID.PalmWoodGreaves, new ArmorTypeInfo(Element.water, Element.normal) },
                //{ItemID.EbonwoodHelmet, new ArmorTypeInfo(Element.dark, Element.normal) },
                //{ItemID.EbonwoodBreastplate, new ArmorTypeInfo(Element.dark, Element.normal) },
                //{ItemID.EbonwoodGreaves, new ArmorTypeInfo(Element.dark, Element.normal) },
                //{ItemID.ShadewoodHelmet, new ArmorTypeInfo(Element.dark, Element.normal) },
                //{ItemID.ShadewoodBreastplate, new ArmorTypeInfo(Element.dark, Element.normal) },
                //{ItemID.ShadewoodGreaves, new ArmorTypeInfo(Element.dark, Element.normal) },
                //{ItemID.RainHat, new ArmorTypeInfo(Element.normal, Element.none) }, // 2
                //{ItemID.RainCoat, new ArmorTypeInfo(Element.normal, Element.none) }, // 2

                //{ItemID.EskimoHood, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.EskimoCoat, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.EskimoPants, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PinkEskimoHood, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PinkEskimoCoat, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PinkEskimoPants, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.AnglerHat, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.AnglerVest, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.AnglerPants, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.CactusHelmet, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.CactusBreastplate, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.CactusLeggings, new ArmorTypeInfo(Element.grass, Element.none) },

                //{ItemID.CopperHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.CopperChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.CopperGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.TinHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.TinChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.TinGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PumpkinHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PumpkinBreastplate, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PumpkinLeggings, new ArmorTypeInfo(Element.normal, Element.none) },

                //{ItemID.GladiatorHelmet, new ArmorTypeInfo(Element.fighting, Element.steel) },
                //{ItemID.GladiatorBreastplate, new ArmorTypeInfo(Element.fighting, Element.steel) },
                //{ItemID.GladiatorLeggings, new ArmorTypeInfo(Element.fighting, Element.steel) },
                //{ItemID.AncientIronHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.IronHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.IronChainmail, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.IronGreaves, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.LeadHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.LeadChainmail, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.LeadGreaves, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.SilverHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.SilverChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.SilverGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.TungstenHelmet, new ArmorTypeInfo(Element.steel, Element.normal) },
                //{ItemID.TungstenChainmail, new ArmorTypeInfo(Element.steel, Element.normal) },
                //{ItemID.TungstenGreaves, new ArmorTypeInfo(Element.steel, Element.normal) },
                //{ItemID.GoldHelmet, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.GoldChainmail, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.GoldGreaves, new ArmorTypeInfo(Element.normal, Element.none) },
                //{ItemID.PlatinumHelmet, new ArmorTypeInfo(Element.steel, Element.normal) },
                //{ItemID.PlatinumChainmail, new ArmorTypeInfo(Element.steel, Element.normal) },
                //{ItemID.PlatinumGreaves, new ArmorTypeInfo(Element.steel, Element.normal) },

                //{ItemID.NinjaHood, new ArmorTypeInfo(Element.dark, Element.fighting) },
                //{ItemID.NinjaShirt, new ArmorTypeInfo(Element.dark, Element.fighting) },
                //{ItemID.NinjaPants, new ArmorTypeInfo(Element.dark, Element.fighting) },
                //{ItemID.FossilHelm, new ArmorTypeInfo(Element.bone, Element.ground) },
                //{ItemID.FossilShirt, new ArmorTypeInfo(Element.bone, Element.ground) },
                //{ItemID.FossilPants, new ArmorTypeInfo(Element.bone, Element.ground) },
                //{ItemID.ObsidianHelm, new ArmorTypeInfo(Element.fire, Element.water) },
                //{ItemID.ObsidianChest, new ArmorTypeInfo(Element.fire, Element.water) },
                //{ItemID.ObsidianPants, new ArmorTypeInfo(Element.fire, Element.water) },
                //{ItemID.BeeHeadgear, new ArmorTypeInfo(Element.bug, Element.flying) },
                //{ItemID.BeeBreastplate, new ArmorTypeInfo(Element.bug, Element.flying) },
                //{ItemID.BeeGreaves, new ArmorTypeInfo(Element.bug, Element.flying) },
                //{ItemID.JungleHat, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.JungleShirt, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.JunglePants, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.AncientCobaltHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.AncientCobaltBreastplate, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.AncientCobaltLeggings, new ArmorTypeInfo(Element.steel, Element.none) },

                //{ItemID.MeteorHelmet, new ArmorTypeInfo(Element.fire, Element.rock) },
                //{ItemID.MeteorSuit, new ArmorTypeInfo(Element.fire, Element.rock) },
                //{ItemID.MeteorLeggings, new ArmorTypeInfo(Element.fire, Element.rock) },
                //{ItemID.NecroHelmet, new ArmorTypeInfo(Element.bone, Element.none) },
                //{ItemID.NecroBreastplate, new ArmorTypeInfo(Element.bone, Element.none) },
                //{ItemID.NecroGreaves, new ArmorTypeInfo(Element.bone, Element.none) },
                //{ItemID.ShadowHelmet, new ArmorTypeInfo(Element.dark, Element.none) },
                //{ItemID.ShadowScalemail, new ArmorTypeInfo(Element.dark, Element.none) },
                //{ItemID.ShadowGreaves, new ArmorTypeInfo(Element.dark, Element.none) },
                //{ItemID.AncientShadowHelmet, new ArmorTypeInfo(Element.dark, Element.none) },
                //{ItemID.AncientShadowScalemail, new ArmorTypeInfo(Element.dark, Element.none) },
                //{ItemID.AncientShadowGreaves, new ArmorTypeInfo(Element.dark, Element.none) },
                //{ItemID.CrimsonHelmet, new ArmorTypeInfo(Element.blood, Element.none) },
                //{ItemID.CrimsonScalemail, new ArmorTypeInfo(Element.blood, Element.none) },
                //{ItemID.CrimsonGreaves, new ArmorTypeInfo(Element.blood, Element.none) },
                //{ItemID.MoltenHelmet, new ArmorTypeInfo(Element.fire, Element.steel) },
                //{ItemID.MoltenBreastplate, new ArmorTypeInfo(Element.fire, Element.steel) },
                //{ItemID.MoltenGreaves, new ArmorTypeInfo(Element.fire, Element.steel) },

                //{ItemID.PearlwoodHelmet, new ArmorTypeInfo(Element.fairy, Element.normal) },
                //{ItemID.PearlwoodBreastplate, new ArmorTypeInfo(Element.fairy, Element.normal) },
                //{ItemID.PearlwoodGreaves, new ArmorTypeInfo(Element.fairy, Element.normal) },
                //{ItemID.SpiderMask, new ArmorTypeInfo(Element.bug, Element.poison) },
                //{ItemID.SpiderBreastplate, new ArmorTypeInfo(Element.bug, Element.poison) },
                //{ItemID.SpiderGreaves, new ArmorTypeInfo(Element.bug, Element.poison) },

                //{ItemID.CobaltHat, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.CobaltHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.CobaltMask, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.CobaltBreastplate, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.CobaltLeggings, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.PalladiumHeadgear, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.PalladiumHelmet, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.PalladiumMask, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.PalladiumBreastplate, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.PalladiumLeggings, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.MythrilHat, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.MythrilHelmet, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.MythrilHood, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.MythrilChainmail, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.MythrilGreaves, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.OrichalcumHeadgear, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.OrichalcumHelmet, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.OrichalcumMask, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.OrichalcumBreastplate, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.OrichalcumLeggings, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.AdamantiteHeadgear, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.AdamantiteHelmet, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.AdamantiteMask, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.AdamantiteBreastplate, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.AdamantiteLeggings, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.TitaniumHeadgear, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.TitaniumHelmet, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.TitaniumMask, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.TitaniumBreastplate, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.TitaniumLeggings, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.FrostHelmet, new ArmorTypeInfo(Element.ice, Element.none) },
                //{ItemID.FrostBreastplate, new ArmorTypeInfo(Element.ice, Element.none) },
                //{ItemID.FrostLeggings, new ArmorTypeInfo(Element.ice, Element.none) },
                //{ItemID.AncientBattleArmorHat, new ArmorTypeInfo(Element.rock, Element.none) },
                //{ItemID.AncientBattleArmorShirt, new ArmorTypeInfo(Element.rock, Element.none) },
                //{ItemID.AncientBattleArmorPants, new ArmorTypeInfo(Element.rock, Element.none) },

                //{ItemID.ApprenticeHat, new ArmorTypeInfo(Element.psychic, Element.none) },
                //{ItemID.ApprenticeRobe, new ArmorTypeInfo(Element.psychic, Element.none) },
                //{ItemID.ApprenticeTrousers, new ArmorTypeInfo(Element.psychic, Element.none) },
                //{ItemID.SquireGreatHelm, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.SquirePlating, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.SquireGreaves, new ArmorTypeInfo(Element.steel, Element.none) },
                //{ItemID.HuntressWig, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.HuntressJerkin, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.HuntressPants, new ArmorTypeInfo(Element.fairy, Element.none) },
                //{ItemID.MonkBrows, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.MonkShirt, new ArmorTypeInfo(Element.fighting, Element.none) },
                //{ItemID.MonkPants, new ArmorTypeInfo(Element.fighting, Element.none) },

                //{ItemID.HallowedHeadgear, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                //{ItemID.HallowedHelmet, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                //{ItemID.HallowedMask, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                //{ItemID.HallowedPlateMail, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                //{ItemID.HallowedGreaves, new ArmorTypeInfo(Element.fairy, Element.fighting) },
                //{ItemID.ChlorophyteHeadgear, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.ChlorophyteHelmet, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.ChlorophyteMask, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.ChlorophytePlateMail, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.ChlorophyteGreaves, new ArmorTypeInfo(Element.grass, Element.none) },
                //{ItemID.TurtleHelmet, new ArmorTypeInfo(Element.grass, Element.rock) },
                //{ItemID.TurtleScaleMail, new ArmorTypeInfo(Element.grass, Element.rock) },
                //{ItemID.TurtleLeggings, new ArmorTypeInfo(Element.grass, Element.rock) },
                //{ItemID.TikiMask, new ArmorTypeInfo(Element.grass, Element.normal) },
                //{ItemID.TikiShirt, new ArmorTypeInfo(Element.grass, Element.normal) },
                //{ItemID.TikiPants, new ArmorTypeInfo(Element.grass, Element.normal) },
                //{ItemID.SpookyHelmet, new ArmorTypeInfo(Element.dark, Element.grass) },
                //{ItemID.SpookyBreastplate, new ArmorTypeInfo(Element.dark, Element.grass) },
                //{ItemID.SpookyLeggings, new ArmorTypeInfo(Element.dark, Element.grass) },
                //{ItemID.ShroomiteHeadgear, new ArmorTypeInfo(Element.grass, Element.steel) },
                //{ItemID.ShroomiteHelmet, new ArmorTypeInfo(Element.grass, Element.steel) },
                //{ItemID.ShroomiteMask, new ArmorTypeInfo(Element.grass, Element.steel) },
                //{ItemID.ShroomiteBreastplate, new ArmorTypeInfo(Element.grass, Element.steel) },
                //{ItemID.ShroomiteLeggings, new ArmorTypeInfo(Element.grass, Element.steel) },
                //{ItemID.SpectreHood, new ArmorTypeInfo(Element.ghost, Element.none) },
                //{ItemID.SpectreMask, new ArmorTypeInfo(Element.ghost, Element.none) },
                //{ItemID.SpectreRobe, new ArmorTypeInfo(Element.ghost, Element.none) },
                //{ItemID.SpectrePants, new ArmorTypeInfo(Element.ghost, Element.none) },
                //{ItemID.BeetleHelmet, new ArmorTypeInfo(Element.bug, Element.rock) },
                //{ItemID.BeetleScaleMail, new ArmorTypeInfo(Element.bug, Element.rock) },
                //{ItemID.BeetleShell, new ArmorTypeInfo(Element.bug, Element.rock) },
                //{ItemID.BeetleLeggings, new ArmorTypeInfo(Element.bug, Element.rock) },

                //{ItemID.ApprenticeAltHead, new ArmorTypeInfo(Element.psychic, Element.dark) }, // psychic
                //{ItemID.ApprenticeAltShirt, new ArmorTypeInfo(Element.psychic, Element.dark) },
                //{ItemID.ApprenticeAltPants, new ArmorTypeInfo(Element.psychic, Element.dark) },
                //{ItemID.MonkAltHead, new ArmorTypeInfo(Element.steel, Element.dark) }, // steel
                //{ItemID.MonkAltShirt, new ArmorTypeInfo(Element.steel, Element.dark) },
                //{ItemID.MonkAltPants, new ArmorTypeInfo(Element.steel, Element.dark) },
                //{ItemID.HuntressAltHead, new ArmorTypeInfo(Element.fairy, Element.dark) }, // fairy
                //{ItemID.HuntressAltShirt, new ArmorTypeInfo(Element.fairy, Element.dark) },
                //{ItemID.HuntressAltPants, new ArmorTypeInfo(Element.fairy, Element.dark) },
                //{ItemID.SquireAltHead, new ArmorTypeInfo(Element.fighting, Element.dark) }, // fighting
                //{ItemID.SquireAltShirt, new ArmorTypeInfo(Element.fighting, Element.dark) },
                //{ItemID.SquireAltPants, new ArmorTypeInfo(Element.fighting, Element.dark) },

                //{ItemID.SolarFlareHelmet, new ArmorTypeInfo(Element.fire, Element.none) },
                //{ItemID.SolarFlareBreastplate, new ArmorTypeInfo(Element.fire, Element.none) },
                //{ItemID.SolarFlareLeggings, new ArmorTypeInfo(Element.fire, Element.none) },
                //{ItemID.VortexHelmet, new ArmorTypeInfo(Element.electric, Element.none) },
                //{ItemID.VortexBreastplate, new ArmorTypeInfo(Element.electric, Element.none) },
                //{ItemID.VortexLeggings, new ArmorTypeInfo(Element.electric, Element.none) },
                //{ItemID.NebulaHelmet, new ArmorTypeInfo(Element.psychic, Element.none) },
                //{ItemID.NebulaBreastplate, new ArmorTypeInfo(Element.psychic, Element.none) },
                //{ItemID.NebulaLeggings, new ArmorTypeInfo(Element.psychic, Element.none) },
                //{ItemID.StardustHelmet, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.StardustBreastplate, new ArmorTypeInfo(Element.dragon, Element.none) },
                //{ItemID.StardustLeggings, new ArmorTypeInfo(Element.dragon, Element.none) },

                //{ItemID.None, new ArmorTypeInfo(Element.none, Element.none) },
            };
        }

        public static void Unload()
        {
            Type = null;
            Helmet = null;
            Chest = null;
            Leggings = null;
        }

        public static Dictionary<int, ArmorTypeInfo> Type;
        public static Dictionary<int, ArmorTypeInfo> Helmet;
        public static Dictionary<int, ArmorTypeInfo> Chest;
        public static Dictionary<int, ArmorTypeInfo> Leggings;
    }
}
