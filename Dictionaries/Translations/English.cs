using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Attributes;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public static class English
    {
        public static void Load()
        {
            Name = new Dictionary<Element, string>
            {
                {Element.normal, "Normal" },
                {Element.fire, "Fire" },
                {Element.water, "Water" },
                {Element.electric, "Electric" },
                {Element.grass, "Grass" },
                {Element.ice, "Ice" },
                {Element.fighting, "Fighting" },
                {Element.poison, "Poison" },
                {Element.ground, "Ground" },
                {Element.flying, "Flying" },
                {Element.psychic, "Psychic" },
                {Element.bug, "Bug" },
                {Element.rock, "Rock" },
                {Element.ghost, "Ghost" },
                {Element.dragon, "Dragon" },
                {Element.dark, "Dark" },
                {Element.steel, "Steel" },
                {Element.fairy, "Fairy" },
                {Element.blood, "Blood" },
                {Element.bone, "Bone" },
                {Element.none, "None" },
                {Element.levitate, "Levitate" }
            };
            Ability = new Dictionary<AbilityID, string>
            {
                { AbilityID.None, "None" },
                { AbilityID.Levitate, "Levitate" },
                { AbilityID.FlashFire, "Flash Fire" },
                { AbilityID.VoltAbsorb, "Volt Absorb" },
                { AbilityID.WaterAbsorb, "Water Absorb" },
                { AbilityID.LightningRod, "Lightning Rod" },
                { AbilityID.StormDrain, "Storm Drain" },
                { AbilityID.MotorDrive, "Motor Drive" },
                { AbilityID.SapSipper, "Sap Sipper" },
                { AbilityID.ThickFat, "Thick Fat" },
                { AbilityID.Fluffy, "Fluffy" },
                { AbilityID.Heatproof, "Heatproof" },
                { AbilityID.WaterBubble, "Water Bubble" },
                { AbilityID.Justified, "Justified" },
                { AbilityID.WaterCompaction, "Water Compaction" },
                { AbilityID.SteamEngine, "Steam Engine" },
                { AbilityID.DrySkin, "Dry Skin" },
                { AbilityID.Mummy, "Mummy" },
                { AbilityID.Corrosion, "Corrosion" },
                { AbilityID.ColorChange, "Color Change" },
                { AbilityID.MoldBreaker, "Mold Breaker" },
                { AbilityID.SandForce, "Sand Force" },
                { AbilityID.Scrappy, "Scrappy" },
                { AbilityID.Flammable, "Flammable" }
            };
        }

        public static void Unload()
        {
            Name = null;
            Ability = null;
        }

        public static Dictionary<Element, string> Name;

        public static Dictionary<AbilityID, string> Ability;
    }
}