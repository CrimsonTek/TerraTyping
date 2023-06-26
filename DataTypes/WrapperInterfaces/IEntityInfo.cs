namespace TerraTyping.DataTypes
{
    public interface IEntityInfo
    {
        EntityType EntityType { get; }
        bool Boss { get; }
        int LifeMax { get; }
    }
}
