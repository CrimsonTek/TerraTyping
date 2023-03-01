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
    public delegate void ModifyAbility(ITarget target, ref AbilityID abilityID);

    public delegate ModifyTypeReturn OldModifyType(ModifyTypeParameters modifyTypeParameters);
    public struct ModifyTypeParameters
    {
        public ElementArray elements;
        public AbilityID abilityID;

        public ITarget targetWrapper;

        public ModifyTypeParameters(ElementArray elements, AbilityID abilityID, ITarget targetWrapper)
        {
            this.elements = elements;
            this.abilityID = abilityID;
            this.targetWrapper = targetWrapper;
        }
    }
    public struct ModifyTypeReturn
    {
        public ElementArray NewElements { get; private set; }
        public AbilityID NewAbility { get; private set; }

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

        public ModifyTypeReturn ReplaceAbility(AbilityID replacementAbility)
        {
            NewAbility = replacementAbility;
            return this;
        }
    }
}
