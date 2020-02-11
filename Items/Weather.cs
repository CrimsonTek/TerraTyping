using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping
{
    class Weather : GlobalItem
    {
        public override bool InstancePerEntity => true;

        static float weatherMult = 1;
        static string weatherReason = string.Empty;

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            ElementHelper elementHelper = new ElementHelper();
            Element element = elementHelper.Quatrinary(item);

            weatherMult = 1;
            weatherReason = string.Empty;
            if (Main.expertMode)
            {
                if (player.ZoneOverworldHeight || player.ZoneSkyHeight || player.ZoneDirtLayerHeight)
                {
                    if (Main.bloodMoon)
                    {
                        if (element == Element.blood)
                        {
                            weatherReason = "Blood moon";
                            weatherMult = Config.RainMultiplier;
                        }
                    }
                    if (Main.eclipse)
                    {
                        if (element == Element.dark)
                        {
                            weatherReason = "Eclipse";
                            weatherMult = Config.RainMultiplier;
                        }
                    }
                    if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                    {
                        if (element == Element.water)
                        {
                            weatherReason = "Rain";
                            weatherMult = Config.RainMultiplier;
                        }
                        if (element == Element.fire)
                        {
                            weatherReason = "Rain";
                            weatherMult = 1 / Config.RainMultiplier;
                        }
                    }
                    if (player.ZoneSnow && player.ZoneSnow)
                    {
                        if (element == Element.ice)
                        {
                            weatherReason = "Snow";
                            weatherMult = Config.RainMultiplier;
                        }
                    }
                    if (player.ZoneSandstorm)
                    {
                        switch (element)
                        {
                            case Element.ground:
                            case Element.rock:
                            case Element.steel:
                                weatherReason = "Sandstorm";
                                weatherMult = Config.RainMultiplier;
                                break;
                        }
                    }
                }
            }

            if (weatherMult != 1)
            {
                mult = weatherMult;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string bonusOrPenalty = string.Empty;
            if (weatherMult > 1)
                bonusOrPenalty = "bonus";
            else if (weatherMult < 1)
                bonusOrPenalty = "penalty";
            if (weatherMult != 1)
            {
                var line = new TooltipLine(mod, "weatherMult", $"{weatherReason} {bonusOrPenalty}: {Math.Round((weatherMult - 1) * 100)}%");
                tooltips.Add(line);
            }
        }
    }
    class WeatherEnemies : GlobalNPC
    {
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            ElementHelper elementHelper = new ElementHelper();
            Element element = elementHelper.Quatrinary(npc);

            if (Main.expertMode)
            {
                if (Main.bloodMoon && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                {
                    if (element == Element.blood)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
                if (Main.eclipse && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                {
                    if (element == Element.dark)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneRain)
                {
                    if (element == Element.water)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                    if (element == Element.fire)
                    {
                        damage = (int)(damage * (1 / Config.RainMultiplier));
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSnow)
                {
                    if (element == Element.ice)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSandstorm)
                {
                    switch (element)
                    {
                        case Element.ground:
                        case Element.rock:
                        case Element.steel:
                            damage = (int)(damage * Config.RainMultiplier);
                            break;
                    }
                }
            }
        }
    }
}
