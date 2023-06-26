using System;
using System.Text;
using Terraria.ID;
using Terraria.Localization;
using TerraTyping.Core;

namespace TerraTyping.Helpers;

public static class LangHelper
{
    public static string ElementName(Element element, bool upperFirst)
    {
        string value = Language.GetText($"Mods.TerraTyping.Type.{element}").Value;
        if (upperFirst)
        {
            return $"{char.ToUpper(value[0])}{value[1..]}";
        }
        else
        {
            return value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="elements"></param>
    /// <param name="upperFirstEach">Each element will have its first letter capitalized if allowed.</param>
    /// <param name="useAndBeforeLast"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string MultipleElements(ElementArray elements, bool upperFirstEach, bool useAndBeforeLast = true)
    {
        if (elements is null)
        {
            throw new ArgumentNullException(nameof(elements));
        }

        StringBuilder stringBuilder = new StringBuilder();
        bool first = true;
        for (int i = 0; i < elements.Length; i++)
        {
            if (first)
            {
                stringBuilder.Append(ElementName(elements[i], upperFirstEach));
            }
            else if (i < elements.Length - 1 || !useAndBeforeLast)
            {
                stringBuilder.Append($", {ElementName(elements[i], upperFirstEach)}");
            }
            else // last
            {
                stringBuilder.Append($", and {ElementName(elements[i], upperFirstEach)}");
            }
            first = false;
        }
        return stringBuilder.ToString();
    }

    public static string AbilityName(Ability ability, bool upperFirst)
    {
        string value = Language.GetText($"Mods.TerraTyping.Ability.{ability}").Value;
        if (upperFirst)
        {
            return $"{char.ToUpper(value[0])}{value[1..]}";
        }
        else
        {
            return value;
        }
    }

    public static string InternalElementName(Element element, bool upperFirstLetter = false)
    {
        if (upperFirstLetter)
        {
            return element switch
            {
                Element.normal => "Normal",
                Element.fire => "Fire",
                Element.water => "Water",
                Element.electric => "Electric",
                Element.grass => "Grass",
                Element.ice => "Ice",
                Element.fighting => "Fighting",
                Element.poison => "Poison",
                Element.ground => "Ground",
                Element.flying => "Flying",
                Element.psychic => "Psychic",
                Element.bug => "Bug",
                Element.rock => "Rock",
                Element.ghost => "Ghost",
                Element.dragon => "Dragon",
                Element.dark => "Dark",
                Element.steel => "Steel",
                Element.fairy => "Fairy",
                Element.blood => "Blood",
                Element.bone => "Bone",
                Element.none or _ => "None",
            };
        }
        else
        {
            return element switch
            {
                Element.normal => "normal",
                Element.fire => "fire",
                Element.water => "water",
                Element.electric => "electric",
                Element.grass => "grass",
                Element.ice => "ice",
                Element.fighting => "fighting",
                Element.poison => "poison",
                Element.ground => "ground",
                Element.flying => "flying",
                Element.psychic => "psychic",
                Element.bug => "bug",
                Element.rock => "rock",
                Element.ghost => "ghost",
                Element.dragon => "dragon",
                Element.dark => "dark",
                Element.steel => "steel",
                Element.fairy => "fairy",
                Element.blood => "blood",
                Element.bone => "bone",
                Element.none or _ => "none",
            };
        }
    }
}
