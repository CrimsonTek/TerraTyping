using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Accessories.HeldItems;
using TerraTyping.Common.Configs;
using TerraTyping.DataTypes;

namespace TerraTyping;

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
        Boost[] emptyArray = Array.Empty<Boost>();
        weatherItem.heldItemBoosts = emptyArray;
        weatherItem.weatherBoosts = emptyArray;
        weatherItem.abilityBoosts = emptyArray;
        weatherItem.buffBoosts = emptyArray;
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
        if (player.TryGetModPlayer(out PlayerTyping playerTyping))
        {
            List<Boost> allBoosts = new List<Boost>(playerTyping.boostsToAllDamageByAbilities);
            for (int i = 0; i < elements.Length; i++)
            {
                allBoosts.AddRange(playerTyping.boostsToEachTypeByAbilities[(int)elements[i]]);
            }
            buffBoosts = allBoosts.ToArray();
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
