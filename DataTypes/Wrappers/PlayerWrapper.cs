using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Abilities;
using TerraTyping.DataTypes.NewInterfaces;

namespace TerraTyping.DataTypes
{
    public class PlayerWrapper : Wrapper, IDefensiveElements, ITarget, IAbility, ITeam, IPlayer, ICharacter, AbilityLookup.IAttractProjectileTarget
    {
        readonly int playerIndex;
        public Player Player => Main.player[playerIndex];
        public PlayerTyping PlayerTyping => Player.GetModPlayer<PlayerTyping>();

        public ElementArray DefensiveElements => PlayerTyping.Elements;
        public AbilityID GetAbility => PlayerTyping.AbilityID;

        #region Biomes
        public bool ZoneBeach { get => Player.ZoneBeach; }
        public bool ZoneCorrupt { get => Player.ZoneCorrupt; }
        public bool ZoneCrimson { get => Player.ZoneCrimson; }
        public bool ZoneDesert { get => Player.ZoneDesert; }
        public bool ZoneDirtLayerHeight { get => Player.ZoneDirtLayerHeight; }
        public bool ZoneDungeon { get => Player.ZoneDungeon; }
        public bool ZoneGlowshroom { get => Player.ZoneGlowshroom; }
        public bool ZoneHoly { get => Player.ZoneHallow; }
        public bool ZoneJungle { get => Player.ZoneJungle; }
        public bool ZoneMeteor { get => Player.ZoneMeteor; }
        public bool ZoneOldOneArmy { get => Player.ZoneOldOneArmy; }
        public bool ZoneOverworldHeight { get => Player.ZoneOverworldHeight; }
        public bool ZonePeaceCandle { get => Player.ZonePeaceCandle; }
        public bool ZoneRain { get => Player.ZoneRain; }
        public bool ZoneRockLayerHeight { get => Player.ZoneRockLayerHeight; }
        public bool ZoneSandstorm { get => Player.ZoneSandstorm; }
        public bool ZoneSkyHeight { get => Player.ZoneSkyHeight; }
        public bool ZoneSnow { get => Player.ZoneSnow; }
        public bool ZoneTowerNebula { get => Player.ZoneTowerNebula; }
        public bool ZoneTowerSolar { get => Player.ZoneTowerSolar; }
        public bool ZoneTowerStardust { get => Player.ZoneTowerStardust; }
        public bool ZoneTowerVortex { get => Player.ZoneTowerVortex; }
        public bool ZoneUndergroundDesert { get => Player.ZoneUndergroundDesert; }
        public bool ZoneUnderworldHeight { get => Player.ZoneUnderworldHeight; }
        public bool ZoneWaterCandle { get => Player.ZoneWaterCandle; }
        #endregion
        public EntityType EntityType => EntityType.Player;
        public bool Boss => false;
        public int Life => Player.statLife;
        public int LifeMax => Player.statLifeMax2;
        public bool Active => Player.active;
        public bool Immortal => false;
        public int LifeRegen { get => Player.lifeRegen; set => Player.lifeRegen = value; }
        public int LifeRegenTime { get => Player.lifeRegenTime; set => Player.lifeRegenTime = value; }

        public AbilityID ModifiedAbility
        {
            get => PlayerTyping.ModifiedAbility;
            set => PlayerTyping.ModifiedAbility = value;
        }
        public ElementArray ModifiedElements
        {
            get => PlayerTyping.ModifiedElements;
            set => PlayerTyping.ModifiedElements = value;
        }

        public bool UseModifiedAbility
        {
            get => PlayerTyping.UseModifiedAbility;
            set => PlayerTyping.UseModifiedAbility = value;
        }
        public bool UseModifiedElements
        {
            get => PlayerTyping.UseModifiedElements;
            set => PlayerTyping.UseModifiedElements = value;
        }

        PlayerWrapper(int whoAmI)
        {
            playerIndex = whoAmI;
        }

        public static PlayerWrapper GetWrapper(Player player)
        {
            return new PlayerWrapper(player.whoAmI);
        }
        public static PlayerWrapper GetWrapper(int playerIndex)
        {
            return new PlayerWrapper(playerIndex);
        }

        public void AddBuff(int type, int time, bool quiet = false)
        {
            Player.AddBuff(type, time, quiet);
        }
        public bool HasBuff(int type)
        {
            return Player.HasBuff(type);
        }
        public void DelBuff(int b)
        {
            Player.DelBuff(b);
        }
        public void RemoveBuff(int type)
        {
            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (Player.buffType[i] > 0 && Player.buffTime[i] > 0 && !BuffID.Sets.NurseCannotRemoveDebuff[Player.buffType[i]])
                {
                    Player.DelBuff(i);
                    i--;
                }
            }
        }

        public Rectangle GetRect()
        {
            return Player.getRect();
        }

        public void HealEffect(int healAmount, bool broadcast = true)
        {
            Player.HealEffect(healAmount, broadcast);
        }

        /// <summary>
        /// Does the effect automatically
        /// </summary>
        public void Heal(int healAmount, bool broadcast = true)
        {
            int heal = Math.Min(Player.statLifeMax2 - Player.statLife, healAmount);
            Player.statLife += heal;
            Player.HealEffect(heal, broadcast);
        }

        public int GetCombatTextCooldown()
        {
            PlayerTyping playerTyping = Player.GetModPlayer<PlayerTyping>();
            return playerTyping.CombatTextCooldown;
        }
        public int GetCombatHealCooldown()
        {
            PlayerTyping playerTyping = Player.GetModPlayer<PlayerTyping>();
            return playerTyping.CombatHealCooldown;
        }

        public void UseCombatTextCooldown()
        {
            PlayerTyping playerTyping = Player.GetModPlayer<PlayerTyping>();
            playerTyping.UseCombatTextCooldown();
        }
        public void UseCombatHealCooldown()
        {
            PlayerTyping playerTyping = Player.GetModPlayer<PlayerTyping>();
            playerTyping.UseCombatHealCooldown();
        }

        public Team GetTeam() => Team.PlayerFriendly;
    }
}
