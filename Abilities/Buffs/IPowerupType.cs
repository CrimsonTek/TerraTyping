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
        BuffPowerupType PowerupType { get; }
    }

    public delegate BuffPowerupTypeReturn BuffPowerupType(BuffPowerupTypeParameters parameters);
    public struct BuffPowerupTypeParameters
    {
        public Element element;
        public Wrapper user;

        public BuffPowerupTypeParameters(Element element, Wrapper user)
        {
            this.element = element;
            this.user = user;
        }
    }
    public struct BuffPowerupTypeReturn
    {
        public Boost boost;

        public BuffPowerupTypeReturn(float multiply, string name)
        {
            boost = new Boost(multiply, name);
        }
    }
}
