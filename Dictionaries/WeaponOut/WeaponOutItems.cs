using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraTyping
{
    public class WeaponOutItems
    {
        public static Dictionary<int, Element> Type = new Dictionary<int, Element>() { };
        public static Dictionary<string, Element> _Type = new Dictionary<string, Element>()
        {
            {"FistsBoxing", Element.fighting},
            {"FistsGranite", Element.ground},
            {"FistsSlime", Element.water},
            {"FistsOfFury", Element.rock},
            {"FistsJungleClaws", Element.grass},
            {"FistsBone", Element.bone},
            {"FistsMolten", Element.fire},

            {"GlovesWooden", Element.fighting},
            {"GlovesPalm", Element.fighting},
            {"GlovesCaestus", Element.fighting},
            {"GlovesCaestusCrimson", Element.blood},
            {"GlovesObsidian", Element.rock},
            {"GlovesFossil", Element.bone},
            {"GlovesBee", Element.bug},

            {"KnucklesIron", Element.steel},
            {"KnucklesLead", Element.steel},
            {"KnucklesGold", Element.steel},
            {"KnucklesPlat", Element.steel},
            {"KnucklesFlintlock", Element.fighting},
            {"KnucklesMeteor", Element.rock},
            {"KnucklesDungeon", Element.dark},
            {"KnucklesDemon", Element.dark},

            {"FistsSparring", Element.fairy},
            {"FistsAdamant", Element.dragon},
            {"FistsTitanium", Element.steel},
            {"FistsCursed", Element.ghost},
            {"FistsForbidden", Element.ground},
            {"FistsLihzarhd", Element.fighting},
            {"FistsFrozen", Element.ice},
            {"FistsBetsy", Element.dragon},
            {"FistsMartian", Element.electric},

            {"GlovesCobalt", Element.steel},
            {"GlovesOrich", Element.fairy},
            {"GlovesCrystal", Element.fairy},
            {"GlovesHallow", Element.fighting},
            {"GlovesButterfly", Element.bug},
            {"GlovesLee", Element.fighting},
            {"GlovesPumpkin", Element.fighting},

            {"KnucklesPalladium", Element.fighting},
            {"KnucklesMithril", Element.dragon},
            {"KnucklesShotty", Element.fighting},
            {"KnucklesIchor", Element.blood},
            {"KnucklesFrost", Element.ice},
            {"KnucklesPlantera", Element.poison},
            {"KnucklesDuke", Element.water},

            {"LeatherWhip", Element.normal},
            {"Whiplash", Element.blood},
            {"NotchedWhip", Element.dark},
            {"BoneWhip", Element.bone},
            {"CoiledThorns", Element.grass},
            {"MoltenChains", Element.fire},
            {"EelWhip", Element.grass},
            {"PuzzlingCutter", Element.fire},
            {"CrystalVileLash", Element.water},

            {"Onsoku", Element.fairy},
            {"Hayauchi", Element.fighting},
            {"Raiden", Element.grass},
            {"BorealWoodSabre", Element.ice},
            {"EnchantedSabre", Element.fighting},
            {"JungleWoodSabre", Element.grass},
            {"PalmWoodSabre", Element.water},
            {"WoodenSabre", Element.normal},

            {"BoneZone", Element.bone},
            {"ChannelerStaff", Element.fighting},
            {"DemonBlaster", Element.dark},
            {"ImmaterialBlade", Element.psychic},
            {"ManaBlast", Element.psychic},
            {"PsyWave", Element.ghost},
            {"Reverb", Element.psychic},
            {"ScrapSalvo", Element.normal},
            {"StaffOfExplosion", Element.fire},
            {"Startillery", Element.flying},
            {"TrashCannon", Element.normal},
            {"WAR", Element.fighting},

            {"AllPorpoiseAssaultRifle", Element.electric},
            {"Capacitor", Element.ice},
            {"DoubleLoader", Element.fighting},
            {"ManaSword", Element.electric},
            {"SparkShovel", Element.fire},
            {"SteamPersuader", Element.steel},
        };
    }
}
