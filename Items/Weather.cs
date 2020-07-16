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

        static float boostMult = 1;
        static float weatherMult = 1;
        static float RainMultiplier => ModContent.GetInstance<Config>().RainMultiplier;
        static string weatherReason = string.Empty;

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            Element element = ElementHelper.Quatrinary(item);
            float[] boosts = player.GetModPlayer<HeldItems.HeldItemsPlayer>().boosts;
            boostMult = boosts[(int)ElementHelper.Quatrinary(item)];

            weatherMult = 1;
            weatherReason = string.Empty;
            if (Main.expertMode || !ModContent.GetInstance<Config>().RainMultOnlyExpert)
            {
                if (player.ZoneOverworldHeight || player.ZoneSkyHeight || player.ZoneDirtLayerHeight)
                {
                    if (Main.bloodMoon)
                    {
                        if (element == Element.blood)
                        {
                            weatherReason = "Blood moon";
                            weatherMult = RainMultiplier;
                        }
                    }
                    if (Main.eclipse)
                    {
                        if (element == Element.dark)
                        {
                            weatherReason = "Eclipse";
                            weatherMult = RainMultiplier;
                        }
                    }
                    if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                    {
                        if (element == Element.water)
                        {
                            weatherReason = "Rain";
                            weatherMult = RainMultiplier;
                        }
                        if (element == Element.fire)
                        {
                            weatherReason = "Rain";
                            weatherMult = 1 / RainMultiplier;
                        }
                    }
                    if (player.ZoneSnow && player.ZoneSnow)
                    {
                        if (element == Element.ice)
                        {
                            weatherReason = "Snow";
                            weatherMult = RainMultiplier;
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
                                weatherMult = RainMultiplier;
                                break;
                        }
                    }
                }
            }

            float myMult = weatherMult + boostMult;
            if (myMult != 1)
            {
                mult = myMult;
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
            if (boostMult != 1)
            {
                var line = new TooltipLine(mod, "weatherMult", $"Held Item bonus: {Math.Round((boostMult - 1) * 100)}%");
                tooltips.Add(line);
            }
        }
    }
    class WeatherEnemies : GlobalNPC
    {
        float RainMultiplier => ModContent.GetInstance<Config>().RainMultiplier;

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            Element element = ElementHelper.Quatrinary(npc);

            if (Main.expertMode)
            {
                if (Main.bloodMoon && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                {
                    if (element == Element.blood)
                    {
                        damage = (int)(damage * RainMultiplier);
                    }
                }
                if (Main.eclipse && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                {
                    if (element == Element.dark)
                    {
                        damage = (int)(damage * RainMultiplier);
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneRain)
                {
                    if (element == Element.water)
                    {
                        damage = (int)(damage * RainMultiplier);
                    }
                    if (element == Element.fire)
                    {
                        damage = (int)(damage * (1 / RainMultiplier));
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSnow)
                {
                    if (element == Element.ice)
                    {
                        damage = (int)(damage * RainMultiplier);
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSandstorm)
                {
                    switch (element)
                    {
                        case Element.ground:
                        case Element.rock:
                        case Element.steel:
                            damage = (int)(damage * RainMultiplier);
                            break;
                    }
                }
            }
        }
    }
}
