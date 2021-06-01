using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Attributes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public static class Formal
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
        }

        public static void Unload()
        {
            Name = null;
        }

        public static Dictionary<Element, string> Name;
    }
}