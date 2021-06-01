using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.Abilities;

namespace TerraTyping.DataTypes
{
    /// <summary>
    /// Used for wrapping the Player and NPCs.
    /// This should only be used for existing objects, not wrappers.
    /// </summary>
    public interface IAbility
    {
        AbilityID GetAbility { get; }
    }
}
