using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes
{
    public readonly struct AbilityContainer
    {
        public AbilityID[] BasicAbilities { get; }
        public AbilityID[] HiddenAbilities { get; }

        public static AbilityContainer None => new AbilityContainer();

        public AbilityContainer()
        {
            BasicAbilities = Array.Empty<AbilityID>();
            HiddenAbilities = Array.Empty<AbilityID>();
        }

        public AbilityContainer(AbilityID[] basicAbilities, AbilityID[] hiddenAbilities)
        {
            BasicAbilities = basicAbilities ?? Array.Empty<AbilityID>();
            HiddenAbilities = hiddenAbilities ?? Array.Empty<AbilityID>();
        }
    }
}
