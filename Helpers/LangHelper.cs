using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class LangHelper
    {
        [Obsolete]
        public static string ElementName(Element element)
        {
            GameCulture culture = Language.ActiveCulture;
            int langID;
            if (culture is not null)
            {
                langID = culture.LegacyId;
            }
            else
            {
                langID = LangID.English;
            }

            switch (langID)
            {
                case LangID.German: return German.Name[element];
                case LangID.French: return French.Name[element];
                case 6: return Russian.Name[element];
                case LangID.English:
                default: return English.Name[element];
            }
        }

        public static string MultipleElements(ElementArray elements, bool useAndBeforeLast = true)
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
                    stringBuilder.Append(ElementName(elements[i]));
                }
                else if ((i < elements.Length - 1) && useAndBeforeLast)
                {
                    stringBuilder.Append($", {ElementName(elements[i])}");
                }
                else // last
                {
                    stringBuilder.Append($", and {ElementName(elements[i])}");
                }
            }
            return stringBuilder.ToString();
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

        public static string AbilityName(AbilityID ability)
        {
            return English.Ability[ability];
        }
    }
}
