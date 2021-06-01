using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TerraTyping.DataTypes
{
    public interface IHitbox
    {
        Rectangle Hitbox { get; }
    }
}
