using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes.Structs
{
    public struct ProjectileTypeInfo
    {
        public Element Offensive { get; set; }

        public ProjectileTypeInfo(Element element)
        {
            Offensive = element;
        }
    }
}
