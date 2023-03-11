using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Data;
using TerraTyping.DataTypes;

namespace TerraTyping.Abilities.Buffs
{
    public class FlashFire : ModBuff, IPowerupType
    {
        public BuffPowerupType PowerupType
        {
            get => (parameters) =>
            {
                if (parameters.element == Element.fire)
                {
                    if (parameters.user is NPCWrapper npcWrapper)
                    {
                        return new BuffPowerupTypeReturn(AbilityData.flashFireDamageBoostNPC, "Flash Fire");
                    }
                    else if (parameters.user is PlayerWrapper playerWrapper)
                    {
                        return new BuffPowerupTypeReturn(AbilityData.flashFireDamageBoostPlayer, "Flash Fire");
                    }
                }
                return new BuffPowerupTypeReturn(1, string.Empty);
            };
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            NPCTyping npcTyping = npc.GetGlobalNPC<NPCTyping>();

            if (NPCWrapper.GetWrapper(npc).OffensiveElements.HasElement(Element.fire))
            {
                npcTyping.DamageMultiplyByBuff *= AbilityData.flashFireDamageBoostNPC;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.damageModifiedByAbilities[(int)Element.fire] *= AbilityData.flashFireDamageBoostPlayer;
            }
        }
    }
}
