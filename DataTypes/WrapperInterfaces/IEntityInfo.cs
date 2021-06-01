using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes
{
    public interface IEntityInfo
    {
        EntityType EntityType { get; }
        bool Boss { get; }
        int LifeMax { get; }
    }
}
