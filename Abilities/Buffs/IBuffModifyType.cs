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
    public interface IBuffModifyType
    {
        ModifyType ModifyType { get; }
    }

    public delegate TypeSet ModifyType(ModifyTypeParameters modifyTypeParameters);
    public struct ModifyTypeParameters
    {
        public TypeSet typeSet;
        public ITarget targetWrapper;

        public ModifyTypeParameters(TypeSet typeSet, ITarget targetWrapper)
        {
            this.typeSet = typeSet;
            this.targetWrapper = targetWrapper;
        }
    }
}
