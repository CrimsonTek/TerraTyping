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
        public PowerupType PowerupType
        {
            get => (parameters) =>
            {
                if (parameters.element == Element.fire)
                {
                    if (parameters.user is NPCWrapper npcWrapper)
                    {
                        return new PowerupTypeReturn(AbilityData.flashFireDamageBoostNPC, "Flash Fire");
                    }
                    else if (parameters.user is PlayerWrapper playerWrapper)
                    {
                        return new PowerupTypeReturn(AbilityData.flashFireDamageBoostPlayer, "Flash Fire");
                    }
                }
                return new PowerupTypeReturn(1, string.Empty);
            };
            set { }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);

            NPCTyping npcTyping = npc.GetGlobalNPC<NPCTyping>();

            Element element = new NPCWrapper(npc).Offensive;
            if (element == Element.fire)
            {
                npcTyping.DamageMultiplyByBuff *= AbilityData.flashFireDamageBoostNPC;
            }

        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
        }
    }
}
