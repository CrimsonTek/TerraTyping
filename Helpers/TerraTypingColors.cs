using Microsoft.Xna.Framework;
using TerraTyping.Core;

namespace TerraTyping.Helpers;

public static class TerraTypingColors
{
    public static Color GetColor(Element element)
    {
        return element switch
        {
            Element.normal => new Color(240, 230, 210),
            Element.fire => new Color(246, 95, 28),
            Element.water => new Color(66, 166, 248),
            Element.electric => new Color(250, 250, 17),
            Element.grass => new Color(66, 235, 53),
            Element.ice => new Color(180, 225, 255),
            Element.fighting => new Color(245, 175, 10),
            Element.poison => new Color(184, 116, 252),
            Element.ground => new Color(186, 140, 78),
            Element.flying => new Color(151, 178, 255),
            Element.psychic => new Color(216, 10, 250),
            Element.bug => new Color(146, 216, 76),
            Element.rock => new Color(211, 185, 89),
            Element.ghost => new Color(194, 158, 214),
            Element.dragon => new Color(83, 18, 178),
            Element.dark => new Color(82, 69, 89),
            Element.steel => new Color(159, 159, 159),
            Element.fairy => new Color(244, 108, 218),
            Element.blood => new Color(214, 54, 54),
            Element.bone => new Color(242, 242, 242),
            Element.none or _ => new Color(255, 255, 255),
        };
    }
}
