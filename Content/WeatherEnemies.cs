using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.DataTypes;

namespace TerraTyping;

public class WeatherEnemies : GlobalNPC
{
    public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
    {
        ServerConfig config = ModContent.GetInstance<ServerConfig>();

        if ((!Main.expertMode && config.WeatherMultOnlyExpert) || config.WeatherMultForEnemies)
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
                        damage = (int)(damage * weatherMultiplier);
                    }
                    break;

                case Element.dark:
                    if (Main.eclipse)
                    {
                        damage = (int)(damage * weatherMultiplier);
                    }
                    break;

                case Element.water:
                    if (closestPlayer.ZoneRain)
                    {
                        damage = (int)(damage * weatherMultiplier);
                    }
                    break;

                case Element.fire:
                    if (closestPlayer.ZoneRain)
                    {
                        damage = (int)(damage * (1 / weatherMultiplier));
                    }
                    break;

                case Element.ice:
                    if (Main.raining && closestPlayer.ZoneSnow)
                    {
                        damage = (int)(damage * weatherMultiplier);
                    }
                    break;

                case Element.ground:
                case Element.rock:
                case Element.steel:
                    if (closestPlayer.ZoneSandstorm)
                    {
                        damage = (int)(damage * weatherMultiplier);
                    }
                    break;
            }
        }
    }
}
