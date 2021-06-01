using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class LangHelper
    {
        public static string ElementName(Element element)
        {
            GameCulture culture = Language.ActiveCulture;

            switch (culture.LegacyId)
            {
                case 2: return German.Name[element];
                case 4: return French.Name[element];
                case 6: return Russian.Name[element];
                default: return English.Name[element];
            }
        }

        public static string AbilityName(AbilityID ability)
        {
            return English.Ability[ability];
        }
    }
}
