//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria.ID;
//using Terraria.ModLoader;
//using TerraTyping.DataTypes;

//namespace TerraTyping.Dictionaries;

//[Obsolete]
//public class Armors : ILoadable
//{
//    private ArmorTypeInfo[] types;

//    public static Armors Instance { get; private set; }

//    void ILoadable.Load(Mod mod)
//    {
//        Instance = this;
//    }

//    void ILoadable.Unload()
//    {
//        Instance = null;
//    }

//    public static ArmorTypeInfo GetInfo(int type)
//    {
//        if (type >= 0 && type < Instance.types.Length)
//        {
//            return Instance.types[type];
//        }
//        else
//        {
//            return ArmorTypeInfo.ArmorDefault;
//        }
//    }

//    public static void SetupTypes()
//    {
//        Instance.types = new ArmorTypeInfo[ItemLoader.ItemCount];
//        Instance.LoadVanillaArmors();

//        int loaded = 0;
//        for (int i = 0; i < Instance.types.Length; i++)
//        {
//            ArmorTypeInfo typeInfo = Instance.types[i];
//            if (typeInfo is null)
//            {
//                Instance.types[i] = ArmorTypeInfo.ArmorDefault;
//            }
//            else
//            {
//                loaded++;
//            }
//        }

//        ModContent.GetInstance<TerraTyping>().Logger.Info($"Loaded {Instance.types.Length} armors.");
//    }

//    private void LoadVanillaArmors()
//    {
//        types[0] = new ArmorTypeInfo(ElementArray.Get(Element.none));
//        types[ItemID.MiningHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.ground));
//        types[ItemID.MiningShirt] = new ArmorTypeInfo(ElementArray.Get(Element.ground));
//        types[ItemID.MiningPants] = new ArmorTypeInfo(ElementArray.Get(Element.ground));
//        types[ItemID.WoodHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.WoodBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.WoodGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.EbonwoodHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.dark));
//        types[ItemID.EbonwoodBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.dark));
//        types[ItemID.EbonwoodGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.dark));
//        types[ItemID.RichMahoganyHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.grass));
//        types[ItemID.RichMahoganyBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.grass));
//        types[ItemID.RichMahoganyGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.grass));
//        types[ItemID.PearlwoodHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.fairy));
//        types[ItemID.PearlwoodBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.fairy));
//        types[ItemID.PearlwoodGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.fairy));
//        types[ItemID.ShadewoodHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.blood));
//        types[ItemID.ShadewoodBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.blood));
//        types[ItemID.ShadewoodGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.blood));
//        types[ItemID.BorealWoodHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.ice));
//        types[ItemID.BorealWoodBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.ice));
//        types[ItemID.BorealWoodGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.ice));
//        types[ItemID.PalmWoodHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.water));
//        types[ItemID.PalmWoodBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.water));
//        types[ItemID.PalmWoodGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.normal, Element.water));
//        types[ItemID.RainHat] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.RainCoat] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.EskimoHood] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.EskimoCoat] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.EskimoPants] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.PinkEskimoHood] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.PinkEskimoCoat] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.PinkEskimoPants] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.AnglerHat] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.AnglerVest] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.AnglerPants] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.CactusHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.CactusBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.CactusLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.PumpkinHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.PumpkinBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.PumpkinLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.NinjaHood] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.NinjaShirt] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.NinjaPants] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.FossilHelm] = new ArmorTypeInfo(ElementArray.Get(Element.bone, Element.ground));
//        types[ItemID.FossilShirt] = new ArmorTypeInfo(ElementArray.Get(Element.bone, Element.ground));
//        types[ItemID.FossilPants] = new ArmorTypeInfo(ElementArray.Get(Element.bone, Element.ground));
//        types[ItemID.BeeHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.BeeBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.BeeGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.ObsidianHelm] = new ArmorTypeInfo(ElementArray.Get(Element.rock, Element.water, Element.fire));
//        types[ItemID.ObsidianShirt] = new ArmorTypeInfo(ElementArray.Get(Element.rock, Element.water, Element.fire));
//        types[ItemID.ObsidianPants] = new ArmorTypeInfo(ElementArray.Get(Element.rock, Element.water, Element.fire));
//        types[ItemID.GladiatorHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fighting, Element.steel));
//        types[ItemID.GladiatorBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.fighting, Element.steel));
//        types[ItemID.GladiatorLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.fighting, Element.steel));
//        types[ItemID.CopperHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.CopperChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.CopperGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TinHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TinChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TinGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.AncientIronHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.IronHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.IronChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.IronGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.LeadHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.LeadChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.LeadGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.SilverHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.SilverChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.SilverGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TungstenHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TungstenChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TungstenGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.AncientGoldHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.GoldChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.GoldGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.GoldHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.PlatinumHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.PlatinumChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.PlatinumGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.MeteorHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.rock, Element.fire));
//        types[ItemID.MeteorSuit] = new ArmorTypeInfo(ElementArray.Get(Element.rock, Element.fire));
//        types[ItemID.MeteorLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.rock, Element.fire));
//        types[ItemID.JungleHat] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.JungleShirt] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.JunglePants] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.AncientCobaltHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.AncientCobaltBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.AncientCobaltLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.AncientNecroHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.bone));
//        types[ItemID.NecroHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.bone));
//        types[ItemID.NecroBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.bone));
//        types[ItemID.NecroGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.bone));
//        types[ItemID.ShadowGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.ShadowScalemail] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.ShadowHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.AncientShadowHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.AncientShadowScalemail] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.AncientShadowGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.CrimsonHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.blood));
//        types[ItemID.CrimsonScalemail] = new ArmorTypeInfo(ElementArray.Get(Element.blood));
//        types[ItemID.CrimsonGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.blood));
//        types[ItemID.MoltenHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fire));
//        types[ItemID.MoltenBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.fire));
//        types[ItemID.MoltenGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.fire));
//        types[ItemID.WizardHat] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.MagicHat] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.TopazRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.SapphireRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.EmeraldRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.RubyRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.DiamondRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.AmberRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.GypsyRobe] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.SpiderMask] = new ArmorTypeInfo(ElementArray.Get(Element.bug, Element.poison));
//        types[ItemID.SpiderBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.bug, Element.poison));
//        types[ItemID.SpiderGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.bug, Element.poison));
//        types[ItemID.CobaltHat] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.CobaltHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.CobaltMask] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.CobaltBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.CobaltLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.dark));
//        types[ItemID.PalladiumMask] = new ArmorTypeInfo(ElementArray.Get(Element.fighting));
//        types[ItemID.PalladiumHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fighting));
//        types[ItemID.PalladiumHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.fighting));
//        types[ItemID.PalladiumBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.fighting));
//        types[ItemID.PalladiumLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.fighting));
//        types[ItemID.MythrilHood] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.MythrilHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.MythrilHat] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.MythrilChainmail] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.MythrilGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.OrichalcumMask] = new ArmorTypeInfo(ElementArray.Get(Element.fairy));
//        types[ItemID.OrichalcumHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fairy));
//        types[ItemID.OrichalcumHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.fairy));
//        types[ItemID.OrichalcumBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.fairy));
//        types[ItemID.OrichalcumLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.fairy));
//        types[ItemID.AdamantiteHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.AdamantiteHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.AdamantiteMask] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.AdamantiteBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.AdamantiteLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.TitaniumMask] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TitaniumHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TitaniumHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TitaniumBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.TitaniumLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.CrystalNinjaHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.dark));
//        types[ItemID.CrystalNinjaChestplate] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.dark));
//        types[ItemID.CrystalNinjaLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.dark));
//        types[ItemID.FrostHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.ice));
//        types[ItemID.FrostBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.ice));
//        types[ItemID.FrostLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.ice));
//        types[ItemID.AncientBattleArmorHat] = new ArmorTypeInfo(ElementArray.Get(Element.ground));
//        types[ItemID.AncientBattleArmorShirt] = new ArmorTypeInfo(ElementArray.Get(Element.ground));
//        types[ItemID.AncientBattleArmorPants] = new ArmorTypeInfo(ElementArray.Get(Element.ground));
//        types[ItemID.HallowedPlateMail] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.HallowedGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.HallowedHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.HallowedHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.HallowedMask] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.HallowedHood] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.AncientHallowedMask] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.AncientHallowedHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.AncientHallowedHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.AncientHallowedHood] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.AncientHallowedPlateMail] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.AncientHallowedGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.fairy, Element.fighting));
//        types[ItemID.ChlorophyteMask] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.ChlorophyteHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.ChlorophyteHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.ChlorophytePlateMail] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.ChlorophyteGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.grass));
//        types[ItemID.TurtleHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.rock));
//        types[ItemID.TurtleScaleMail] = new ArmorTypeInfo(ElementArray.Get(Element.rock));
//        types[ItemID.TurtleLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.rock));
//        types[ItemID.TikiMask] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.poison));
//        types[ItemID.TikiShirt] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.poison));
//        types[ItemID.TikiPants] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.poison));
//        types[ItemID.BeetleHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.BeetleScaleMail] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.BeetleShell] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.BeetleLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.bug));
//        types[ItemID.ShroomiteHeadgear] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.steel));
//        types[ItemID.ShroomiteMask] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.steel));
//        types[ItemID.ShroomiteHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.steel));
//        types[ItemID.ShroomiteBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.steel));
//        types[ItemID.ShroomiteLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.steel));
//        types[ItemID.SpectreMask] = new ArmorTypeInfo(ElementArray.Get(Element.ghost));
//        types[ItemID.SpectreHood] = new ArmorTypeInfo(ElementArray.Get(Element.ghost));
//        types[ItemID.SpectreRobe] = new ArmorTypeInfo(ElementArray.Get(Element.ghost));
//        types[ItemID.SpectrePants] = new ArmorTypeInfo(ElementArray.Get(Element.ghost));
//        types[ItemID.SpookyHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.ghost));
//        types[ItemID.SpookyBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.ghost));
//        types[ItemID.SpookyLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.grass, Element.ghost));
//        types[ItemID.VortexHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.electric));
//        types[ItemID.VortexBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.electric));
//        types[ItemID.VortexLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.electric));
//        types[ItemID.NebulaHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.NebulaBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.NebulaLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.psychic));
//        types[ItemID.SolarFlareHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.fire));
//        types[ItemID.SolarFlareBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.fire));
//        types[ItemID.SolarFlareLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.fire));
//        types[ItemID.StardustHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.StardustBreastplate] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.StardustLeggings] = new ArmorTypeInfo(ElementArray.Get(Element.dragon));
//        types[ItemID.ApprenticeHat] = new ArmorTypeInfo(ElementArray.Get(Element.dark), AbilityID.DD2Stab);
//        types[ItemID.ApprenticeRobe] = new ArmorTypeInfo(ElementArray.Get(Element.dark), AbilityID.DD2Stab);
//        types[ItemID.ApprenticeTrousers] = new ArmorTypeInfo(ElementArray.Get(Element.dark), AbilityID.DD2Stab);
//        types[ItemID.ApprenticeAltHead] = new ArmorTypeInfo(ElementArray.Get(Element.dark, Element.fire), AbilityID.DD2Stab);
//        types[ItemID.ApprenticeAltShirt] = new ArmorTypeInfo(ElementArray.Get(Element.dark, Element.fire), AbilityID.DD2Stab);
//        types[ItemID.ApprenticeAltPants] = new ArmorTypeInfo(ElementArray.Get(Element.dark, Element.fire), AbilityID.DD2Stab);
//        types[ItemID.SquireGreatHelm] = new ArmorTypeInfo(ElementArray.Get(Element.steel), AbilityID.DD2Stab);
//        types[ItemID.SquirePlating] = new ArmorTypeInfo(ElementArray.Get(Element.steel), AbilityID.DD2Stab);
//        types[ItemID.SquireGreaves] = new ArmorTypeInfo(ElementArray.Get(Element.steel), AbilityID.DD2Stab);
//        types[ItemID.SquireAltHead] = new ArmorTypeInfo(ElementArray.Get(Element.steel, Element.fighting), AbilityID.DD2Stab);
//        types[ItemID.SquireAltShirt] = new ArmorTypeInfo(ElementArray.Get(Element.steel, Element.fighting), AbilityID.DD2Stab);
//        types[ItemID.SquireAltPants] = new ArmorTypeInfo(ElementArray.Get(Element.steel, Element.fighting), AbilityID.DD2Stab);
//        types[ItemID.MonkBrows] = new ArmorTypeInfo(ElementArray.Get(Element.fighting), AbilityID.DD2Stab);
//        types[ItemID.MonkShirt] = new ArmorTypeInfo(ElementArray.Get(Element.fighting), AbilityID.DD2Stab);
//        types[ItemID.MonkPants] = new ArmorTypeInfo(ElementArray.Get(Element.fighting), AbilityID.DD2Stab);
//        types[ItemID.MonkAltHead] = new ArmorTypeInfo(ElementArray.Get(Element.fighting, Element.dark), AbilityID.DD2Stab);
//        types[ItemID.MonkAltShirt] = new ArmorTypeInfo(ElementArray.Get(Element.fighting, Element.dark), AbilityID.DD2Stab);
//        types[ItemID.MonkAltPants] = new ArmorTypeInfo(ElementArray.Get(Element.fighting, Element.dark), AbilityID.DD2Stab);
//        types[ItemID.HuntressWig] = new ArmorTypeInfo(ElementArray.Get(Element.grass), AbilityID.DD2Stab);
//        types[ItemID.HuntressJerkin] = new ArmorTypeInfo(ElementArray.Get(Element.grass), AbilityID.DD2Stab);
//        types[ItemID.HuntressPants] = new ArmorTypeInfo(ElementArray.Get(Element.grass), AbilityID.DD2Stab);
//        types[ItemID.HuntressAltHead] = new ArmorTypeInfo(ElementArray.Get(Element.grass), AbilityID.DD2Stab);
//        types[ItemID.HuntressAltShirt] = new ArmorTypeInfo(ElementArray.Get(Element.grass), AbilityID.DD2Stab);
//        types[ItemID.HuntressAltPants] = new ArmorTypeInfo(ElementArray.Get(Element.grass), AbilityID.DD2Stab);
//        types[ItemID.Goggles] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.EmptyBucket] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.DivingHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.water));
//        types[ItemID.GreenCap] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.VikingHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.steel));
//        types[ItemID.Gi] = new ArmorTypeInfo(ElementArray.Get(Element.fighting));
//        types[ItemID.NightVisionHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.UltrabrightHelmet] = new ArmorTypeInfo(ElementArray.Get(Element.normal));
//        types[ItemID.FlinxFurCoat] = new ArmorTypeInfo(ElementArray.Get(Element.ice), AbilityID.Fluffy);
//        types[ItemID.MoonLordLegs] = new ArmorTypeInfo(ElementArray.Get(Element.dragon, Element.dark));
//    }
//}
