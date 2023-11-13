using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class MotorDrive : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Motor Drive");
            // Description.SetDefault($"Acceleration boosted by {ServerConfig.Instance.AbilityConfigInstance.MotorDriveSpeedBoostPlayer}");
        }

        public static void ModifySpeed(Player player)
        {
            player.runAcceleration *= ServerConfig.Instance.AbilityConfigInstance.MotorDriveSpeedBoostPlayer;
        }
    }
}
