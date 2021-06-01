using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TerraTyping.DataTypes
{
    public struct TypeSet : IPrimaryType, ISecondaryType, IAbility
    {
        public Element Primary { get; set; }
        public Element Secondary { get; set; }
        public AbilityID GetAbility { get; set; }

        public static TypeSet PlayerDefault => new TypeSet()
        {
            Primary = Element.normal,
            Secondary = Element.none,
            GetAbility = AbilityID.None
        };

        public TypeSet(Element primary, Element secondary, AbilityID abilityID = AbilityID.None)
        {
            Primary = primary;
            Secondary = secondary;
            GetAbility = abilityID;
        }

        public TypeSet(ArmorTypeInfo armorTypeInfo)
        {
            Primary = armorTypeInfo.Primary;
            Secondary = armorTypeInfo.Secondary;
            GetAbility = armorTypeInfo.AbilityID;
        }

        public override string ToString()
        {
            return $"TypeSet: {{{Primary}, {Secondary}, {GetAbility}}}";
        }
    }
}
