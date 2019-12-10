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

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            ElementHelper elementHelper = new ElementHelper();
            Element element = elementHelper.Quatrinary(item);

            if (Main.expertMode)
            {
                if (player.ZoneOverworldHeight || player.ZoneSkyHeight || player.ZoneDirtLayerHeight)
                {
                    if (Main.bloodMoon)
                    {
                        if (element == Element.blood)
                        {
                            mult = Config.RainMultiplier;
                        }
                    }
                    if (Main.eclipse)
                    {
                        if (element == Element.dark)
                        {
                            mult = Config.RainMultiplier;
                        }
                    }
                    if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                    {
                        if (element == Element.water)
                        {
                            mult = Config.RainMultiplier;
                        }
                        if (element == Element.fire)
                        {
                            mult = 1 / Config.RainMultiplier;
                        }
                    }
                    if (player.ZoneSnow && player.ZoneSnow)
                    {
                        if (element == Element.ice)
                        {
                            mult = Config.RainMultiplier;
                        }
                    }
                    if (player.ZoneSandstorm)
                    {
                        switch (element)
                        {
                            case Element.ground:
                            case Element.rock:
                            case Element.steel:
                                mult = Config.RainMultiplier;
                                break;
                        }
                    }
                }
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
