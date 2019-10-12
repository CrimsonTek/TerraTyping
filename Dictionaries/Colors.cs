using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terramon.Items
{
    public class Colors
    {
        public static Dictionary<Element.Type, Tuple<int, int, int>> Type = new Dictionary<Element.Type, Tuple<int, int, int>>
        {
            {Element.Type.normal, new Tuple<int, int, int>(240, 230, 210) },
            {Element.Type.fire, new Tuple<int, int, int>(246, 95, 28) },
            {Element.Type.water, new Tuple<int, int, int>(66, 166, 248) },
            {Element.Type.electric, new Tuple<int, int, int>(250, 250, 17) },
            {Element.Type.grass, new Tuple<int, int, int>(66, 235, 53) },
            {Element.Type.ice, new Tuple<int, int, int>(180, 225, 255) },
            {Element.Type.fighting, new Tuple<int, int, int>(245, 175, 10) },
            {Element.Type.poison, new Tuple<int, int, int>(184, 116, 252) },
            {Element.Type.ground, new Tuple<int, int, int>(186, 140, 78) },
            {Element.Type.flying, new Tuple<int, int, int>(151, 178, 255) },
            {Element.Type.psychic, new Tuple<int, int, int>(216, 10, 250) },
            {Element.Type.bug, new Tuple<int, int, int>(146, 216, 76) },
            {Element.Type.rock, new Tuple<int, int, int>(211, 185, 89) },
            {Element.Type.ghost, new Tuple<int, int, int>(194, 158, 214) },
            {Element.Type.dragon, new Tuple<int, int, int>(83, 18, 178) },
            {Element.Type.dark, new Tuple<int, int, int>(82, 69, 89) },
            {Element.Type.steel, new Tuple<int, int, int>(159, 159, 159) },
            {Element.Type.fairy, new Tuple<int, int, int>(244, 108, 218) },
            {Element.Type.blood, new Tuple<int, int, int>(214, 54, 54) },
            {Element.Type.bone, new Tuple<int, int, int>(242, 242, 242) },
        };
    }
}
