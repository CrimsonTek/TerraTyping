using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes
{
    /// <summary>
    /// Used for wrapping Armor, NPCs, and other existing Terraria objects.
    /// This should only be used for existing objects, not wrappers.
    /// </summary>
    public interface IDefensiveElements
    {
        ElementArray DefensiveElements { get; }
    }
}
