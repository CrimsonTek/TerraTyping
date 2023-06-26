using System;
using TerraTyping.Core;

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

        public static string AbilityName(Ability abilityID)
        {
            return abilityID switch
            {
                Ability.None => "None",
                Ability.Levitate => "Levitate",
                Ability.FlashFire => "Flash Fire",
                Ability.VoltAbsorb => "Volt Absorb",
                Ability.WaterAbsorb => "Water Absorb",
                Ability.LightningRod => "Lightning Rod",
                Ability.StormDrain => "Storm Drain",
                Ability.MotorDrive => "Motor Drive",
                Ability.SapSipper => "Sap Sipper",
                Ability.ThickFat => "Thick Fat",
                Ability.Fluffy => "Fluffy",
                Ability.Heatproof => "Heatproof",
                Ability.WaterBubble => "Water Bubble",
                Ability.Justified => "Justified",
                Ability.WaterCompaction => "Water Compaction",
                Ability.SteamEngine => "Steam Engine",
                Ability.DrySkin => "Dry Skin",
                Ability.Mummy => "Mummy",
                Ability.Corrosion => "Corrosion",
                Ability.ColorChange => "Color Change",
                Ability.MoldBreaker => "Mold Breaker",
                Ability.SandForce => "Sand Force",
                Ability.Scrappy => "Scrappy",
                Ability.Flammable => "Flammable",
                Ability.Grounded => "Grounded",
                Ability.PrismArmor => "PrismArmor",
                Ability.DD2Stab => "Old One's STAB",
                _ => "Unknown Ability",
            };
        }
    }
}