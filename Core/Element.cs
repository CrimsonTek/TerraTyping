using System;
using System.Linq;

namespace TerraTyping.Core
{
    public enum Element : byte
    {
        // if any Elements are added <0 or >=32, adjust the ElementArray hash algorithm accordingly.

        normal = 0,
        fire = 1,
        water = 2,
        electric = 3,
        grass = 4,
        ice = 5,
        fighting = 6,
        poison = 7,
        ground = 8,
        flying = 9,
        psychic = 10,
        bug = 11,
        rock = 12,
        ghost = 13,
        dragon = 14,
        dark = 15,
        steel = 16,
        fairy = 17,
        blood = 18,
        bone = 19,
        none = 20,
    }

    public static class ElementUtils
    {
        public static Element[] GetAll(bool includeNone)
        {
            Element[] elements = Enum.GetValues<Element>();
            if (includeNone)
            {
                return elements;
            }
            else
            {
                return elements.Take(0..^1).ToArray();
            }
        }

        public static Element[] GetAllReal()
        {
            return Enum.GetValues<Element>().Take(0..^1).ToArray();
        }
    }
}
