using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
