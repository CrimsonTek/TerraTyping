using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public static class English
    {
        public static string ElementName(Element element)
        {
            return element switch
            {
                Element.normal => "Normal",
                Element.fire => "Fire",
                Element.water => "Water",
                Element.electric => "Electric",
                Element.grass => "Grass",
                Element.ice => "Ice",
                Element.fighting => "Fighting",
                Element.poison => "Poison",
                Element.ground => "Ground",
                Element.flying => "Flying",
                Element.psychic => "Psychic",
                Element.bug => "Bug",
                Element.rock => "Rock",
                Element.ghost => "Ghost",
                Element.dragon => "Dragon",
                Element.dark => "Dark",
                Element.steel => "Steel",
                Element.fairy => "Fairy",
                Element.blood => "Blood",
                Element.bone => "Bone",
                Element.none => "None",
                _ => throw new ArgumentException($"Unexpected element: {element}.")
            };
        }

        public static string AbilityName(AbilityID abilityID)
        {
            return abilityID switch
            {
                AbilityID.None => "None",
                AbilityID.Levitate => "Levitate",
                AbilityID.FlashFire => "Flash Fire",
                AbilityID.VoltAbsorb => "Volt Absorb",
                AbilityID.WaterAbsorb => "Water Absorb",
                AbilityID.LightningRod => "Lightning Rod",
                AbilityID.StormDrain => "Storm Drain",
                AbilityID.MotorDrive => "Motor Drive",
                AbilityID.SapSipper => "Sap Sipper",
                AbilityID.ThickFat => "Thick Fat",
                AbilityID.Fluffy => "Fluffy",
                AbilityID.Heatproof => "Heatproof",
                AbilityID.WaterBubble => "Water Bubble",
                AbilityID.Justified => "Justified",
                AbilityID.WaterCompaction => "Water Compaction",
                AbilityID.SteamEngine => "Steam Engine",
                AbilityID.DrySkin => "Dry Skin",
                AbilityID.Mummy => "Mummy",
                AbilityID.Corrosion => "Corrosion",
                AbilityID.ColorChange => "Color Change",
                AbilityID.MoldBreaker => "Mold Breaker",
                AbilityID.SandForce => "Sand Force",
                AbilityID.Scrappy => "Scrappy",
                AbilityID.Flammable => "Flammable",
                AbilityID.Grounded => "Grounded",
                AbilityID.PrismArmor => "PrismArmor",
                AbilityID.DD2Stab => "Old One's STAB",
                _ => "Unknown Ability",
            };
        }
    }
}