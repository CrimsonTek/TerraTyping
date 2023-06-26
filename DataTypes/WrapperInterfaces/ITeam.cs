namespace TerraTyping.DataTypes
{
    public interface ITeam
    {
        Team GetTeam();
    }

    public enum Team
    {
        PlayerFriendly,
        EnemyNPC,
        Unknown
    }
}
