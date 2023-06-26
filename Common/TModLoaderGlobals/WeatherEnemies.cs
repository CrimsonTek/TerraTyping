using Terraria;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Core;
using TerraTyping.DataTypes;

namespace TerraTyping.Common.TModLoaderGlobals;

public class WeatherEnemies : GlobalNPC
{
    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
        ServerConfig config = ModContent.GetInstance<ServerConfig>();

        if (!Main.expertMode && config.WeatherMultOnlyExpert || config.WeatherMultForEnemies)
        {
            return;
        }

        Player closestPlayer = Main.player[npc.FindClosestPlayer()];
        if (!closestPlayer.ZoneOverworldHeight && !closestPlayer.ZoneSkyHeight && !closestPlayer.ZoneDirtLayerHeight)
        {
            return;
        }

        float weatherMultiplier = ModContent.GetInstance<ServerConfig>().WeatherMultiplier;
        if (weatherMultiplier == 1)
        {
            return;
        }

        NPCWrapper npcWrapper = NPCWrapper.GetWrapper(npc);
        ElementArray offensive = npcWrapper.OffensiveElements;

        for (int i = 0; i < offensive.Length; i++)
        {
            switch (offensive[i])
            {
                case Element.blood:
                    if (Main.bloodMoon)
                    {
                        modifiers.FinalDamage *= weatherMultiplier;
                    }
                    break;

                case Element.dark:
                    if (Main.eclipse)
                    {
                        modifiers.FinalDamage *= weatherMultiplier;
                    }
                    break;

                case Element.water:
                    if (closestPlayer.ZoneRain)
                    {
                        modifiers.FinalDamage *= weatherMultiplier;
                    }
                    break;

                case Element.fire:
                    if (closestPlayer.ZoneRain)
                    {
                        modifiers.FinalDamage /= weatherMultiplier;
                    }
                    break;

                case Element.ice:
                    if (Main.raining && closestPlayer.ZoneSnow)
                    {
                        modifiers.FinalDamage *= weatherMultiplier;
                    }
                    break;

                case Element.ground:
                case Element.rock:
                case Element.steel:
                    if (closestPlayer.ZoneSandstorm)
                    {
                        modifiers.FinalDamage *= weatherMultiplier;
                    }
                    break;
            }
        }
    }
}
