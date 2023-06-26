using System;
using TerraTyping.Core;

namespace TerraTyping.DataTypes
{
    public readonly struct AbilityContainer
    {
        public Ability[] BasicAbilities { get; }
        public Ability[] HiddenAbilities { get; }

        public static AbilityContainer None => new AbilityContainer();

        public AbilityContainer()
        {
            BasicAbilities = Array.Empty<Ability>();
            HiddenAbilities = Array.Empty<Ability>();
        }

        public AbilityContainer(Ability[] basicAbilities, Ability[] hiddenAbilities)
        {
            BasicAbilities = basicAbilities ?? Array.Empty<Ability>();
            HiddenAbilities = hiddenAbilities ?? Array.Empty<Ability>();
        }
    }
}
