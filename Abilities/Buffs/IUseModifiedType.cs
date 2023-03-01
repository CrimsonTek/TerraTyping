using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.DataTypes;

namespace TerraTyping.Abilities.Buffs
{
    public interface IUseModifiedType
    {
        Element MyElement { get; }
        ElementArray Elements { get; }
    }
}
