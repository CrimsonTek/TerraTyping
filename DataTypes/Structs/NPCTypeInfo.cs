using System;
using static TerraTyping.DataTypes.ModifyTypeParameters;

namespace TerraTyping.DataTypes
{
    public struct NPCTypeInfo
    {
        public Element Primary { get; set; }
        public Element Secondary { get; set; }
        public Element Offensive { get; set; }
        public AbilityContainer Container { get; set; }
        public ModifyTypeByEnvironment ModifyType { get; set; }

        public NPCTypeInfo(Element primary, Element secondary, Element offensive)
        {
            Primary = primary;
            Secondary = secondary;
            Offensive = offensive;
            Container = AbilityContainer.None;
            ModifyType = ModifyTypeByEnvironmentDefault;
        }

        public NPCTypeInfo(Element primary, Element secondary, Element offensive, AbilityContainer abilityContainer)
        {
            Primary = primary;
            Secondary = secondary;
            Offensive = offensive;
            Container = abilityContainer;
            ModifyType = ModifyTypeByEnvironmentDefault;
        }

        public NPCTypeInfo(Element primary, Element secondary, AbilityID ability, Element offensive)
        {
            Primary = primary;
            Secondary = secondary;
            Offensive = offensive;
            Container = new AbilityContainer(ability);
            ModifyType = ModifyTypeByEnvironmentDefault;
        }

        public NPCTypeInfo(Element primary, Element secondary, Element offensive, AbilityID ability)
        {
            Primary = primary;
            Secondary = secondary;
            Offensive = offensive;
            Container = new AbilityContainer(ability);
            ModifyType = ModifyTypeByEnvironmentDefault;
        }

        public NPCTypeInfo(Element primary, Element secondary, Element offensive, AbilityContainer abilityContainer, ModifyTypeByEnvironment modifyType)
        {
            Primary = primary;
            Secondary = secondary;
            Offensive = offensive;
            Container = abilityContainer;
            ModifyType = modifyType;
        }

    }
}
