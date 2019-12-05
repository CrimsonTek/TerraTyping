using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping
{
    public class Colors
    {
        public static Dictionary<Element, Tuple<int, int, int>> Type = new Dictionary<Element, Tuple<int, int, int>>
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
    }
}
