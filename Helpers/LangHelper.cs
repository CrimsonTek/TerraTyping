using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;

namespace TerraTyping
{
    public class LangHelper
    {
        public static LangHelper langHelper = new LangHelper();

        public string ElementName(Element element)
        {
            GameCulture culture = Language.ActiveCulture;

            switch (culture.LegacyId)
            {
                case 6: return Russian.Name[element];
                default: return English.Name[element];
            }
        }
    }
}
