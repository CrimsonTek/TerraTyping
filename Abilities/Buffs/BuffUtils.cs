using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.Abilities.Buffs
{
    public static class BuffUtils
    {
        public static ModifyType ReplaceType(Element newType)
        {
            return (defType) =>
            {
                defType.typeSet.Primary = newType;
                defType.typeSet.Secondary = Element.none;
                return defType.typeSet;
            };
        }

        public static ModBuff ModBuffAddType(Element element) => ModBuffChangeType(element, true);
        public static ModBuff ModBuffReplaceType(Element element) => ModBuffChangeType(element, false);
        static ModBuff ModBuffChangeType(Element element, bool add)
        {
            string addOrReplace = add ? "Add" : "Replace";
            if (element != Element.none && element != Element.levitate)
            {
                return ModLoader.GetMod("TerraTyping").GetBuff(element.ToString().First().ToString().ToUpper() + string.Join("", element.ToString().Skip(1)) + addOrReplace);
            }
            return null;
        }
    }
}
