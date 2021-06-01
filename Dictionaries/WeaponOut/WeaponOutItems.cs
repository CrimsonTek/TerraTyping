using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Attributes;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public class WeaponOutItems
    {
        public static void Load()
        {
            Type = new Dictionary<int, ItemTypeInfo>();
            _Type = new Dictionary<string, ItemTypeInfo>()
            {
                {"FistsBoxing", new ItemTypeInfo(Element.fighting) },
                {"FistsGranite", new ItemTypeInfo(Element.ground) },
                {"FistsSlime", new ItemTypeInfo(Element.water) },
                {"FistsOfFury", new ItemTypeInfo(Element.rock) },
                {"FistsJungleClaws", new ItemTypeInfo(Element.grass) },
                {"FistsBone", new ItemTypeInfo(Element.bone) },
                {"FistsMolten", new ItemTypeInfo(Element.fire) },

                {"GlovesWooden", new ItemTypeInfo(Element.fighting) },
                {"GlovesPalm", new ItemTypeInfo(Element.fighting) },
                {"GlovesCaestus", new ItemTypeInfo(Element.fighting) },
                {"GlovesCaestusCrimson", new ItemTypeInfo(Element.blood) },
                {"GlovesObsidian", new ItemTypeInfo(Element.rock) },
                {"GlovesFossil", new ItemTypeInfo(Element.bone) },
                {"GlovesBee", new ItemTypeInfo(Element.bug) },

                {"KnucklesIron", new ItemTypeInfo(Element.steel) },
                {"KnucklesLead", new ItemTypeInfo(Element.steel) },
                {"KnucklesGold", new ItemTypeInfo(Element.steel) },
                {"KnucklesPlat", new ItemTypeInfo(Element.steel) },
                {"KnucklesFlintlock", new ItemTypeInfo(Element.fighting) },
                {"KnucklesMeteor", new ItemTypeInfo(Element.rock) },
                {"KnucklesDungeon", new ItemTypeInfo(Element.dark) },
                {"KnucklesDemon", new ItemTypeInfo(Element.dark) },

                {"FistsSparring", new ItemTypeInfo(Element.fairy) },
                {"FistsAdamant", new ItemTypeInfo(Element.dragon) },
                {"FistsTitanium", new ItemTypeInfo(Element.steel) },
                {"FistsCursed", new ItemTypeInfo(Element.ghost) },
                {"FistsForbidden", new ItemTypeInfo(Element.ground) },
                {"FistsLihzarhd", new ItemTypeInfo(Element.fighting) },
                {"FistsFrozen", new ItemTypeInfo(Element.ice) },
                {"FistsBetsy", new ItemTypeInfo(Element.dragon) },
                {"FistsMartian", new ItemTypeInfo(Element.electric) },

                {"GlovesCobalt", new ItemTypeInfo(Element.steel) },
                {"GlovesOrich", new ItemTypeInfo(Element.fairy) },
                {"GlovesCrystal", new ItemTypeInfo(Element.fairy) },
                {"GlovesHallow", new ItemTypeInfo(Element.fighting) },
                {"GlovesButterfly", new ItemTypeInfo(Element.bug) },
                {"GlovesLee", new ItemTypeInfo(Element.fighting) },
                {"GlovesPumpkin", new ItemTypeInfo(Element.fighting) },

                {"KnucklesPalladium", new ItemTypeInfo(Element.fighting) },
                {"KnucklesMithril", new ItemTypeInfo(Element.dragon) },
                {"KnucklesShotty", new ItemTypeInfo(Element.fighting) },
                {"KnucklesIchor", new ItemTypeInfo(Element.blood) },
                {"KnucklesFrost", new ItemTypeInfo(Element.ice) },
                {"KnucklesPlantera", new ItemTypeInfo(Element.poison) },
                {"KnucklesDuke", new ItemTypeInfo(Element.water) },

                {"LeatherWhip", new ItemTypeInfo(Element.normal) },
                {"Whiplash", new ItemTypeInfo(Element.blood) },
                {"NotchedWhip", new ItemTypeInfo(Element.dark) },
                {"BoneWhip", new ItemTypeInfo(Element.bone) },
                {"CoiledThorns", new ItemTypeInfo(Element.grass) },
                {"MoltenChains", new ItemTypeInfo(Element.fire) },
                {"EelWhip", new ItemTypeInfo(Element.grass) },
                {"PuzzlingCutter", new ItemTypeInfo(Element.fire) },
                {"CrystalVileLash", new ItemTypeInfo(Element.water) },

                {"Onsoku", new ItemTypeInfo(Element.fairy) },
                {"Hayauchi", new ItemTypeInfo(Element.fighting) },
                {"Raiden", new ItemTypeInfo(Element.grass) },
                {"BorealWoodSabre", new ItemTypeInfo(Element.ice) },
                {"EnchantedSabre", new ItemTypeInfo(Element.fighting) },
                {"JungleWoodSabre", new ItemTypeInfo(Element.grass) },
                {"PalmWoodSabre", new ItemTypeInfo(Element.water) },
                {"WoodenSabre", new ItemTypeInfo(Element.normal) },

                {"BoneZone", new ItemTypeInfo(Element.bone) },
                {"ChannelerStaff", new ItemTypeInfo(Element.fighting) },
                {"DemonBlaster", new ItemTypeInfo(Element.dark) },
                {"ImmaterialBlade", new ItemTypeInfo(Element.psychic) },
                {"ManaBlast", new ItemTypeInfo(Element.psychic) },
                {"PsyWave", new ItemTypeInfo(Element.ghost) },
                {"Reverb", new ItemTypeInfo(Element.psychic) },
                {"ScrapSalvo", new ItemTypeInfo(Element.normal) },
                {"StaffOfExplosion", new ItemTypeInfo(Element.fire) },
                {"Startillery", new ItemTypeInfo(Element.flying) },
                {"TrashCannon", new ItemTypeInfo(Element.normal) },
                {"WAR", new ItemTypeInfo(Element.fighting) },

                {"AllPorpoiseAssaultRifle", new ItemTypeInfo(Element.electric) },
                {"Capacitor", new ItemTypeInfo(Element.ice) },
                {"DoubleLoader", new ItemTypeInfo(Element.fighting) },
                {"ManaSword", new ItemTypeInfo(Element.electric) },
                {"SparkShovel", new ItemTypeInfo(Element.fire) },
                {"SteamPersuader", new ItemTypeInfo(Element.steel) },
            };
        }

        public static void Unload()
        {
            Type = null;
            _Type = null;
        }

        public static Dictionary<int, ItemTypeInfo> Type;
        public static Dictionary<string, ItemTypeInfo> _Type;
    }
}
