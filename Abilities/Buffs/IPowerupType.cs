using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraTyping.DataTypes;
using TerraTyping.DataTypes.NewInterfaces;

namespace TerraTyping.Abilities.Buffs
{
    public interface IPowerupType
    {
        PowerupType PowerupType { get; set; }
    }

    public delegate PowerupTypeReturn PowerupType(PowerupTypeParameters parameters);
    public struct PowerupTypeParameters
    {
        public Element element;
        public Wrapper user;

        public PowerupTypeParameters(Element element, Wrapper user)
        {
            this.element = element;
            this.user = user;
        }
    }
    public struct PowerupTypeReturn
    {
        public Boost boost;

        public PowerupTypeReturn(float multiply, string name)
        {
            boost = new Boost(multiply, name);
        }
    }
}
