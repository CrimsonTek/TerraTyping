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
using TerraTyping.DataTypes;

namespace TerraTyping
{
    public class Weather : GlobalItem
    {
        public override bool InstancePerEntity => true;

        Boost heldItem = default;
        Boost weather = default;
        Boost abilityBoost = default;
        Boost buffBoost = default;
        float RainMultiplier => ModContent.GetInstance<Config>().RainMultiplier;

        public override GlobalItem NewInstance(Item item)
        {
            return base.NewInstance(item);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            Element element = new ItemWrapper(item, player).Offensive;
            GetHeldItemBoost(player, element);
            GetWeatherBoost(player, element);
            GetAbilityBoost(player, element);
            GetBuffBoost(player, element);

            Boost[] boosts = new Boost[]
            {
                weather, heldItem, abilityBoost, buffBoost
            };
            float myMult = boosts.Sum((boost) => { return boost.Multiplier - 1; }) + 1;
            if (myMult != 1)
            {
                mult *= myMult;
            }
        }

        private void GetHeldItemBoost(Player player, Element element)
        {
            Boost[] boosts = player.GetModPlayer<HeldItemsPlayer>().heldItemBoosts;
            heldItem = boosts[(int)element];
        }
        private void GetWeatherBoost(Player player, Element element)
        {
            weather = new Boost(1, string.Empty);
            if (Main.expertMode || !ModContent.GetInstance<Config>().RainMultOnlyExpert)
            {
                if (player.ZoneOverworldHeight || player.ZoneSkyHeight || player.ZoneDirtLayerHeight)
                {
                    if (Main.bloodMoon)
                    {
                        if (element == Element.blood)
                        {
                            weather.reason = "Blood moon";
                            weather.Multiplier = RainMultiplier;
                        }
                    }
                    if (Main.eclipse)
                    {
                        if (element == Element.dark)
                        {
                            weather.reason = "Eclipse";
                            weather.Multiplier = RainMultiplier;
                        }
                    }
                    if (player.ZoneRain && !player.ZoneDesert && !player.ZoneSnow)
                    {
                        if (element == Element.water)
                        {
                            weather.reason = "Rain";
                            weather.Multiplier = RainMultiplier;
                        }
                        if (element == Element.fire)
                        {
                            weather.reason = "Rain";
                            weather.Multiplier = 1 / RainMultiplier;
                        }
                    }
                    if (player.ZoneSnow && player.ZoneSnow)
                    {
                        if (element == Element.ice)
                        {
                            weather.reason = "Snow";
                            weather.Multiplier = RainMultiplier;
                        }
                    }
                    if (player.ZoneSandstorm)
                    {
                        switch (element)
                        {
                            case Element.ground:
                            case Element.rock:
                            case Element.steel:
                                weather.reason = "Sandstorm";
                                weather.Multiplier = RainMultiplier;
                                break;
                        }
                    }
                }
            }
        }
        private void GetAbilityBoost(Player player, Element element)
        {
            Ability ability = player.GetModPlayer<PlayerTyping>().GetAbility();
            AbilityLookup.PowerupTypeReturn powerupTypeReturn = ability.PowerupType(new AbilityLookup.PowerupTypeParameters(element, new PlayerWrapper(player), null));
            abilityBoost = new Boost(powerupTypeReturn.powerupMultiplier, ability.ToString());
        }
        private void GetBuffBoost(Player player, Element element)
        {
            buffBoost = new Boost(1, string.Empty);
            for (int i = 0; i < player.buffType.Length; i++)
            {
                ModBuff modBuff = ModContent.GetModBuff(player.buffType[i]);
                if (modBuff != null && modBuff is IPowerupType powerupType)
                {
                    buffBoost = powerupType.PowerupType(new PowerupTypeParameters(element, new PlayerWrapper(player))).boost;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.damage > 0)
            {
                ModifyTooltip(weather, "weatherMult");
                ModifyTooltip(heldItem, "heldItemMult");
                ModifyTooltip(abilityBoost, "abilityBoostMult");
                ModifyTooltip(buffBoost, "buffBoostMult");
            }

            void ModifyTooltip(Boost boost, string name)
            {
                string bonusOrPenalty = string.Empty;
                if (boost.Multiplier > 1)
                    bonusOrPenalty = "bonus";
                else if (boost.Multiplier < 1)
                    bonusOrPenalty = "penalty";
                if (boost.Multiplier != 1)
                {
                    var line = new TooltipLine(mod, name, $"{boost.reason} {bonusOrPenalty}: {Math.Round((boost.Multiplier - 1) * 100)}%");
                    tooltips.Add(line);
                }
            }
        }
    }
    public class WeatherEnemies : GlobalNPC
    {
        float RainMultiplier => ModContent.GetInstance<Config>().RainMultiplier;

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            Element element = new NPCWrapper(npc).Offensive;

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
