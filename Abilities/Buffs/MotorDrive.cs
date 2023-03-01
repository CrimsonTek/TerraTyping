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
    public class MotorDrive : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
        }

        public static void ModifySpeed(Player player)
        {
            player.runAcceleration *= AbilityData.motorDriveSpeedBoost;
        }
    }
}
