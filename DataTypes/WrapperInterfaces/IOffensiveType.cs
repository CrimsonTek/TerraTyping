using TerraTyping.Core;

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
