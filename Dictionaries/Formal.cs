using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terramon.Items
{
    public class Formal
    {
        public static Dictionary<Element.Type, string> Name = new Dictionary<Element.Type, string>
        {
            {Element.Type.normal, "Normal" },
            {Element.Type.fire, "Fire" },
            {Element.Type.water, "Water" },
            {Element.Type.electric, "Electric" },
            {Element.Type.grass, "Grass" },
            {Element.Type.ice, "Ice" },
            {Element.Type.fighting, "Fighting" },
            {Element.Type.poison, "Poison" },
            {Element.Type.ground, "Ground" },
            {Element.Type.flying, "Flying" },
            {Element.Type.psychic, "Psychic" },
            {Element.Type.bug, "Bug" },
            {Element.Type.rock, "Rock" },
            {Element.Type.ghost, "Ghost" },
            {Element.Type.dragon, "Dragon" },
            {Element.Type.dark, "Dark" },
            {Element.Type.steel, "Steel" },
            {Element.Type.fairy, "Fairy" },
            {Element.Type.blood, "Blood" },
            {Element.Type.bone, "Bone" }
        };
    }
}