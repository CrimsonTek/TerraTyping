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
    public class BuffUtils
    {
        internal static ModBuff[] addTypeBuffs;
        internal static ModBuff[] replaceTypeBuffs;

        public static OldModifyType ReplaceType(Element newType) => (defType) => new ModifyTypeReturn(defType).ReplaceTypes(ElementArray.Get(newType));

        [Obsolete]
        /// <summary>
        /// Dont include <see cref="Element.none"/>
        /// </summary>
        public static ModBuff ModBuffAddType(Element element) => ModBuffChangeType(element, true);
        [Obsolete]
        /// <summary>
        /// Dont include <see cref="Element.none"/>
        /// </summary>
        public static ModBuff ModBuffReplaceType(Element element) => ModBuffChangeType(element, false);
        static ModBuff ModBuffChangeType(ElementArray element, bool add)
        {
            throw new NotImplementedException();
        }
        [Obsolete]
        static ModBuff ModBuffChangeType(Element element, bool add)
        {
            //string addOrReplace = add ? "Add" : "Replace";
            if (element != Element.none)
            {
                //string str = element.ToString().First().ToString().ToUpper() + string.Join("", element.ToString().Skip(1)) + addOrReplace;
                //Mod terraTyping = ModLoader.GetMod("TerraTyping");

                if (add)
                {
                    return addTypeBuffs[(int)element];
                }
                else
                {
                    return replaceTypeBuffs[(int)element];
                }
            }
            return null;
        }

        /// <summary>
        /// Whether or not a given buff indicates that an entity should use their assigned modified defensive types.
        /// </summary>
        /// <returns></returns>
        public static bool UseModifiedDefensiveTypes(ModBuff modBuff)
        {
            return modBuff is ColorChange;
        }

        /// <summary>
        /// Whether or not a given buff indicates that an entity should use their assigned modified defensive types.
        /// </summary>
        /// <returns></returns>
        public static bool UseModifiedDefensiveTypes(int buffType)
        {
            return buffType == ModContent.BuffType<ColorChange>();
        }

        /// <summary>
        /// Whether or not a given buff indicates that an entity should use their assigned modified ability. 
        /// </summary>
        /// <param name="modBuff"></param>
        /// <returns></returns>
        public static bool UseModifiedAbility(ModBuff modBuff)
        {
            return modBuff is Mummy;
        }

        /// <summary>
        /// Whether or not a given buff indicates that an entity should use their assigned modified ability. 
        /// </summary>
        /// <param name="buffType"></param>
        /// <returns></returns>
        public static bool UseModifiedAbility(int buffType)
        {
            return buffType == ModContent.BuffType<Mummy>();
        }
    }
}
