using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Data;

namespace TerraTyping.Abilities.Buffs
{
    public class SteamEngine : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steam Engine");
            Description.SetDefault($"Acceleration boosted by {ServerConfig.Instance.AbilityConfigInstance.SteamEngineSpeedBoostPlayer}");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.runAcceleration *= ServerConfig.Instance.AbilityConfigInstance.SteamEngineSpeedBoostPlayer;
        }
    }
}
