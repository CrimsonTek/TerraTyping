using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TerraTyping.DataTypes
{
    /// <summary>
    /// Used for wrapping damage targets for the sake of passing as generic variables.
    /// This should only be used for existing objects, not structs.
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
        int LifeRegenTime { get; set; }
        Element ModifyType { get; set; }

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
        Projectile,
        NPC
    }
}
