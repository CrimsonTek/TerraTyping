using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Common.TModLoaderGlobals;
using TerraTyping.DataTypes;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class FlashFire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flash Fire");
            // Description.SetDefault($"Fire damage boosted by {ServerConfig.Instance.AbilityConfigInstance.FlashFireDamageBoostPlayer:P2}");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            NPCTyping npcTyping = npc.GetGlobalNPC<NPCTyping>();

            if (NPCWrapper.GetWrapper(npc).OffensiveElements.HasElement(Element.fire))
            {
                npcTyping.damageMultiplyByBuff += ServerConfig.Instance.AbilityConfigInstance.FlashFireDamageBoostNPC - 1;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.boostsToEachTypeByAbilities[(int)Element.fire].Add(new Boost(ServerConfig.Instance.AbilityConfigInstance.FlashFireDamageBoostPlayer, "Flash Fire"));
            }
        }
    }
}
