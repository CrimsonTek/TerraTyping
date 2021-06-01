using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.DataTypes.NewInterfaces;

namespace TerraTyping.DataTypes
{
    public class PlayerWrapper : Wrapper, IPrimaryType, ISecondaryType, ITarget, IAbility, ITeam, IPlayer, ICharacter, AbilityLookup.IAttractProjectileTarget
    {
        readonly int player;
        public Player GetPlayer() => Main.player[player];
        public T GetModPlayer<T>() where T : ModPlayer => GetPlayer().GetModPlayer<T>();

        public Element Primary
        {
            get
            {
                PlayerTyping armorPlayer = GetPlayer().GetModPlayer<PlayerTyping>();
                return armorPlayer.newTypeSet.Primary;
            }
        }
        public Element Secondary
        {
            get
            {
                PlayerTyping armorPlayer = GetPlayer().GetModPlayer<PlayerTyping>();
                return armorPlayer.newTypeSet.Secondary;
            }
        }
        public AbilityID GetAbility => GetModPlayer<PlayerTyping>().newTypeSet.GetAbility;

        #region Biomes
        public bool ZoneBeach { get => GetPlayer().ZoneBeach; }
        public bool ZoneCorrupt { get => GetPlayer().ZoneCorrupt; }
        public bool ZoneCrimson { get => GetPlayer().ZoneCrimson; }
        public bool ZoneDesert { get => GetPlayer().ZoneDesert; }
        public bool ZoneDirtLayerHeight { get => GetPlayer().ZoneDirtLayerHeight; }
        public bool ZoneDungeon { get => GetPlayer().ZoneDungeon; }
        public bool ZoneGlowshroom { get => GetPlayer().ZoneGlowshroom; }
        public bool ZoneHoly { get => GetPlayer().ZoneHoly; }
        public bool ZoneJungle { get => GetPlayer().ZoneJungle; }
        public bool ZoneMeteor { get => GetPlayer().ZoneMeteor; }
        public bool ZoneOldOneArmy { get => GetPlayer().ZoneOldOneArmy; }
        public bool ZoneOverworldHeight { get => GetPlayer().ZoneOverworldHeight; }
        public bool ZonePeaceCandle { get => GetPlayer().ZonePeaceCandle; }
        public bool ZoneRain { get => GetPlayer().ZoneRain; }
        public bool ZoneRockLayerHeight { get => GetPlayer().ZoneRockLayerHeight; }
        public bool ZoneSandstorm { get => GetPlayer().ZoneSandstorm; }
        public bool ZoneSkyHeight { get => GetPlayer().ZoneSkyHeight; }
        public bool ZoneSnow { get => GetPlayer().ZoneSnow; }
        public bool ZoneTowerNebula { get => GetPlayer().ZoneTowerNebula; }
        public bool ZoneTowerSolar { get => GetPlayer().ZoneTowerSolar; }
        public bool ZoneTowerStardust { get => GetPlayer().ZoneTowerStardust; }
        public bool ZoneTowerVortex { get => GetPlayer().ZoneTowerVortex; }
        public bool ZoneUndergroundDesert { get => GetPlayer().ZoneUndergroundDesert; }
        public bool ZoneUnderworldHeight { get => GetPlayer().ZoneUnderworldHeight; }
        public bool ZoneWaterCandle { get => GetPlayer().ZoneWaterCandle; }
        #endregion
        public EntityType EntityType => EntityType.Player;
        public bool Boss => false;
        public int Life => GetPlayer().statLife;
        public int LifeMax => GetPlayer().statLifeMax2;
        public bool Active => GetPlayer().active;
        public bool Immortal => false;
        public int LifeRegen { get => GetPlayer().lifeRegen; set => GetPlayer().lifeRegen = value; }
        public int LifeRegenTime { get => GetPlayer().lifeRegenTime; set => GetPlayer().lifeRegenTime = value; }
        public Element ModifyType { get => GetModPlayer<PlayerTyping>().ModifyType; set => GetModPlayer<PlayerTyping>().ModifyType = value; }


        public PlayerWrapper(Player player)
        {
            this.player = player.whoAmI;
        }
        public PlayerWrapper(ModPlayer modPlayer)
        {
            player = modPlayer.player.whoAmI;
        }
        public PlayerWrapper(int whoAmI)
        {
            player = whoAmI;
        }

        public void AddBuff(int type, int time, bool quiet = false)
        {
            GetPlayer().AddBuff(type, time, quiet);
        }
        public bool HasBuff(int type)
        {
            return GetPlayer().HasBuff(type);
        }
        public void DelBuff(int b)
        {
            GetPlayer().DelBuff(b);
        }
        public void RemoveBuff(int type)
        {
            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (GetPlayer().buffType[i] > 0 && GetPlayer().buffTime[i] > 0 && BuffLoader.CanBeCleared(GetPlayer().buffType[i]))
                {
                    GetPlayer().DelBuff(i);
                    i--;
                }
            }
        }

        public Rectangle GetRect()
        {
            return GetPlayer().getRect();
        }

        public void HealEffect(int healAmount, bool broadcast = true)
        {
            GetPlayer().HealEffect(healAmount, broadcast);
        }

        /// <summary>
        /// Does the effect automatically
        /// </summary>
        public void Heal(int healAmount, bool broadcast = true)
        {
            int heal = Math.Min(GetPlayer().statLifeMax2 - GetPlayer().statLife, healAmount);
            GetPlayer().statLife += heal;
            GetPlayer().HealEffect(heal, broadcast);
        }

        public int GetCombatTextCooldown()
        {
            PlayerTyping playerTyping = GetPlayer().GetModPlayer<PlayerTyping>();
            return playerTyping.CombatTextCooldown;
        }
        public int GetCombatHealCooldown()
        {
            PlayerTyping playerTyping = GetPlayer().GetModPlayer<PlayerTyping>();
            return playerTyping.CombatHealCooldown;
        }

        public void UseCombatTextCooldown()
        {
            PlayerTyping playerTyping = GetPlayer().GetModPlayer<PlayerTyping>();
            playerTyping.UseCombatTextCooldown();
        }
        public void UseCombatHealCooldown()
        {
            PlayerTyping playerTyping = GetPlayer().GetModPlayer<PlayerTyping>();
            playerTyping.UseCombatHealCooldown();
        }

        public Team GetTeam() => Team.PlayerFriendly;
    }
}
