using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.DataTypes
{
    public struct AbilityContainer
    {
        public AbilityID PrimaryAbility { get; }
        public AbilityID SecondaryAbility { get; }
        public AbilityID HiddenAbility { get; }

        public static AbilityContainer None => new AbilityContainer(AbilityID.None);

        //[Obsolete]
        ///// <summary>
        ///// Will create an Ability Container for an NPC with no secondary or hidden ability.
        ///// </summary>
        //public AbilityContainer(AbilityID primaryAbility)
        //{
        //    PrimaryAbility = primaryAbility;
        //    SecondaryAbility = AbilityID.None;
        //    HiddenAbility = AbilityID.None;
        //}

        /// <summary>
        /// Will create an Ability Container for an NPC with a primary, secondary, and hidden ability.
        /// </summary>
        public AbilityContainer(AbilityID primaryAbility, AbilityID secondaryAbility = AbilityID.None, AbilityID hiddenAbility = AbilityID.None)
        {
            PrimaryAbility = primaryAbility;
            SecondaryAbility = secondaryAbility;
            HiddenAbility = hiddenAbility;
        }
    }
}
