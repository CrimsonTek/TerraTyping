using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.Abilities.Buffs
{
    public class Mummy : ModBuff, IBuffModifyType
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
        }

        public ModifyType ModifyType => (parameters) =>
        {
            parameters.typeSet.GetAbility = AbilityID.Mummy;
            return parameters.typeSet;
        };
    }
}
