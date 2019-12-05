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
            if (Main.expertMode)
            {
                if (Items.Type.ContainsKey(item.type))
                {
                    if (player.ZoneOverworldHeight || player.ZoneSkyHeight || player.ZoneDirtLayerHeight)
                    {
                        if (Main.bloodMoon)
                        {
                            if (Items.Type[item.type] == Element.blood)
                            {
                                mult = Config.RainMultiplier;
                            }
                        }
                        if (Main.eclipse)
                        {
                            if (Items.Type[item.type] == Element.dark)
                            {
                                mult = Config.RainMultiplier;
                            }
                        }
                        if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                        {
                            if (Items.Type[item.type] == Element.water)
                            {
                                mult = Config.RainMultiplier;
                            }
                            if (Items.Type[item.type] == Element.fire)
                            {
                                mult = 1 / Config.RainMultiplier;
                            }
                        }
                        if (player.ZoneSnow && player.ZoneSnow)
                        {
                            if (Items.Type[item.type] == Element.ice)
                            {
                                mult = Config.RainMultiplier;
                            }
                        }
                        if (player.ZoneSandstorm)
                        {
                            if (Items.Type[item.type] == Element.ground || Items.Type[item.type] == Element.rock || Items.Type[item.type] == Element.steel)
                            {
                                mult = Config.RainMultiplier;
                            }
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
            if (Main.expertMode)
            {
                if (Enemies.Type.ContainsKey(npc.type))
                {
                    if (Main.bloodMoon && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                    {
                        if (Enemies.Type[npc.type].Item4 == Element.blood)
                        {
                            damage = (int)(damage * Config.RainMultiplier);
                        }
                    }
                    if (Main.eclipse && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                    {
                        if (Enemies.Type[npc.type].Item4 == Element.dark)
                        {
                            damage = (int)(damage * Config.RainMultiplier);
                        }
                    }
                    if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneRain)
                    {
                        if (Enemies.Type[npc.type].Item4 == Element.water)
                        {
                            damage = (int)(damage * Config.RainMultiplier);
                        }
                        if (Enemies.Type[npc.type].Item4 == Element.fire)
                        {
                            damage = (int)(damage * (1 / Config.RainMultiplier));
                        }
                    }
                    if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSnow)
                    {
                        if (Enemies.Type[npc.type].Item4 == Element.ice)
                        {
                            damage = (int)(damage * Config.RainMultiplier);
                        }
                    }
                    if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSandstorm)
                    {
                        if (Enemies.Type[npc.type].Item4 == Element.ground || Enemies.Type[npc.type].Item4 == Element.rock || Enemies.Type[npc.type].Item4 == Element.steel)
                        {
                            damage = (int)(damage * Config.RainMultiplier);
                        }
                    }
                }
            }
        }
    }
}
