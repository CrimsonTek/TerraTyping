using System;
using Microsoft.Xna.Framework;
using TerraTyping.Core;

namespace TerraTyping.DataTypes
{
    /// <summary>
    /// Used for wrapping damage targets for the sake of passing as generic variables.
    /// </summary>
    public interface ITarget
    {
        void AddBuff(int type, int time, bool quiet = false);
        bool HasBuff(int type);
        [Obsolete] void DelBuff(int type);
        void RemoveBuff(int type);
        Rectangle GetRect();
        void HealEffect(int healAmount, bool broadcast = true);
        void Heal(int healAmount, bool broadcast = true);
        int GetCombatTextCooldown();
        int GetCombatHealCooldown();
        void UseCombatTextCooldown();
        void UseCombatHealCooldown();
        bool Active { get; }
        bool Immortal { get; }
        EntityType EntityType { get; }
        bool Boss { get; }
        int Life { get; }
        int LifeMax { get; }
        int LifeRegen { get; set; }
        float LifeRegenTime { get; set; }
        /// <summary>
        /// Currently used for the ability Mummy
        /// </summary>
        Ability ModifiedAbility { get; set; }
        /// <summary>
        /// Currently used for the ability Color Change
        /// </summary>
        ElementArray ModifiedElements { get; set; }
        bool UseModifiedAbility { get; set; }
        bool UseModifiedElements { get; set; }

        #region Biomes
        bool ZoneBeach { get; }
        bool ZoneCorrupt { get; }
        bool ZoneCrimson { get; }
        bool ZoneDesert { get; }
        bool ZoneDirtLayerHeight { get; }
        bool ZoneDungeon { get; }
        bool ZoneGlowshroom { get; }
        bool ZoneHoly { get; }
        bool ZoneJungle { get; }
        bool ZoneMeteor { get; }
        bool ZoneOldOneArmy { get; }
        bool ZoneOverworldHeight { get; }
        bool ZonePeaceCandle { get; }
        bool ZoneRain { get; }
        bool ZoneRockLayerHeight { get; }
        bool ZoneSandstorm { get; }
        bool ZoneSkyHeight { get; }
        bool ZoneSnow { get; }
        bool ZoneTowerNebula { get; }
        bool ZoneTowerSolar { get; }
        bool ZoneTowerStardust { get; }
        bool ZoneTowerVortex { get; }
        bool ZoneUndergroundDesert { get; }
        bool ZoneUnderworldHeight { get; }
        bool ZoneWaterCandle { get; }
        #endregion
    }

    public enum EntityType
    {
        Player,
        NPC
    }
}
