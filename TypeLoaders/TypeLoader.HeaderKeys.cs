namespace TerraTyping.TypeLoaders;

public abstract partial class TypeLoader
{
    protected static class HeaderKeys
    {
        public const string InternalName = "internalname|id|internalid";
        public const string GenericElement = "type";
        public const string SpecialTooltip = "tooltipoverride|specialtooltip|tooltip";
        public const string BasicAbility = "ability|basicability|abilitybasic";
        public const string HiddenAbility = "hiddenability|abilityhidden";
        public const string DefensiveElement = "deftype";
        public const string OffensiveElement = "offtype";
        public const string ModifyType = "modifytype";
        public const string ModifyEffectiveness = "specialeffectiveness|modifyeffectiveness";
    }
}
