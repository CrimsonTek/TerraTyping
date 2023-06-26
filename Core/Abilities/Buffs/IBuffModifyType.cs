using TerraTyping.DataTypes;

namespace TerraTyping.Core.Abilities.Buffs
{
    /// <summary>
    /// Used for Color Change, and theoretically other buffs
    /// </summary>
    public interface IOldBuffModifyType
    {
        OldModifyType ModifyType { get; }
    }

    /// <summary>
    /// Provides an interface for modifying the type of an entity.
    /// </summary>
    public interface IBuffModifyType
    {
        ModifyType ModifyType { get; }
    }
    public delegate void ModifyType(ITarget target, ref ElementArray elements);

    public interface IBuffModifyActiveAbility
    {
        ModifyAbility ModifyAbility { get; }
    }
    public delegate void ModifyAbility(ITarget target, ref Ability abilityID);

    public delegate ModifyTypeReturn OldModifyType(ModifyTypeParameters modifyTypeParameters);
    public struct ModifyTypeParameters
    {
        public ElementArray elements;
        public Ability abilityID;

        public ITarget targetWrapper;

        public ModifyTypeParameters(ElementArray elements, Ability abilityID, ITarget targetWrapper)
        {
            this.elements = elements;
            this.abilityID = abilityID;
            this.targetWrapper = targetWrapper;
        }
    }
    public struct ModifyTypeReturn
    {
        public ElementArray NewElements { get; private set; }
        public Ability NewAbility { get; private set; }

        public ModifyTypeReturn(ModifyTypeParameters modifyTypeParameters)
        {
            NewElements = modifyTypeParameters.elements;
            NewAbility = modifyTypeParameters.abilityID;
        }

        public ModifyTypeReturn ReplaceTypes(ElementArray replacementElements)
        {
            NewElements = replacementElements;
            return this;
        }

        public ModifyTypeReturn ReplaceAbility(Ability replacementAbility)
        {
            NewAbility = replacementAbility;
            return this;
        }
    }
}
