using TerraTyping.Core;

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
