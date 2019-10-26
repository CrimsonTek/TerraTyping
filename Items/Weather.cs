using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTyping.Items
{
    class Weather : GlobalItem
    {
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            if (Items.Type.ContainsKey(item.type))
            {
                if (Main.bloodMoon && player.ZoneOverworldHeight || player.ZoneSkyHeight)
                {
                    if (Items.Type[item.type] == Element.Type.blood)
                    {
                        mult = Config.RainMultiplier;
                    }
                }
                if (Main.eclipse && player.ZoneOverworldHeight || player.ZoneSkyHeight)
                {
                    if (Items.Type[item.type] == Element.Type.dark)
                    {
                        mult = Config.RainMultiplier;
                    }
                }
                if (player.ZoneRain)
                {
                    if (Items.Type[item.type] == Element.Type.water)
                    {
                        mult = Config.RainMultiplier;
                    }
                    if (Items.Type[item.type] == Element.Type.fire)
                    {
                        mult = 1 / Config.RainMultiplier;
                    }
                }
                if (player.ZoneSnow)
                {
                    if (Items.Type[item.type] == Element.Type.ice)
                    {
                        mult = Config.RainMultiplier;
                    }
                }
                if (player.ZoneSandstorm)
                {
                    if (Items.Type[item.type] == Element.Type.ground || Items.Type[item.type] == Element.Type.rock || Items.Type[item.type] == Element.Type.steel)
                    {
                        mult = Config.RainMultiplier;
                    }
                }
            }
        }
    }
    class WeatherEnemies : GlobalNPC
    {
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (Enemies.Type.ContainsKey(npc.type))
            {
                if (Main.bloodMoon && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                {
                    if (Enemies.Type[npc.type].Item4 == Element.Type.blood)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
                if (Main.eclipse && Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneOverworldHeight || Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSkyHeight)
                {
                    if (Enemies.Type[npc.type].Item4 == Element.Type.dark)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneRain)
                {
                    if (Enemies.Type[npc.type].Item4 == Element.Type.water)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                    if (Enemies.Type[npc.type].Item4 == Element.Type.fire)
                    {
                        damage = (int)(damage * (1 / Config.RainMultiplier));
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSnow)
                {
                    if (Enemies.Type[npc.type].Item4 == Element.Type.ice)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
                if (Main.player[(int)(Player.FindClosest(npc.position, npc.width, npc.height))].ZoneSandstorm)
                {
                    if (Enemies.Type[npc.type].Item4 == Element.Type.ground || Enemies.Type[npc.type].Item4 == Element.Type.rock || Enemies.Type[npc.type].Item4 == Element.Type.steel)
                    {
                        damage = (int)(damage * Config.RainMultiplier);
                    }
                }
            }
        }
    }
}
