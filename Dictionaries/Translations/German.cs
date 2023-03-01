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
    public class German
    {
        public static void Load()
        {
            Name = new Dictionary<Element, string>
            {
                {Element.normal, "Normal" },
                {Element.fire, "Feuer" },
                {Element.water, "Wasser" },
                {Element.electric, "Elektro" },
                {Element.grass, "Pflanze" },
                {Element.ice, "Eis" },
                {Element.fighting, "Kampf" },
                {Element.poison, "Gift" },
                {Element.ground, "Boden" },
                {Element.flying, "Flug" },
                {Element.psychic, "Psycho" },
                {Element.bug, "Käfer" },
                {Element.rock, "Gestein" },
                {Element.ghost, "Geist" },
                {Element.dragon, "Drache" },
                {Element.dark, "Unlicht" },
                {Element.steel, "Stahl" },
                {Element.fairy, "Fee" },
                {Element.blood, "Blut" },
                {Element.bone, "Knochen" },
                {Element.none, "Keine" }
            };
        }

        public static void Unload()
        {
            Name = null;
        }

        public static Dictionary<Element, string> Name;
    }
}