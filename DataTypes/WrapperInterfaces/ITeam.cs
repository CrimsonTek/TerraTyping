using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
