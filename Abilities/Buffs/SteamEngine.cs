using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Data;

namespace TerraTyping.Abilities.Buffs
{
    public class SteamEngine : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.accRunSpeed *= AbilityData.steamEngineSpeedBoost;
        }
    }
}
