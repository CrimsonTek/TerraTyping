namespace TerraTyping.DataTypes
{
    public interface IDamageClass
    {
        bool Melee { get; }
        bool Ranged { get; }
        bool Magic { get; }
        bool Summon { get; }
    }
}
