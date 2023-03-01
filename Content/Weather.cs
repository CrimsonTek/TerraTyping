using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Accessories.HeldItems;
using TerraTyping.Common.Configs;
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class ItemDamageBoost : GlobalItem
    {
        Boost[] heldItemBoosts = default;
        Boost[] weatherBoosts = default;
        Boost[] abilityBoosts = default;
        Boost[] buffBoosts = default;

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item from, Item to)
        {
            ItemDamageBoost weatherItem = (ItemDamageBoost)base.Clone(from, to);
            weatherItem.heldItemBoosts = Array.Empty<Boost>();
            weatherItem.weatherBoosts = Array.Empty<Boost>();
            weatherItem.abilityBoosts = Array.Empty<Boost>();
            weatherItem.buffBoosts = Array.Empty<Boost>();
            return weatherItem;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            ElementArray elements = new WeaponWrapper(item, player).OffensiveElements;
            UpdateHeldItemBoost(player, elements);
            UpdateWeatherBoost(player, elements);
            GetAbilityBoost(player, elements);
            GetBuffBoost(player, elements);

            float sum = 1;
            for (int i = 0; i < weatherBoosts.Length; i++)
            {
                sum += weatherBoosts[i].Multiplier - 1;
            }
            for (int i = 0; i < heldItemBoosts.Length; i++)
            {
                sum += heldItemBoosts[i].Multiplier - 1;
            }
            for (int i = 0; i < abilityBoosts.Length; i++)
            {
                sum += abilityBoosts[i].Multiplier - 1;
            }
            for (int i = 0; i < buffBoosts.Length; i++)
            {
                sum += buffBoosts[i].Multiplier - 1;
            }

            if (sum != 1)
            {
                damage = damage.CombineWith(new StatModifier(1, sum));
            }
        }

        private void UpdateHeldItemBoost(Player player, ElementArray elements)
        {
            Boost[] boosts = player.GetModPlayer<HeldItemsPlayer>().heldItemBoosts;

            heldItemBoosts = new Boost[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                heldItemBoosts[i] = boosts[(int)elements[i]];
            }
        }
        private void UpdateWeatherBoost(Player player, ElementArray elements)
        {
            weatherBoosts = new Boost[elements.Length];

            if (!Main.expertMode && ModContent.GetInstance<ServerConfig>().WeatherMultOnlyExpert)
            {
                return;
            }

            if (!player.ZoneOverworldHeight && !player.ZoneSkyHeight && !player.ZoneDirtLayerHeight)
            {
                return;
            }

            float weatherMultiplier = ModContent.GetInstance<ServerConfig>().WeatherMultiplier;
            for (int i = 0; i < elements.Length; i++)
            {
                switch (elements[i])
                {
                    case Element.blood:
                        if (Main.bloodMoon)
                        {
                            weatherBoosts[i].reason = "Blood moon";
                            weatherBoosts[i].Multiplier = weatherMultiplier;
                        }
                        break;

                    case Element.dark:
                        if (Main.eclipse)
                        {
                            weatherBoosts[i].reason = "Eclipse";
                            weatherBoosts[i].Multiplier = weatherMultiplier;
                        }
                        break;

                    case Element.water:
                        if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                        {
                            weatherBoosts[i].reason = "Rain";
                            weatherBoosts[i].Multiplier = weatherMultiplier;
                        }
                        break;

                    case Element.fire:
                        if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                        {
                            weatherBoosts[i].reason = "Rain";
                            weatherBoosts[i].Multiplier = 1 / weatherMultiplier;
                        }
                        break;

                    case Element.ice:
                        if (player.ZoneSnow && player.ZoneSnow)
                        {
                            weatherBoosts[i].reason = "Snow";
                            weatherBoosts[i].Multiplier = weatherMultiplier;
                        }
                        break;
                }
                if (player.ZoneSandstorm)
                {
                    switch (elements[i])
                    {
                        case Element.ground:
                        case Element.rock:
                        case Element.steel:
                            if (player.ZoneSandstorm)
                            {
                                weatherBoosts[i].reason = "Sandstorm";
                                weatherBoosts[i].Multiplier = weatherMultiplier;
                            }
                            break;
                    }
                }

            }
        }
        private void GetAbilityBoost(Player player, ElementArray elements)
        {
            Ability ability = player.GetModPlayer<PlayerTyping>().GetAbility();
            abilityBoosts = new Boost[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                AbilityLookup.PowerupTypeReturn powerupTypeReturn = ability.PowerupType(new AbilityLookup.PowerupTypeParameters(elements[i], PlayerWrapper.GetWrapper(player), null));
                abilityBoosts[i] = new Boost(powerupTypeReturn.powerupMultiplier, ability.ToString());
            }
        }
        private void GetBuffBoost(Player player, ElementArray elements)
        {
            buffBoosts = new Boost[elements.Length];
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (player.buffTime[i] <= 0)
                {
                    ModBuff modBuff = ModContent.GetModBuff(player.buffType[i]);
                    if (modBuff != null && modBuff is IPowerupType powerupType)
                    {
                        for (int j = 0; j < elements.Length; j++)
                        {
                            buffBoosts[i] = powerupType.PowerupType(new PowerupTypeParameters(elements[i], PlayerWrapper.GetWrapper(player))).boost;
                        }
                    }
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.damage > 0)
            {
                ModifyTooltip(weatherBoosts, "weatherMult");
                ModifyTooltip(heldItemBoosts, "heldItemMult");
                ModifyTooltip(abilityBoosts, "abilityBoostMult");
                ModifyTooltip(buffBoosts, "buffBoostMult");
            }

            void ModifyTooltip(Boost[] boosts, string name)
            {
                for (int i = 0; i < boosts.Length; i++)
                {
                    if ((float)boosts[i].Multiplier == 1)
                    {
                        continue;
                    }

                    string bonusOrPenalty = (float)boosts[i].Multiplier > 1 ? "bonus" : "penalty";
                    double rounded = Math.Round(((float)boosts[i].Multiplier - 1) * 100);
                    TooltipLine line = new TooltipLine(Mod, name, $"{boosts[i].reason} {bonusOrPenalty}: {rounded}%");
                    tooltips.Add(line);
                }
            }
        }
    }
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
}
