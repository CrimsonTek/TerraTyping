using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class Mummy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            // DisplayName.SetDefault("Mummy");
            // Description.SetDefault("Your ability is now mummy");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.TryGetGlobalNPC(out NPCTyping npcTyping))
            {
                npcTyping.ModifiedAbility = Ability.Mummy;
                npcTyping.UseModifiedAbility = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.ModifiedAbility = Ability.Mummy;
                playerTyping.UseModifiedAbility = true;
            }
        }
    }
}
