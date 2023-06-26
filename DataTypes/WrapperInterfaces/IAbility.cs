using TerraTyping.Core;

namespace TerraTyping.DataTypes
{
    /// <summary>
    /// Used for wrapping the Player and NPCs.
    /// This should only be used for existing objects, not wrappers.
    /// </summary>
    public interface IAbility
    {
        Ability GetAbility { get; }
    }
}
