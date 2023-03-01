using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes
{
    /// <summary>
    /// Used for wrapping Items, Projectiles, and other existing Terraria objects.
    /// This should only be used for existing objects, not structs.
    /// </summary>
    public interface IOffensiveType
    {
        ElementArray OffensiveElements { get; }
        void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement);
    }
}
