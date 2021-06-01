using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TerraTyping.Attributes;

namespace TerraTyping
{
    [Load]
    [Unload]
    public static class Colors
    {
        public static void Load()
        {
            Type = new Dictionary<Element, Tuple<int, int, int>>
            {
                {Element.normal, new Tuple<int, int, int>(240, 230, 210) },
                {Element.fire, new Tuple<int, int, int>(246, 95, 28) },
                {Element.water, new Tuple<int, int, int>(66, 166, 248) },
                {Element.electric, new Tuple<int, int, int>(250, 250, 17) },
                {Element.grass, new Tuple<int, int, int>(66, 235, 53) },
                {Element.ice, new Tuple<int, int, int>(180, 225, 255) },
                {Element.fighting, new Tuple<int, int, int>(245, 175, 10) },
                {Element.poison, new Tuple<int, int, int>(184, 116, 252) },
                {Element.ground, new Tuple<int, int, int>(186, 140, 78) },
                {Element.flying, new Tuple<int, int, int>(151, 178, 255) },
                {Element.psychic, new Tuple<int, int, int>(216, 10, 250) },
                {Element.bug, new Tuple<int, int, int>(146, 216, 76) },
                {Element.rock, new Tuple<int, int, int>(211, 185, 89) },
                {Element.ghost, new Tuple<int, int, int>(194, 158, 214) },
                {Element.dragon, new Tuple<int, int, int>(83, 18, 178) },
                {Element.dark, new Tuple<int, int, int>(82, 69, 89) },
                {Element.steel, new Tuple<int, int, int>(159, 159, 159) },
                {Element.fairy, new Tuple<int, int, int>(244, 108, 218) },
                {Element.blood, new Tuple<int, int, int>(214, 54, 54) },
                {Element.bone, new Tuple<int, int, int>(242, 242, 242) },
                {Element.none, new Tuple<int, int, int>(255, 255, 255) },
                {Element.levitate, new Tuple<int, int, int>(255, 255, 255) }
            };

            type = new Dictionary<Element, Color>
            {
                {Element.normal, new Color(240, 230, 210) },
                {Element.fire, new Color(246, 95, 28) },
                {Element.water, new Color(66, 166, 248) },
                {Element.electric, new Color(250, 250, 17) },
                {Element.grass, new Color(66, 235, 53) },
                {Element.ice, new Color(180, 225, 255) },
                {Element.fighting, new Color(245, 175, 10) },
                {Element.poison, new Color(184, 116, 252) },
                {Element.ground, new Color(186, 140, 78) },
                {Element.flying, new Color(151, 178, 255) },
                {Element.psychic, new Color(216, 10, 250) },
                {Element.bug, new Color(146, 216, 76) },
                {Element.rock, new Color(211, 185, 89) },
                {Element.ghost, new Color(194, 158, 214) },
                {Element.dragon, new Color(83, 18, 178) },
                {Element.dark, new Color(82, 69, 89) },
                {Element.steel, new Color(159, 159, 159) },
                {Element.fairy, new Color(244, 108, 218) },
                {Element.blood, new Color(214, 54, 54) },
                {Element.bone, new Color(242, 242, 242) },
                {Element.none, new Color(255, 255, 255) },
                {Element.levitate, new Color(255, 255, 255) }
            };
        }

        public static void Unload()
        {
            Type = null;
            type = null;
        }

        public static Dictionary<Element, Tuple<int, int, int>> Type;

        public static Dictionary<Element, Color> type;
    }
}
