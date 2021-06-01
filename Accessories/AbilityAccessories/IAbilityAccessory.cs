using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.DataTypes;

namespace TerraTyping.Accessories.AbilityAccessories
{
    public interface IAbilityAccessory
    {
        AbilityID GivenAbility { get; }
    }
}
