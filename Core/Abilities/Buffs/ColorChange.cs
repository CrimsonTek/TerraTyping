using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.TModLoaderGlobals;

namespace TerraTyping.Core.Abilities.Buffs
{
    public class ColorChange : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            // DisplayName.SetDefault("Color Change");
            // Description.SetDefault("Your type is different.");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.TryGetGlobalNPC(out NPCTyping npcTyping))
            {
                npcTyping.UseModifiedElements = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out PlayerTyping playerTyping))
            {
                playerTyping.UseModifiedElements = true;
            }
        }
    }
}
