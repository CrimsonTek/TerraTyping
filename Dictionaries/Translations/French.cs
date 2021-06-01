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
    public static class French
    {
        public static void Load()
        {
            Name = new Dictionary<Element, string>
            {
                {Element.normal, "Normal" },
                {Element.fire, "Feu" },
                {Element.water, "Eau" },
                {Element.electric, "Électrique" },
                {Element.grass, "Plante" },
                {Element.ice, "Glace" },
                {Element.fighting, "Combat" },
                {Element.poison, "Poison" },
                {Element.ground, "Sol" },
                {Element.flying, "Vol" },
                {Element.psychic, "Psy" },
                {Element.bug, "Insecte" },
                {Element.rock, "Roche" },
                {Element.ghost, "Spectre" },
                {Element.dragon, "Dragon" },
                {Element.dark, "Ténèbres" },
                {Element.steel, "Acier" },
                {Element.fairy, "Fée" },
                {Element.blood, "Sang" },
                {Element.bone, "Os" },
                {Element.none, "Aucun" },
                {Element.levitate, "Lévitation" }
            };
        }

        public static void Unload()
        {
            Name = null;
        }

        public static Dictionary<Element, string> Name;
    }
}