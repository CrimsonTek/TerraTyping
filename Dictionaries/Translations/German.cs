using System;
using TerraTyping.Core;

namespace TerraTyping
{
    public class German
    {
        public static string ElementName(Element element)
        {
            return element switch
            {
                Element.normal => "Normal",
                Element.fire => "Feuer",
                Element.water => "Wasser",
                Element.electric => "Elektro",
                Element.grass => "Pflanze",
                Element.ice => "Eis",
                Element.fighting => "Kampf",
                Element.poison => "Gift",
                Element.ground => "Boden",
                Element.flying => "Flug",
                Element.psychic => "Psycho",
                Element.bug => "Käfer",
                Element.rock => "Gestein",
                Element.ghost => "Geist",
                Element.dragon => "Drache",
                Element.dark => "Unlicht",
                Element.steel => "Stahl",
                Element.fairy => "Fee",
                Element.blood => "Blut",
                Element.bone => "Knochen",
                Element.none => "Keine",
                _ => throw new ArgumentException("Unexpected element: {element}.")
            };
        }
    }
}