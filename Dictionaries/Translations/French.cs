using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Attributes;

namespace TerraTyping
{
    public static class French
    {
        public static string ElementName(Element element)
        {
            return element switch
            {
                Element.normal => "Normal",
                Element.fire => "Feu",
                Element.water => "Eau",
                Element.electric => "Électrique",
                Element.grass => "Plante",
                Element.ice => "Glace",
                Element.fighting => "Combat",
                Element.poison => "Poison",
                Element.ground => "Sol",
                Element.flying => "Vol",
                Element.psychic => "Psy",
                Element.bug => "Insecte",
                Element.rock => "Roche",
                Element.ghost => "Spectre",
                Element.dragon => "Dragon",
                Element.dark => "Ténèbres",
                Element.steel => "Acier",
                Element.fairy => "Fée",
                Element.blood => "Sang",
                Element.bone => "Os",
                Element.none => "Aucun",
                _ => throw new ArgumentException("Unexpected element: {element}.")
            };
        }
    }
}