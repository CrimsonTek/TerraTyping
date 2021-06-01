using Terraria;

namespace TerraTyping.DataTypes
{
    public struct ModifyTypeParameters
    {
        public delegate ThreeType ModifyTypeByEnvironment(ModifyTypeParameters parameters);

        public readonly ThreeType defaultTypes;
        public readonly NPC npc;

        public static ModifyTypeByEnvironment ModifyTypeByEnvironmentDefault => (parameters) => parameters.defaultTypes;

        public ModifyTypeParameters(ThreeType defaultTypes, NPC npc)
        {
            this.defaultTypes = defaultTypes;
            this.npc = npc;
        }

        public ModifyTypeParameters(NPCTypeInfo typeInfo, NPC npc)
        {
            this.defaultTypes = new ThreeType(typeInfo.Primary, typeInfo.Secondary, typeInfo.Offensive);
            this.npc = npc;
        }

    }
    public enum ElementIndex
    {
        Primary, Secondary, Offensive
    }
    public struct ThreeType
    {
        public Element Primary { get; set; }
        public Element Secondary { get; set; }
        public Element Offensive { get; set; }

        public ThreeType(Element primary, Element secondary, Element offensive)
        {
            Primary = primary;
            Secondary = secondary;
            Offensive = offensive;
        }
    }
}
