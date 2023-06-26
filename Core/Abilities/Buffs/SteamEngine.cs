using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class SteamEngine : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steam Engine");
            // Description.SetDefault($"Acceleration boosted by {ServerConfig.Instance.AbilityConfigInstance.SteamEngineSpeedBoostPlayer}");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.runAcceleration *= ServerConfig.Instance.AbilityConfigInstance.SteamEngineSpeedBoostPlayer;
        }
    }
}
